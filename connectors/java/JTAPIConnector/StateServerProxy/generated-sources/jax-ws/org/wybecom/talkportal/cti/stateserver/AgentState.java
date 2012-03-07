
package org.wybecom.talkportal.cti.stateserver;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for AgentState.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * <p>
 * <pre>
 * &lt;simpleType name="AgentState">
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     &lt;enumeration value="BUSY"/>
 *     &lt;enumeration value="LOG_IN"/>
 *     &lt;enumeration value="LOG_OUT"/>
 *     &lt;enumeration value="NOT_READY"/>
 *     &lt;enumeration value="READY"/>
 *     &lt;enumeration value="UNKNOWN"/>
 *     &lt;enumeration value="WORK_NOT_READY"/>
 *     &lt;enumeration value="WORK_READY"/>
 *   &lt;/restriction>
 * &lt;/simpleType>
 * </pre>
 * 
 */
@XmlType(name = "AgentState")
@XmlEnum
public enum AgentState {

    BUSY,
    LOG_IN,
    LOG_OUT,
    NOT_READY,
    READY,
    UNKNOWN,
    WORK_NOT_READY,
    WORK_READY;

    public String value() {
        return name();
    }

    public static AgentState fromValue(String v) {
        return valueOf(v);
    }

}
