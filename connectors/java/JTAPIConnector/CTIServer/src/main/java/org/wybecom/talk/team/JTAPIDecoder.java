/*
 *  WYBECOM T.A.L.K. -- Telephony Application Library Kit
 *  Copyright (C) 2010 WYBECOM
 *
 *  Yohann BARRE <y.barre@wybecom.com>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  T.A.L.K. is based upon:
 *  - Sun JTAPI http://java.sun.com/products/jtapi/
 *  - JulMar TAPI http://julmar.com/
 *  - Asterisk.Net http://sourceforge.net/projects/asterisk-dotnet/
 */
/**
 * \package
 * This package contains all JTAPI Call Center implementation
 */

package org.wybecom.talk.team;

import javax.telephony.events.*;
import javax.telephony.callcontrol.events.*;
import javax.telephony.callcenter.events.*;
import com.cisco.jtapi.extensions.*;
/**
 * 
 * @author Yohann BARRE <y.barre@wybecom.com>
 */
public class JTAPIDecoder {

	static public String getEvent ( int id ) {

		switch ( id ) {
		//
		// ProviderObserver events
		//
		case ProvInServiceEv.ID:
			return "ProvInServiceEv";
		case ProvOutOfServiceEv.ID:
			return "ProvOutOfServiceEv";
		case ProvObservationEndedEv.ID:
			return "ProvObservationEndedEv";
		case ProvShutdownEv.ID:
			return "ProvShutdownEv";
		//
		// CallObserver events
		 //
		case CallActiveEv.ID:
			return "CallActiveEvent";
		case CallInvalidEv.ID:
			return "CallInvalidEvent";
		case CallObservationEndedEv.ID:
			return "CallObservationEndedEv";
		case ConnAlertingEv.ID:
			return "ConnAlertingEvent";
		case ConnInProgressEv.ID:
			return "ConnInprogressEvent";
		case ConnConnectedEv.ID:
			return "ConnConnectedEvent";
		case ConnCreatedEv.ID:
			return "ConnCreatedEvent";
		case ConnDisconnectedEv.ID:
			return "ConnDisconnectedEvent";
		case ConnFailedEv.ID:
			return "ConnFailedEvent";
		case ConnUnknownEv.ID:
			return "ConnUnknownEvent";
		case TermConnPassiveEv.ID:
			return "TermConnPassiveEvent";
		case TermConnActiveEv.ID:
			return "TermConnActiveEvent";
		case TermConnCreatedEv.ID:
			return "TermConnCreatedEvent";
		case TermConnRingingEv.ID:
			return "TermConnRingingEvent";
		case TermConnDroppedEv.ID:
			return "TermConnDroppedEv";
		case CallCtlConnAlertingEv.ID:
			return "CallCtlConnAlertingEv";
		case CallCtlConnDialingEv.ID:
			return "CallCtlConnDialingEv";
		case CallCtlConnDisconnectedEv.ID:
			return "CallCtlConnDisconnectedEv";
		case CallCtlConnEstablishedEv.ID:
			return "CallCtlConnEstablishedEv";
		case CallCtlConnFailedEv.ID:
			return "CallCtlConnFailedEv";
		case CallCtlConnInitiatedEv.ID:
			return "CallCtlConnInitiatedEv";
		case CallCtlConnNetworkAlertingEv.ID:
			return "CallCtlConnNetworkAlertingEv";
		case CallCtlConnNetworkReachedEv.ID:
			return "CallCtlConnNetworkReachedEv";
		case CallCtlConnOfferedEv.ID:
			return "CallCtlConnOfferedEv";
		case CallCtlConnQueuedEv.ID:
			return "CallCtlConnQueuedEv";
		case CallCtlConnUnknownEv.ID:
			return "CallCtlConnUnknownEv";
		case CallCtlTermConnBridgedEv.ID:
			return "CallCtlTermConnBridgedEv";
		case CallCtlTermConnDroppedEv.ID:
			return "CallCtlTermConnDroppedEv";
		case CallCtlTermConnHeldEv.ID:
			return "CallCtlTermConnHeldEv";
		case CallCtlTermConnInUseEv.ID:
			return "CallCtlTermConnInUseEv";
		case CallCtlTermConnRingingEv.ID:
			return "CallCtlTermConnRingingEv";
		case CallCtlTermConnTalkingEv.ID:
			return "CallCtlTermConnTalkingEv";
		case CallCtlTermConnUnknownEv.ID:
			return "CallCtlTermConnUnknownEv";
		//
		// TerminalObserver events
		//
		case TermObservationEndedEv.ID:
			return "TermObservationEndedEv";
		case AgentTermBusyEv.ID:
			return "AgentTermBusyEv";
		case AgentTermLoggedOffEv.ID:
			return "AgentTermLoggedOffEv";
		case AgentTermLoggedOnEv.ID:
			return "AgentTermLoggedOnEv";
		case AgentTermNotReadyEv.ID:
			return "AgentTermNotReadyEv";
		case AgentTermReadyEv.ID:
			return "AgentTermReadyEv";
		case AgentTermUnknownEv.ID:
			return "AgentTermUnknownEv";
		case AgentTermWorkNotReadyEv.ID:
			return "AgentTermWorkNotReadyEv";
		case AgentTermWorkReadyEv.ID:
			return "AgentTermWorkReadyEv";
		case CallCtlTermDoNotDisturbEv.ID:
			return "CallCtlTermDoNotDisturbEv";
		//
		// AddressObserver events
		//
		case AddrObservationEndedEv.ID:
			return "AddrObservationEndedEv";
		case CallCtlAddrDoNotDisturbEv.ID:
			return "CallCtlAddrDoNotDisturbEv";
		case CallCtlAddrForwardEv.ID:
			return "CallCtlAddrForwardEv";
		case CallCtlAddrMessageWaitingEv.ID:
			return "CallCtlAddrMessageWaitingEv";
		case ACDAddrBusyEv.ID:
			return "ACDAddrBusyEv";
		case ACDAddrLoggedOffEv.ID:
			return "ACDAddrLoggedOffEv";
		case ACDAddrLoggedOnEv.ID:
			return "ACDAddrLoggedOnEv";
		case ACDAddrNotReadyEv.ID:
			return "ACDAddrNotReadyEv";
		case ACDAddrReadyEv.ID:
			return "ACDAddrReadyEv";
		case ACDAddrUnknownEv.ID:
			return "ACDAddrUnknownEv";
		case ACDAddrWorkNotReadyEv.ID:
			return "ACDAddrWorkNotReadyEv";
		case ACDAddrWorkReadyEv.ID:
			return "ACDAddrWorkReadyEv";
		//
		// Other CallCenter-related events
		//
		case CallCentCallAppDataEv.ID:
			return "CallCentCallAppDataEv";
		case CallCentConnInProgressEv.ID:
			return "CallCentConnInProgressEv";
		case CallCentTrunkInvalidEv.ID:
			return "CallCentTrunkInvalidEv";
		case CallCentTrunkValidEv.ID:
			return "CallCentTrunkValidEv";

		//
		// Cisco events
		//
		case CiscoRTPOutputStartedEv.ID:
			return "CiscoRTPOutputStartedEv";
		case CiscoRTPOutputStoppedEv.ID:
			return "CiscoRTPOutputStoppedEv";
		case CiscoRTPInputStartedEv.ID:
			return "CiscoRTPInputStartedEv";
		case CiscoRTPInputStoppedEv.ID:
			return "CiscoRTPInputStoppedEv";

		default:
			return "Other: " + id ;
		}
	}

