//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vhudson-jaxb-ri-2.1-463 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2009.06.25 at 04:06:34 PM CEST 
//
/*
    This file is part of TALK (Wybecom).

    TALK is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License, version 2 as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    TALK is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with TALK.  If not, see <http://www.gnu.org/licenses/gpl-2.0.html>.
 
    TALK is based upon:
    - Sun JTAPI http://java.sun.com/products/jtapi/
    - JulMar TAPI http://julmar.com/
    - Asterisk.Net http://sourceforge.net/projects/asterisk-dotnet/
 
*/

package org.wybecom.talk.jtapi.configuration;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlType;


/**
 * \brief Java class for cticonfig complex type.
 * <p>Java class for cticonfig complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="cticonfig">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="provider" type="{http://xml.netbeans.org/schema/cticonfig}xmlprovider" maxOccurs="unbounded" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "cticonfig", propOrder = {
    "provider"
})
public class Cticonfig {

    protected List<Xmlprovider> provider;

    /**
     * Gets the value of the provider property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the provider property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getProvider().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link Xmlprovider }
     * 
     * 
     */
    public List<Xmlprovider> getProvider() {
        if (provider == null) {
            provider = new ArrayList<Xmlprovider>();
        }
        return this.provider;
    }

}