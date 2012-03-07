/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
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
package org.wybecom.talk.jtapi.stateserver.client.config;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlType;


/**
 * \brief Java class for xmlevent complex type.
 * <p>Java class for xmlevent complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="xmlevent">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;attribute name="name" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       &lt;attribute name="enabled" type="{http://www.w3.org/2001/XMLSchema}boolean" />
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "xmlevent")
public class Xmlevent {

    @XmlAttribute
    protected String name;
    @XmlAttribute
    protected Boolean enabled;

    /**
     * Gets the value of the name property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getName() {
        return name;
    }

    /**
     * Sets the value of the name property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setName(String value) {
        this.name = value;
    }

    /**
     * Gets the value of the enabled property.
     * 
     * @return
     *     possible object is
     *     {@link Boolean }
     *     
     */
    public Boolean isEnabled() {
        return enabled;
    }

    /**
     * Sets the value of the enabled property.
     * 
     * @param value
     *     allowed object is
     *     {@link Boolean }
     *     
     */
    public void setEnabled(Boolean value) {
        this.enabled = value;
    }

}
