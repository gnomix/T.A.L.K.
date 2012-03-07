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
 
namespace Wybecom.TalkPortal.Connectors.Asterisk.Properties {
    
    
    // Cette classe vous permet de gérer des événements spécifiques dans la classe de paramètres :
    //  L'événement SettingChanging est déclenché avant la modification d'une valeur de paramètre.
    //  L'événement PropertyChanged est déclenché après la modification d'une valeur de paramètre.
    //  L'événement SettingsLoaded est déclenché après le chargement des valeurs de paramètre.
    //  L'événement SettingsSaving est déclenché avant l'enregistrement des valeurs de paramètre.
    internal sealed partial class Settings {
        
        public Settings() {
            // // Pour ajouter des gestionnaires d'événements afin d'enregistrer et de modifier les paramètres, supprimez les marques de commentaire des lignes ci-dessous :
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Ajouter du code pour gérer l'événement SettingChangingEvent ici.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Ajouter du code pour gérer l'événement SettingsSaving ici.
        }
    }
}
