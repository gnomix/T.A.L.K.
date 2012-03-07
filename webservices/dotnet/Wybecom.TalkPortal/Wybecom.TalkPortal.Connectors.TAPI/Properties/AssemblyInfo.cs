using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using log4net;
// Les informations générales relatives à un assembly dépendent de 
// l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
// associées à un assembly.
[assembly: AllowPartiallyTrustedCallers()]
[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "talkLog", Watch = true)]
[assembly: log4net.Config.Repository("Wybecom.TalkPortal.Connectors.TAPI")]
[assembly: AssemblyTitle("Wybecom.TalkPortal.Connectors.TAPI")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Wybecom")]
[assembly: AssemblyProduct("Wybecom.TalkPortal.Connectors.TAPI")]
[assembly: AssemblyCopyright("Copyright © Wybecom 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly 
// aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de 
// COM, affectez la valeur true à l'attribut ComVisible sur ce type.
[assembly: ComVisible(false)]

// Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
[assembly: Guid("f3a8cfde-0752-4210-b886-56351ccac3ac")]

// Les informations de version pour un assembly se composent des quatre valeurs suivantes :
//
//      Version principale
//      Version secondaire 
//      Numéro de build
//      Révision
//
// Vous pouvez spécifier toutes les valeurs ou indiquer les numéros de build et de révision par défaut 
// en utilisant '*', comme indiqué ci-dessous :
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
