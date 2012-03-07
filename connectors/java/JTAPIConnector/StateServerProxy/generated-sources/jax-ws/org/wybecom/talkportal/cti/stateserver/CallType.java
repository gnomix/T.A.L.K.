
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlEnumValue;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for CallType.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * &lt;simpleType name="CallType">
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     &lt;enumeration value="missed"/>
 *     &lt;enumeration value="placed"/>
 *     &lt;enumeration value="received"/>
 *   &lt;/restriction>
 * &lt;/simpleType>
 * </pre>
 * 
 */
@XmlType(name = "CallType")
@XmlEnum
public enum CallType {

    @XmlEnumValue("missed")
    MISSED("missed"),
    @XmlEnumValue("placed")
    PLACED("placed"),
    @XmlEnumValue("received")
    RECEIVED("received");
    private final String value;

    CallType(String v) {
        value = v;
    }

    public String value() {
        return value;
    }

    public static CallType fromValue(String v) {
        for (CallType c: CallType.values()) {
            if (c.value.equals(v)) {
                return c;
            }
        }
        throw new IllegalArgumentException(v);
    }

}
