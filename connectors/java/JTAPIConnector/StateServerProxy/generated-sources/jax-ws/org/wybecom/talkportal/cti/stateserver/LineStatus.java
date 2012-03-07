
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlSeeAlso;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for LineStatus complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="LineStatus">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="directoryNumber" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="forward" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="doNotDisturb" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *         &lt;element name="mwiOn" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *         &lt;element name="status" type="{http://wybecom.org/talkportal/cti/stateserver}Status"/>
 *         &lt;element name="monitored" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "LineStatus", propOrder = {
    "directoryNumber",
    "forward",
    "doNotDisturb",
    "mwiOn",
    "status",
    "monitored"
})
@XmlSeeAlso({
    LineControl.class
})
public class LineStatus {

    protected String directoryNumber;
    protected String forward;
    protected boolean doNotDisturb;
    protected boolean mwiOn;
    @XmlElement(required = true)
    protected Status status;
    protected String monitored;

    /**
     * Gets the value of the directoryNumber property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getDirectoryNumber() {
        return directoryNumber;
    }

    /**
     * Sets the value of the directoryNumber property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setDirectoryNumber(String value) {
        this.directoryNumber = value;
    }

    /**
     * Gets the value of the forward property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getForward() {
        return forward;
    }

    /**
     * Sets the value of the forward property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setForward(String value) {
        this.forward = value;
    }

    /**
     * Gets the value of the doNotDisturb property.
     * 
     */
    public boolean isDoNotDisturb() {
        return doNotDisturb;
    }

    /**
     * Sets the value of the doNotDisturb property.
     * 
     */
    public void setDoNotDisturb(boolean value) {
        this.doNotDisturb = value;
    }

    /**
     * Gets the value of the mwiOn property.
     * 
     */
    public boolean isMwiOn() {
        return mwiOn;
    }

    /**
     * Sets the value of the mwiOn property.
     * 
     */
    public void setMwiOn(boolean value) {
        this.mwiOn = value;
    }

    /**
     * Gets the value of the status property.
     * 
     * @return
     *     possible object is
     *     {@link Status }
     *     
     */
    public Status getStatus() {
        return status;
    }

    /**
     * Sets the value of the status property.
     * 
     * @param value
     *     allowed object is
     *     {@link Status }
     *     
     */
    public void setStatus(Status value) {
        this.status = value;
    }

    /**
     * Gets the value of the monitored property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMonitored() {
        return monitored;
    }

    /**
     * Sets the value of the monitored property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMonitored(String value) {
        this.monitored = value;
    }

}
