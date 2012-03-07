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
 * Implementation of ctiserver
 */

package org.wybecom.talk.jtapi;


import java.util.*;
import java.io.*;
import javax.telephony.*;
import javax.telephony.callcontrol.*;
import javax.telephony.events.*;
import javax.telephony.media.MediaTerminalConnection;
import org.wybecom.talk.jtapi.configuration.*;
import org.wybecom.talk.jtapi.stateserver.client.config.*;
import javax.xml.bind.*;

/**
 * \brief Implementation of ctiserver
 * 
 * @author Yohann BARRE
 */
public class ctiserver extends traceserver implements ProviderObserver{
    
    private Vector providers = new Vector();
    private Vector stateclients = new Vector();
    private Vector monitors = new Vector();
    private Cticonfig ctiConfig;
    private Stateserverclientconfig stateServerConfig;
    
    /**
     * \brief Initializes the JTAPI server
     * <p>Loads ctiserver.properties</p>
     * <p>Loads stateserver.properties</p>
     */
    public ctiserver() {
        super();
        ctiConfig = this.GetCTIConfig();
        stateServerConfig = this.GetStateServerConfig();
    }

    /**
     * \brief Initializes the JTAPI server
     * @param ctiConfigPath The path where CTI settings are stored
     * @param stateServerConfigPath The path where state server settings are stored
     */
    public ctiserver(String ctiConfigPath, String stateServerConfigPath){
        super();
        ctiConfig = this.GetCTIConfig(ctiConfigPath);
        stateServerConfig = this.GetStateServerConfig(stateServerConfigPath);
    }
    /**
     * Accessor
     * @return All state server clients
     */
    public Vector getStateClients() {
        return this.stateclients;
    }
    /**
     * Accessor
     * @return All monitored lines @see monitor
     */
    public Vector getMonitors() {
        return this.monitors;
    }

    /**
     * Accessor
     * @return All JTAPI provider
     */
    public Vector getProviders() {
        return this.providers;
    }

    /**
     * \brief Adds a monitored line
     * @param mon A monitored line @see monitor
     */
    public void addMonitor(monitor mon) {
        monitors.add(mon);
    }

    /**
     *
     * @return The cti server settings
     */
    public Cticonfig getCTIConfig(){
        return ctiConfig;
    }

    /**
     * \brief Starts the ctiserver
     */
    public void start() {
        try {
            this.initStateServerClients();
            this.println(this.stateclients.size() + " state clients available");
        }
        catch (Exception stateServerInitException) {
            this.printerror("Failed to initialize state server clients: " + stateServerInitException.getMessage());
        }
        
        try {
            this.initProviders();
            this.println(this.providers.size() + " providers available");
        }
        catch (Exception providerInitException) {
            this.printerror("Failed to initialize providers: " + providerInitException.getMessage());
        }
        
        try {
            this.initMonitors();
            this.println(this.monitors.size() + " monitors available");
        }
        catch (Exception monitorInitException) {
            this.printerror("Failed to initialize monitors: " + monitorInitException.getMessage());
        }
    }

    /**
     * \brief Stops the ctiserver
     */
    public void stop() {
        try {
            this.stopMonitors();
        }
        catch (Exception stopMonitorException) {
            this.printerror("Error while stopping monitors: " + stopMonitorException.getMessage());
        }
        
        try {
            this.stopProviders();
        }
        catch (Exception stopProviderException) {
            this.printerror("Error while stopping monitors: " + stopProviderException.getMessage());
        }
    }

