
package org.wybecom.talkportal.cti.stateserver;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for ArrayOfLineControlConnection complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ArrayOfLineControlConnection">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="LineControlConnection" type="{http://wybecom.org/talkportal/cti/stateserver}LineControlConnection" maxOccurs="unbounded" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "ArrayOfLineControlConnection", propOrder = {
    "lineControlConnection"
})
public class ArrayOfLineControlConnection {

    @XmlElement(name = "LineControlConnection", nillable = true)
    protected List<LineControlConnection> lineControlConnection;

    /**
     * Gets the value of the lineControlConnection property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the lineControlConnection property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getLineControlConnection().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link LineControlConnection }
     * 
     * 
     */
    public List<LineControlConnection> getLineControlConnection() {
        if (lineControlConnection == null) {
            lineControlConnection = new ArrayList<LineControlConnection>();
        }
        return this.lineControlConnection;
    }

}