	static public String getCause ( int cause ) {

		switch ( cause ) {
		case Ev.CAUSE_CALL_CANCELLED:
			return "CAUSE_CALL_CANCELLED";
		case Ev.CAUSE_DEST_NOT_OBTAINABLE:
			return "CAUSE_DEST_NOT_OBTAINABLE";
		case Ev.CAUSE_INCOMPATIBLE_DESTINATION:
			return "CAUSE_INCOMPATIBLE_DESTINATION";
		case Ev.CAUSE_LOCKOUT:
			return "CAUSE_LOCKOUT";
		case Ev.CAUSE_NETWORK_CONGESTION:
			return "CAUSE_NETWORK_CONGESTION";
		case Ev.CAUSE_NETWORK_NOT_OBTAINABLE:
			return "CAUSE_NETWORK_NOT_OBTAINABLE";
		case Ev.CAUSE_NEW_CALL:
			return "CAUSE_NEW_CALL";
		case Ev.CAUSE_NORMAL:
			return "CAUSE_NORMAL";
		case Ev.CAUSE_RESOURCES_NOT_AVAILABLE:
			return "CAUSE_RESOURCES_NOT_AVAILABLE";
		case Ev.CAUSE_SNAPSHOT:
			return "CAUSE_SNAPSHOT";
		case Ev.CAUSE_UNKNOWN:
			return "CAUSE_UNKNOWN";
		default:
			return "Other: " + cause ;
		}
	}