    /**
     * \brief Implementation of providerChangedEvent inherited from ProviderObserver
     * 
     * @param events @see javax.telephony.events.ProvEv
     * 
     */
    public void providerChangedEvent ( ProvEv [] events ) {
        this.println(String.valueOf(events.length) + " provider changed events received");
        
        for (int i=0; i<events.length; i++ ) {
            Provider prov = ((ProvEv)events[i]).getProvider();
            this.println("Event type " + events[i].toString() + " from " + prov.getName() );
            switch (events[i].getID()) {
                case ProvInServiceEv.ID:
                    println("Provider in service.");
                    this.setProviderState(prov.getState(), prov.hashCode());
                    break;
                case ProvOutOfServiceEv.ID:
                    println("Provider out of service.");
                    if (this.getProviderState(prov.hashCode()) == Provider.IN_SERVICE) {
                        this.setProviderState(prov.getState(), prov.hashCode());
                        //clean
                        /*Enumeration enumeration = monitors.elements();
                        println ("Search for monitors with " + prov.getName());
                        while (enumeration.hasMoreElements()) {
                            monitor m = (monitor)enumeration.nextElement();
                            println ("monitor " + m.getDirNum() + " has " + m.getAddress().getProvider().getName());
                            if (m.getAddress().getProvider().getName().matches(prov.getName())) {
                                m.stop();
                                monitors.remove(m);
                            }
                        }
                        //backup
                        this.initBackupProvider(prov.hashCode());*/
                        
                    }
                    break;
                case ProvShutdownEv.ID:
                    println("Provider shutdown.");
                    this.setProviderState(prov.getState(), prov.hashCode());
                    break;
                case ProvObservationEndedEv.ID:
                    println("Provider observation ended.");
                    this.setProviderState(prov.getState(), prov.hashCode());
                    break;
            }
        }
    }
    
    private Cticonfig GetCTIConfig()    {
        Cticonfig conf = null;
        try
        {
            JAXBContext jc = JAXBContext.newInstance("org.wybecom.talk.jtapi.configuration");
            Unmarshaller unmarshaller = jc.createUnmarshaller();
            //conf = ((JAXBElement<Cticonfig>)unmarshaller.unmarshal(new File("ctiserver.properties"))).getValue();
            conf = ((JAXBElement<Cticonfig>)unmarshaller.unmarshal(getClass().getResourceAsStream("/ctiserver.properties"))).getValue();
            return conf;
        }
        catch (Exception e)
        {
            this.printerror("Failed to get cti configuration: " + e.getMessage());
            return conf;
        }
    }
    private Cticonfig GetCTIConfig(String path)    {
        Cticonfig conf = null;
        try
        {
            JAXBContext jc = JAXBContext.newInstance("org.wybecom.talk.jtapi.configuration");
            Unmarshaller unmarshaller = jc.createUnmarshaller();
            //conf = ((JAXBElement<Cticonfig>)unmarshaller.unmarshal(new File(path))).getValue();
            conf = ((JAXBElement<Cticonfig>)unmarshaller.unmarshal(getClass().getResourceAsStream("/ctiserver.properties"))).getValue();
            return conf;
        }
        catch (Exception e)
        {
            this.printerror("Failed to get cti configuration: " + e.getMessage());
            return conf;
        }
    }

    private Stateserverclientconfig GetStateServerConfig() {
        Stateserverclientconfig conf = null;
        try {
            JAXBContext jc = JAXBContext.newInstance("org.wybecom.talk.jtapi.stateserver.client.config");
            Unmarshaller unmarshaller = jc.createUnmarshaller();
            //conf = ((JAXBElement<Stateserverclientconfig>)unmarshaller.unmarshal(new File("stateserver.properties"))).getValue();
            conf = ((JAXBElement<Stateserverclientconfig>)unmarshaller.unmarshal(getClass().getResourceAsStream("/stateserver.properties"))).getValue();
            return conf;
        }
        catch (Exception e) {
            this.printerror("Failed to get state server client configuration: " + e.getMessage());
            return conf;
        }
    }

