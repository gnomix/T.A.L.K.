//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vhudson-jaxb-ri-2.2-147 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2012.03.01 at 03:03:49 PM CET 
//


package org.wybecom.talk.team.config;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for teamconfig complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="teamconfig">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="team" type="{http://xml.netbeans.org/schema/teamconfig}xmlteam" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="acdmanageraddressgroup" type="{http://xml.netbeans.org/schema/teamconfig}xmlacdmanageraddressgroup" maxOccurs="unbounded" minOccurs="0"/>
 *         &lt;element name="teamprovider" type="{http://xml.netbeans.org/schema/teamconfig}xmlteamprovider"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "teamconfig", propOrder = {
    "team",
    "acdmanageraddressgroup",
    "teamprovider"
})
public class Teamconfig {

    protected List<Xmlteam> team;
    protected List<Xmlacdmanageraddressgroup> acdmanageraddressgroup;
    @XmlElement(required = true)
    protected Xmlteamprovider teamprovider;

    /**
     * Gets the value of the team property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the team property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getTeam().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link Xmlteam }
     * 
     * 
     */
    public List<Xmlteam> getTeam() {
        if (team == null) {
            team = new ArrayList<Xmlteam>();
        }
        return this.team;
    }

    /**
     * Gets the value of the acdmanageraddressgroup property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the acdmanageraddressgroup property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getAcdmanageraddressgroup().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link Xmlacdmanageraddressgroup }
     * 
     * 
     */
    public List<Xmlacdmanageraddressgroup> getAcdmanageraddressgroup() {
        if (acdmanageraddressgroup == null) {
            acdmanageraddressgroup = new ArrayList<Xmlacdmanageraddressgroup>();
        }
        return this.acdmanageraddressgroup;
    }

    /**
     * Gets the value of the teamprovider property.
     * 
     * @return
     *     possible object is
     *     {@link Xmlteamprovider }
     *     
     */
    public Xmlteamprovider getTeamprovider() {
        return teamprovider;
    }

    /**
     * Sets the value of the teamprovider property.
     * 
     * @param value
     *     allowed object is
     *     {@link Xmlteamprovider }
     *     
     */
    public void setTeamprovider(Xmlteamprovider value) {
        this.teamprovider = value;
    }

}