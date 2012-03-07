
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
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
 *         &lt;element name="SetLineControlResult" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
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
    "setLineControlResult"
})
@XmlRootElement(name = "SetLineControlResponse")
public class SetLineControlResponse {

    @XmlElement(name = "SetLineControlResult")
    protected boolean setLineControlResult;

    /**
     * Gets the value of the setLineControlResult property.
     * 
     */
    public boolean isSetLineControlResult() {
        return setLineControlResult;
    }

    /**
     * Sets the value of the setLineControlResult property.
     * 
     */
    public void setSetLineControlResult(boolean value) {
        this.setLineControlResult = value;
    }

}