    private Stateserverclientconfig GetStateServerConfig(String path) {
        Stateserverclientconfig conf = null;
        try {
            JAXBContext jc = JAXBContext.newInstance("org.wybecom.talk.jtapi.stateserver.client.config");
            Unmarshaller unmarshaller = jc.createUnmarshaller();
            //conf = ((JAXBElement<Stateserverclientconfig>)unmarshaller.unmarshal(new File(path))).getValue();
            conf = ((JAXBElement<Stateserverclientconfig>)unmarshaller.unmarshal(getClass().getResourceAsStream("/stateserver.properties"))).getValue();
            return conf;
        }
        catch (Exception e) {
            this.printerror("Failed to get state server client configuration: " + e.getMessage());
            return conf;
        }
    }
    
    private int getProviderState(int providerhash) {
        int state = Provider.OUT_OF_SERVICE;
        try {
            Enumeration enumeration = providers.elements();
            while (enumeration.hasMoreElements()) {
                ctiprovider prov = (ctiprovider)enumeration.nextElement();
                if (prov.hashCode() == providerhash) {
                    state = prov.getProviderState();
                    break;
                }
            }
            return state;
        }
        catch (Exception e) {
            return state;
        }
    }
    
    private void setProviderState(int state, int providerhash) {
        try {
            Enumeration enumeration = providers.elements();
            while (enumeration.hasMoreElements()) {
                ctiprovider prov = (ctiprovider)enumeration.nextElement();
                if (prov.hashCode() == providerhash) {
                    prov.setProviderState(state);
                    break;
                }
            }
        }
        catch (Exception e) {
            this.printerror("Failed to set provider state: " + e.getMessage());
        }
    }
    
