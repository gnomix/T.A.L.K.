﻿/*
    This file is part of TALK (Wybecom).

    TALK is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License, version 2 as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    TALK is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with TALK.  If not, see <http://www.gnu.org/licenses/gpl-2.0.html>.
 
    TALK is based upon:
    - Sun JTAPI http://java.sun.com/products/jtapi/
    - JulMar TAPI http://julmar.com/
    - Asterisk.Net http://sourceforge.net/projects/asterisk-dotnet/
 
*/
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web.UI;

// Les informations générales relatives à un assembly dépendent de 
// l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
// associées à un assembly.
[assembly: AssemblyTitle("Wybecom.TalkPortal.CTI.Controls")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Wybecom")]
[assembly: AssemblyProduct("Wybecom.TalkPortal.CTI.Controls")]
[assembly: AssemblyCopyright("Copyright © Wybecom 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly 
// aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de 
// COM, affectez la valeur true à l'attribut ComVisible sur ce type.
[assembly: ComVisible(false)]

// Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
[assembly: Guid("17489482-6130-44f6-ae7f-a277d48074ac")]

// Les informations de version pour un assembly se composent des quatre valeurs suivantes :
//
//      Version principale
//      Version secondaire 
//      Numéro de build
//      Révision
//
// Vous pouvez spécifier toutes les valeurs ou indiquer les numéros de révision et de build par défaut 
// en utilisant '*', comme indiqué ci-dessous :
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.popup.js", "text/javascript")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.jquery-1.2.6.min.js", "text/javascript")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.CTIClient.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.CTIClient.css", "text/css",PerformSubstitution=true)]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.popup.css", "text/css")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.answer_call_32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.headphones_32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.disable_dnd-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.divert_call-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.dnd-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.end_answer_call_32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.end_place_call_32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.hold_call-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.mail-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.new_mail-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.phone-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.place_call_32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.transfer_call-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.unhold_call-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.arrow-left.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.arrow-right.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.calllogs.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.phonebook.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.small_phonebook.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.status_spacer.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.status_left.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.login-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.logout-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.ready-32x32.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.CTI.Controls.notready-32x32.png", "img/png")]
[assembly: ScriptResource("Wybecom.TalkPortal.CTI.Controls.CTIClient.js",
   "Wybecom.TalkPortal.CTI.Controls.CTIClient", "Wybecom.TalkPortal.CTI.Controls.Resource")]
[assembly: ScriptResource("Wybecom.TalkPortal.CTI.Controls.jquery-1.2.6.min.js",
   "Wybecom.TalkPortal.CTI.Controls.CTIClient", "Wybecom.TalkPortal.CTI.Controls.Resource")]
[assembly: ScriptResource("Wybecom.TalkPortal.CTI.Controls.popup.js",
   "Wybecom.TalkPortal.CTI.Controls.CTIClient", "Wybecom.TalkPortal.CTI.Controls.Resource")]