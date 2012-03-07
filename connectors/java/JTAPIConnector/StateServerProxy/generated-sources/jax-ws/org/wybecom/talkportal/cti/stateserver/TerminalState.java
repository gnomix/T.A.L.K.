
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlEnumValue;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for TerminalState.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * &lt;simpleType name="TerminalState">
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     &lt;enumeration value="idle"/>
 *     &lt;enumeration value="ringing"/>
 *     &lt;enumeration value="talking"/>
 *     &lt;enumeration value="held"/>
 *     &lt;enumeration value="bridged"/>
 *     &lt;enumeration value="inuse"/>
 *     &lt;enumeration value="dropped"/>
 *     &lt;enumeration value="unknown"/>
 *   &lt;/restriction>
 * &lt;/simpleType>
 * </pre>
 * 
 */
@XmlType(name = "TerminalState")
@XmlEnum
public enum TerminalState {

    @XmlEnumValue("idle")
    IDLE("idle"),
    @XmlEnumValue("ringing")
    RINGING("ringing"),
    @XmlEnumValue("talking")
    TALKING("talking"),
    @XmlEnumValue("held")
    HELD("held"),
    @XmlEnumValue("bridged")
    BRIDGED("bridged"),
    @XmlEnumValue("inuse")
    INUSE("inuse"),
    @XmlEnumValue("dropped")
    DROPPED("dropped"),
    @XmlEnumValue("unknown")
    UNKNOWN("unknown");
    private final String value;

    TerminalState(String v) {
        value = v;
    }

    public String value() {
        return value;
    }

    public static TerminalState fromValue(String v) {
        for (TerminalState c: TerminalState.values()) {
            if (c.value.equals(v)) {
                return c;
            }
        }
        throw new IllegalArgumentException(v);
    }

}