    private void initStateServerClients() {
        this.stateclients.clear();
        try {
            if (this.stateServerConfig != null) {
                for (Xmlstateserverclient stateserverclient : this.stateServerConfig.getStateserverclient()) {
                    this.println("Adding a state server client: " + stateserverclient.getWsdlurl());
                    stateclient sc = new stateclient(stateserverclient.getWsdlurl());
                    for (Xmlevent ev : stateserverclient.getEvent()) {
                        if (ev.isEnabled()) {
                            this.println(stateserverclient.getWsdlurl() + " will monitor this event: "+ ev.getName());
                        }
                        else {
                            this.println(stateserverclient.getWsdlurl() + " will not monitor this event: "+ ev.getName());
                        }
                        sc.events.put(ev.getName(), ev.isEnabled().toString());
                    }
                    this.println("Unknown events will not be monitored!");
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
            this.printerror("Unable to initialize state server configuration, state server will not receive events: " + e.getMessage());
        }
    }

    public void reloadStateServerClients(String path){
        this.stateServerConfig = this.GetStateServerConfig(path);
        this.initStateServerClients();
    }

    /**
     * \brief Initialize the JTAPI providers
     */
    public void initProviders() {
        try {
            if (this.ctiConfig != null) {
                this.println("Construct jtapi peer...");
                JtapiPeer peer = JtapiPeerFactory.getJtapiPeer ( null );
                for (Xmlprovider prov : this.ctiConfig.getProvider()) {
                    ctiprovider ctiprov = new ctiprovider(true, prov.getCtiprovider(), prov.getBackupctiprovider(), prov.getCtiuser(), prov.getCtiuserpassword());
                    this.println("Opening provider: " + ctiprov.getProviderString());
                    Provider jtapiprov = peer.getProvider(ctiprov.getProviderString());
                    ctiprov.setProvider(jtapiprov);
                    providers.add(ctiprov);
                    this.println("Adding observer to the provider: " + ctiprov.getProviderString());
                    jtapiprov.addObserver(this);
                    this.println("Waiting for in service signal: " + ctiprov.getProviderString());
                    
                    int maxwait = 60000;
                    int compteur = 0;
                    int stepwait = 200;
                    while (jtapiprov.getState() != Provider.IN_SERVICE && compteur < maxwait) {
                        Thread.sleep(stepwait);
                        compteur += stepwait;
                    }
                    if (compteur >= maxwait) {
                        this.println("Provider is not in service after a while...");
                    }
                }
            }
            else {
                throw new Exception ("provider configuration unavailable");
            }
        }
        catch (Exception e) {
            this.printerror("Unable to initialize provider configuration: " + e.getMessage());
        }
    }

    /**
     * \brief Opens a backup JTAPI provider
     * @param providerhash The primary JTAPI provider identifier
     */
    public void initBackupProvider(int providerhash) {
            Enumeration enumeration = providers.elements();
            try {
                    
                JtapiPeer peer = JtapiPeerFactory.getJtapiPeer ( null );
                while (enumeration.hasMoreElements()) {
                    ctiprovider prov = (ctiprovider)enumeration.nextElement();
                    if (prov.hashCode() == providerhash) {
                        this.println("Opening provider: " + prov.getProviderString());
                        Provider jtapiprov = peer.getProvider(prov.getProviderString());
                        prov.setProvider(jtapiprov);
                        providers.add(prov);
                        this.println("Adding observer to the provider: " + prov.getProviderString());
                        jtapiprov.addObserver(this);
                        this.println("Waiting for in service signal: " + prov.getProviderString());

                        int maxwait = 60000;
                        int compteur = 0;
                        int stepwait = 200;
                        while (jtapiprov.getState() != Provider.IN_SERVICE && compteur < maxwait) {
                            Thread.sleep(stepwait);
                            compteur += stepwait;
                        }
                        if (compteur >= maxwait) {
                            this.println("Provider is not in service after a while...");
                        }
                        else {
                            Address[] ads = prov.getProvider().getAddresses();
                            this.println(String.valueOf(ads.length) + " addresses finded from provider: " + prov.getProviderString());
                            for(Address a : ads) {
                                this.println("Adding address " + a.getName());
                                monitor m = new monitor(this.stateclients,a,(trace)this);
                                monitors.add(m);
                                m.start();
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception e) {
                this.printerror("Unable to initialize backup provider configuration: " + e.getMessage());
            }
    }

    /**
     * \brief Initialize the lines
     */
    public void initMonitors() {
        try {
            for(Object prov : this.providers) {
                if (prov instanceof ctiprovider) {
                    Address[] ads = ((ctiprovider)prov).getProvider().getAddresses();
                    this.println(String.valueOf(ads.length) + " addresses finded from provider: " + ((ctiprovider)prov).getProviderString());
                    for(Address a : ads) {
                        /*if (a instanceof RouteAddress && teamconfig != null) {
                            Xmlteam currentTeam = this.getXmlTeam(a.getName());
                            if (currentTeam != null){
                                this.println("Adding RouteAddress " + a.getName());
                                routemonitors.add(new routemonitor((RouteAddress)a,(trace)this, currentTeam));
                            }
                        }
                        else {*/
                            this.println("Adding address " + a.getName());
                            monitors.add(new monitor(this.stateclients,a,(trace)this));
                        /*}*/
                    }
                }
            }
            this.println("Starting monitors...");
            Enumeration mons = monitors.elements();
            while (mons.hasMoreElements()) {
                monitor mon = (monitor)mons.nextElement();
                mon.start();
            }
            /*this.println("Starting route monitors...");
            Enumeration routemons = routemonitors.elements();
            while (routemons.hasMoreElements()) {
                routemonitor routemon = (routemonitor)routemons.nextElement();
                routemon.start();
            }*/
        }
        catch (Exception e) {
            this.printerror("Unable to initialize monitors: " + e.getMessage());
        }
    }
    
    private void stopMonitors() {
        try {
            for (Object mon : this.monitors) {
                if (mon instanceof monitor){
                    ((monitor)mon).stop();
                }
            }
        }
        catch (Exception e) {
            this.printerror("Error while stopping monitors: " + e.getMessage());
        }
    }

    public void reloadLine(String line){
        monitor m = this.getMonitorFromDirNum(line);
        m.removeObservers();
        m.addObservers();
    }
    
    private void stopProviders() {
        try {
            for (Object prov : this.providers) {
                if (prov instanceof ctiprovider) {
                    this.println("Removing observer from provider: " + ((ctiprovider)prov).getProviderString());
                    ((ctiprovider)prov).getProvider().removeObserver(this);
                    this.println("Shutting down provider: " + ((ctiprovider)prov).getProviderString());
                    ((ctiprovider)prov).getProvider().shutdown();
                }
            }
        }
        catch (Exception e ){
            this.printerror("Error while stopping providers: " + e.getMessage());
        }
    }

    private void stopProvider(String provider){
        try{
            for (Object prov : this.providers) {
                if (prov instanceof ctiprovider && ((ctiprovider)prov).getProviderName().equalsIgnoreCase(provider)) {
                    this.println("Removing observer from provider: " + ((ctiprovider)prov).getProviderString());
                    ((ctiprovider)prov).getProvider().removeObserver(this);
                    this.println("Shutting down provider: " + ((ctiprovider)prov).getProviderString());
                    ((ctiprovider)prov).getProvider().shutdown();
                    break;
                }
            }
        } catch (Exception e){
            this.printerror("Error while stopping the provider " + provider);
        }
    }

    private void startProvider(String provider){
        try{
            for (Object prov : this.providers) {
                if (prov instanceof ctiprovider && ((ctiprovider)prov).getProviderName().equalsIgnoreCase(provider)) {
                    this.println("Adding observer to the provider: " + ((ctiprovider)prov).getProviderString());
                    ((ctiprovider)prov).getProvider().addObserver(this);
                    this.println("Waiting for in service signal: " + ((ctiprovider)prov).getProviderString());

                    int maxwait = 60000;
                    int compteur = 0;
                    int stepwait = 200;
                    while (((ctiprovider)prov).getProvider().getState() != Provider.IN_SERVICE && compteur < maxwait) {
                        Thread.sleep(stepwait);
                        compteur += stepwait;
                    }
                    if (compteur >= maxwait) {
                        this.println("Provider is not in service after a while...");
                    }
                    break;
                }
            }
        } catch (Exception e){
            this.printerror("Error while stopping the provider " + provider);
        }
    }

    /**
     *
     * @param provider
     * @throws InterruptedException
     */
    public void reloadProvider(String provider) throws InterruptedException{
        this.stopProvider(provider);
        Thread.sleep(2000);
        this.startProvider(provider);
    }

    
    /**
     * \brief Implémentation de la méthode d'appel
     * @param caller Extension de la ligne appelante
     * @param callee Destination de l'appel
     * @return Identificateur de l'appel
     */
    public String Call(String caller, String callee) {
        String callid = null;
        monitor m = this.getMonitorFromDirNum(caller);
        if (m != null){
            try {
                Call c = m.createCall();
                c.connect(m.getTerminal(), m.getAddress(), callee);
                callid = m.getCallId(c.toString());
            }
            catch (Exception createCallException) {
                this.printerror("Failed to createCall or connect: " + createCallException.toString());
            }
        }
        return callid;
    }
    /**
     * \brief Implémentation de la méthode de renvoi inconditionnel
     * @param caller Extension de la ligne à renvoyer
     * @param destination Destination du renvoi
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean Forward(String caller, String destination) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(caller);
        if (m != null){
            try {
                if (m.getAddress() instanceof CallControlAddress) {
                    if (destination.matches("")) {
                        ((CallControlAddress)m.getAddress()).cancelForwarding();
                        result = true;
                    }
                    else {
                        CallControlForwarding ccf = new CallControlForwarding(destination);
                        CallControlForwarding[] ccfs = new CallControlForwarding[1];
                        ccfs[0] = ccf;
                        ((CallControlAddress)m.getAddress()).setForwarding(ccfs);
                        result = true;
                    }
                }
            }
            catch (Exception forwardException) {
                this.printerror("Failed to forward from: " + caller + " " + forwardException.toString());
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode de transfert consultatif
     * @param callid Identificateur de l'appel
     * @param callee Extension de la ligne qui initie le transfert
     * @param destination Destination du transfert
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean ConsultTransfer(String callid, String callee, String destination) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(callee);
        if (m != null){
            try {
                Call c = m.createCall();
                TerminalConnection tc = m.getTerminalConnectionFromCallId(callid);
                if (c instanceof CallControlCall) {
                    ((CallControlCall)c).consult(tc, destination);
                    result = true;
                }
            }
            catch (Exception consulttransferException) {
                this.printerror("Failed to consult transfer "+ callid + " from : "+ callee + " " + consulttransferException.toString());
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode prise d'appel
     * @param callee Extension de la ligne appelée
     * @param callid Identificateur de l'appel
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean UnHook(String callee, String callid) {
        String message = "Unhook call: " + callid + " from " + callee;
        this.println(message);
        Boolean result = false;
        this.println("Getting monitor for " + message);
        monitor m = this.getMonitorFromDirNum(callee);
        if (m != null){
            this.println("Monitor retreived for "+message);
            try {
                this.println("Getting terminal connection for "+ message);
                TerminalConnection tc = m.getTerminalConnectionFromCallId(callid);
                this.print("Answering for "+ message);
                tc.answer();
                result = true;
            }
            catch (Exception unHookException) {
                this.printerror(message+ " failed: " + unHookException.toString());
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode de renvoi immédiat
     * @param callid Identificateur de l'appel
     * @param caller Extension de la ligne appelée
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean Divert(String callid, String caller) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(caller);
        if (m != null){
            try {
                Connection c = m.getConnectionFromCallId(callid);
                if (c instanceof CallControlConnection) {
                    ((CallControlConnection)c).reject();
                    result = true;
                }
            }
            catch (Exception divertException) {
                this.printerror("Failed to divert "+ callid + " from : "+ caller + " " + divertException.toString());
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode d'action ou désactivation de la fonction 'Ne pas déranger'
     * @param caller Extension de la ligne pour laquelle configurer l'option
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean DoNotDisturb(String caller) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(caller);
        if (m != null){
            try {
                if (m.getAddress() instanceof CallControlAddress) {
                    ((CallControlAddress)m.getAddress()).setDoNotDisturb(!m.getDnd());
                }
            }
            catch (Exception dndException) {
                this.printerror("Failed to dnd from: " + caller + " " + dndException.toString());
                this.println("trying with terminal from " + caller) ;
                Terminal t = m.getTerminal();
                if (t instanceof CallControlTerminal) {
                    this.println(caller + " has a CallControlTerminal...");
                    try {
                        ((CallControlTerminal)t).setDoNotDisturb(!m.getDnd());
                    }
                    catch (Exception callControlTerminalException) {
                        this.println("Failed to set dmd from :" + caller + ": " + callControlTerminalException.toString() );
                    }
                }
                else {
                    this.println(caller + " has no CallControlTerminal...");
                }
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode de mise en attente
     * @param callid Identificateur de l'appel
     * @param callee Extension de la ligne qui met en attente
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean Hold(String callid, String callee) {
        String message = "Hold call from " + callid + " by " + callee;
        this.println(message);
        Boolean result = false;
        this.println("Getting monitor for " + message);
        monitor m = this.getMonitorFromDirNum(callee);
        if (m != null){
            this.println("Monitor retreived for "+message);
            try {
                this.println("Getting terminal connection for "+ message);
                TerminalConnection tc = m.getTerminalConnectionFromCallId(callid);
                
                if (tc instanceof CallControlTerminalConnection) {
                    ((CallControlTerminalConnection)tc).hold();
                    result = true;
                }
                else {
                    this.println("Terminal connection in not CallControlTerminalConnection for: " + message);
                }
            }
            catch (Exception holdException) {
                this.printerror(message + " failed: " + holdException.toString());
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode de reprise d'appel
     * @param callid Identificateur de l'appel
     * @param callee Extension de la ligne qui reprends l'appel
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean UnHold(String callid, String callee){
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(callee);
        if (m != null){
            try {
                TerminalConnection tc = m.getTerminalConnectionFromCallId(callid);
                if (tc instanceof CallControlTerminalConnection) {
                    ((CallControlTerminalConnection)tc).unhold();
                    result = true;
                }
            }
            catch (Exception unholdException) {
                this.printerror("Failed to unhook "+ callid + " from : "+ callee + " " + unholdException.toString());
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode de transfert direct
     * @param callid Identificateur de l'appel
     * @param callee Extension de la ligne qui effectue le transfert
     * @param destination Destination du transfert
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean Transfer(String callid, String callee, String destination) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(callee);
        if (m != null){
            try {
                Call c = m.getCallFromCallid(callid);
                TerminalConnection tc = m.getTerminalConnectionFromCallId(callid);
                if (c instanceof CallControlCall) {
                    ((CallControlCall)c).setTransferController(tc);
                    ((CallControlCall)c).transfer(destination);
                    result = true;
                }
            }
            catch (Exception transferException) {
                this.printerror("Failed to transfer "+ callid + " from : "+ callee + " " + transferException.toString());
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode de mise en relation de deux parties (suite à un transfert consultatif)
     * @param caller Extension de la ligne qui a initié le transfert
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean Transfer(String caller) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(caller);
        
        if (m != null){
            try {
                TerminalConnection[] tcs = m.getTerminal().getTerminalConnections();
                this.println("Transfer requested by " + m.getDirNum() + " with " + tcs.length + " terminal connections");
                TerminalConnection transferCtrl = null;
                CallControlCall callFrom = null;
                CallControlCall callTo = null;
                
                if (tcs.length == 2) {
                    this.println("Parsing terminal connection from " + m.getDirNum());
                    for (TerminalConnection tc : tcs) {
                        if (tc instanceof CallControlTerminalConnection) {
                            this.println("Terminal connection: " + tc.toString() + " from " + m.getDirNum());
                            if (((CallControlTerminalConnection)tc).getCallControlState() == CallControlTerminalConnection.HELD) {
                                transferCtrl = tc;
                                callFrom = (CallControlCall)tc.getConnection().getCall();
                            }
                            if (((CallControlTerminalConnection)tc).getCallControlState() == CallControlTerminalConnection.TALKING) {
                                callTo = (CallControlCall)tc.getConnection().getCall();
                            }
                        }
                    }
                    
                    if (transferCtrl != null && callFrom != null && callTo != null) {
                        callFrom.setTransferController(transferCtrl);
                        callFrom.transfer(callTo);
                        result = true;
                    }
                    else {
                        this.printerror("Unable to transfer: either call or transfer controller is null" );
                    }
                }
                else {
                    this.printerror("Unable to transfer whithout at least two Terminal Connection: " + m.getTerminalName());
                }
            }
            catch (Exception transferException) {
                this.printerror("Failed to transfer from : "+ caller + " " + transferException.toString());
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode de fin d'appel
     * @param caller Extension de la ligne
     * @param callid Identificateur de la ligne
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean HangUp(String caller, String callid) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(caller);
        if (m != null){
            try {
                Connection c = m.getConnectionFromCallId(callid);
                if (c instanceof CallControlConnection) {
                    ((CallControlConnection)c).disconnect();
                    result = true;
                }
            }
            catch (Exception hangUpException) {
                this.printerror("Failed to hangup "+ callid + " from : "+ caller + " " + hangUpException.toString());
            }
        }
        return result;
    }

    /**
     * \brief Implémentation de la méthode d'écoute discrète
     * @param monitorer Extension de la ligne de supervision
     * @param monitored Extension de la ligne à superviser
     * @return Vrai ou faux selon le succès de l'opération
     */
    public Boolean Monitor(String monitorer, String monitored) {
        return false;
    }
    /**
     * \brief Implémentation de la méthode d'interception depuis groupement
     * @param picker Numérodu groupement
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean PickupFromGroup(String picker) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(picker);
        if (m != null){
            Terminal t = m.getTerminal();
            if (t instanceof CallControlTerminal) {
                try {
                    if (((CallControlTerminal)t).pickupFromGroup(m.getAddress()) != null) {
                        result = true;
                    }
                }
                catch (Exception e) {
                    this.printerror("Pickup from " + picker + " failed: " + e.getMessage());
                }
            }
            else {
                this.println("Unable to pickup from " + picker + ", terminal is not instance of CallControlTerminal");
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode d'interception 
     * @param picker Extension de la ligne qui intercepte
     * @param picked Extension de la ligne à intercepter
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean Pickup(String picker, String picked) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(picker);
        if (m != null) {
            Terminal t = m.getTerminal();
            if (t instanceof CallControlTerminal){
                Address pickedAddress = null;
                try {
                    Enumeration pros = providers.elements();
                    while (pros.hasMoreElements()) {
                        pickedAddress = ((Provider)pros.nextElement()).getAddress(picked);
                        if (pickedAddress != null) {
                            break;
                        }
                    }
                    if (((CallControlTerminal)t).pickup(m.getAddress(), pickedAddress) != null) {
                        result = true;
                    }
                }
                catch (Exception e) {
                    this.printerror("Pickup "+ picked+" from " + picker + " failed: " + e.getMessage());
                }
            }
            else {
                this.println("Unable to pickup from " + picker + ", terminal is not instance of CallControlTerminal");
            }
        }
        return result;
    }
    /**
     * \brief Implémentation de la méthode de mise en conference
     * @param caller Extension de la ligne qui initie la conférence
     * @param callee Extension du nouveau participant
     * @return Vrai ou faux sleon le succès de l'opération
     */
    public Boolean Conference(String caller, String callee) {
        Boolean result = false;
        monitor m = this.getMonitorFromDirNum(caller);
        if (m != null) {
            CallControlCall ccc = m.getCurrentCallControlCall();
            if (ccc != null) {
                try {
                    Call c = m.createCall();
                    c.connect(m.getTerminal(), m.getAddress(), callee);
                    if (c != null) {
                        ccc.conference(c);
                    }
                }
                catch (Exception e) {
                    this.printerror("Unable to make a conference from " + caller + ": " + e.getMessage());
                }
            }
        }
        return result;
    }

    public Boolean SendDTMF(String caller, String dtmf) {
        Boolean result = false;
        try {
            monitor m = this.getMonitorFromDirNum(caller);
            if (m != null) {

                 TerminalConnection[] tcs = m.getTerminal().getTerminalConnections();
                 if (tcs != null && tcs.length > 0 ) {
                    this.println("Try to instanciate TerminalConnection as MediaTerminalConnection");
                    tcs[0] = (MediaTerminalConnection)tcs[0];
                    if (tcs[0] instanceof MediaTerminalConnection){
                        this.println("Generate DTMF from " + m.getDirNum() + " with " + tcs.length + " terminal connections");
                        ((MediaTerminalConnection)tcs[0]).generateDtmf(dtmf);
                        result = true;
                    }
                 }
            }
        } catch (Exception e) {
            this.printerror("Unable to generate DTMF " +dtmf+ " from " + caller + ": " + e.getMessage());
        }
        return result;
    }
    
    /**
     * \brief Représente un monitor @see monitor
     * @param dir Extension
     * @return monitor
     */
    public monitor getMonitorFromDirNum(String dir) {
        monitor m = null;
        for(Object addr : this.monitors) {
            if (addr instanceof monitor) {
                if(((monitor)addr).getDirNum().matches(dir) ) {
                    m = (monitor)addr;
                }
            }
        }
        return m;
    }
    
}
