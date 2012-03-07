#if _DYNAMIC_XMLSERIALIZER_COMPILATION
[assembly:System.Security.AllowPartiallyTrustedCallers()]
[assembly:System.Security.SecurityTransparent()]
#endif
[assembly:System.Xml.Serialization.XmlSerializerVersionAttribute(ParentAssemblyId=@"d2cce9e8-02b8-43d2-9475-5412ee6ad657,", Version=@"2.0.0.0")]
namespace Wybecom.TalkPortal.Cisco {

    public class XmlSerializationWriter1 : System.Xml.Serialization.XmlSerializationWriter {

        private System.Xml.Serialization.XmlSerializerNamespaces xsn;
        

        public XmlSerializationWriter1()
        {
            xsn = new System.Xml.Serialization.XmlSerializerNamespaces();
            xsn.Add("", "");
         }

        

        public void Write25_CiscoIPPhoneMenu(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneMenu", @"");
                return;
            }
            TopLevelElement();
            Write4_CiscoIPPhoneMenuType(@"CiscoIPPhoneMenu", @"", ((CiscoIPPhoneMenuType)o), false, false);
        }

        public void Write26_CiscoIPPhoneMenuItemType(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneMenuItemType", @"");
                return;
            }
            TopLevelElement();
            Write2_CiscoIPPhoneMenuItemType(@"CiscoIPPhoneMenuItemType", @"", ((CiscoIPPhoneMenuItemType)o), true, false);
        }

        public void Write27_CiscoIPPhoneResponseItemType(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneResponseItemType", @"");
                return;
            }
            TopLevelElement();
            Write5_CiscoIPPhoneResponseItemType(@"CiscoIPPhoneResponseItemType", @"", ((CiscoIPPhoneResponseItemType)o), true, false);
        }

        public void Write28_CiscoIPPhoneExecuteItemType(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneExecuteItemType", @"");
                return;
            }
            TopLevelElement();
            Write6_CiscoIPPhoneExecuteItemType(@"CiscoIPPhoneExecuteItemType", @"", ((CiscoIPPhoneExecuteItemType)o), true, false);
        }

        public void Write29_CiscoIPPhoneInputItemType(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneInputItemType", @"");
                return;
            }
            TopLevelElement();
            Write7_CiscoIPPhoneInputItemType(@"CiscoIPPhoneInputItemType", @"", ((CiscoIPPhoneInputItemType)o), true, false);
        }

        public void Write30_CiscoIPPhoneTouchArea(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneTouchArea", @"");
                return;
            }
            TopLevelElement();
            Write8_CiscoIPPhoneTouchArea(@"CiscoIPPhoneTouchArea", @"", ((CiscoIPPhoneTouchArea)o), true, false);
        }

        public void Write31_Item(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneTouchAreaMenuItemType", @"");
                return;
            }
            TopLevelElement();
            Write9_Item(@"CiscoIPPhoneTouchAreaMenuItemType", @"", ((CiscoIPPhoneTouchAreaMenuItemType)o), true, false);
        }

        public void Write32_CiscoIPPhoneDirectoryEntryType(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneDirectoryEntryType", @"");
                return;
            }
            TopLevelElement();
            Write10_CiscoIPPhoneDirectoryEntryType(@"CiscoIPPhoneDirectoryEntryType", @"", ((CiscoIPPhoneDirectoryEntryType)o), true, false);
        }

        public void Write33_CiscoIPPhoneIconItemType(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneIconItemType", @"");
                return;
            }
            TopLevelElement();
            Write11_CiscoIPPhoneIconItemType(@"CiscoIPPhoneIconItemType", @"", ((CiscoIPPhoneIconItemType)o), true, false);
        }

        public void Write34_CiscoIPPhoneIconMenuItemType(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneIconMenuItemType", @"");
                return;
            }
            TopLevelElement();
            Write12_CiscoIPPhoneIconMenuItemType(@"CiscoIPPhoneIconMenuItemType", @"", ((CiscoIPPhoneIconMenuItemType)o), true, false);
        }

        public void Write35_CiscoIPPhoneSoftKeyType(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteNullTagLiteral(@"CiscoIPPhoneSoftKeyType", @"");
                return;
            }
            TopLevelElement();
            Write3_CiscoIPPhoneSoftKeyType(@"CiscoIPPhoneSoftKeyType", @"", ((CiscoIPPhoneSoftKeyType)o), true, false);
        }

        public void Write36_CiscoIPPhoneImage(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneImage", @"");
                return;
            }
            TopLevelElement();
            Write13_CiscoIPPhoneImageType(@"CiscoIPPhoneImage", @"", ((CiscoIPPhoneImageType)o), false, false);
        }

        public void Write37_CiscoIPPhoneImageFile(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneImageFile", @"");
                return;
            }
            TopLevelElement();
            Write14_CiscoIPPhoneImageFileType(@"CiscoIPPhoneImageFile", @"", ((CiscoIPPhoneImageFileType)o), false, false);
        }

        public void Write38_CiscoIPPhoneIconMenu(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneIconMenu", @"");
                return;
            }
            TopLevelElement();
            Write15_CiscoIPPhoneIconMenuType(@"CiscoIPPhoneIconMenu", @"", ((CiscoIPPhoneIconMenuType)o), false, false);
        }

        public void Write39_CiscoIPPhoneDirectory(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneDirectory", @"");
                return;
            }
            TopLevelElement();
            Write16_CiscoIPPhoneDirectoryType(@"CiscoIPPhoneDirectory", @"", ((CiscoIPPhoneDirectoryType)o), false, false);
        }

        public void Write40_CiscoIPPhoneGraphicMenu(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneGraphicMenu", @"");
                return;
            }
            TopLevelElement();
            Write17_CiscoIPPhoneGraphicMenuType(@"CiscoIPPhoneGraphicMenu", @"", ((CiscoIPPhoneGraphicMenuType)o), false, false);
        }

        public void Write41_CiscoIPPhoneGraphicFileMenu(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneGraphicFileMenu", @"");
                return;
            }
            TopLevelElement();
            Write18_Item(@"CiscoIPPhoneGraphicFileMenu", @"", ((CiscoIPPhoneGraphicFileMenuType)o), false, false);
        }

        public void Write42_CiscoIPPhoneInput(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneInput", @"");
                return;
            }
            TopLevelElement();
            Write19_CicsoIPPhoneInputType(@"CiscoIPPhoneInput", @"", ((CicsoIPPhoneInputType)o), false, false);
        }

        public void Write43_CiscoIPPhoneText(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneText", @"");
                return;
            }
            TopLevelElement();
            Write20_CiscoIPPhoneTextType(@"CiscoIPPhoneText", @"", ((CiscoIPPhoneTextType)o), false, false);
        }

        public void Write44_CiscoIPPhoneExecute(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneExecute", @"");
                return;
            }
            TopLevelElement();
            Write21_CiscoIPPhoneExecuteType(@"CiscoIPPhoneExecute", @"", ((CiscoIPPhoneExecuteType)o), false, false);
        }

        public void Write45_CiscoIPPhoneResponse(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneResponse", @"");
                return;
            }
            TopLevelElement();
            Write22_CiscoIPhoneResponseType(@"CiscoIPPhoneResponse", @"", ((CiscoIPhoneResponseType)o), false, false);
        }

        public void Write46_CiscoIPPhoneError(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneError", @"");
                return;
            }
            TopLevelElement();
            Write23_CiscoIPPhoneError(@"CiscoIPPhoneError", @"", ((CiscoIPPhoneError)o), false, false);
        }

        public void Write47_CiscoIPPhoneStatus(object o) {
            WriteStartDocument();
            if (o == null) {
                WriteEmptyTag(@"CiscoIPPhoneStatus", @"");
                return;
            }
            TopLevelElement();
            Write24_CiscoIPPhoneStatusType(@"CiscoIPPhoneStatus", @"", ((CiscoIPPhoneStatusType)o), false, false);
        }

        void Write24_CiscoIPPhoneStatusType(string n, string ns, CiscoIPPhoneStatusType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneStatusType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneStatusType", @"");
            WriteNullableStringLiteral(@"Text", @"", ((global::System.String)o.@Text));
            if (o.@TimerSpecified) {
                WriteElementStringRaw(@"Timer", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Timer)));
            }
            if (o.@LocationXSpecified) {
                WriteElementStringRaw(@"LocationX", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationX)));
            }
            if (o.@LocationYSpecified) {
                WriteElementStringRaw(@"LocationY", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationY)));
            }
            WriteElementStringRaw(@"Width", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Width)));
            WriteElementStringRaw(@"Height", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Height)));
            WriteElementStringRaw(@"Depth", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Depth)));
            WriteElementStringRaw(@"Data", @"", FromByteArrayHex(((global::System.Byte[])o.@Data)));
            WriteEndElement(o);
        }

        void Write23_CiscoIPPhoneError(string n, string ns, CiscoIPPhoneError o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneError)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(null, @"");
            WriteAttribute(@"Number", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Number)));
            WriteEndElement(o);
        }

        void Write22_CiscoIPhoneResponseType(string n, string ns, CiscoIPhoneResponseType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPhoneResponseType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPhoneResponseType", @"");
            {
                CiscoIPPhoneResponseItemType[] a = (CiscoIPPhoneResponseItemType[])o.@ResponseItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write5_CiscoIPPhoneResponseItemType(@"ResponseItem", @"", ((CiscoIPPhoneResponseItemType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write5_CiscoIPPhoneResponseItemType(string n, string ns, CiscoIPPhoneResponseItemType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneResponseItemType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneResponseItemType", @"");
            WriteElementStringRaw(@"Status", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@Status)));
            WriteElementString(@"Data", @"", ((global::System.String)o.@Data));
            WriteElementString(@"URL", @"", ((global::System.String)o.@URL));
            WriteEndElement(o);
        }

        void Write21_CiscoIPPhoneExecuteType(string n, string ns, CiscoIPPhoneExecuteType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneExecuteType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneExecuteType", @"");
            {
                CiscoIPPhoneExecuteItemType[] a = (CiscoIPPhoneExecuteItemType[])o.@ExecuteItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write6_CiscoIPPhoneExecuteItemType(@"ExecuteItem", @"", ((CiscoIPPhoneExecuteItemType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write6_CiscoIPPhoneExecuteItemType(string n, string ns, CiscoIPPhoneExecuteItemType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneExecuteItemType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneExecuteItemType", @"");
            if (o.@PrioritySpecified) {
                WriteAttribute(@"Priority", @"", System.Xml.XmlConvert.ToString((global::System.Byte)((global::System.Byte)o.@Priority)));
            }
            WriteAttribute(@"URL", @"", ((global::System.String)o.@URL));
            if (o.@PrioritySpecified) {
            }
            WriteEndElement(o);
        }

        void Write20_CiscoIPPhoneTextType(string n, string ns, CiscoIPPhoneTextType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneTextType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneTextType", @"");
            WriteNullableStringLiteral(@"Title", @"", ((global::System.String)o.@Title));
            WriteNullableStringLiteral(@"Prompt", @"", ((global::System.String)o.@Prompt));
            WriteElementString(@"Text", @"", ((global::System.String)o.@Text));
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKey;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write3_CiscoIPPhoneSoftKeyType(string n, string ns, CiscoIPPhoneSoftKeyType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneSoftKeyType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneSoftKeyType", @"");
            WriteNullableStringLiteral(@"Name", @"", ((global::System.String)o.@Name));
            WriteNullableStringLiteral(@"URL", @"", ((global::System.String)o.@URL));
            if (o.@PostionSpecified) {
                WriteElementStringRaw(@"Position", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Postion)));
            }
            WriteNullableStringLiteral(@"URLDown", @"", ((global::System.String)o.@URLDown));
            WriteEndElement(o);
        }

        void Write19_CicsoIPPhoneInputType(string n, string ns, CicsoIPPhoneInputType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CicsoIPPhoneInputType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CicsoIPPhoneInputType", @"");
            WriteNullableStringLiteral(@"Title", @"", ((global::System.String)o.@Title));
            WriteNullableStringLiteral(@"Prompt", @"", ((global::System.String)o.@Prompt));
            WriteNullableStringLiteral(@"URL", @"", ((global::System.String)o.@URL));
            {
                CiscoIPPhoneInputItemType[] a = (CiscoIPPhoneInputItemType[])o.@InputItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write7_CiscoIPPhoneInputItemType(@"InputItem", @"", ((CiscoIPPhoneInputItemType)a[ia]), false, false);
                    }
                }
            }
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKeyItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write7_CiscoIPPhoneInputItemType(string n, string ns, CiscoIPPhoneInputItemType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneInputItemType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneInputItemType", @"");
            WriteNullableStringLiteral(@"DisplayName", @"", ((global::System.String)o.@DisplayName));
            WriteElementString(@"QueryStringParam", @"", ((global::System.String)o.@QueryStringParam));
            WriteElementString(@"InputFlags", @"", ((global::System.String)o.@InputFlags));
            WriteNullableStringLiteral(@"DefaultValue", @"", ((global::System.String)o.@DefaultValue));
            WriteEndElement(o);
        }

        void Write18_Item(string n, string ns, CiscoIPPhoneGraphicFileMenuType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneGraphicFileMenuType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneGraphicFileMenuType", @"");
            WriteNullableStringLiteral(@"Title", @"", ((global::System.String)o.@Title));
            WriteNullableStringLiteral(@"Prompt", @"", ((global::System.String)o.@Prompt));
            if (o.@LocationXSpecified) {
                WriteElementStringRaw(@"LocationX", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationX)));
            }
            if (o.@LocationYSpecified) {
                WriteElementStringRaw(@"LocationY", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationY)));
            }
            WriteElementString(@"URL", @"", ((global::System.String)o.@URL));
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKeyItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            {
                CiscoIPPhoneTouchAreaMenuItemType[] a = (CiscoIPPhoneTouchAreaMenuItemType[])o.@MenuItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write9_Item(@"MenuItem", @"", ((CiscoIPPhoneTouchAreaMenuItemType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write9_Item(string n, string ns, CiscoIPPhoneTouchAreaMenuItemType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneTouchAreaMenuItemType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneTouchAreaMenuItemType", @"");
            WriteNullableStringLiteral(@"Name", @"", ((global::System.String)o.@Name));
            WriteNullableStringLiteral(@"URL", @"", ((global::System.String)o.@URL));
            Write8_CiscoIPPhoneTouchArea(@"TouchArea", @"", ((CiscoIPPhoneTouchArea)o.@TouchArea), false, false);
            WriteEndElement(o);
        }

        void Write8_CiscoIPPhoneTouchArea(string n, string ns, CiscoIPPhoneTouchArea o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneTouchArea)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneTouchArea", @"");
            WriteAttribute(@"X1", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@X1)));
            WriteAttribute(@"Y1", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Y1)));
            WriteAttribute(@"X2", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@X2)));
            WriteAttribute(@"Y2", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Y2)));
            WriteEndElement(o);
        }

        void Write17_CiscoIPPhoneGraphicMenuType(string n, string ns, CiscoIPPhoneGraphicMenuType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneGraphicMenuType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneGraphicMenuType", @"");
            WriteNullableStringLiteral(@"Title", @"", ((global::System.String)o.@Title));
            WriteNullableStringLiteral(@"Prompt", @"", ((global::System.String)o.@Prompt));
            if (o.@LocationXSpecified) {
                WriteElementStringRaw(@"LocationX", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationX)));
            }
            if (o.@LocationYSpecified) {
                WriteElementStringRaw(@"LocationY", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationY)));
            }
            WriteElementStringRaw(@"Width", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Width)));
            WriteElementStringRaw(@"Height", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Height)));
            WriteElementStringRaw(@"Depth", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Depth)));
            WriteElementStringRaw(@"Data", @"", FromByteArrayHex(((global::System.Byte[])o.@Data)));
            {
                CiscoIPPhoneMenuItemType[] a = (CiscoIPPhoneMenuItemType[])o.@MenuItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write2_CiscoIPPhoneMenuItemType(@"MenuItem", @"", ((CiscoIPPhoneMenuItemType)a[ia]), false, false);
                    }
                }
            }
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKeyItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write2_CiscoIPPhoneMenuItemType(string n, string ns, CiscoIPPhoneMenuItemType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneMenuItemType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneMenuItemType", @"");
            WriteNullableStringLiteral(@"Name", @"", ((global::System.String)o.@Name));
            WriteNullableStringLiteral(@"URL", @"", ((global::System.String)o.@URL));
            WriteEndElement(o);
        }

        void Write16_CiscoIPPhoneDirectoryType(string n, string ns, CiscoIPPhoneDirectoryType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneDirectoryType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneDirectoryType", @"");
            WriteNullableStringLiteral(@"Title", @"", ((global::System.String)o.@Title));
            WriteNullableStringLiteral(@"Prompt", @"", ((global::System.String)o.@Prompt));
            {
                CiscoIPPhoneDirectoryEntryType[] a = (CiscoIPPhoneDirectoryEntryType[])o.@DirectoryEntry;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write10_CiscoIPPhoneDirectoryEntryType(@"DirectoryEntry", @"", ((CiscoIPPhoneDirectoryEntryType)a[ia]), false, false);
                    }
                }
            }
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKey;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write10_CiscoIPPhoneDirectoryEntryType(string n, string ns, CiscoIPPhoneDirectoryEntryType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneDirectoryEntryType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneDirectoryEntryType", @"");
            WriteNullableStringLiteral(@"Name", @"", ((global::System.String)o.@Name));
            WriteElementString(@"Telephone", @"", ((global::System.String)o.@Telephone));
            WriteEndElement(o);
        }

        void Write15_CiscoIPPhoneIconMenuType(string n, string ns, CiscoIPPhoneIconMenuType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneIconMenuType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneIconMenuType", @"");
            WriteNullableStringLiteral(@"Title", @"", ((global::System.String)o.@Title));
            WriteNullableStringLiteral(@"Prompt", @"", ((global::System.String)o.@Prompt));
            {
                CiscoIPPhoneIconMenuItemType[] a = (CiscoIPPhoneIconMenuItemType[])o.@MenuItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write12_CiscoIPPhoneIconMenuItemType(@"MenuItem", @"", ((CiscoIPPhoneIconMenuItemType)a[ia]), false, false);
                    }
                }
            }
            {
                CiscoIPPhoneIconItemType[] a = (CiscoIPPhoneIconItemType[])o.@IconItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write11_CiscoIPPhoneIconItemType(@"IconItem", @"", ((CiscoIPPhoneIconItemType)a[ia]), false, false);
                    }
                }
            }
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKeyItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write11_CiscoIPPhoneIconItemType(string n, string ns, CiscoIPPhoneIconItemType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneIconItemType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneIconItemType", @"");
            WriteElementStringRaw(@"Index", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@Index)));
            WriteElementStringRaw(@"Width", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Width)));
            WriteElementStringRaw(@"Height", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Height)));
            WriteElementStringRaw(@"Depth", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Depth)));
            WriteElementStringRaw(@"Data", @"", FromByteArrayHex(((global::System.Byte[])o.@Data)));
            WriteEndElement(o);
        }

        void Write12_CiscoIPPhoneIconMenuItemType(string n, string ns, CiscoIPPhoneIconMenuItemType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneIconMenuItemType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneIconMenuItemType", @"");
            WriteNullableStringLiteral(@"Name", @"", ((global::System.String)o.@Name));
            WriteNullableStringLiteral(@"URL", @"", ((global::System.String)o.@URL));
            WriteElementStringRaw(@"IconIndex", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@IconIndex)));
            WriteEndElement(o);
        }

        void Write14_CiscoIPPhoneImageFileType(string n, string ns, CiscoIPPhoneImageFileType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneImageFileType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneImageFileType", @"");
            WriteElementString(@"Title", @"", ((global::System.String)o.@Title));
            WriteElementString(@"Prompt", @"", ((global::System.String)o.@Prompt));
            if (o.@LocationXSpecified) {
                WriteElementStringRaw(@"LocationX", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationX)));
            }
            if (o.@LocationYSpecified) {
                WriteElementStringRaw(@"LocationY", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationY)));
            }
            WriteElementString(@"URL", @"", ((global::System.String)o.@URL));
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKeyItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write13_CiscoIPPhoneImageType(string n, string ns, CiscoIPPhoneImageType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneImageType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneImageType", @"");
            WriteNullableStringLiteral(@"Title", @"", ((global::System.String)o.@Title));
            WriteNullableStringLiteral(@"Prompt", @"", ((global::System.String)o.@Prompt));
            if (o.@LocationXSpecified) {
                WriteElementStringRaw(@"LocationX", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationX)));
            }
            if (o.@LocationYSpecified) {
                WriteElementStringRaw(@"LocationY", @"", System.Xml.XmlConvert.ToString((global::System.Int16)((global::System.Int16)o.@LocationY)));
            }
            WriteElementStringRaw(@"Width", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Width)));
            WriteElementStringRaw(@"Height", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Height)));
            WriteElementStringRaw(@"Depth", @"", System.Xml.XmlConvert.ToString((global::System.UInt16)((global::System.UInt16)o.@Depth)));
            WriteElementStringRaw(@"Data", @"", FromByteArrayHex(((global::System.Byte[])o.@Data)));
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKeyItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        void Write4_CiscoIPPhoneMenuType(string n, string ns, CiscoIPPhoneMenuType o, bool isNullable, bool needType) {
            if ((object)o == null) {
                if (isNullable) WriteNullTagLiteral(n, ns);
                return;
            }
            if (!needType) {
                System.Type t = o.GetType();
                if (t == typeof(CiscoIPPhoneMenuType)) {
                }
                else {
                    throw CreateUnknownTypeException(o);
                }
            }
            WriteStartElement(n, ns, o, false, null);
            if (needType) WriteXsiType(@"CiscoIPPhoneMenuType", @"");
            WriteElementString(@"Title", @"", ((global::System.String)o.@Title));
            WriteElementString(@"Prompt", @"", ((global::System.String)o.@Prompt));
            {
                CiscoIPPhoneMenuItemType[] a = (CiscoIPPhoneMenuItemType[])o.@MenuItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write2_CiscoIPPhoneMenuItemType(@"MenuItem", @"", ((CiscoIPPhoneMenuItemType)a[ia]), false, false);
                    }
                }
            }
            {
                CiscoIPPhoneSoftKeyType[] a = (CiscoIPPhoneSoftKeyType[])o.@SoftKeyItem;
                if (a != null) {
                    for (int ia = 0; ia < a.Length; ia++) {
                        Write3_CiscoIPPhoneSoftKeyType(@"SoftKeyItem", @"", ((CiscoIPPhoneSoftKeyType)a[ia]), false, false);
                    }
                }
            }
            WriteEndElement(o);
        }

        protected override void InitCallbacks() {
        }
    }

    public class XmlSerializationReader1 : System.Xml.Serialization.XmlSerializationReader {

        public object Read25_CiscoIPPhoneMenu() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id1_CiscoIPPhoneMenu && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read4_CiscoIPPhoneMenuType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneMenu");
            }
            return (object)o;
        }

        public object Read26_CiscoIPPhoneMenuItemType() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id3_CiscoIPPhoneMenuItemType && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read2_CiscoIPPhoneMenuItemType(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneMenuItemType");
            }
            return (object)o;
        }

        public object Read27_CiscoIPPhoneResponseItemType() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id4_CiscoIPPhoneResponseItemType && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read5_CiscoIPPhoneResponseItemType(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneResponseItemType");
            }
            return (object)o;
        }

        public object Read28_CiscoIPPhoneExecuteItemType() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id5_CiscoIPPhoneExecuteItemType && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read6_CiscoIPPhoneExecuteItemType(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneExecuteItemType");
            }
            return (object)o;
        }

        public object Read29_CiscoIPPhoneInputItemType() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id6_CiscoIPPhoneInputItemType && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read7_CiscoIPPhoneInputItemType(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneInputItemType");
            }
            return (object)o;
        }

        public object Read30_CiscoIPPhoneTouchArea() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id7_CiscoIPPhoneTouchArea && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read8_CiscoIPPhoneTouchArea(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneTouchArea");
            }
            return (object)o;
        }

        public object Read31_Item() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id8_Item && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read9_Item(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneTouchAreaMenuItemType");
            }
            return (object)o;
        }

        public object Read32_CiscoIPPhoneDirectoryEntryType() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id9_CiscoIPPhoneDirectoryEntryType && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read10_CiscoIPPhoneDirectoryEntryType(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneDirectoryEntryType");
            }
            return (object)o;
        }

        public object Read33_CiscoIPPhoneIconItemType() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id10_CiscoIPPhoneIconItemType && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read11_CiscoIPPhoneIconItemType(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneIconItemType");
            }
            return (object)o;
        }

        public object Read34_CiscoIPPhoneIconMenuItemType() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id11_CiscoIPPhoneIconMenuItemType && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read12_CiscoIPPhoneIconMenuItemType(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneIconMenuItemType");
            }
            return (object)o;
        }

        public object Read35_CiscoIPPhoneSoftKeyType() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id12_CiscoIPPhoneSoftKeyType && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read3_CiscoIPPhoneSoftKeyType(true, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneSoftKeyType");
            }
            return (object)o;
        }

        public object Read36_CiscoIPPhoneImage() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id13_CiscoIPPhoneImage && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read13_CiscoIPPhoneImageType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneImage");
            }
            return (object)o;
        }

        public object Read37_CiscoIPPhoneImageFile() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id14_CiscoIPPhoneImageFile && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read14_CiscoIPPhoneImageFileType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneImageFile");
            }
            return (object)o;
        }

        public object Read38_CiscoIPPhoneIconMenu() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id15_CiscoIPPhoneIconMenu && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read15_CiscoIPPhoneIconMenuType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneIconMenu");
            }
            return (object)o;
        }

        public object Read39_CiscoIPPhoneDirectory() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id16_CiscoIPPhoneDirectory && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read16_CiscoIPPhoneDirectoryType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneDirectory");
            }
            return (object)o;
        }

        public object Read40_CiscoIPPhoneGraphicMenu() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id17_CiscoIPPhoneGraphicMenu && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read17_CiscoIPPhoneGraphicMenuType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneGraphicMenu");
            }
            return (object)o;
        }

        public object Read41_CiscoIPPhoneGraphicFileMenu() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id18_CiscoIPPhoneGraphicFileMenu && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read18_Item(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneGraphicFileMenu");
            }
            return (object)o;
        }

        public object Read42_CiscoIPPhoneInput() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id19_CiscoIPPhoneInput && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read19_CicsoIPPhoneInputType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneInput");
            }
            return (object)o;
        }

        public object Read43_CiscoIPPhoneText() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id20_CiscoIPPhoneText && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read20_CiscoIPPhoneTextType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneText");
            }
            return (object)o;
        }

        public object Read44_CiscoIPPhoneExecute() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id21_CiscoIPPhoneExecute && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read21_CiscoIPPhoneExecuteType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneExecute");
            }
            return (object)o;
        }

        public object Read45_CiscoIPPhoneResponse() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id22_CiscoIPPhoneResponse && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read22_CiscoIPhoneResponseType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneResponse");
            }
            return (object)o;
        }

        public object Read46_CiscoIPPhoneError() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id23_CiscoIPPhoneError && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read23_CiscoIPPhoneError(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneError");
            }
            return (object)o;
        }

        public object Read47_CiscoIPPhoneStatus() {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                if (((object) Reader.LocalName == (object)id24_CiscoIPPhoneStatus && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o = Read24_CiscoIPPhoneStatusType(false, true);
                }
                else {
                    throw CreateUnknownNodeException();
                }
            }
            else {
                UnknownNode(null, @":CiscoIPPhoneStatus");
            }
            return (object)o;
        }

        CiscoIPPhoneStatusType Read24_CiscoIPPhoneStatusType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id25_CiscoIPPhoneStatusType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneStatusType o;
            o = new CiscoIPPhoneStatusType();
            bool[] paramsRead = new bool[8];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations0 = 0;
            int readerCount0 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id26_Text && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Text = null;
                        }
                        else {
                            o.@Text = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id27_Timer && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@TimerSpecified = true;
                        {
                            o.@Timer = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id28_LocationX && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationXSpecified = true;
                        {
                            o.@LocationX = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[2] = true;
                    }
                    else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id29_LocationY && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationYSpecified = true;
                        {
                            o.@LocationY = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[3] = true;
                    }
                    else if (!paramsRead[4] && ((object) Reader.LocalName == (object)id30_Width && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Width = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[4] = true;
                    }
                    else if (!paramsRead[5] && ((object) Reader.LocalName == (object)id31_Height && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Height = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[5] = true;
                    }
                    else if (!paramsRead[6] && ((object) Reader.LocalName == (object)id32_Depth && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Depth = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[6] = true;
                    }
                    else if (!paramsRead[7] && ((object) Reader.LocalName == (object)id33_Data && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Data = ToByteArrayHex(false);
                        }
                        paramsRead[7] = true;
                    }
                    else {
                        UnknownNode((object)o, @":Text, :Timer, :LocationX, :LocationY, :Width, :Height, :Depth, :Data");
                    }
                }
                else {
                    UnknownNode((object)o, @":Text, :Timer, :LocationX, :LocationY, :Width, :Height, :Depth, :Data");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations0, ref readerCount0);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneError Read23_CiscoIPPhoneError(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id2_Item && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneError o;
            o = new CiscoIPPhoneError();
            bool[] paramsRead = new bool[1];
            while (Reader.MoveToNextAttribute()) {
                if (!paramsRead[0] && ((object) Reader.LocalName == (object)id34_Number && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o.@Number = System.Xml.XmlConvert.ToUInt16(Reader.Value);
                    paramsRead[0] = true;
                }
                else if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o, @":Number");
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations1 = 0;
            int readerCount1 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    UnknownNode((object)o, @"");
                }
                else {
                    UnknownNode((object)o, @"");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations1, ref readerCount1);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPhoneResponseType Read22_CiscoIPhoneResponseType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id35_CiscoIPhoneResponseType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPhoneResponseType o;
            o = new CiscoIPhoneResponseType();
            CiscoIPPhoneResponseItemType[] a_0 = null;
            int ca_0 = 0;
            bool[] paramsRead = new bool[1];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@ResponseItem = (CiscoIPPhoneResponseItemType[])ShrinkArray(a_0, ca_0, typeof(CiscoIPPhoneResponseItemType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations2 = 0;
            int readerCount2 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (((object) Reader.LocalName == (object)id36_ResponseItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_0 = (CiscoIPPhoneResponseItemType[])EnsureArrayIndex(a_0, ca_0, typeof(CiscoIPPhoneResponseItemType));a_0[ca_0++] = Read5_CiscoIPPhoneResponseItemType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":ResponseItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":ResponseItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations2, ref readerCount2);
            }
            o.@ResponseItem = (CiscoIPPhoneResponseItemType[])ShrinkArray(a_0, ca_0, typeof(CiscoIPPhoneResponseItemType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneResponseItemType Read5_CiscoIPPhoneResponseItemType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id4_CiscoIPPhoneResponseItemType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneResponseItemType o;
            o = new CiscoIPPhoneResponseItemType();
            bool[] paramsRead = new bool[3];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations3 = 0;
            int readerCount3 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id37_Status && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Status = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id33_Data && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Data = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@URL = Reader.ReadElementString();
                        }
                        paramsRead[2] = true;
                    }
                    else {
                        UnknownNode((object)o, @":Status, :Data, :URL");
                    }
                }
                else {
                    UnknownNode((object)o, @":Status, :Data, :URL");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations3, ref readerCount3);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneExecuteType Read21_CiscoIPPhoneExecuteType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id39_CiscoIPPhoneExecuteType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneExecuteType o;
            o = new CiscoIPPhoneExecuteType();
            CiscoIPPhoneExecuteItemType[] a_0 = null;
            int ca_0 = 0;
            bool[] paramsRead = new bool[1];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@ExecuteItem = (CiscoIPPhoneExecuteItemType[])ShrinkArray(a_0, ca_0, typeof(CiscoIPPhoneExecuteItemType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations4 = 0;
            int readerCount4 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (((object) Reader.LocalName == (object)id40_ExecuteItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_0 = (CiscoIPPhoneExecuteItemType[])EnsureArrayIndex(a_0, ca_0, typeof(CiscoIPPhoneExecuteItemType));a_0[ca_0++] = Read6_CiscoIPPhoneExecuteItemType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":ExecuteItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":ExecuteItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations4, ref readerCount4);
            }
            o.@ExecuteItem = (CiscoIPPhoneExecuteItemType[])ShrinkArray(a_0, ca_0, typeof(CiscoIPPhoneExecuteItemType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneExecuteItemType Read6_CiscoIPPhoneExecuteItemType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id5_CiscoIPPhoneExecuteItemType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneExecuteItemType o;
            o = new CiscoIPPhoneExecuteItemType();
            bool[] paramsRead = new bool[2];
            while (Reader.MoveToNextAttribute()) {
                if (!paramsRead[0] && ((object) Reader.LocalName == (object)id41_Priority && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o.@Priority = System.Xml.XmlConvert.ToByte(Reader.Value);
                    o.@PrioritySpecified = true;
                    paramsRead[0] = true;
                }
                else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o.@URL = Reader.Value;
                    paramsRead[1] = true;
                }
                else if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o, @":Priority, :URL");
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations5 = 0;
            int readerCount5 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    UnknownNode((object)o, @"");
                }
                else {
                    UnknownNode((object)o, @"");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations5, ref readerCount5);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneTextType Read20_CiscoIPPhoneTextType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id42_CiscoIPPhoneTextType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneTextType o;
            o = new CiscoIPPhoneTextType();
            CiscoIPPhoneSoftKeyType[] a_3 = null;
            int ca_3 = 0;
            bool[] paramsRead = new bool[4];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@SoftKey = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations6 = 0;
            int readerCount6 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Title = null;
                        }
                        else {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Prompt = null;
                        }
                        else {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id26_Text && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Text = Reader.ReadElementString();
                        }
                        paramsRead[2] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id45_SoftKey && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_3 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType));a_3[ca_3++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :Text, :SoftKey");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :Text, :SoftKey");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations6, ref readerCount6);
            }
            o.@SoftKey = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneSoftKeyType Read3_CiscoIPPhoneSoftKeyType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id12_CiscoIPPhoneSoftKeyType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneSoftKeyType o;
            o = new CiscoIPPhoneSoftKeyType();
            bool[] paramsRead = new bool[4];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations7 = 0;
            int readerCount7 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id46_Name && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Name = null;
                        }
                        else {
                            o.@Name = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@URL = null;
                        }
                        else {
                            o.@URL = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id47_Postion && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@PostionSpecified = true;
                        {
                            o.@Postion = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[2] = true;
                    }
                    else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id48_URLDown && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@URLDown = null;
                        }
                        else {
                            o.@URLDown = Reader.ReadElementString();
                        }
                        paramsRead[3] = true;
                    }
                    else {
                        UnknownNode((object)o, @":Name, :URL, :Postion, :URLDown");
                    }
                }
                else {
                    UnknownNode((object)o, @":Name, :URL, :Postion, :URLDown");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations7, ref readerCount7);
            }
            ReadEndElement();
            return o;
        }

        CicsoIPPhoneInputType Read19_CicsoIPPhoneInputType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id49_CicsoIPPhoneInputType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CicsoIPPhoneInputType o;
            o = new CicsoIPPhoneInputType();
            CiscoIPPhoneInputItemType[] a_3 = null;
            int ca_3 = 0;
            CiscoIPPhoneSoftKeyType[] a_4 = null;
            int ca_4 = 0;
            bool[] paramsRead = new bool[5];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@InputItem = (CiscoIPPhoneInputItemType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneInputItemType), true);
                o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_4, ca_4, typeof(CiscoIPPhoneSoftKeyType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations8 = 0;
            int readerCount8 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Title = null;
                        }
                        else {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Prompt = null;
                        }
                        else {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@URL = null;
                        }
                        else {
                            o.@URL = Reader.ReadElementString();
                        }
                        paramsRead[2] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id50_InputItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_3 = (CiscoIPPhoneInputItemType[])EnsureArrayIndex(a_3, ca_3, typeof(CiscoIPPhoneInputItemType));a_3[ca_3++] = Read7_CiscoIPPhoneInputItemType(false, true);
                    }
                    else if (((object) Reader.LocalName == (object)id51_SoftKeyItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_4 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_4, ca_4, typeof(CiscoIPPhoneSoftKeyType));a_4[ca_4++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :URL, :InputItem, :SoftKeyItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :URL, :InputItem, :SoftKeyItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations8, ref readerCount8);
            }
            o.@InputItem = (CiscoIPPhoneInputItemType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneInputItemType), true);
            o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_4, ca_4, typeof(CiscoIPPhoneSoftKeyType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneInputItemType Read7_CiscoIPPhoneInputItemType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id6_CiscoIPPhoneInputItemType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneInputItemType o;
            o = new CiscoIPPhoneInputItemType();
            bool[] paramsRead = new bool[4];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations9 = 0;
            int readerCount9 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id52_DisplayName && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@DisplayName = null;
                        }
                        else {
                            o.@DisplayName = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id53_QueryStringParam && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@QueryStringParam = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id54_InputFlags && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@InputFlags = Reader.ReadElementString();
                        }
                        paramsRead[2] = true;
                    }
                    else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id55_DefaultValue && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@DefaultValue = null;
                        }
                        else {
                            o.@DefaultValue = Reader.ReadElementString();
                        }
                        paramsRead[3] = true;
                    }
                    else {
                        UnknownNode((object)o, @":DisplayName, :QueryStringParam, :InputFlags, :DefaultValue");
                    }
                }
                else {
                    UnknownNode((object)o, @":DisplayName, :QueryStringParam, :InputFlags, :DefaultValue");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations9, ref readerCount9);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneGraphicFileMenuType Read18_Item(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id56_Item && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneGraphicFileMenuType o;
            o = new CiscoIPPhoneGraphicFileMenuType();
            CiscoIPPhoneSoftKeyType[] a_5 = null;
            int ca_5 = 0;
            CiscoIPPhoneTouchAreaMenuItemType[] a_6 = null;
            int ca_6 = 0;
            bool[] paramsRead = new bool[7];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_5, ca_5, typeof(CiscoIPPhoneSoftKeyType), true);
                o.@MenuItem = (CiscoIPPhoneTouchAreaMenuItemType[])ShrinkArray(a_6, ca_6, typeof(CiscoIPPhoneTouchAreaMenuItemType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations10 = 0;
            int readerCount10 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Title = null;
                        }
                        else {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Prompt = null;
                        }
                        else {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id28_LocationX && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationXSpecified = true;
                        {
                            o.@LocationX = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[2] = true;
                    }
                    else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id29_LocationY && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationYSpecified = true;
                        {
                            o.@LocationY = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[3] = true;
                    }
                    else if (!paramsRead[4] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@URL = Reader.ReadElementString();
                        }
                        paramsRead[4] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id51_SoftKeyItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_5 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_5, ca_5, typeof(CiscoIPPhoneSoftKeyType));a_5[ca_5++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else if (((object) Reader.LocalName == (object)id57_MenuItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_6 = (CiscoIPPhoneTouchAreaMenuItemType[])EnsureArrayIndex(a_6, ca_6, typeof(CiscoIPPhoneTouchAreaMenuItemType));a_6[ca_6++] = Read9_Item(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :LocationX, :LocationY, :URL, :SoftKeyItem, :MenuItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :LocationX, :LocationY, :URL, :SoftKeyItem, :MenuItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations10, ref readerCount10);
            }
            o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_5, ca_5, typeof(CiscoIPPhoneSoftKeyType), true);
            o.@MenuItem = (CiscoIPPhoneTouchAreaMenuItemType[])ShrinkArray(a_6, ca_6, typeof(CiscoIPPhoneTouchAreaMenuItemType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneTouchAreaMenuItemType Read9_Item(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id8_Item && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneTouchAreaMenuItemType o;
            o = new CiscoIPPhoneTouchAreaMenuItemType();
            bool[] paramsRead = new bool[3];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations11 = 0;
            int readerCount11 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id46_Name && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Name = null;
                        }
                        else {
                            o.@Name = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@URL = null;
                        }
                        else {
                            o.@URL = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id58_TouchArea && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@TouchArea = Read8_CiscoIPPhoneTouchArea(false, true);
                        paramsRead[2] = true;
                    }
                    else {
                        UnknownNode((object)o, @":Name, :URL, :TouchArea");
                    }
                }
                else {
                    UnknownNode((object)o, @":Name, :URL, :TouchArea");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations11, ref readerCount11);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneTouchArea Read8_CiscoIPPhoneTouchArea(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id7_CiscoIPPhoneTouchArea && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneTouchArea o;
            o = new CiscoIPPhoneTouchArea();
            bool[] paramsRead = new bool[4];
            while (Reader.MoveToNextAttribute()) {
                if (!paramsRead[0] && ((object) Reader.LocalName == (object)id59_X1 && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o.@X1 = System.Xml.XmlConvert.ToUInt16(Reader.Value);
                    paramsRead[0] = true;
                }
                else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id60_Y1 && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o.@Y1 = System.Xml.XmlConvert.ToUInt16(Reader.Value);
                    paramsRead[1] = true;
                }
                else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id61_X2 && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o.@X2 = System.Xml.XmlConvert.ToUInt16(Reader.Value);
                    paramsRead[2] = true;
                }
                else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id62_Y2 && (object) Reader.NamespaceURI == (object)id2_Item)) {
                    o.@Y2 = System.Xml.XmlConvert.ToUInt16(Reader.Value);
                    paramsRead[3] = true;
                }
                else if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o, @":X1, :Y1, :X2, :Y2");
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations12 = 0;
            int readerCount12 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    UnknownNode((object)o, @"");
                }
                else {
                    UnknownNode((object)o, @"");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations12, ref readerCount12);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneGraphicMenuType Read17_CiscoIPPhoneGraphicMenuType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id63_CiscoIPPhoneGraphicMenuType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneGraphicMenuType o;
            o = new CiscoIPPhoneGraphicMenuType();
            CiscoIPPhoneMenuItemType[] a_8 = null;
            int ca_8 = 0;
            CiscoIPPhoneSoftKeyType[] a_9 = null;
            int ca_9 = 0;
            bool[] paramsRead = new bool[10];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@MenuItem = (CiscoIPPhoneMenuItemType[])ShrinkArray(a_8, ca_8, typeof(CiscoIPPhoneMenuItemType), true);
                o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_9, ca_9, typeof(CiscoIPPhoneSoftKeyType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations13 = 0;
            int readerCount13 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Title = null;
                        }
                        else {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Prompt = null;
                        }
                        else {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id28_LocationX && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationXSpecified = true;
                        {
                            o.@LocationX = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[2] = true;
                    }
                    else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id29_LocationY && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationYSpecified = true;
                        {
                            o.@LocationY = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[3] = true;
                    }
                    else if (!paramsRead[4] && ((object) Reader.LocalName == (object)id30_Width && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Width = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[4] = true;
                    }
                    else if (!paramsRead[5] && ((object) Reader.LocalName == (object)id31_Height && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Height = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[5] = true;
                    }
                    else if (!paramsRead[6] && ((object) Reader.LocalName == (object)id32_Depth && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Depth = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[6] = true;
                    }
                    else if (!paramsRead[7] && ((object) Reader.LocalName == (object)id33_Data && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Data = ToByteArrayHex(false);
                        }
                        paramsRead[7] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id57_MenuItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_8 = (CiscoIPPhoneMenuItemType[])EnsureArrayIndex(a_8, ca_8, typeof(CiscoIPPhoneMenuItemType));a_8[ca_8++] = Read2_CiscoIPPhoneMenuItemType(false, true);
                    }
                    else if (((object) Reader.LocalName == (object)id51_SoftKeyItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_9 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_9, ca_9, typeof(CiscoIPPhoneSoftKeyType));a_9[ca_9++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :LocationX, :LocationY, :Width, :Height, :Depth, :Data, :MenuItem, :SoftKeyItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :LocationX, :LocationY, :Width, :Height, :Depth, :Data, :MenuItem, :SoftKeyItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations13, ref readerCount13);
            }
            o.@MenuItem = (CiscoIPPhoneMenuItemType[])ShrinkArray(a_8, ca_8, typeof(CiscoIPPhoneMenuItemType), true);
            o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_9, ca_9, typeof(CiscoIPPhoneSoftKeyType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneMenuItemType Read2_CiscoIPPhoneMenuItemType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id3_CiscoIPPhoneMenuItemType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneMenuItemType o;
            o = new CiscoIPPhoneMenuItemType();
            bool[] paramsRead = new bool[2];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations14 = 0;
            int readerCount14 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id46_Name && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Name = null;
                        }
                        else {
                            o.@Name = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@URL = null;
                        }
                        else {
                            o.@URL = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else {
                        UnknownNode((object)o, @":Name, :URL");
                    }
                }
                else {
                    UnknownNode((object)o, @":Name, :URL");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations14, ref readerCount14);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneDirectoryType Read16_CiscoIPPhoneDirectoryType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id64_CiscoIPPhoneDirectoryType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneDirectoryType o;
            o = new CiscoIPPhoneDirectoryType();
            CiscoIPPhoneDirectoryEntryType[] a_2 = null;
            int ca_2 = 0;
            CiscoIPPhoneSoftKeyType[] a_3 = null;
            int ca_3 = 0;
            bool[] paramsRead = new bool[4];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@DirectoryEntry = (CiscoIPPhoneDirectoryEntryType[])ShrinkArray(a_2, ca_2, typeof(CiscoIPPhoneDirectoryEntryType), true);
                o.@SoftKey = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations15 = 0;
            int readerCount15 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Title = null;
                        }
                        else {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Prompt = null;
                        }
                        else {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id65_DirectoryEntry && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_2 = (CiscoIPPhoneDirectoryEntryType[])EnsureArrayIndex(a_2, ca_2, typeof(CiscoIPPhoneDirectoryEntryType));a_2[ca_2++] = Read10_CiscoIPPhoneDirectoryEntryType(false, true);
                    }
                    else if (((object) Reader.LocalName == (object)id45_SoftKey && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_3 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType));a_3[ca_3++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :DirectoryEntry, :SoftKey");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :DirectoryEntry, :SoftKey");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations15, ref readerCount15);
            }
            o.@DirectoryEntry = (CiscoIPPhoneDirectoryEntryType[])ShrinkArray(a_2, ca_2, typeof(CiscoIPPhoneDirectoryEntryType), true);
            o.@SoftKey = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneDirectoryEntryType Read10_CiscoIPPhoneDirectoryEntryType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id9_CiscoIPPhoneDirectoryEntryType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneDirectoryEntryType o;
            o = new CiscoIPPhoneDirectoryEntryType();
            bool[] paramsRead = new bool[2];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations16 = 0;
            int readerCount16 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id46_Name && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Name = null;
                        }
                        else {
                            o.@Name = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id66_Telephone && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Telephone = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else {
                        UnknownNode((object)o, @":Name, :Telephone");
                    }
                }
                else {
                    UnknownNode((object)o, @":Name, :Telephone");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations16, ref readerCount16);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneIconMenuType Read15_CiscoIPPhoneIconMenuType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id67_CiscoIPPhoneIconMenuType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneIconMenuType o;
            o = new CiscoIPPhoneIconMenuType();
            CiscoIPPhoneIconMenuItemType[] a_2 = null;
            int ca_2 = 0;
            CiscoIPPhoneIconItemType[] a_3 = null;
            int ca_3 = 0;
            CiscoIPPhoneSoftKeyType[] a_4 = null;
            int ca_4 = 0;
            bool[] paramsRead = new bool[5];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@MenuItem = (CiscoIPPhoneIconMenuItemType[])ShrinkArray(a_2, ca_2, typeof(CiscoIPPhoneIconMenuItemType), true);
                o.@IconItem = (CiscoIPPhoneIconItemType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneIconItemType), true);
                o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_4, ca_4, typeof(CiscoIPPhoneSoftKeyType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations17 = 0;
            int readerCount17 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Title = null;
                        }
                        else {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Prompt = null;
                        }
                        else {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id57_MenuItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_2 = (CiscoIPPhoneIconMenuItemType[])EnsureArrayIndex(a_2, ca_2, typeof(CiscoIPPhoneIconMenuItemType));a_2[ca_2++] = Read12_CiscoIPPhoneIconMenuItemType(false, true);
                    }
                    else if (((object) Reader.LocalName == (object)id68_IconItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_3 = (CiscoIPPhoneIconItemType[])EnsureArrayIndex(a_3, ca_3, typeof(CiscoIPPhoneIconItemType));a_3[ca_3++] = Read11_CiscoIPPhoneIconItemType(false, true);
                    }
                    else if (((object) Reader.LocalName == (object)id51_SoftKeyItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_4 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_4, ca_4, typeof(CiscoIPPhoneSoftKeyType));a_4[ca_4++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :MenuItem, :IconItem, :SoftKeyItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :MenuItem, :IconItem, :SoftKeyItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations17, ref readerCount17);
            }
            o.@MenuItem = (CiscoIPPhoneIconMenuItemType[])ShrinkArray(a_2, ca_2, typeof(CiscoIPPhoneIconMenuItemType), true);
            o.@IconItem = (CiscoIPPhoneIconItemType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneIconItemType), true);
            o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_4, ca_4, typeof(CiscoIPPhoneSoftKeyType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneIconItemType Read11_CiscoIPPhoneIconItemType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id10_CiscoIPPhoneIconItemType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneIconItemType o;
            o = new CiscoIPPhoneIconItemType();
            bool[] paramsRead = new bool[5];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations18 = 0;
            int readerCount18 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id69_Index && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Index = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id30_Width && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Width = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id31_Height && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Height = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[2] = true;
                    }
                    else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id32_Depth && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Depth = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[3] = true;
                    }
                    else if (!paramsRead[4] && ((object) Reader.LocalName == (object)id33_Data && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Data = ToByteArrayHex(false);
                        }
                        paramsRead[4] = true;
                    }
                    else {
                        UnknownNode((object)o, @":Index, :Width, :Height, :Depth, :Data");
                    }
                }
                else {
                    UnknownNode((object)o, @":Index, :Width, :Height, :Depth, :Data");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations18, ref readerCount18);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneIconMenuItemType Read12_CiscoIPPhoneIconMenuItemType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id11_CiscoIPPhoneIconMenuItemType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneIconMenuItemType o;
            o = new CiscoIPPhoneIconMenuItemType();
            bool[] paramsRead = new bool[3];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations19 = 0;
            int readerCount19 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id46_Name && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Name = null;
                        }
                        else {
                            o.@Name = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@URL = null;
                        }
                        else {
                            o.@URL = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id70_IconIndex && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@IconIndex = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[2] = true;
                    }
                    else {
                        UnknownNode((object)o, @":Name, :URL, :IconIndex");
                    }
                }
                else {
                    UnknownNode((object)o, @":Name, :URL, :IconIndex");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations19, ref readerCount19);
            }
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneImageFileType Read14_CiscoIPPhoneImageFileType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id71_CiscoIPPhoneImageFileType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneImageFileType o;
            o = new CiscoIPPhoneImageFileType();
            CiscoIPPhoneSoftKeyType[] a_5 = null;
            int ca_5 = 0;
            bool[] paramsRead = new bool[6];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_5, ca_5, typeof(CiscoIPPhoneSoftKeyType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations20 = 0;
            int readerCount20 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id28_LocationX && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationXSpecified = true;
                        {
                            o.@LocationX = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[2] = true;
                    }
                    else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id29_LocationY && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationYSpecified = true;
                        {
                            o.@LocationY = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[3] = true;
                    }
                    else if (!paramsRead[4] && ((object) Reader.LocalName == (object)id38_URL && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@URL = Reader.ReadElementString();
                        }
                        paramsRead[4] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id51_SoftKeyItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_5 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_5, ca_5, typeof(CiscoIPPhoneSoftKeyType));a_5[ca_5++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :LocationX, :LocationY, :URL, :SoftKeyItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :LocationX, :LocationY, :URL, :SoftKeyItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations20, ref readerCount20);
            }
            o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_5, ca_5, typeof(CiscoIPPhoneSoftKeyType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneImageType Read13_CiscoIPPhoneImageType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id72_CiscoIPPhoneImageType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneImageType o;
            o = new CiscoIPPhoneImageType();
            CiscoIPPhoneSoftKeyType[] a_8 = null;
            int ca_8 = 0;
            bool[] paramsRead = new bool[9];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_8, ca_8, typeof(CiscoIPPhoneSoftKeyType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations21 = 0;
            int readerCount21 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Title = null;
                        }
                        else {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        if (ReadNull()) {
                            o.@Prompt = null;
                        }
                        else {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (!paramsRead[2] && ((object) Reader.LocalName == (object)id28_LocationX && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationXSpecified = true;
                        {
                            o.@LocationX = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[2] = true;
                    }
                    else if (!paramsRead[3] && ((object) Reader.LocalName == (object)id29_LocationY && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        o.@LocationYSpecified = true;
                        {
                            o.@LocationY = System.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                        }
                        paramsRead[3] = true;
                    }
                    else if (!paramsRead[4] && ((object) Reader.LocalName == (object)id30_Width && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Width = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[4] = true;
                    }
                    else if (!paramsRead[5] && ((object) Reader.LocalName == (object)id31_Height && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Height = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[5] = true;
                    }
                    else if (!paramsRead[6] && ((object) Reader.LocalName == (object)id32_Depth && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Depth = System.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                        }
                        paramsRead[6] = true;
                    }
                    else if (!paramsRead[7] && ((object) Reader.LocalName == (object)id33_Data && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Data = ToByteArrayHex(false);
                        }
                        paramsRead[7] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id51_SoftKeyItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_8 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_8, ca_8, typeof(CiscoIPPhoneSoftKeyType));a_8[ca_8++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :LocationX, :LocationY, :Width, :Height, :Depth, :Data, :SoftKeyItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :LocationX, :LocationY, :Width, :Height, :Depth, :Data, :SoftKeyItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations21, ref readerCount21);
            }
            o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_8, ca_8, typeof(CiscoIPPhoneSoftKeyType), true);
            ReadEndElement();
            return o;
        }

        CiscoIPPhoneMenuType Read4_CiscoIPPhoneMenuType(bool isNullable, bool checkType) {
            System.Xml.XmlQualifiedName xsiType = checkType ? GetXsiType() : null;
            bool isNull = false;
            if (isNullable) isNull = ReadNull();
            if (checkType) {
            if (xsiType == null || ((object) ((System.Xml.XmlQualifiedName)xsiType).Name == (object)id73_CiscoIPPhoneMenuType && (object) ((System.Xml.XmlQualifiedName)xsiType).Namespace == (object)id2_Item)) {
            }
            else
                throw CreateUnknownTypeException((System.Xml.XmlQualifiedName)xsiType);
            }
            if (isNull) return null;
            CiscoIPPhoneMenuType o;
            o = new CiscoIPPhoneMenuType();
            CiscoIPPhoneMenuItemType[] a_2 = null;
            int ca_2 = 0;
            CiscoIPPhoneSoftKeyType[] a_3 = null;
            int ca_3 = 0;
            bool[] paramsRead = new bool[4];
            while (Reader.MoveToNextAttribute()) {
                if (!IsXmlnsAttribute(Reader.Name)) {
                    UnknownNode((object)o);
                }
            }
            Reader.MoveToElement();
            if (Reader.IsEmptyElement) {
                Reader.Skip();
                o.@MenuItem = (CiscoIPPhoneMenuItemType[])ShrinkArray(a_2, ca_2, typeof(CiscoIPPhoneMenuItemType), true);
                o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType), true);
                return o;
            }
            Reader.ReadStartElement();
            Reader.MoveToContent();
            int whileIterations22 = 0;
            int readerCount22 = ReaderCount;
            while (Reader.NodeType != System.Xml.XmlNodeType.EndElement && Reader.NodeType != System.Xml.XmlNodeType.None) {
                if (Reader.NodeType == System.Xml.XmlNodeType.Element) {
                    if (!paramsRead[0] && ((object) Reader.LocalName == (object)id43_Title && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Title = Reader.ReadElementString();
                        }
                        paramsRead[0] = true;
                    }
                    else if (!paramsRead[1] && ((object) Reader.LocalName == (object)id44_Prompt && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        {
                            o.@Prompt = Reader.ReadElementString();
                        }
                        paramsRead[1] = true;
                    }
                    else if (((object) Reader.LocalName == (object)id57_MenuItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_2 = (CiscoIPPhoneMenuItemType[])EnsureArrayIndex(a_2, ca_2, typeof(CiscoIPPhoneMenuItemType));a_2[ca_2++] = Read2_CiscoIPPhoneMenuItemType(false, true);
                    }
                    else if (((object) Reader.LocalName == (object)id51_SoftKeyItem && (object) Reader.NamespaceURI == (object)id2_Item)) {
                        a_3 = (CiscoIPPhoneSoftKeyType[])EnsureArrayIndex(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType));a_3[ca_3++] = Read3_CiscoIPPhoneSoftKeyType(false, true);
                    }
                    else {
                        UnknownNode((object)o, @":Title, :Prompt, :MenuItem, :SoftKeyItem");
                    }
                }
                else {
                    UnknownNode((object)o, @":Title, :Prompt, :MenuItem, :SoftKeyItem");
                }
                Reader.MoveToContent();
                CheckReaderCount(ref whileIterations22, ref readerCount22);
            }
            o.@MenuItem = (CiscoIPPhoneMenuItemType[])ShrinkArray(a_2, ca_2, typeof(CiscoIPPhoneMenuItemType), true);
            o.@SoftKeyItem = (CiscoIPPhoneSoftKeyType[])ShrinkArray(a_3, ca_3, typeof(CiscoIPPhoneSoftKeyType), true);
            ReadEndElement();
            return o;
        }

        protected override void InitCallbacks() {
        }

        string id14_CiscoIPPhoneImageFile;
        string id2_Item;
        string id42_CiscoIPPhoneTextType;
        string id11_CiscoIPPhoneIconMenuItemType;
        string id56_Item;
        string id45_SoftKey;
        string id8_Item;
        string id69_Index;
        string id20_CiscoIPPhoneText;
        string id68_IconItem;
        string id61_X2;
        string id30_Width;
        string id67_CiscoIPPhoneIconMenuType;
        string id19_CiscoIPPhoneInput;
        string id7_CiscoIPPhoneTouchArea;
        string id73_CiscoIPPhoneMenuType;
        string id46_Name;
        string id53_QueryStringParam;
        string id37_Status;
        string id39_CiscoIPPhoneExecuteType;
        string id49_CicsoIPPhoneInputType;
        string id59_X1;
        string id33_Data;
        string id6_CiscoIPPhoneInputItemType;
        string id47_Postion;
        string id38_URL;
        string id16_CiscoIPPhoneDirectory;
        string id44_Prompt;
        string id40_ExecuteItem;
        string id26_Text;
        string id54_InputFlags;
        string id55_DefaultValue;
        string id43_Title;
        string id4_CiscoIPPhoneResponseItemType;
        string id34_Number;
        string id1_CiscoIPPhoneMenu;
        string id57_MenuItem;
        string id60_Y1;
        string id29_LocationY;
        string id28_LocationX;
        string id17_CiscoIPPhoneGraphicMenu;
        string id18_CiscoIPPhoneGraphicFileMenu;
        string id25_CiscoIPPhoneStatusType;
        string id52_DisplayName;
        string id58_TouchArea;
        string id10_CiscoIPPhoneIconItemType;
        string id71_CiscoIPPhoneImageFileType;
        string id24_CiscoIPPhoneStatus;
        string id36_ResponseItem;
        string id50_InputItem;
        string id23_CiscoIPPhoneError;
        string id35_CiscoIPhoneResponseType;
        string id3_CiscoIPPhoneMenuItemType;
        string id64_CiscoIPPhoneDirectoryType;
        string id51_SoftKeyItem;
        string id63_CiscoIPPhoneGraphicMenuType;
        string id15_CiscoIPPhoneIconMenu;
        string id32_Depth;
        string id5_CiscoIPPhoneExecuteItemType;
        string id48_URLDown;
        string id66_Telephone;
        string id27_Timer;
        string id22_CiscoIPPhoneResponse;
        string id31_Height;
        string id21_CiscoIPPhoneExecute;
        string id12_CiscoIPPhoneSoftKeyType;
        string id9_CiscoIPPhoneDirectoryEntryType;
        string id70_IconIndex;
        string id62_Y2;
        string id65_DirectoryEntry;
        string id41_Priority;
        string id72_CiscoIPPhoneImageType;
        string id13_CiscoIPPhoneImage;

        protected override void InitIDs() {
            id14_CiscoIPPhoneImageFile = Reader.NameTable.Add(@"CiscoIPPhoneImageFile");
            id2_Item = Reader.NameTable.Add(@"");
            id42_CiscoIPPhoneTextType = Reader.NameTable.Add(@"CiscoIPPhoneTextType");
            id11_CiscoIPPhoneIconMenuItemType = Reader.NameTable.Add(@"CiscoIPPhoneIconMenuItemType");
            id56_Item = Reader.NameTable.Add(@"CiscoIPPhoneGraphicFileMenuType");
            id45_SoftKey = Reader.NameTable.Add(@"SoftKey");
            id8_Item = Reader.NameTable.Add(@"CiscoIPPhoneTouchAreaMenuItemType");
            id69_Index = Reader.NameTable.Add(@"Index");
            id20_CiscoIPPhoneText = Reader.NameTable.Add(@"CiscoIPPhoneText");
            id68_IconItem = Reader.NameTable.Add(@"IconItem");
            id61_X2 = Reader.NameTable.Add(@"X2");
            id30_Width = Reader.NameTable.Add(@"Width");
            id67_CiscoIPPhoneIconMenuType = Reader.NameTable.Add(@"CiscoIPPhoneIconMenuType");
            id19_CiscoIPPhoneInput = Reader.NameTable.Add(@"CiscoIPPhoneInput");
            id7_CiscoIPPhoneTouchArea = Reader.NameTable.Add(@"CiscoIPPhoneTouchArea");
            id73_CiscoIPPhoneMenuType = Reader.NameTable.Add(@"CiscoIPPhoneMenuType");
            id46_Name = Reader.NameTable.Add(@"Name");
            id53_QueryStringParam = Reader.NameTable.Add(@"QueryStringParam");
            id37_Status = Reader.NameTable.Add(@"Status");
            id39_CiscoIPPhoneExecuteType = Reader.NameTable.Add(@"CiscoIPPhoneExecuteType");
            id49_CicsoIPPhoneInputType = Reader.NameTable.Add(@"CicsoIPPhoneInputType");
            id59_X1 = Reader.NameTable.Add(@"X1");
            id33_Data = Reader.NameTable.Add(@"Data");
            id6_CiscoIPPhoneInputItemType = Reader.NameTable.Add(@"CiscoIPPhoneInputItemType");
            id47_Postion = Reader.NameTable.Add(@"Postion");
            id38_URL = Reader.NameTable.Add(@"URL");
            id16_CiscoIPPhoneDirectory = Reader.NameTable.Add(@"CiscoIPPhoneDirectory");
            id44_Prompt = Reader.NameTable.Add(@"Prompt");
            id40_ExecuteItem = Reader.NameTable.Add(@"ExecuteItem");
            id26_Text = Reader.NameTable.Add(@"Text");
            id54_InputFlags = Reader.NameTable.Add(@"InputFlags");
            id55_DefaultValue = Reader.NameTable.Add(@"DefaultValue");
            id43_Title = Reader.NameTable.Add(@"Title");
            id4_CiscoIPPhoneResponseItemType = Reader.NameTable.Add(@"CiscoIPPhoneResponseItemType");
            id34_Number = Reader.NameTable.Add(@"Number");
            id1_CiscoIPPhoneMenu = Reader.NameTable.Add(@"CiscoIPPhoneMenu");
            id57_MenuItem = Reader.NameTable.Add(@"MenuItem");
            id60_Y1 = Reader.NameTable.Add(@"Y1");
            id29_LocationY = Reader.NameTable.Add(@"LocationY");
            id28_LocationX = Reader.NameTable.Add(@"LocationX");
            id17_CiscoIPPhoneGraphicMenu = Reader.NameTable.Add(@"CiscoIPPhoneGraphicMenu");
            id18_CiscoIPPhoneGraphicFileMenu = Reader.NameTable.Add(@"CiscoIPPhoneGraphicFileMenu");
            id25_CiscoIPPhoneStatusType = Reader.NameTable.Add(@"CiscoIPPhoneStatusType");
            id52_DisplayName = Reader.NameTable.Add(@"DisplayName");
            id58_TouchArea = Reader.NameTable.Add(@"TouchArea");
            id10_CiscoIPPhoneIconItemType = Reader.NameTable.Add(@"CiscoIPPhoneIconItemType");
            id71_CiscoIPPhoneImageFileType = Reader.NameTable.Add(@"CiscoIPPhoneImageFileType");
            id24_CiscoIPPhoneStatus = Reader.NameTable.Add(@"CiscoIPPhoneStatus");
            id36_ResponseItem = Reader.NameTable.Add(@"ResponseItem");
            id50_InputItem = Reader.NameTable.Add(@"InputItem");
            id23_CiscoIPPhoneError = Reader.NameTable.Add(@"CiscoIPPhoneError");
            id35_CiscoIPhoneResponseType = Reader.NameTable.Add(@"CiscoIPhoneResponseType");
            id3_CiscoIPPhoneMenuItemType = Reader.NameTable.Add(@"CiscoIPPhoneMenuItemType");
            id64_CiscoIPPhoneDirectoryType = Reader.NameTable.Add(@"CiscoIPPhoneDirectoryType");
            id51_SoftKeyItem = Reader.NameTable.Add(@"SoftKeyItem");
            id63_CiscoIPPhoneGraphicMenuType = Reader.NameTable.Add(@"CiscoIPPhoneGraphicMenuType");
            id15_CiscoIPPhoneIconMenu = Reader.NameTable.Add(@"CiscoIPPhoneIconMenu");
            id32_Depth = Reader.NameTable.Add(@"Depth");
            id5_CiscoIPPhoneExecuteItemType = Reader.NameTable.Add(@"CiscoIPPhoneExecuteItemType");
            id48_URLDown = Reader.NameTable.Add(@"URLDown");
            id66_Telephone = Reader.NameTable.Add(@"Telephone");
            id27_Timer = Reader.NameTable.Add(@"Timer");
            id22_CiscoIPPhoneResponse = Reader.NameTable.Add(@"CiscoIPPhoneResponse");
            id31_Height = Reader.NameTable.Add(@"Height");
            id21_CiscoIPPhoneExecute = Reader.NameTable.Add(@"CiscoIPPhoneExecute");
            id12_CiscoIPPhoneSoftKeyType = Reader.NameTable.Add(@"CiscoIPPhoneSoftKeyType");
            id9_CiscoIPPhoneDirectoryEntryType = Reader.NameTable.Add(@"CiscoIPPhoneDirectoryEntryType");
            id70_IconIndex = Reader.NameTable.Add(@"IconIndex");
            id62_Y2 = Reader.NameTable.Add(@"Y2");
            id65_DirectoryEntry = Reader.NameTable.Add(@"DirectoryEntry");
            id41_Priority = Reader.NameTable.Add(@"Priority");
            id72_CiscoIPPhoneImageType = Reader.NameTable.Add(@"CiscoIPPhoneImageType");
            id13_CiscoIPPhoneImage = Reader.NameTable.Add(@"CiscoIPPhoneImage");
        }
    }

    public abstract class XmlSerializer1 : System.Xml.Serialization.XmlSerializer {
        protected override System.Xml.Serialization.XmlSerializationReader CreateReader() {
            return new XmlSerializationReader1();
        }
        protected override System.Xml.Serialization.XmlSerializationWriter CreateWriter() {
            return new XmlSerializationWriter1();
        }
    }

    public sealed class CiscoIPPhoneMenuTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneMenu", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write25_CiscoIPPhoneMenu(objectToSerialize);
        }

        

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read25_CiscoIPPhoneMenu();
        }
    }

    public sealed class CiscoIPPhoneMenuItemTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneMenuItemType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write26_CiscoIPPhoneMenuItemType(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read26_CiscoIPPhoneMenuItemType();
        }
    }

    public sealed class CiscoIPPhoneResponseItemTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneResponseItemType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write27_CiscoIPPhoneResponseItemType(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read27_CiscoIPPhoneResponseItemType();
        }
    }

    public sealed class CiscoIPPhoneExecuteItemTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneExecuteItemType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write28_CiscoIPPhoneExecuteItemType(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read28_CiscoIPPhoneExecuteItemType();
        }
    }

    public sealed class CiscoIPPhoneInputItemTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneInputItemType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write29_CiscoIPPhoneInputItemType(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read29_CiscoIPPhoneInputItemType();
        }
    }

    public sealed class CiscoIPPhoneTouchAreaSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneTouchArea", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write30_CiscoIPPhoneTouchArea(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read30_CiscoIPPhoneTouchArea();
        }
    }

    public sealed class CiscoIPPhoneTouchAreaMenuItemTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneTouchAreaMenuItemType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write31_Item(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read31_Item();
        }
    }

    public sealed class CiscoIPPhoneDirectoryEntryTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneDirectoryEntryType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write32_CiscoIPPhoneDirectoryEntryType(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read32_CiscoIPPhoneDirectoryEntryType();
        }
    }

    public sealed class CiscoIPPhoneIconItemTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneIconItemType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write33_CiscoIPPhoneIconItemType(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read33_CiscoIPPhoneIconItemType();
        }
    }

    public sealed class CiscoIPPhoneIconMenuItemTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneIconMenuItemType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write34_CiscoIPPhoneIconMenuItemType(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read34_CiscoIPPhoneIconMenuItemType();
        }
    }

    public sealed class CiscoIPPhoneSoftKeyTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneSoftKeyType", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write35_CiscoIPPhoneSoftKeyType(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read35_CiscoIPPhoneSoftKeyType();
        }
    }

    public sealed class CiscoIPPhoneImageTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneImage", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write36_CiscoIPPhoneImage(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read36_CiscoIPPhoneImage();
        }
    }

    public sealed class CiscoIPPhoneImageFileTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneImageFile", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write37_CiscoIPPhoneImageFile(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read37_CiscoIPPhoneImageFile();
        }
    }

    public sealed class CiscoIPPhoneIconMenuTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneIconMenu", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write38_CiscoIPPhoneIconMenu(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read38_CiscoIPPhoneIconMenu();
        }
    }

    public sealed class CiscoIPPhoneDirectoryTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneDirectory", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write39_CiscoIPPhoneDirectory(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read39_CiscoIPPhoneDirectory();
        }
    }

    public sealed class CiscoIPPhoneGraphicMenuTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneGraphicMenu", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write40_CiscoIPPhoneGraphicMenu(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read40_CiscoIPPhoneGraphicMenu();
        }
    }

    public sealed class CiscoIPPhoneGraphicFileMenuTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneGraphicFileMenu", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write41_CiscoIPPhoneGraphicFileMenu(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read41_CiscoIPPhoneGraphicFileMenu();
        }
    }

    public sealed class CicsoIPPhoneInputTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneInput", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write42_CiscoIPPhoneInput(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read42_CiscoIPPhoneInput();
        }
    }

    public sealed class CiscoIPPhoneTextTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneText", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write43_CiscoIPPhoneText(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read43_CiscoIPPhoneText();
        }
    }

    public sealed class CiscoIPPhoneExecuteTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneExecute", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write44_CiscoIPPhoneExecute(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read44_CiscoIPPhoneExecute();
        }
    }

    public sealed class CiscoIPhoneResponseTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneResponse", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write45_CiscoIPPhoneResponse(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read45_CiscoIPPhoneResponse();
        }
    }

    public sealed class CiscoIPPhoneErrorSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneError", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write46_CiscoIPPhoneError(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read46_CiscoIPPhoneError();
        }
    }

    public sealed class CiscoIPPhoneStatusTypeSerializer : XmlSerializer1 {

        public override System.Boolean CanDeserialize(System.Xml.XmlReader xmlReader) {
            return xmlReader.IsStartElement(@"CiscoIPPhoneStatus", @"");
        }

        protected override void Serialize(object objectToSerialize, System.Xml.Serialization.XmlSerializationWriter writer) {
            ((XmlSerializationWriter1)writer).Write47_CiscoIPPhoneStatus(objectToSerialize);
        }

        protected override object Deserialize(System.Xml.Serialization.XmlSerializationReader reader) {
            return ((XmlSerializationReader1)reader).Read47_CiscoIPPhoneStatus();
        }
    }

    public class XmlSerializerContract : global::System.Xml.Serialization.XmlSerializerImplementation {
        public override global::System.Xml.Serialization.XmlSerializationReader Reader { get { return new XmlSerializationReader1(); } }
        public override global::System.Xml.Serialization.XmlSerializationWriter Writer { get { return new XmlSerializationWriter1(); } }
        System.Collections.Hashtable readMethods = null;
        public override System.Collections.Hashtable ReadMethods {
            get {
                if (readMethods == null) {
                    System.Collections.Hashtable _tmp = new System.Collections.Hashtable();
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneMenuType::CiscoIPPhoneMenu:False:"] = @"Read25_CiscoIPPhoneMenu";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneMenuItemType::"] = @"Read26_CiscoIPPhoneMenuItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneResponseItemType::"] = @"Read27_CiscoIPPhoneResponseItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneExecuteItemType::"] = @"Read28_CiscoIPPhoneExecuteItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneInputItemType::"] = @"Read29_CiscoIPPhoneInputItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneTouchArea::"] = @"Read30_CiscoIPPhoneTouchArea";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneTouchAreaMenuItemType::"] = @"Read31_Item";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneDirectoryEntryType::"] = @"Read32_CiscoIPPhoneDirectoryEntryType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneIconItemType::"] = @"Read33_CiscoIPPhoneIconItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneIconMenuItemType::"] = @"Read34_CiscoIPPhoneIconMenuItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneSoftKeyType::"] = @"Read35_CiscoIPPhoneSoftKeyType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneImageType::CiscoIPPhoneImage:False:"] = @"Read36_CiscoIPPhoneImage";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneImageFileType::CiscoIPPhoneImageFile:False:"] = @"Read37_CiscoIPPhoneImageFile";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneIconMenuType::CiscoIPPhoneIconMenu:False:"] = @"Read38_CiscoIPPhoneIconMenu";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneDirectoryType::CiscoIPPhoneDirectory:False:"] = @"Read39_CiscoIPPhoneDirectory";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneGraphicMenuType::CiscoIPPhoneGraphicMenu:False:"] = @"Read40_CiscoIPPhoneGraphicMenu";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneGraphicFileMenuType::CiscoIPPhoneGraphicFileMenu:False:"] = @"Read41_CiscoIPPhoneGraphicFileMenu";
                    _tmp[@"CiscoIpPhone.CicsoIPPhoneInputType::CiscoIPPhoneInput:False:"] = @"Read42_CiscoIPPhoneInput";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneTextType::CiscoIPPhoneText:False:"] = @"Read43_CiscoIPPhoneText";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneExecuteType::CiscoIPPhoneExecute:False:"] = @"Read44_CiscoIPPhoneExecute";
                    _tmp[@"CiscoIpPhone.CiscoIPhoneResponseType::CiscoIPPhoneResponse:False:"] = @"Read45_CiscoIPPhoneResponse";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneError:::False:"] = @"Read46_CiscoIPPhoneError";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneStatusType::CiscoIPPhoneStatus:False:"] = @"Read47_CiscoIPPhoneStatus";
                    if (readMethods == null) readMethods = _tmp;
                }
                return readMethods;
            }
        }
        System.Collections.Hashtable writeMethods = null;
        public override System.Collections.Hashtable WriteMethods {
            get {
                if (writeMethods == null) {
                    System.Collections.Hashtable _tmp = new System.Collections.Hashtable();
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneMenuType::CiscoIPPhoneMenu:False:"] = @"Write25_CiscoIPPhoneMenu";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneMenuItemType::"] = @"Write26_CiscoIPPhoneMenuItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneResponseItemType::"] = @"Write27_CiscoIPPhoneResponseItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneExecuteItemType::"] = @"Write28_CiscoIPPhoneExecuteItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneInputItemType::"] = @"Write29_CiscoIPPhoneInputItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneTouchArea::"] = @"Write30_CiscoIPPhoneTouchArea";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneTouchAreaMenuItemType::"] = @"Write31_Item";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneDirectoryEntryType::"] = @"Write32_CiscoIPPhoneDirectoryEntryType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneIconItemType::"] = @"Write33_CiscoIPPhoneIconItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneIconMenuItemType::"] = @"Write34_CiscoIPPhoneIconMenuItemType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneSoftKeyType::"] = @"Write35_CiscoIPPhoneSoftKeyType";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneImageType::CiscoIPPhoneImage:False:"] = @"Write36_CiscoIPPhoneImage";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneImageFileType::CiscoIPPhoneImageFile:False:"] = @"Write37_CiscoIPPhoneImageFile";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneIconMenuType::CiscoIPPhoneIconMenu:False:"] = @"Write38_CiscoIPPhoneIconMenu";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneDirectoryType::CiscoIPPhoneDirectory:False:"] = @"Write39_CiscoIPPhoneDirectory";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneGraphicMenuType::CiscoIPPhoneGraphicMenu:False:"] = @"Write40_CiscoIPPhoneGraphicMenu";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneGraphicFileMenuType::CiscoIPPhoneGraphicFileMenu:False:"] = @"Write41_CiscoIPPhoneGraphicFileMenu";
                    _tmp[@"CiscoIpPhone.CicsoIPPhoneInputType::CiscoIPPhoneInput:False:"] = @"Write42_CiscoIPPhoneInput";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneTextType::CiscoIPPhoneText:False:"] = @"Write43_CiscoIPPhoneText";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneExecuteType::CiscoIPPhoneExecute:False:"] = @"Write44_CiscoIPPhoneExecute";
                    _tmp[@"CiscoIpPhone.CiscoIPhoneResponseType::CiscoIPPhoneResponse:False:"] = @"Write45_CiscoIPPhoneResponse";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneError:::False:"] = @"Write46_CiscoIPPhoneError";
                    _tmp[@"CiscoIpPhone.CiscoIPPhoneStatusType::CiscoIPPhoneStatus:False:"] = @"Write47_CiscoIPPhoneStatus";
                    if (writeMethods == null) writeMethods = _tmp;
                }
                return writeMethods;
            }
        }
        System.Collections.Hashtable typedSerializers = null;
        public override System.Collections.Hashtable TypedSerializers {
            get {
                if (typedSerializers == null) {
                    System.Collections.Hashtable _tmp = new System.Collections.Hashtable();
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneTouchAreaMenuItemType::", new CiscoIPPhoneTouchAreaMenuItemTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CicsoIPPhoneInputType::CiscoIPPhoneInput:False:", new CicsoIPPhoneInputTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneError:::False:", new CiscoIPPhoneErrorSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneIconMenuItemType::", new CiscoIPPhoneIconMenuItemTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneExecuteType::CiscoIPPhoneExecute:False:", new CiscoIPPhoneExecuteTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneImageType::CiscoIPPhoneImage:False:", new CiscoIPPhoneImageTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneTouchArea::", new CiscoIPPhoneTouchAreaSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneGraphicFileMenuType::CiscoIPPhoneGraphicFileMenu:False:", new CiscoIPPhoneGraphicFileMenuTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneMenuItemType::", new CiscoIPPhoneMenuItemTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneResponseItemType::", new CiscoIPPhoneResponseItemTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneStatusType::CiscoIPPhoneStatus:False:", new CiscoIPPhoneStatusTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneGraphicMenuType::CiscoIPPhoneGraphicMenu:False:", new CiscoIPPhoneGraphicMenuTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneImageFileType::CiscoIPPhoneImageFile:False:", new CiscoIPPhoneImageFileTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneDirectoryEntryType::", new CiscoIPPhoneDirectoryEntryTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneDirectoryType::CiscoIPPhoneDirectory:False:", new CiscoIPPhoneDirectoryTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneMenuType::CiscoIPPhoneMenu:False:", new CiscoIPPhoneMenuTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneExecuteItemType::", new CiscoIPPhoneExecuteItemTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPhoneResponseType::CiscoIPPhoneResponse:False:", new CiscoIPhoneResponseTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneIconItemType::", new CiscoIPPhoneIconItemTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneSoftKeyType::", new CiscoIPPhoneSoftKeyTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneInputItemType::", new CiscoIPPhoneInputItemTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneIconMenuType::CiscoIPPhoneIconMenu:False:", new CiscoIPPhoneIconMenuTypeSerializer());
                    _tmp.Add(@"CiscoIpPhone.CiscoIPPhoneTextType::CiscoIPPhoneText:False:", new CiscoIPPhoneTextTypeSerializer());
                    if (typedSerializers == null) typedSerializers = _tmp;
                }
                return typedSerializers;
            }
        }
        public override System.Boolean CanSerialize(System.Type type) {
            if (type == typeof(CiscoIPPhoneMenuType)) return true;
            if (type == typeof(CiscoIPPhoneMenuItemType)) return true;
            if (type == typeof(CiscoIPPhoneResponseItemType)) return true;
            if (type == typeof(CiscoIPPhoneExecuteItemType)) return true;
            if (type == typeof(CiscoIPPhoneInputItemType)) return true;
            if (type == typeof(CiscoIPPhoneTouchArea)) return true;
            if (type == typeof(CiscoIPPhoneTouchAreaMenuItemType)) return true;
            if (type == typeof(CiscoIPPhoneDirectoryEntryType)) return true;
            if (type == typeof(CiscoIPPhoneIconItemType)) return true;
            if (type == typeof(CiscoIPPhoneIconMenuItemType)) return true;
            if (type == typeof(CiscoIPPhoneSoftKeyType)) return true;
            if (type == typeof(CiscoIPPhoneImageType)) return true;
            if (type == typeof(CiscoIPPhoneImageFileType)) return true;
            if (type == typeof(CiscoIPPhoneIconMenuType)) return true;
            if (type == typeof(CiscoIPPhoneDirectoryType)) return true;
            if (type == typeof(CiscoIPPhoneGraphicMenuType)) return true;
            if (type == typeof(CiscoIPPhoneGraphicFileMenuType)) return true;
            if (type == typeof(CicsoIPPhoneInputType)) return true;
            if (type == typeof(CiscoIPPhoneTextType)) return true;
            if (type == typeof(CiscoIPPhoneExecuteType)) return true;
            if (type == typeof(CiscoIPhoneResponseType)) return true;
            if (type == typeof(CiscoIPPhoneError)) return true;
            if (type == typeof(CiscoIPPhoneStatusType)) return true;
            return false;
        }
        public override System.Xml.Serialization.XmlSerializer GetSerializer(System.Type type) {
            if (type == typeof(CiscoIPPhoneMenuType)) return new CiscoIPPhoneMenuTypeSerializer();
            if (type == typeof(CiscoIPPhoneMenuItemType)) return new CiscoIPPhoneMenuItemTypeSerializer();
            if (type == typeof(CiscoIPPhoneResponseItemType)) return new CiscoIPPhoneResponseItemTypeSerializer();
            if (type == typeof(CiscoIPPhoneExecuteItemType)) return new CiscoIPPhoneExecuteItemTypeSerializer();
            if (type == typeof(CiscoIPPhoneInputItemType)) return new CiscoIPPhoneInputItemTypeSerializer();
            if (type == typeof(CiscoIPPhoneTouchArea)) return new CiscoIPPhoneTouchAreaSerializer();
            if (type == typeof(CiscoIPPhoneTouchAreaMenuItemType)) return new CiscoIPPhoneTouchAreaMenuItemTypeSerializer();
            if (type == typeof(CiscoIPPhoneDirectoryEntryType)) return new CiscoIPPhoneDirectoryEntryTypeSerializer();
            if (type == typeof(CiscoIPPhoneIconItemType)) return new CiscoIPPhoneIconItemTypeSerializer();
            if (type == typeof(CiscoIPPhoneIconMenuItemType)) return new CiscoIPPhoneIconMenuItemTypeSerializer();
            if (type == typeof(CiscoIPPhoneSoftKeyType)) return new CiscoIPPhoneSoftKeyTypeSerializer();
            if (type == typeof(CiscoIPPhoneImageType)) return new CiscoIPPhoneImageTypeSerializer();
            if (type == typeof(CiscoIPPhoneImageFileType)) return new CiscoIPPhoneImageFileTypeSerializer();
            if (type == typeof(CiscoIPPhoneIconMenuType)) return new CiscoIPPhoneIconMenuTypeSerializer();
            if (type == typeof(CiscoIPPhoneDirectoryType)) return new CiscoIPPhoneDirectoryTypeSerializer();
            if (type == typeof(CiscoIPPhoneGraphicMenuType)) return new CiscoIPPhoneGraphicMenuTypeSerializer();
            if (type == typeof(CiscoIPPhoneGraphicFileMenuType)) return new CiscoIPPhoneGraphicFileMenuTypeSerializer();
            if (type == typeof(CicsoIPPhoneInputType)) return new CicsoIPPhoneInputTypeSerializer();
            if (type == typeof(CiscoIPPhoneTextType)) return new CiscoIPPhoneTextTypeSerializer();
            if (type == typeof(CiscoIPPhoneExecuteType)) return new CiscoIPPhoneExecuteTypeSerializer();
            if (type == typeof(CiscoIPhoneResponseType)) return new CiscoIPhoneResponseTypeSerializer();
            if (type == typeof(CiscoIPPhoneError)) return new CiscoIPPhoneErrorSerializer();
            if (type == typeof(CiscoIPPhoneStatusType)) return new CiscoIPPhoneStatusTypeSerializer();
            return null;
        }
    }
}
