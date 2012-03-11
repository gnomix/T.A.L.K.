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

/**
 * \file
 * Implementation of ciscoctimonitor
 * \package
 * All JTAPI related libraries
 */

package org.wybecom.talk.jtapi;

import javax.telephony.*;
import javax.telephony.callcontrol.*;
import javax.telephony.callcontrol.events.*;
import javax.telephony.events.*;
import com.cisco.jtapi.extensions.*;
import org.wybecom.talk.jmf.*;

/**
 *
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class ciscoctimonitor extends ctimonitor  {

    rtpmanager player;
    CallControlCall consultCall;
    CallControlCall activeCall;
    TerminalConnection controller;
    

    /**
     * Initialize a cti monitor for cisco implementation (CTI Port)
     * @param address The JTAPI Address
     * @param log The logger
     * @param mp An RTP Manager
     */
    public ciscoctimonitor(Address address, trace log, rtpmanager mp) {
        super(address, log);
        player  = mp;
    }

    @Override public void start() {
        try {
            this.log("Starting cisco cti monitor: " + this.getTerminalName() + ":" + this.getDirNum());
            this.addObservers();
        }
        catch (Exception e) {
            this.log("exception while starting this cisco cti monitor: " + e.toString());
        }
    }

    @Override public void terminalChangedEvent ( TermEv [] events ) {
        super.terminalChangedEvent(events);
        for(int i=0; i<events.length; i++ ) {
            switch (events[i].getID()){
                case CiscoRTPInputStartedEv.ID:
                    this.log("RTP input started: " + ((CiscoRTPInputStartedEv)events[i]).getRTPInputProperties().getLocalAddress().getHostAddress() + ":" + String.valueOf(((CiscoRTPInputStartedEv)events[i]).getRTPInputProperties().getLocalPort()) + ", " + ((CiscoRTPInputStartedEv)events[i]).getCallID().toString());

                    /*if (((CallControlCall)((CiscoRTPInputStartedEv)events[i]).getCallID().getCall()).equals(consultCall)){
                            this.log("This call is the consult call: " + ((CallControlCall)((CiscoRTPInputStartedEv)events[i]).getCallID().getCall()).toString() );
                            try {
                                activeCall.setTransferController(controller);
                                activeCall.transfer(consultCall);
                                activeCall = null;
                                consultCall = null;
                            }
                            catch (Exception transferException){
                                this.log("Unable to transfer call: " + transferException.toString());
                            }
                        }*/
                    break;
                case CiscoRTPInputStoppedEv.ID:
                    this.log("RTP input stopped: " + ((CiscoRTPInputStoppedEv)events[i]).getCallID().toString());
                    break;
                case CiscoRTPOutputStartedEv.ID:
                    this.log("RTP output started: " + ((CiscoRTPOutputStartedEv)events[i]).getRTPOutputProperties().getRemoteAddress().getHostAddress() + ":" + String.valueOf(((CiscoRTPOutputStartedEv)events[i]).getRTPOutputProperties().getRemotePort()) + ", " + ((CiscoRTPOutputStartedEv)events[i]).getCallID().toString());
                    try {
                        /*player.addTarget(((CiscoRTPOutputStartedEv)events[i]).getRTPOutputProperties().getRemoteAddress().getHostAddress(), ((CiscoRTPOutputStartedEv)events[i]).getRTPOutputProperties().getRemotePort());
                        player.start();*/
                    }
                    catch (Exception playerException) {
                        this.log("Unable to start playing: " + playerException.toString());
                    }
                    break;
                case CiscoRTPOutputStoppedEv.ID:
                    this.log("RTP output stopped: " + ((CiscoRTPOutputStoppedEv)events[i]).getCallID().toString());
                    /*player.stop();*/
                    break;
                default:
                    this.log(events[i].toString());
                    break;
            }
        }
    }

    @Override public void addressChangedEvent ( AddrEv [] events ){
        this.log(String.valueOf(events.length) + " address changed events received for " + this.getDirNum());
            for (int i=0; i<events.length; i++ ) {



                    switch (events[i].getID()){
                        case AddrObservationEndedEv.ID:
                            this.log("Address observation ended for " + this.getDirNum());
                            /*this.stop();*/
                            break;
                        default:
                            this.log("This event is not implemented: " + events[i].toString());
                            break;
                    }
                //}
            }
    }



    @Override public void callChangedEvent ( CallEv [] events ){
        this.log(String.valueOf(events.length) + " call changed events received for "+ this.getDirNum());
            for (int i=0; i<events.length; i++ ) {


                switch (events[i].getID()){
                    case CallActiveEv.ID:
                        //CallEv (getCall())
                        this.log("Call active event received for "+ this.getDirNum());
                        break;
                    case CallInvalidEv.ID:
                        //CallEv (getCall())
                        this.log("Call inactive event received for "+ this.getDirNum());
                        break;
                    case CallObservationEndedEv.ID:
                        //CallEv (getCall())
                        this.log("Call observation ended event received for "+ this.getDirNum());
                        break;
                    case CallCtlConnOfferedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection offered for " + this.getDirNum() + " : " + events[i].toString() );
                        try {
                            if (activeCall == null){
                                ((CallControlConnection)((CallCtlConnOfferedEv)events[i]).getConnection()).accept();
                            }
                        }
                        catch (Exception acceptException){
                            this.log("Unable to accept this call: " + acceptException.toString());
                        }
                        break;
                    case CallCtlConnQueuedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection queued for " + this.getDirNum());
                        break;
                    case CallCtlConnAlertingEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection alerting for " + this.getDirNum());
                        try{
                            if (activeCall == null){
                                ((CallCtlConnAlertingEv)events[i]).getConnection().getTerminalConnections()[0].answer();
                                ((CallControlTerminalConnection)((CallCtlConnAlertingEv)events[i]).getConnection().getTerminalConnections()[0]).hold();
                                /*controller = ((CallCtlConnAlertingEv)events[i]).getConnection().getTerminalConnections()[0];
                                activeCall = (CallControlCall)((CallCtlConnAlertingEv)events[i]).getCall();
                                this.log("Active call from: " + this.getDirNum() + ", " + activeCall.getCalledAddress().getName() + ", " + activeCall.getCallingAddress().getName());
                                if (!activeCall.getCallingAddress().getName().equalsIgnoreCase(this.getDirNum())){
                                    consultCall = (CallControlCall)this.createCall();
                                    consultCall.consult(((CallCtlConnAlertingEv)events[i]).getConnection().getTerminalConnections()[0], "7248");
                                }
                            }
                            if (consultCall.equals((CallControlCall)((CallCtlConnAlertingEv)events[i]).getCall())) {
                                this.log("Alerting on remote side: " + consultCall.toString());
                                Timer t = new Timer();
                                t.start();
                            }*/
                            }
                        }
                        catch (Exception answerException){
                            this.log("Unable to answer this call: " + answerException.toString());
                        }
                        break;
                    case CallCtlConnInitiatedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection initiated for " + this.getDirNum());
                        break;
                    case CallCtlConnDialingEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection dialing for " + this.getDirNum());
                        break;
                    case CallCtlConnNetworkReachedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection network reached for " + this.getDirNum());
                        break;
                    case CallCtlConnNetworkAlertingEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection network alerting for " + this.getDirNum());
                        break;
                    case CallCtlConnFailedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection failed for " + this.getDirNum());
                        break;
                    case CallCtlConnEstablishedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection established for " + this.getDirNum());
                        break;
                    case CallCtlConnUnknownEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection unknown for " + this.getDirNum());
                        break;
                    case CallCtlConnDisconnectedEv.ID:
                        //CallCtlCallEv ( getCalledAddress() getCallingAddress() getCallingTerminal() getLastRedirectedAddress())
                        this.log("Call Control Connection disconnected for " + this.getDirNum());
                        break;
                    case ConnAlertingEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.log("Conn Alerting for " + this.getDirNum());
                        break;
                    case ConnConnectedEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.log("Conn Connected for " + this.getDirNum());
                        break;
                    case ConnCreatedEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.log("Conn Created for " + this.getDirNum());
                        break;
                    case ConnDisconnectedEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.log("Conn Disconnected for " + this.getDirNum());
                        break;
                    case ConnFailedEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.log("Conn Failed for " + this.getDirNum());
                        break;
                    case ConnInProgressEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.log("Conn In progress for " + this.getDirNum());
                        break;
                    case ConnUnknownEv.ID:
                        // ConnEv (getConnection() getCall())
                        this.log("Conn Unknown for " + this.getDirNum());
                        break;
                    case CallCtlTermConnHeldEv.ID:
                        this.log("Call Control Terminal Connection Held for " + this.getDirNum());
                        break;
                    case CallCtlTermConnTalkingEv.ID:
                        this.log("Call Control Terminal Connection Talking for " + this.getDirNum());
                        break;
                }
            }
    }

    /**
     * \todo
     * Everything
     */
    public synchronized void waitBeforeDrop(){
            Connection agentConnection = null;
            int time = 0;
            while (consultCall != null){
                if (agentConnection == null){
                    for (Connection c : consultCall.getConnections()){
                        this.log("Consult call connection: " + c.toString() );

                        if (c.getState() == Connection.ALERTING){
                            this.log("Current connection from consult call: " + c.toString());
                            agentConnection = c;
                            this.log("Current agentConnection: " + agentConnection.toString());
                            break;
                        }
                    }
                }
                if (agentConnection.getState() == Connection.ALERTING && time < 12000){
                    try {
                        Thread.sleep(500);
                        time += 500;
                    }
                    catch (Exception tException){
                        this.log("Error while waiting for agentConnection: " + tException.toString());
                    }

                }
                else if (time == 12000){
                    try {
                        this.log("Exceeded timeout... dropping consult call: " + consultCall.toString());
                        consultCall.drop();
                        consultCall = null;
                    }
                    catch (Exception callException) {
                        this.log("Error while dropping consult call: " + callException.toString());
                    }
                    break;
                }
            }
        }
    

    class Timer extends Thread {

        public void run()  {
            waitBeforeDrop();
        }
    }
}
