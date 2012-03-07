using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web.UI;
using log4net;
// Les informations générales relatives à un assembly dépendent de 
// l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
// associées à un assembly.
[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "talkLog", Watch = true)]
[assembly: log4net.Config.Repository("Wybecom.TalkPortal.DMD.Controls")]
[assembly: AssemblyTitle("Wybecom.TalkPortal.DMD.Controls")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Wybecom")]
[assembly: AssemblyProduct("Wybecom.TalkPortal.DMD.Controls")]
[assembly: AssemblyCopyright("Copyright © Wybecom 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly 
// aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de 
// COM, affectez la valeur true à l'attribut ComVisible sur ce type.
[assembly: ComVisible(false)]

// Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
[assembly: Guid("f0702d86-4e0c-4e52-9874-5590b37bed20")]

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
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.popup.js", "text/javascript")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.jquery-1.2.6.min.js", "text/javascript")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.DMDClient.js", "text/javascript")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.DMDClient.css", "text/css")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.popup.css", "text/css")]
[assembly: ScriptResource("Wybecom.TalkPortal.DMD.Controls.DMDClient.js",
   "Wybecom.TalkPortal.DMD.Controls.DMDClient", "Wybecom.TalkPortal.DMD.Controls.Resource")]
[assembly: ScriptResource("Wybecom.TalkPortal.DMD.Controls.jquery-1.2.6.min.js",
   "Wybecom.TalkPortal.DMD.Controls.DMDClient", "Wybecom.TalkPortal.DMD.Controls.Resource")]
[assembly: ScriptResource("Wybecom.TalkPortal.DMD.Controls.popup.js",
   "Wybecom.TalkPortal.DMD.Controls.DMDClient", "Wybecom.TalkPortal.DMD.Controls.Resource")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.available.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.busy.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.logout.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.private.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.unknown.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.bookmark-new.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.consulttransfer.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.directtransfer.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.forwardalldn.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.forwardallmevo.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.donotdisturb.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.transfer.png", "img/png")]
[assembly: WebResource("Wybecom.TalkPortal.DMD.Controls.monitor.png", "img/png")]
