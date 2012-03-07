/*
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

// Les informations générales relatives à un assembly dépendent de 
// l'ensemble d'attributs suivant. Changez les valeurs de ces attributs pour modifier les informations
// associées à un assembly.
[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "talkLog", Watch = true)]
[assembly: log4net.Config.Repository("Wybecom.TalkPortal.CTI")]
[assembly: AssemblyTitle("Wybecom.TalkPortal.CTI")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Wybecom")]
[assembly: AssemblyProduct("Wybecom.TalkPortal.CTI")]
[assembly: AssemblyCopyright("Copyright © Wybecom 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// L'affectation de la valeur false à ComVisible rend les types invisibles dans cet assembly 
// aux composants COM. Si vous devez accéder à un type dans cet assembly à partir de 
// COM, affectez la valeur true à l'attribut ComVisible sur ce type.
[assembly: ComVisible(false)]

// Le GUID suivant est pour l'ID de la typelib si ce projet est exposé à COM
[assembly: Guid("3d5900ae-111a-45be-96b3-d9e4606ca793")]

// Les informations de version pour un assembly se composent des quatre valeurs suivantes :
//
//      Version principale
//      Version secondaire 
//      Numéro de build
//      Révision
//
// Vous pouvez spécifier toutes les valeurs ou indiquer les numéros de révision et de build par défaut 
// en utilisant '*', comme indiqué ci-dessous :
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
