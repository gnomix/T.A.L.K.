
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlEnumValue;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for Status.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * &lt;simpleType name="Status">
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     &lt;enumeration value="available"/>
 *     &lt;enumeration value="donotdisturb"/>
 *     &lt;enumeration value="forwarded"/>
 *     &lt;enumeration value="busy"/>
 *     &lt;enumeration value="hidden"/>
 *     &lt;enumeration value="dialing"/>
 *     &lt;enumeration value="ringing"/>
 *     &lt;enumeration value="inactive"/>
 *     &lt;enumeration value="unknown"/>
 *   &lt;/restriction>
 * &lt;/simpleType>
 * </pre>
 * 
 */
@XmlType(name = "Status")
@XmlEnum
public enum Status {

    @XmlEnumValue("available")
    AVAILABLE("available"),
    @XmlEnumValue("donotdisturb")
    DONOTDISTURB("donotdisturb"),
    @XmlEnumValue("forwarded")
    FORWARDED("forwarded"),
    @XmlEnumValue("busy")
    BUSY("busy"),
    @XmlEnumValue("hidden")
    HIDDEN("hidden"),
    @XmlEnumValue("dialing")
    DIALING("dialing"),
    @XmlEnumValue("ringing")
    RINGING("ringing"),
    @XmlEnumValue("inactive")
    INACTIVE("inactive"),
    @XmlEnumValue("unknown")
    UNKNOWN("unknown");
    private final String value;

    Status(String v) {
        value = v;
    }

    public String value() {
        return value;
    }

    public static Status fromValue(String v) {
        for (Status c: Status.values()) {
            if (c.value.equals(v)) {
                return c;
            }
        }
        throw new IllegalArgumentException(v);
    }

}
