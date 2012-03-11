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


import javax.telephony.callcenter.*;
import javax.telephony.*;
import javax.telephony.capabilities.*;
import java.util.*;
import org.wybecom.talk.team.config.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class callcenterprovider implements CallCenterProvider{

    private Provider provider;
    private Teamconfig team;
    private ACDAddress[] acdaddresses;
    private ACDManagerAddress[] acdmanageraddresses;

    public callcenterprovider(Provider p, Teamconfig config) throws MethodNotSupportedException, ResourceUnavailableException {
        provider = p;
        team = config;
        init();
    }

    public RouteAddress[] getRouteableAddresses() throws MethodNotSupportedException, ResourceUnavailableException {
        Vector v = new Vector();
        for (Address a : this.getAddresses()){
            if (a instanceof RouteAddress){
                v.add((RouteAddress)a);
            }
        }
        return (RouteAddress[])v.toArray(new RouteAddress[0]);
    }

    public ACDAddress[] getACDAddresses() throws MethodNotSupportedException, ResourceUnavailableException {
        return acdaddresses;
    }

    public ACDManagerAddress[] getACDManagerAddresses() throws MethodNotSupportedException, ResourceUnavailableException {
        return acdmanageraddresses;
    }

    private void init()throws MethodNotSupportedException, ResourceUnavailableException{
        //acdmanageraddresses
        Vector v = new Vector();
        List<String> agList = null;
        for (Xmlacdmanageraddressgroup group : team.getAcdmanageraddressgroup()){
                
                if (agList == null){
                    agList = group.getAcdmanageraddress();
                }
                else{
                    agList.addAll(group.getAcdmanageraddress());
                }
        }
        
        for (Address a : this.getAddresses()){
            if (agList != null){
                for(String s : agList.toArray(new String[0])){
                    if (a.getName().equalsIgnoreCase(s)){
                        v.add(new acdmanageraddress(a));
                    }
                }
            }
        }
        acdmanageraddresses = (ACDManagerAddress[])v.toArray(new ACDManagerAddress[0]);
        //acdaddresses
        v = new Vector();
        for (Address a : this.getAddresses()){
            if (a instanceof RouteAddress){
                Vector managers = new Vector();
                for (Xmlteam t : team.getTeam()){
                    if (t.getRouteaddress().equalsIgnoreCase(a.getName())){
                        for(Xmlacdmanageraddressgroup group : team.getAcdmanageraddressgroup()){
                            if (t.getAcdmanageraddressgroupname().equalsIgnoreCase(group.getName())){


                                for (ACDManagerAddress ama : acdmanageraddresses){
                                    for (String s : group.getAcdmanageraddress()){
                                        if (ama.getName().equalsIgnoreCase(s)){
                                            managers.add(ama);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                acdaddress aa = new acdaddress(a, (ACDManagerAddress[])managers.toArray(new ACDManagerAddress[0]));
                v.add(aa);
            }
        }
        acdaddresses = (ACDAddress[])v.toArray(new ACDAddress[0]);
    }

    public int getState(){
        return provider.getState();
    }

    public String getName() {
        return provider.getName();
    }

    public Call[] getCalls() throws ResourceUnavailableException {
        return provider.getCalls();
    }

    public Address getAddress(String number) throws InvalidArgumentException {
        return provider.getAddress(number);
    }

    public Address[] getAddresses() throws ResourceUnavailableException {
        return provider.getAddresses();
    }

    public Terminal[] getTerminals() throws ResourceUnavailableException {
        return provider.getTerminals();
    }

    public Terminal getTerminal(String name) throws InvalidArgumentException {
        return provider.getTerminal(name);
    }

    public void shutdown() {
        provider.shutdown();
    }

    public Call createCall() throws ResourceUnavailableException, InvalidStateException, PrivilegeViolationException, MethodNotSupportedException {
        return provider.createCall();
    }

    public void addObserver(ProviderObserver observer) throws ResourceUnavailableException, MethodNotSupportedException {
        provider.addObserver(observer);
    }

    public ProviderObserver[] getObservers() {
        return provider.getObservers();
    }

    public void removeObserver(ProviderObserver observer) {
        provider.removeObserver(observer);
    }

    public ProviderCapabilities getProviderCapabilities() {
        return provider.getProviderCapabilities();
    }

    public CallCapabilities getCallCapabilities() {
        return provider.getCallCapabilities();
    }

    public AddressCapabilities getAddressCapabilities() {
        return provider.getAddressCapabilities();
    }

    public TerminalCapabilities getTerminalCapabilities() {
        return provider.getTerminalCapabilities();
    }

    public ConnectionCapabilities getConnectionCapabilities() {
        return provider.getConnectionCapabilities();
    }
    public TerminalConnectionCapabilities getTerminalConnectionCapabilities() {
        return provider.getTerminalConnectionCapabilities();
    }

    public ProviderCapabilities getCapabilities() {
        return provider.getCapabilities();
    }

    public ProviderCapabilities getProviderCapabilities(Terminal terminal) throws InvalidArgumentException, PlatformException {
        return provider.getProviderCapabilities(terminal);
    }

    public CallCapabilities getCallCapabilities(Terminal terminal, Address address) throws InvalidArgumentException, PlatformException {
        return provider.getCallCapabilities(terminal, address);
    }

    public ConnectionCapabilities getConnectionCapabilities(Terminal terminal, Address address) throws InvalidArgumentException, PlatformException {
        return provider.getConnectionCapabilities(terminal, address);
    }

    public AddressCapabilities getAddressCapabilities(Terminal terminal) throws InvalidArgumentException, PlatformException {
        return provider.getAddressCapabilities(terminal);
    }

    public TerminalConnectionCapabilities getTerminalConnectionCapabilities(Terminal terminal) throws InvalidArgumentException, PlatformException {
        return provider.getTerminalConnectionCapabilities(terminal);
    }

    public TerminalCapabilities getTerminalCapabilities(Terminal terminal) throws InvalidArgumentException, PlatformException {
        return provider.getTerminalCapabilities(terminal);
    }





}
