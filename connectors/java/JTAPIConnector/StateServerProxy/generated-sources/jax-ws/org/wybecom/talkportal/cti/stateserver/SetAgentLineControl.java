
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
 *         &lt;element name="extension" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="agentid" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="state" type="{http://wybecom.org/talkportal/cti/stateserver}AgentState"/>
 *         &lt;element name="ccc" type="{http://wybecom.org/talkportal/cti/stateserver}CallCenterCall" minOccurs="0"/>
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
    "extension",
    "agentid",
    "state",
    "ccc"
})
@XmlRootElement(name = "SetAgentLineControl")
public class SetAgentLineControl {

    protected String extension;
    protected String agentid;
    @XmlElement(required = true)
    protected AgentState state;
    protected CallCenterCall ccc;

    /**
     * Gets the value of the extension property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getExtension() {
        return extension;
    }

    /**
     * Sets the value of the extension property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setExtension(String value) {
        this.extension = value;
    }

    /**
     * Gets the value of the agentid property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getAgentid() {
        return agentid;
    }

    /**
     * Sets the value of the agentid property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setAgentid(String value) {
        this.agentid = value;
    }

    /**
     * Gets the value of the state property.
     * 
     * @return
     *     possible object is
     *     {@link AgentState }
     *     
     */
    public AgentState getState() {
        return state;
    }

    /**
     * Sets the value of the state property.
     * 
     * @param value
     *     allowed object is
     *     {@link AgentState }
     *     
     */
    public void setState(AgentState value) {
        this.state = value;
    }

    /**
     * Gets the value of the ccc property.
     * 
     * @return
     *     possible object is
     *     {@link CallCenterCall }
     *     
     */
    public CallCenterCall getCcc() {
        return ccc;
    }

    /**
     * Sets the value of the ccc property.
     * 
     * @param value
     *     allowed object is
     *     {@link CallCenterCall }
     *     
     */
    public void setCcc(CallCenterCall value) {
        this.ccc = value;
    }

}
