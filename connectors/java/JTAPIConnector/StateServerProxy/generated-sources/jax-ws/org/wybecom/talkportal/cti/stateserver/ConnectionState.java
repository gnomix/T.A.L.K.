
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlEnumValue;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for ConnectionState.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * &lt;simpleType name="ConnectionState">
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     &lt;enumeration value="unknown"/>
 *     &lt;enumeration value="idle"/>
 *     &lt;enumeration value="failed"/>
 *     &lt;enumeration value="disconnected"/>
 *     &lt;enumeration value="established"/>
 *     &lt;enumeration value="alerting"/>
 *     &lt;enumeration value="offered"/>
 *     &lt;enumeration value="queued"/>
 *     &lt;enumeration value="network_reached"/>
 *     &lt;enumeration value="network_alerting"/>
 *     &lt;enumeration value="initiated"/>
 *     &lt;enumeration value="dialing"/>
 *   &lt;/restriction>
 * &lt;/simpleType>
 * </pre>
 * 
 */
@XmlType(name = "ConnectionState")
@XmlEnum
public enum ConnectionState {

    @XmlEnumValue("unknown")
    UNKNOWN("unknown"),
    @XmlEnumValue("idle")
    IDLE("idle"),
    @XmlEnumValue("failed")
    FAILED("failed"),
    @XmlEnumValue("disconnected")
    DISCONNECTED("disconnected"),
    @XmlEnumValue("established")
    ESTABLISHED("established"),
    @XmlEnumValue("alerting")
    ALERTING("alerting"),
    @XmlEnumValue("offered")
    OFFERED("offered"),
    @XmlEnumValue("queued")
    QUEUED("queued"),
    @XmlEnumValue("network_reached")
    NETWORK_REACHED("network_reached"),
    @XmlEnumValue("network_alerting")
    NETWORK_ALERTING("network_alerting"),
    @XmlEnumValue("initiated")
    INITIATED("initiated"),
    @XmlEnumValue("dialing")
    DIALING("dialing");
    private final String value;

    ConnectionState(String v) {
        value = v;
    }

    public String value() {
        return value;
    }

    public static ConnectionState fromValue(String v) {
        for (ConnectionState c: ConnectionState.values()) {
            if (c.value.equals(v)) {
                return c;
            }
        }
        throw new IllegalArgumentException(v);
    }

}
