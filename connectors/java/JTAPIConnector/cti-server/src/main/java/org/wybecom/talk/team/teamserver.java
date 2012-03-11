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

import java.util.*;
import org.wybecom.talk.team.config.*;
import org.wybecom.talk.jtapi.configuration.*;
import javax.xml.bind.*;
import java.io.*;
import javax.telephony.callcenter.*;
import javax.telephony.*;
import javax.telephony.events.*;
import javax.telephony.callcenter.events.*;
import com.cisco.cti.util.Condition;
import org.wybecom.talk.jtapi.stateserver.client.config.*;
import org.wybecom.talk.jtapi.stateclient;
import org.wybecom.talk.team.db.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class teamserver {

    Cticonfig ctiConfig;
    Teamconfig teamConfig;
    Condition provInService = new Condition();
    Condition provShutdownCondition = new Condition();
    Provider provider;
    Provider teamProvider;
    Condition teamProvInService = new Condition();
    Condition teamProvShutdownCondition = new Condition();
    Hashtable callbackHashtable;
    Vector agentTerminals = new Vector();
    Vector stateclients = new Vector();
    Stateserverclientconfig stateServerConfig;
    Controller dbController;
    
    public teamserver() {
        ctiConfig = this.GetCTIConfig();
        teamConfig = this.GetTeamConfig();
        stateServerConfig = this.GetStateServerConfig();
        dbController = new Controller();
    }

    /**
     *
     * @return 
     */
    public Vector getStateClients() {
        return this.stateclients;
    }

    private Teamconfig GetTeamConfig() {
        Teamconfig conf = null;
        try {
            JAXBContext jc = JAXBContext.newInstance("org.wybecom.talk.team.config");
            Unmarshaller unmarshaller = jc.createUnmarshaller();
            conf = ((JAXBElement<Teamconfig>)unmarshaller.unmarshal(new File("teamconfig.properties"))).getValue();
            return conf;
        }
        catch (Exception e)
        {
            System.out.println("Failed to get team configuration: " + e.getMessage());
            return conf;
        }
    }

    private Cticonfig GetCTIConfig()    {
        Cticonfig conf = null;
        try
        {
            JAXBContext jc = JAXBContext.newInstance("org.wybecom.talk.jtapi.configuration");
            Unmarshaller unmarshaller = jc.createUnmarshaller();
            conf = ((JAXBElement<Cticonfig>)unmarshaller.unmarshal(new File("ctiserver.properties"))).getValue();
            return conf;
        }
        catch (Exception e)
        {
            System.out.println("Failed to get cti configuration: " + e.toString());
            return conf;
        }
    }

    private Stateserverclientconfig GetStateServerConfig() {
        Stateserverclientconfig conf = null;
        try {
            JAXBContext jc = JAXBContext.newInstance("org.wybecom.talk.jtapi.stateserver.client.config");
            Unmarshaller unmarshaller = jc.createUnmarshaller();
            conf = ((JAXBElement<Stateserverclientconfig>)unmarshaller.unmarshal(new File("stateserver.properties"))).getValue();
            return conf;
        }
        catch (Exception e) {
            System.out.println("Failed to get state server client configuration: " + e.getMessage());
            return conf;
        }
    }

    private List<Team> GetTeams(){
        return (List<Team>)dbController.GetTeams();
    }

   public void start(){
       for (Team t : GetTeams()){
            System.out.println( "Team: " + t.getTeamname() + "\n" );
        }
        String providerString = null;
        try {
            this.initStateServerClients();
            System.out.println(this.stateclients.size() + " state clients available");
        }
        catch (Exception stateServerInitException) {
            System.out.println("Failed to initialize state server clients: " + stateServerInitException.getMessage());
        }
        try{
                        JtapiPeer peer = JtapiPeerFactory.getJtapiPeer ( null );

			teamproviderobserver providerObserver = new teamproviderobserver ();

                        providerString = this.ctiConfig.getProvider().get(0).getCtiprovider() + ";login=" + this.ctiConfig.getProvider().get(0).getCtiuser() + ";passwd=" + this.ctiConfig.getProvider().get(0).getCtiuserpassword();
			provider = peer.getProvider ( providerString);

			if ( provider != null ) {

				System.out.println( "Provider name: " + provider.getName () + "\n" );

				provider.addObserver ( providerObserver );
				provInService.waitTrue();

				Address [] addresses = provider.getAddresses ();
				if ( addresses != null ) {

					if (provider instanceof CallCenterProvider)
					{
                                                System.out.println(provider.getName () + " is a callcenterprovider");
                                                Hashtable acdmanageraddresses = new Hashtable();
                                                

						RouteAddress[] routePointAddress = ((CallCenterProvider)provider).getRouteableAddresses();
						if (routePointAddress.length > 0)
						    callbackHashtable = new Hashtable(routePointAddress.length);
						for (int i = 0; i< routePointAddress.length; i++) {
                                                     String groupName = null;
                                                     Vector managerAddresses = new Vector();
                                                     /*for (Xmlteam team : this.teamConfig.getTeam()){
                                                         if (team.getRouteaddress().equalsIgnoreCase(routePointAddress[i].getName())){
                                                             groupName = team.getAcdmanageraddressgroupname();
                                                         }
                                                     }*/

                                                     for (Team team : GetTeams()){
                                                         if (team.getTeampattern().equalsIgnoreCase(routePointAddress[i].getName())) {
                                                             groupName = team.getAcdmanageraddressgroupid().getAcdmanageraddressgroupname();
                                                             for (org.wybecom.talk.team.db.ACDManagerAddress ama : team.getAcdmanageraddressgroupid().getACDManagerAddressCollection()){
                                                                managerAddresses.add(acdmanageraddresses.get(ama.getPattern()));
                                                             }
                                                         }
                                                     }

                                                     /*for (Xmlacdmanageraddressgroup group : this.teamConfig.getAcdmanageraddressgroup()){
                                                         if (group.getName().equalsIgnoreCase(groupName)){
                                                             for (String s : group.getAcdmanageraddress()){
                                                                 managerAddresses.add(acdmanageraddresses.get(s));
                                                             }
                                                         }
                                                     }*/

                                                     acdaddress aa = new acdaddress(routePointAddress[i], (javax.telephony.callcenter.ACDManagerAddress[])managerAddresses.toArray(new javax.telephony.callcenter.ACDManagerAddress[0]));
                                                     teamaddressobserver tao = new teamaddressobserver();
                                                     aa.addObserver(tao);
						     RouteCallback callback = new teamroutecallback(aa);
						     callbackHashtable.put(routePointAddress[i], callback);
                                                     routePointAddress[i].registerRouteCallback(callback);
                                            }

                                            startAgentProvider();

                                        }



				}


			} else {
				System.out.println( "Provider is null\n" );
			}
		} catch ( Exception e ) {
			e.printStackTrace ();
			System.out.println ( e.getMessage () );
		}
    }

   public void startAgentProvider(){
       String providerString = null;
        try{
                        JtapiPeer peer = JtapiPeerFactory.getJtapiPeer ( null );

			teamproviderobserver providerObserver = new teamproviderobserver ();

                        providerString = this.teamConfig.getTeamprovider().getTeamctiprovider() + ";login=" + this.teamConfig.getTeamprovider().getTeamctiuser() + ";passwd=" + this.teamConfig.getTeamprovider().getTeamctiuserpassword();
			teamProvider = peer.getProvider ( providerString);

			if ( teamProvider != null ) {

				System.out.println( "Provider name: " + teamProvider.getName () + "\n" );

				teamProvider.addObserver ( providerObserver );
				teamProvInService.waitTrue();

				Terminal [] terminals = teamProvider.getTerminals();
				if ( terminals != null ) {

					for (Terminal t:terminals){
                                            agentTerminals.add(new agentterminal(t, stateclients));
                                        }


				}


			} else {
				System.out.println( "Provider is null\n" );
			}
		} catch ( Exception e ) {
			e.printStackTrace ();
			System.out.println ( e.getMessage () );
		}
   }

   public void stop() {

    }

   private void initStateServerClients() {
        try {
            if (this.stateServerConfig != null) {
                for (Xmlstateserverclient stateserverclient : this.stateServerConfig.getStateserverclient()) {
                    System.out.println("Adding a state server client: " + stateserverclient.getWsdlurl());
                    stateclient sc = new stateclient(stateserverclient.getWsdlurl());
                    for (Xmlevent ev : stateserverclient.getEvent()) {
                        if (ev.isEnabled()) {
                            System.out.println(stateserverclient.getWsdlurl() + " will monitor this event: "+ ev.getName());
                        }
                        else {
                            System.out.println(stateserverclient.getWsdlurl() + " will not monitor this event: "+ ev.getName());
                        }
                        sc.events.put(ev.getName(), ev.isEnabled().toString());
                    }
                    System.out.println("Unknown events will not be monitored!");
                    if (sc.inService) {
                        this.stateclients.add(sc);
                    }
                }
            }
            else {
                throw new Exception ("state server configuration unavailable");
            }
        }
        catch (Exception e) {
            System.out.println("Unable to initialize state server configuration, state server will not receive events: " + e.getMessage());
        }
    }

   public boolean Login(String agentId, String pwd, String extension)throws InvalidArgumentException, InvalidStateException, ResourceUnavailableException{
       System.out.println( "Login request for: " + agentId + " with extension " + extension );
       for (Team team : GetTeams()){
           for (TeamMember member : team.getTeamMemberCollection()){
               if (member.getTeammemberextension().equalsIgnoreCase(extension)){
                   for (Object o : agentTerminals){
                       for (Address a : ((agentterminal)o).getAddresses()){
                           if (a.getName().equalsIgnoreCase(extension)){
                               if (((agentterminal)o).addAgent(a, getACDAddress(team.getTeampattern()), Agent.LOG_IN, agentId, pwd) != null){
                                   return true;
                               }
                               else {
                                   System.out.println( "Login request for: " + agentId + " with extension " + extension + " failed, this agent is probably already logged");
                               }
                               return false;

                           }
                       }
                   }
               }
           }
       }
       /*for (Xmlteam team : this.teamConfig.getTeam()){
           for (String agent : team.getAgent()){
               if (agent.equalsIgnoreCase(agentId)){
                   for (Object o : agentTerminals){
                       for (Address a : ((agentterminal)o).getAddresses()){
                           if (a.getName().equalsIgnoreCase(extension)){
                               if (((agentterminal)o).addAgent(a, getACDAddress(team.getRouteaddress()), Agent.LOG_IN, agentId, pwd) != null){
                                   return true;
                               }
                               else {
                                   System.out.println( "Login request for: " + agentId + " with extension " + extension + " failed, this agent is probably already logged");
                               }
                               return false;

                           }
                       }
                   }
               }
           }
       }*/
       return false;
   }

   public boolean Login(String agentId, String pwd, String extension, String teamextension)throws InvalidArgumentException, InvalidStateException, ResourceUnavailableException{
       System.out.println( "Login request for: " + agentId + " with extension " + extension + " for team " + teamextension );
       for (Team team : GetTeams()){
           if (team.getTeampattern().equalsIgnoreCase(teamextension)){
           for (TeamMember member : team.getTeamMemberCollection()){
               if (member.getTeammemberextension().equalsIgnoreCase(extension)){
                   for (Object o : agentTerminals){
                       for (Address a : ((agentterminal)o).getAddresses()){
                           if (a.getName().equalsIgnoreCase(extension)){
                               if (((agentterminal)o).addAgent(a, getACDAddress(team.getTeampattern()), Agent.LOG_IN, agentId, pwd) != null){
                                   return true;
                               }
                               else {
                                   System.out.println( "Login request for: " + agentId + " with extension " + extension + " failed, this agent is probably already logged");
                               }
                               return false;

                           }
                       }
                   }
               }
           }
           }
       }
       return false;
   }

   public boolean Logoff(String agentId) throws InvalidArgumentException, InvalidStateException{
        boolean result = false;
       for (Object o : agentTerminals){
           for (Agent a : ((agentterminal)o).getAgents()){
               if (a.getAgentID().equalsIgnoreCase(agentId)){
                   a.setState(Agent.LOG_OUT);
                   result =  true;
               }
           }
       }
       return result;
   }

   public boolean Logoff(String agentId, String teamextension) throws InvalidArgumentException, InvalidStateException{
       boolean result = false;
       for (Object o : agentTerminals){
           for (Agent a : ((agentterminal)o).getAgents()){
               if (a.getAgentID().equalsIgnoreCase(agentId) && a.getACDAddress().getName().equalsIgnoreCase(teamextension)){
                   a.setState(Agent.LOG_OUT);
                   result =  true;
               }
           }
       }
       return result;
   }

   private ACDAddress getACDAddress(String extension){
       Enumeration e = callbackHashtable.elements();
       ACDAddress aa = null;
       while(e.hasMoreElements()) {
           RouteCallback rcb = (RouteCallback)e.nextElement();
           if (rcb instanceof teamroutecallback){
               if (((teamroutecallback)rcb).acdaddress.getName().equalsIgnoreCase(extension)){
                   aa = ((teamroutecallback)rcb).acdaddress;
               }
           }
       }
       return aa;
   }

   class teamproviderobserver  implements ProviderObserver{

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
                                                                if (ev.getProvider().equals(provider)){
                                                                    provInService.set();
                                                                }
                                                                else if (ev.getProvider().equals(teamProvider)){
                                                                    teamProvInService.set();
                                                                }
								break;
							case ProvShutdownEv.ID:
                                                                if (ev.getProvider().equals(provider)){
                                                                    provShutdownCondition.set ();
                                                                }
                                                                else if (ev.getProvider().equals(teamProvider)){
                                                                    teamProvShutdownCondition.set ();
                                                                }
								
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

   class teamaddressobserver implements ACDAddressObserver{
       public synchronized void addressChangedEvent(AddrEv events[]){
           for (AddrEv ev : events){
               switch (ev.getID()){
                   case ACDAddrReadyEv.ID:
                       System.out.println( ((ACDAddrReadyEv)ev).getAgent().toString() + " is ready." );
                       break;
                   case ACDAddrBusyEv.ID:
                       System.out.println( ((ACDAddrBusyEv)ev).getAgent().toString() + " is busy." );
                       break;
                   case ACDAddrLoggedOffEv.ID:
                       System.out.println( ((ACDAddrLoggedOffEv)ev).getAgent().toString() + " is logged off." );
                       break;
                   case ACDAddrLoggedOnEv.ID:
                       System.out.println( ((ACDAddrLoggedOnEv)ev).getAgent().toString() + " is logged on." );
                       break;
                   case ACDAddrNotReadyEv.ID:
                       System.out.println( ((ACDAddrNotReadyEv)ev).getAgent().toString() + " is not ready." );
                       break;
                   case ACDAddrUnknownEv.ID:
                       System.out.println( ((ACDAddrUnknownEv)ev).getAgent().toString() + " is in unknown state." );
                       break;
                   case ACDAddrWorkNotReadyEv.ID:
                       System.out.println( ((ACDAddrWorkNotReadyEv)ev).getAgent().toString() + " is work not ready." );
                       break;
                   case ACDAddrWorkReadyEv.ID:
                       System.out.println( ((ACDAddrWorkReadyEv)ev).getAgent().toString() + " is work ready." );
                       break;
               }
           }
       }
   }
}
