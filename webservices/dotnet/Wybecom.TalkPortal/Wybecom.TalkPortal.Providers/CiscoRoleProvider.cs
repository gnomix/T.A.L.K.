using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Wybecom.TalkPortal.Cisco.AXL.Proxy;
using log4net;

namespace Wybecom.TalkPortal.Providers
{
    public class CiscoRoleProvider : RoleProvider
    {
        private string _applicationName;
        private AXLAPIService _axlService;
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CiscoRoleProvider()
        {
            try
            {
                _axlService = new AXLAPIService();
            }
            catch (Exception axlException)
            {
                log.Error("Impossible d'instancier le client AXL: " + axlException.ToString());
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> result = new List<string>();
            try
            {
                GetUserReq gur = new GetUserReq();
                gur.userid = username;
                GetUserRes gurresponse = _axlService.getUser(gur);
                if (gurresponse != null && gurresponse.@return != null && gurresponse.@return.user != null && gurresponse.@return.user.associatedGroups != null)
                {
                    foreach (XUserUserGroup group in gurresponse.@return.user.associatedGroups)
                    {
                        result.Add(group.name);
                    }
                }
            }
            catch (Exception getRoleException)
            {
                log.Error("Impossible de connaitre les roles de " + username + ": " + getRoleException.ToString());
            }
            return result.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            string[] result = null;
            List<string> userList = new List<string>();
            try
            {
                NameAndGUIDRequest userGroupReq = new NameAndGUIDRequest();
                userGroupReq.ItemElementName = ItemChoiceType61.name;
                userGroupReq.Item = roleName;
                GetUserGroupRes res = _axlService.getUserGroup(userGroupReq);
                if (res != null && res.@return != null && res.@return.userGroup != null)
                {
                    foreach (XUserGroupMember member in res.@return.userGroup.members)
                    {
                        userList.Add(member.Item.ToString());
                    }
                }
            }
            catch (Exception getGroupException)
            {
                log.Error("Impossible de récupérer la liste des utilisateurs appartenants au groupe " + roleName + ": " + getGroupException.ToString());
            }
            if (userList != null)
            {
                result = userList.ToArray();
            }
            return result;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool result = false;
                
            try
            {
                GetUserReq gur = new GetUserReq();
                gur.userid = username;
                GetUserRes gurresponse = _axlService.getUser(gur);
                if (gurresponse != null && gurresponse.@return != null && gurresponse.@return.user != null && gurresponse.@return.user.associatedGroups != null)
                {
                    foreach (XUserUserGroup group in gurresponse.@return.user.associatedGroups)
                    {
                        if (String.Equals(roleName, group.name))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception getUserException)
            {
                log.Error("Impossible de savoir si l'utilisateur " + username + " appartient au groupe " + roleName + ": " + getUserException.ToString());
            }
            return result;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            bool result = false;

            try
            {
                NameAndGUIDRequest userGroupReq = new NameAndGUIDRequest();
                userGroupReq.ItemElementName = ItemChoiceType61.name;
                userGroupReq.Item = roleName;
                GetUserGroupRes res = _axlService.getUserGroup(userGroupReq);
                if (res != null && res.@return != null && res.@return.userGroup != null)
                {
                    result = true;
                }
            }
            catch (Exception roleExistsException)
            {
                log.Error("Impossible de savoir si le role " + roleName + " existe: " + roleExistsException.ToString());
            }
            return result;
        }
    }
}