	static public String getCallCtlCause ( int cause ) {

		switch ( cause ) {
		case CallCtlEv.CAUSE_ALTERNATE:
			return "CAUSE_ALTERNATE";
		case CallCtlEv.CAUSE_BUSY:
			return "CAUSE_BUSY";
		case CallCtlEv.CAUSE_CALL_BACK:
			return "CAUSE_CALL_BACK";
		case CallCtlEv.CAUSE_CALL_NOT_ANSWERED:
			return "CAUSE_CALL_NOT_ANSWERED";
		case CallCtlEv.CAUSE_CALL_PICKUP:
			return "CAUSE_CALL_PICKUP";
		case CallCtlEv.CAUSE_CONFERENCE:
			return "CAUSE_CONFERENCE";
		case CallCtlEv.CAUSE_DO_NOT_DISTURB:
			return "CAUSE_DO_NOT_DISTURB";
		case CallCtlEv.CAUSE_PARK:
			return "CAUSE_PARK";
		case CallCtlEv.CAUSE_REDIRECTED:
			return "CAUSE_REDIRECTED";
		case CallCtlEv.CAUSE_REORDER_TONE:
			return "CAUSE_REORDER_TONE";
		case CallCtlEv.CAUSE_TRANSFER:
			return "CAUSE_TRANSFER";
		case CallCtlEv.CAUSE_TRUNKS_BUSY:
			return "CAUSE_TRUNKS_BUSY";
		case CallCtlEv.CAUSE_UNHOLD:
			return "CAUSE_UNHOLD";
		//case CiscoCallEv.CAUSE_QSIG_PR:
		//	return "CAUSE_QSIG_PR";
		case CiscoCallEv.CAUSE_BARGE:
			return "CAUSE_BARGE";
		case Ev.CAUSE_NORMAL:
			return "CAUSE_NORMAL";
		case Ev.CAUSE_UNKNOWN:
			return "CAUSE_UNKNOWN";
		default:
			return "Other: " + cause ;
		}
	}

	static public String getCallCenterCause ( int cause ) {

		switch ( cause ) {
		case CallCentEv.CAUSE_NO_AVAILABLE_AGENTS:
			return "CAUSE_NO_AVAILABLE_AGENTS";
		default:
			return "Other: " + cause ;
		}
	}

	static public String getMetaCode ( int code ) {

		switch ( code ) {
		case Ev.META_CALL_ADDITIONAL_PARTY:
			return "META_CALL_ADDITIONAL_PARTY";
		case Ev.META_CALL_ENDING:
			return "META_CALL_ENDING";
		case Ev.META_CALL_MERGING:
			return "META_CALL_MERGING";
		case Ev.META_CALL_PROGRESS:
			return "META_CALL_PROGRESS";
		case Ev.META_CALL_REMOVING_PARTY:
			return "META_CALL_REMOVING_PARTY";
		case Ev.META_CALL_STARTING:
			return "META_CALL_STARTING";
		case Ev.META_CALL_TRANSFERRING:
			return "META_CALL_TRANSFERRING";
		case Ev.META_SNAPSHOT:
			return "META_SNAPSHOT";
		case Ev.META_UNKNOWN:
			return "META_UNKNOWN";
		default:
			return "Other: " + code ;
		}
	}

