
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for CallCenterCall complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="CallCenterCall">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="caller" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="applicationData" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="lastredirectnumber" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="callvariables" type="{http://wybecom.org/talkportal/cti/stateserver}ArrayOfString" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "CallCenterCall", propOrder = {
    "caller",
    "applicationData",
    "lastredirectnumber",
    "callvariables"
})
public class CallCenterCall {

    protected String caller;
    protected String applicationData;
    protected String lastredirectnumber;
    protected ArrayOfString callvariables;

    /**
     * Gets the value of the caller property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCaller() {
        return caller;
    }

    /**
     * Sets the value of the caller property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCaller(String value) {
        this.caller = value;
    }

    /**
     * Gets the value of the applicationData property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getApplicationData() {
        return applicationData;
    }

    /**
     * Sets the value of the applicationData property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setApplicationData(String value) {
        this.applicationData = value;
    }

    /**
     * Gets the value of the lastredirectnumber property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLastredirectnumber() {
        return lastredirectnumber;
    }

    /**
     * Sets the value of the lastredirectnumber property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLastredirectnumber(String value) {
        this.lastredirectnumber = value;
    }

    /**
     * Gets the value of the callvariables property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfString }
     *     
     */
    public ArrayOfString getCallvariables() {
        return callvariables;
    }

    /**
     * Sets the value of the callvariables property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfString }
     *     
     */
    public void setCallvariables(ArrayOfString value) {
        this.callvariables = value;
    }

}
