
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
 *         &lt;element name="SetAgentLineControlResult" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
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
    "setAgentLineControlResult"
})
@XmlRootElement(name = "SetAgentLineControlResponse")
public class SetAgentLineControlResponse {

    @XmlElement(name = "SetAgentLineControlResult")
    protected boolean setAgentLineControlResult;

    /**
     * Gets the value of the setAgentLineControlResult property.
     * 
     */
    public boolean isSetAgentLineControlResult() {
        return setAgentLineControlResult;
    }

    /**
     * Sets the value of the setAgentLineControlResult property.
     * 
     */
    public void setSetAgentLineControlResult(boolean value) {
        this.setAgentLineControlResult = value;
    }

}
