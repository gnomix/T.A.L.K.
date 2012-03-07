
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for LineControl complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="LineControl">
 *   &lt;complexContent>
 *     &lt;extension base="{http://wybecom.org/talkportal/cti/stateserver}LineStatus">
 *       &lt;sequence>
 *         &lt;element name="lineControlConnection" type="{http://wybecom.org/talkportal/cti/stateserver}ArrayOfLineControlConnection" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/extension>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "LineControl", propOrder = {
    "lineControlConnection"
})
public class LineControl
    extends LineStatus
{

    protected ArrayOfLineControlConnection lineControlConnection;

    /**
     * Gets the value of the lineControlConnection property.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfLineControlConnection }
     *     
     */
    public ArrayOfLineControlConnection getLineControlConnection() {
        return lineControlConnection;
    }

    /**
     * Sets the value of the lineControlConnection property.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfLineControlConnection }
     *     
     */
    public void setLineControlConnection(ArrayOfLineControlConnection value) {
        this.lineControlConnection = value;
    }

}