	static public String getPayloadType ( int payloadType ) {
		String payloadName;
		switch ( payloadType ) {
		case CiscoRTPPayload.NONSTANDARD:
			payloadName = "NonStandard";
			break;
		case CiscoRTPPayload.G711ALAW64K:
			payloadName = "G.711 A-law 64k";
			break;
		case CiscoRTPPayload.G711ALAW56K:
			payloadName = "G.711 A-law 56k";
			break;
		case CiscoRTPPayload.G711ULAW64K:
			payloadName = "G.711 U-law 64k";
			break;
		case CiscoRTPPayload.G711ULAW56K:
			payloadName = "G.711 U-law 56k";
			break;
		case CiscoRTPPayload.G722_64K:
			payloadName = "G.722 64k";
			break;
		case CiscoRTPPayload.G722_56K:
			payloadName = "G.722 56k";
			break;
		case CiscoRTPPayload.G722_48K:
			payloadName = "G.722 48k";
			break;
		case CiscoRTPPayload.G7231:
			payloadName = "G.723.1";
			break;
		case CiscoRTPPayload.G728:
			payloadName = "G.728";
			break;
		case CiscoRTPPayload.G729:
			payloadName = "G.729";
			break;
		case CiscoRTPPayload.G729ANNEXA:
			payloadName = "G.729a";
			break;
		case CiscoRTPPayload.IS11172AUDIOCAP:
			payloadName = "IS11172AUDIOCAP";
			break;
		case CiscoRTPPayload.ACY_G729AASSN:
			payloadName = "ACY G.729a assn";
			break;
		case CiscoRTPPayload.DATA64:
			payloadName = "Data64";
			break;
		case CiscoRTPPayload.DATA56:
			payloadName = "Data56";
			break;
		case CiscoRTPPayload.GSM:
			payloadName = "GSM";
			break;
		case CiscoRTPPayload.ACTIVEVOICE:
			payloadName = "ActiveVoice";
			break;
		default:
			payloadName = "Unknown Payload(" + payloadType + ")";
			break;
		}

		return payloadType+"="+payloadName;
	}

        static public String getReason( int reason ){
            String CiscoReason = "other=" + reason;
            switch ( reason ) {

            case CiscoFeatureReason.REASON_BARGE:
                CiscoReason = "BARGE";
                break;
            case CiscoFeatureReason.REASON_CONFERENCE:
                CiscoReason = "CONFERENCE";
                break;
            case CiscoFeatureReason.REASON_FORWARDALL:
                CiscoReason = "FORWARDALL";
                break;
            case CiscoFeatureReason.REASON_FORWARDBUSY:
                CiscoReason = "FORWARDBUSY";
                break;
            case CiscoFeatureReason.REASON_FORWARDNOANSWER:
                CiscoReason = "FORWARDNOANSWER";
                break;
            case CiscoFeatureReason.REASON_NORMAL:
                CiscoReason = "NORMAL";
                break;
            case CiscoFeatureReason.REASON_PARK:
                CiscoReason = "PARK";
                break;
            case CiscoFeatureReason.REASON_PARKREMAINDER:
                CiscoReason = "PARKREMAINDER";
                break;
            case CiscoFeatureReason.REASON_REDIRECT:
                CiscoReason = "REDIRECT";
                break;
            case CiscoFeatureReason.REASON_TRANSFER:
                CiscoReason = "TRANSFER";
                break;
            case CiscoFeatureReason.REASON_UNPARK:
                CiscoReason = "UNPARK";
                break;
            case CiscoFeatureReason.REASON_FAC_CMC:
                CiscoReason = "FAC_CMC";
                break;
            case CiscoFeatureReason.REASON_BLINDTRANSFER:
                CiscoReason = "BLINDTRANSFER";
                break;
            case CiscoFeatureReason.REASON_CALLPICKUP:
                CiscoReason = "PICKUP";
                break;
            case CiscoFeatureReason.REASON_QSIG_PR:
              CiscoReason = "QSIG_PR";
              break;

            }
            return "REASON=" + CiscoReason;
        }

}