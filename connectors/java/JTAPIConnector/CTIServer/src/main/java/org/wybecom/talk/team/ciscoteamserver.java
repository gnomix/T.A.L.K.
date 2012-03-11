/*
 *  WYBECOM T.A.L.K. -- Telephony Application Library Kit
 *  Copyright (C) 2010 WYBECOM
 *
 *  Yohann BARRE <y.barre@wybecom.com>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  T.A.L.K. is based upon:
 *  - Sun JTAPI http://java.sun.com/products/jtapi/
 *  - JulMar TAPI http://julmar.com/
 *  - Asterisk.Net http://sourceforge.net/projects/asterisk-dotnet/
 */

package org.wybecom.talk.team;


import javax.telephony.*;
import javax.telephony.events.*;
import javax.telephony.callcontrol.*;
import javax.telephony.callcontrol.events.*;
import javax.telephony.media.*;
import javax.telephony.media.events.*;
import com.cisco.jtapi.extensions.*;
import java.util.*;
import javax.telephony.callcenter.*;
import com.cisco.cti.util.Condition;
import org.wybecom.talk.jtapi.configuration.*;
import org.wybecom.talk.jmf.*;
import javax.xml.bind.*;
import java.io.*;
import org.wybecom.talk.team.config.*;
import org.wybecom.talk.team.db.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class ciscoteamserver extends teamserver{
    
    
    Hashtable mediaHashtable;
    Controller dbController;
    

    public ciscoteamserver(){
        super();
        dbController = new Controller();
    }

    

    @Override public void start(){
        for (Team t : (List<Team>)dbController.GetTeams()){
            System.out.println( "Team: " + t.getTeamname() + "\n" );
        }
        String providerString = null;
        try{
                        JtapiPeer peer = JtapiPeerFactory.getJtapiPeer ( null );

			ciscoteamproviderobserver providerObserver = new ciscoteamproviderobserver ();

                        providerString = this.ctiConfig.getProvider().get(0).getCtiprovider() + ";login=" + this.ctiConfig.getProvider().get(0).getCtiuser() + ";passwd=" + this.ctiConfig.getProvider().get(0).getCtiuserpassword();
			provider = peer.getProvider ( providerString);

			if ( provider != null ) {

				System.out.println( "Provider name: " + provider.getName () + "\n" );

				provider.addObserver ( providerObserver );
				provInService.waitTrue();
                                
				Address [] addresses = provider.getAddresses ();
				if ( addresses != null ) {

					//
					// Define RouteAddress array for list of RoutePoints in the addresses
					// in the Provider List
					if (provider instanceof CallCenterProvider)
					{
                                                System.out.println(provider.getName () + " is a callcenterprovider");
                                                Hashtable acdmanageraddresses = new Hashtable();
                                                for (Address address : addresses){

                                                    Terminal[] addressTerminal = address.getTerminals();
                                                    try{
                                                    if (addressTerminal[0] instanceof CiscoMediaTerminal && !(address instanceof RouteAddress)){
                                                        acdmanageraddresses.put(address.getName(), new acdmanageraddress(address));
                                                        if (mediaHashtable == null)
                                                            mediaHashtable = new Hashtable();
                                                        CiscoMediaTerminal cmt = (CiscoMediaTerminal)addressTerminal[0];
                                                        ciscoteammediaobserver media = new ciscoteammediaobserver(cmt, new rtpmanager());
                                                        mediaHashtable.put(address.getName(), media);
                                                        address.addCallObserver(media);
                                                        address.addObserver(media);
                                                    }
                                                    } catch ( Exception e ) {
                                                                        System.out.println( "While registering terminal " + addressTerminal[0] + " caught exception:\n" );
                                                                        System.out.println ( e.toString () );
                                                    }
                                                }

						RouteAddress[] routePointAddress = ((CallCenterProvider)provider).getRouteableAddresses();
						if (routePointAddress.length > 0)
						    callbackHashtable = new Hashtable(routePointAddress.length);
						for (int i = 0; i< routePointAddress.length; i++) {
                                                     String groupName = null;
                                                     Vector managerAddresses = new Vector();
                                                     for (Xmlteam team : this.teamConfig.getTeam()){
                                                         if (team.getRouteaddress().equalsIgnoreCase(routePointAddress[i].getName())){
                                                             groupName = team.getAcdmanageraddressgroupname();
                                                         }
                                                     }

                                                     for (Xmlacdmanageraddressgroup group : this.teamConfig.getAcdmanageraddressgroup()){
                                                         if (group.getName().equalsIgnoreCase(groupName)){
                                                             for (String s : group.getAcdmanageraddress()){
                                                                 managerAddresses.add(acdmanageraddresses.get(s));
                                                             }
                                                         }
                                                     }

                                                     acdaddress aa = new acdaddress(routePointAddress[i], (javax.telephony.callcenter.ACDManagerAddress[])managerAddresses.toArray(new javax.telephony.callcenter.ACDManagerAddress[0]));
                                                     teamaddressobserver tao = new teamaddressobserver();
                                                     aa.addObserver(tao);
						     RouteCallback callback = new ciscoteamroutecallback(aa);
						     callbackHashtable.put(routePointAddress[i], callback);
                                                     CiscoRouteTerminal routeTerminal =
                                                                             (CiscoRouteTerminal) routePointAddress[i].getTerminals()[0];
                                                     System.out.println("Registering CiscoRouteTerminal: " + routeTerminal.getName());
                                                     routeTerminal.register (null,CiscoRouteTerminal.NO_MEDIA_REGISTRATION );

                                                     routePointAddress[i].registerRouteCallback(callback);
                                            }



                                        }

                                        startAgentProvider();

				}


			} else {
				System.out.println( "Provider is null\n" );
			}
		} catch ( Exception e ) {
			e.printStackTrace ();
			System.out.println ( e.getMessage () );
		}
    }

    class ciscoteamproviderobserver  implements ProviderObserver{

    public synchronized void providerChangedEvent(ProvEv[] eventList){
        try {
				if ( eventList != null ) {
					for ( int i = 0; i < eventList.length; i++ ) {
						if ( eventList[i].isNewMetaEvent () ) {
							System.out.println ( "NEW META EVENT_________" + JTAPIDecoder.getMetaCode( eventList[i].getMetaCode() ) );
							System.out.println ( " for: " + eventList[i].getProvider().getName() + ", events= " + eventList.length  );
						}
                                                ProvEv ev = eventList[i];

						System.out.println ( "Received " + eventList[i].getClass().getName() +" "+ ev.getCause()+ "\n" );



						switch ( ev.getID () ) {
							case ProvInServiceEv.ID:
								provInService.set();
								break;
							case ProvShutdownEv.ID:
								provShutdownCondition.set ();
								break;
							case CiscoAddrCreatedEv.ID:
								/*Address addr = ((CiscoAddrCreatedEv)ev).getAddress ();
								new AddressWindowThread ( addr );*/
								break;
							case CiscoTermCreatedEv.ID:
								//Terminal term = ((CiscoTermCreatedEv)ev).getTerminal ();
								//if ( term instanceof CiscoMediaTerminal ) {
								//	new MediaTermWindowThread ( (CiscoMediaTerminal) term ).start ();
								//}
								break;
						}
					}
				}
				else {
					System.out.println ( "Error: providerChangedEvent was invoked with a null eventList\n" );
				}
			}
			finally {
				
			}
    }

}

    class ciscoteammediaobserver implements CallControlCallObserver, MediaCallObserver, CiscoTerminalObserver, CiscoAddressObserver {
        CiscoMediaTerminal terminal;
        rtpmanager player;

        ciscoteammediaobserver(CiscoMediaTerminal terminal, rtpmanager player){
            this.terminal = terminal;
            this.player = player;
            try{
                register();
                this.terminal.addObserver(this);
            }catch ( Throwable t ) {
			System.out.println( "Caught exception while adding observer: " + t );
		}

        }

        void register() throws Throwable{
            terminal.register(java.net.InetAddress.getLocalHost(), player.getPort(), new CiscoMediaCapability[] {CiscoMediaCapability.G711_64K_30_MILLISECONDS});
        }

        void unregister() throws Throwable{
            terminal.unregister();
        }

        void digit(MediaTermConnDtmfEv ev){

        }

        void accept(CallControlConnection conn){
            try {
			System.out.print( "Got offering, trying to accept... " );
			conn.accept ();
			System.out.print ( "...accepted.\n" );
		}
		catch ( Exception e ) {
			System.out.println ( "Couldn't accept, caught " + e );
		}
        }

        void answer ( CallControlConnection conn ) {
		TerminalConnection tc = conn.getTerminalConnections ()[0];
		try {
			System.out.print ( "Got alerting, trying to answer... " );
			tc.answer ();
			System.out.print  ( "...answered.\n" );
                        if (this.isCallCenterCall((CiscoCall)conn.getCall())){
                            System.out.print ( "This is a call center call, putting on hold... " );
                            ((CallControlTerminalConnection)tc).hold();
                            System.out.print  ( "...holded.\n" );
                        }
		}
		catch ( Exception e ) {
			System.out.println ( "Couldn't answer, caught " + e );
		}
	}

        void offered ( CallCtlConnOfferedEv ev ) {
		CallControlConnection conn = (CallControlConnection) ev.getConnection ();
		//
		// not entirely safe, but it works!
		//
		Address address = terminal.getAddresses ()[0];
		if ( address.equals ( conn.getAddress () ) ) {
			accept ( conn );
		}
	}

        void alerting ( CallCtlConnAlertingEv ev ) {
		CallControlConnection conn = (CallControlConnection) ev.getConnection ();
		//
		// not entirely safe, but it works!
		//
		Address address = terminal.getAddresses ()[0];
		if ( address.equals ( conn.getAddress () ) ) {
			answer ( conn );
		}
	}

        public synchronized void callChangedEvent( CallEv[] eventList ){
            try {
			for ( int i = 0; i < eventList.length; i++ )
			{
			    if (eventList[i].isNewMetaEvent()) {
				    System.out.println ( "NEW META EVENT_________" + JTAPIDecoder.getMetaCode( eventList[i].getMetaCode() ) + "\n");
				}

			    System.out.print ( "Received " + eventList[i] + " for " );

				if ( eventList[i] instanceof ConnEv ) {
					System.out.print( ((ConnEv)eventList[i]).getConnection().getAddress().getName() );
					CiscoConnection conn  = (CiscoConnection) ((ConnEv)eventList[i]).getConnection();
					System.out.print (  " " + conn.getConnectionID());
                    System.out.print( " " + JTAPIDecoder.getReason(((CiscoCallEv)eventList[i]).getCiscoFeatureReason()));
				} else if ( eventList[i] instanceof TermConnEv ) {
					System.out.print ( ((TermConnEv)eventList[i]).getTerminalConnection().getTerminal().getName() );
                    System.out.print( " " + JTAPIDecoder.getReason(((CiscoCallEv)eventList[i]).getCiscoFeatureReason()));
				} else if ( eventList[i] instanceof CallEv ) {
					 System.out.print ( "callID=" + ((CallEv)eventList[i]).getID() );
					 if ( eventList[i] instanceof CiscoCallChangedEv ){
						 CiscoCallChangedEv ev = (CiscoCallChangedEv)eventList[i];
						 System.out.print ("Surviving= " + ev.getSurvivingCall().getCallID().getCallManagerID() + "/" + ev.getSurvivingCall().getCallID().getGlobalCallID() );
						 System.out.print (" origcall= " + ev.getOriginalCall().getCallID().getCallManagerID() + "/" + ev.getOriginalCall().getCallID().getGlobalCallID() );
						 if (ev.getConnection() != null ){
							 System.out.print(" address= " + ev.getConnection().getAddress().getName());
							 System.out.print(" connectionID = " + ev.getConnection().getConnectionID() );

						 }
						 if (ev.getTerminalConnection() != null ){
							 System.out.print (ev.getTerminalConnection().getTerminal().getName());
						 }

					 }
                     System.out.print( " " + JTAPIDecoder.getReason(((CiscoCallEv)eventList[i]).getCiscoFeatureReason()));
				}


				System.out.print ( " Cause: " + JTAPIDecoder.getCause( eventList[i].getCause() ) );
				if ( eventList[i] instanceof CallCtlEv ) {
					System.out.print ( " CallControlCause: " + JTAPIDecoder.getCallCtlCause( ((CallCtlEv)eventList[i]).getCallControlCause() ) );
				}
				System.out.print ( "\n" );

                CiscoCall localCall;

				switch ( eventList[i].getID () ) {
				case CallCtlConnOfferedEv.ID:

                    localCall = (CiscoCall)((CallCtlConnOfferedEv) eventList[i]).getCall();
                    printCallInfo(localCall);
                    System.out.println("CallingPartyIpAddr=" + ((CiscoCallCtlConnOfferedEv)eventList[i]).getCallingPartyIpAddr());
					offered ( (CallCtlConnOfferedEv) eventList[i] );
					break;
				case CallCtlConnAlertingEv.ID:
					alerting ( (CallCtlConnAlertingEv) eventList[i] );
                    localCall = (CiscoCall)((CallCtlConnAlertingEv) eventList[i]).getCall();
                    printCallInfo(localCall);
					break;
                case CallCtlConnEstablishedEv.ID:
                    localCall = (CiscoCall)((CallCtlConnEstablishedEv) eventList[i]).getCall();
                    printCallInfo(localCall);
                    break;
				case MediaTermConnDtmfEv.ID:
					digit ( (MediaTermConnDtmfEv) eventList[i] );
					break;
				}
			}
		}
		finally {

		}
        }

        public void printCallInfo(CiscoCall localCall){
     if ( localCall != null ){
        CiscoPartyInfo currCalling = localCall.getCurrentCallingPartyInfo();
        CiscoPartyInfo currCalled = localCall.getCurrentCalledPartyInfo();
        CiscoPartyInfo lrpInfo = localCall.getLastRedirectingPartyInfo();
        CiscoPartyInfo calledInfo = localCall.getCalledPartyInfo();
        if ( currCalling != null ){
            CiscoUrlInfo urlInfo = currCalling.getUrlInfo();
            System.out.print("Current Calling PartyInfo= " + "Addr= " + currCalling.getAddress().getName() +
                           " AddrPI=" + currCalling.getAddressPI() +
                           " DispName=" + currCalling.getDisplayName() +
                           " DispNamePI=" + currCalling.getDisplayNamePI() +
                           " Unicode=" + currCalling.getUnicodeDisplayName() +
                           " locale=" + currCalling.getlocale());
            if ( urlInfo != null ){
                System.out.println(" user=" + urlInfo.getUser() +
                               " Host=" + urlInfo.getHost() +
                               " TransType=" + urlInfo.getTransportType() +
                               " Port=" + urlInfo.getPort() +
                               " urlType=" + urlInfo.getUrlType() + "\n" );
            }
        } else {System.out.println("Null CurrCalling Info" );}
        if ( currCalled != null ){
            CiscoUrlInfo urlInfo = currCalled.getUrlInfo();
            System.out.print("Current Called PartyInfo= " + "Addr= " + currCalled.getAddress().getName() +
                           " AddrPI=" + currCalled.getAddressPI() +
                           " DispName=" + currCalled.getDisplayName() +
                           " DispNamePI=" + currCalled.getDisplayNamePI() +
                           " Unicode=" + currCalled.getUnicodeDisplayName() +
                           " locale=" + currCalled.getlocale() );
            if ( urlInfo != null ){
                System.out.println(" user=" + urlInfo.getUser() +
                               " Host=" + urlInfo.getHost() +
                               " TransType=" + urlInfo.getTransportType() +
                               " Port=" + urlInfo.getPort() +
                               " urlType=" + urlInfo.getUrlType() + "\n" );
            }
        } else {System.out.println("Null Curr Called Info");}
        System.out.println("Current Calling CallInfo: " + "Addr= " + localCall.getCurrentCallingAddress().getName() +
                       " AddrPI=" + localCall.getCallingAddressPI() +
                       " DispName=" + localCall.getCurrentCallingPartyDisplayName() +
                       " DispNamePI=" + localCall.getCurrentCallingDisplayNamePI() +
                       " Unicode=" + localCall.getCurrentCallingPartyUnicodeDisplayName() +
                       " locale=" + localCall.getCurrentCallingPartyUnicodeDisplayNamelocale() + "\n" );
         System.out.println("Current Called CallInfo: " + "Addr= " + localCall.getCurrentCalledAddress().getName() +
                       " AddrPI=" + localCall.getCalledAddressPI() +
                       " DispName=" + localCall.getCurrentCalledPartyDisplayName() +
                       " DispNamePI=" + localCall.getCurrentCalledDisplayNamePI() +
                       " Unicode=" + localCall.getCurrentCalledPartyUnicodeDisplayName() +
                       " locale=" + localCall.getCurrentCalledPartyUnicodeDisplayNamelocale() + "\n" );
         if (lrpInfo == null ){
             System.out.println("Null LRP");
         } else {
             CiscoUrlInfo urlInfo = lrpInfo.getUrlInfo();
            System.out.print("LRP PartyInfo= " + "Addr= " + lrpInfo.getAddress().getName() +
                           " AddrPI=" + lrpInfo.getAddressPI() +
                           " DispName=" + lrpInfo.getDisplayName() +
                           " DispNamePI=" + lrpInfo.getDisplayNamePI() +
                           " Unicode=" + lrpInfo.getUnicodeDisplayName() +
                           " locale=" + lrpInfo.getlocale() );
            if ( urlInfo != null ){
                System.out.println(" user=" + urlInfo.getUser() +
                               " Host=" + urlInfo.getHost() +
                               " TransType=" + urlInfo.getTransportType() +
                               " Port=" + urlInfo.getPort() +
                               " urlType=" + urlInfo.getUrlType() + "\n" );
            }

         }

         if (calledInfo == null ){
             System.out.println("Null CalledInfo");
         } else {
             CiscoUrlInfo urlInfo = calledInfo.getUrlInfo();
            System.out.print("Called PartyInfo= " + "Addr= " + calledInfo.getAddress().getName() +
                           " AddrPI=" + calledInfo.getAddressPI() +
                           " DispName=" + calledInfo.getDisplayName() +
                           " DispNamePI=" + calledInfo.getDisplayNamePI() +
                           " Unicode=" + calledInfo.getUnicodeDisplayName() +
                           " locale=" + calledInfo.getlocale() );
            if ( urlInfo != null ){
                System.out.println(" user=" + urlInfo.getUser() +
                               " Host=" + urlInfo.getHost() +
                               " TransType=" + urlInfo.getTransportType() +
                               " Port=" + urlInfo.getPort() +
                               " urlType=" + urlInfo.getUrlType() + "\n" );
            }

         }


        }
        }

        public boolean isCallCenterCall(CiscoCall localCall){
            if (callbackHashtable.get(localCall.getLastRedirectingPartyInfo().getAddress()) != null){
                return true;
            }
            return false;
        }
        
        public synchronized void addressChangedEvent ( AddrEv [] eventList ) {
		traceGenericEvents ( eventList );
	}

	public synchronized void terminalChangedEvent ( TermEv [] eventList ) {
		traceGenericEvents ( eventList );
	}

	void traceGenericEvents ( Ev [] eventList ) {
		try {
			for ( int i = 0; i < eventList.length; i++ )
			{
			    if (eventList[i].isNewMetaEvent()) {
				    System.out.println ( "NEW META EVENT_________" + JTAPIDecoder.getMetaCode( eventList[i].getMetaCode() ) + "\n");
				}
				System.out.println ( "Received " + eventList[i] );
				System.out.println ( " Cause: " + JTAPIDecoder.getCause( eventList[i].getCause() ) );
				System.out.println ( "\n" );
			}
		}
		finally {
			
		}
	}
    }
}
