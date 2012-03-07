
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
 *         &lt;element name="AddCallLogsResult" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
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
    "addCallLogsResult"
})
@XmlRootElement(name = "AddCallLogsResponse")
public class AddCallLogsResponse {

    @XmlElement(name = "AddCallLogsResult")
    protected boolean addCallLogsResult;

    /**
     * Gets the value of the addCallLogsResult property.
     * 
     */
    public boolean isAddCallLogsResult() {
        return addCallLogsResult;
    }

    /**
     * Sets the value of the addCallLogsResult property.
     * 
     */
    public void setAddCallLogsResult(boolean value) {
        this.addCallLogsResult = value;
    }

}
