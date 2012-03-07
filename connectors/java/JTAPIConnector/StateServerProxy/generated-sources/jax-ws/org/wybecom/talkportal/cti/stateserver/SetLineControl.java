
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
 *         &lt;element name="lc" type="{http://wybecom.org/talkportal/cti/stateserver}LineControl" minOccurs="0"/>
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
    "lc"
})
@XmlRootElement(name = "SetLineControl")
public class SetLineControl {

    protected LineControl lc;

    /**
     * Gets the value of the lc property.
     * 
     * @return
     *     possible object is
     *     {@link LineControl }
     *     
     */
    public LineControl getLc() {
        return lc;
    }

    /**
     * Sets the value of the lc property.
     * 
     * @param value
     *     allowed object is
     *     {@link LineControl }
     *     
     */
    public void setLc(LineControl value) {
        this.lc = value;
    }

}
