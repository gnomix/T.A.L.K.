 ///
 ///  WYBECOM T.A.L.K. -- Telephony Application Library Kit
 ///  Copyright (C) 2010 WYBECOM
 ///
 ///  Yohann BARRE <y.barre@wybecom.com>
 ///
 ///  This program is free software: you can redistribute it and/or modify
 ///  it under the terms of the GNU General Public License as published by
 ///  the Free Software Foundation, either version 3 of the License, or
 ///  (at your option) any later version.
 ///
 ///  This program is distributed in the hope that it will be useful,
 ///  but WITHOUT ANY WARRANTY; without even the implied warranty of
 ///  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 ///  GNU General Public License for more details.
 ///
 ///  You should have received a copy of the GNU General Public License
 ///  along with this program.  If not, see <http:///www.gnu.org/licenses/>.
 ///
 ///  T.A.L.K. is based upon:
 ///  - Sun JTAPI http:///java.sun.com/products/jtapi/
 ///  - JulMar TAPI http:///julmar.com/
 ///  - Asterisk.Net http:///sourceforge.net/projects/asterisk-dotnet/
 ///
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace Wybecom.TalkPortal.Providers
{
    [TypeConverter(typeof(ReasonCodeTypeConverter))]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    [Serializable()]
    public class ReasonCode
    {
        private ushort _id;
        private string _description;

        public ReasonCode()
        {
        }

        public ReasonCode(ushort reasonid, string reasondescription)
        {
            _id = reasonid;
            _description = reasondescription;
        }

        public ushort id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }
    }

    [TypeConverter(typeof(ReasonCodeCollectionTypeConverter))]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    [Serializable()]
    public class ReasonCodeCollection : ICollection<ReasonCode>
    {
        private List<ReasonCode> _reasoncodes;

        public ReasonCodeCollection()
        {
            _reasoncodes = new List<ReasonCode>();
        }

        #region ICollection<ReasonCode> Membres

        public void Add(ReasonCode item)
        {
            _reasoncodes.Add(item);
        }

        public void Clear()
        {
            _reasoncodes.Clear();
        }

        public bool Contains(ReasonCode item)
        {
            return _reasoncodes.Contains(item);
        }

        public void CopyTo(ReasonCode[] array, int arrayIndex)
        {
            _reasoncodes.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _reasoncodes.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ReasonCode item)
        {
            return _reasoncodes.Remove(item);
        }

        #endregion

        #region IEnumerable<ReasonCode> Membres

        public IEnumerator<ReasonCode> GetEnumerator()
        {
            return _reasoncodes.GetEnumerator();
        }

        #endregion

        #region IEnumerable Membres

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _reasoncodes.GetEnumerator();
        }

        #endregion
    }

    public class ReasonCodeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                ReasonCode rc = new ReasonCode();
                string str = value as string;
                if (str != null)
                {
                    str = str.Trim();
                    string[] props = str.Split(new char[] { ',' }, StringSplitOptions.None);
                    rc.id = ushort.Parse(props[0]);
                    rc.description = props[1];
                }
                return rc;
            }
            else return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string str = String.Empty;
                ReasonCode rc = value as ReasonCode;
                if (rc != null)
                {
                    str = string.Format("{0},{1}", rc.id, rc.description);
                }
                return str;
            }
            else return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class ReasonCodeCollectionTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                ReasonCodeCollection rcc = new ReasonCodeCollection();
                string str = value as string;
                if (str != null)
                {
                    str = str.Trim();
                    string[] reasoncodes = str.Split(new char[] { ';' }, StringSplitOptions.None);
                    foreach (string rcode in reasoncodes)
                    {
                        ReasonCodeTypeConverter rctc = new ReasonCodeTypeConverter();
                        ReasonCode rc = (ReasonCode)rctc.ConvertFrom(rcode);
                        if (rc != null)
                        {
                            rcc.Add(rc);
                        }
                    }
                }
                return rcc;
            }
            else return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string str = String.Empty;
                ReasonCodeCollection rcc = value as ReasonCodeCollection;
                if (rcc != null)
                {
                    foreach (ReasonCode rc in rcc)
                    {
                        ReasonCodeTypeConverter rctc = new ReasonCodeTypeConverter();

                        if (str == String.Empty)
                        {
                            str += string.Format("{0}", (string)rctc.ConvertTo(rc, typeof(string)) );
                        }
                        else
                        {
                            str += string.Format(";{0}", (string)rctc.ConvertTo(rc, typeof(string)));
                        }
                    }
                }
                return str;
            }
            else return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
