
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for anonymous complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="dn" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="call" type="{http://wybecom.org/talkportal/cti/stateserver}Call" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "dn",
    "call"
})
@XmlRootElement(name = "AddCallLogs")
public class AddCallLogs {

    protected String dn;
    protected Call call;

    /**
     * Gets the value of the dn property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getDn() {
        return dn;
    }

    /**
     * Sets the value of the dn property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setDn(String value) {
        this.dn = value;
    }

    /**
     * Gets the value of the call property.
     * 
     * @return
     *     possible object is
     *     {@link Call }
     *     
     */
    public Call getCall() {
        return call;
    }

    /**
     * Sets the value of the call property.
     * 
     * @param value
     *     allowed object is
     *     {@link Call }
     *     
     */
    public void setCall(Call value) {
        this.call = value;
    }

}
