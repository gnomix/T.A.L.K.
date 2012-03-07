using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Wybecom.TalkPortal.Cisco.AXL.Proxy;
using log4net;

namespace Wybecom.TalkPortal.Providers
{
    public class CiscoMembershipProvider : MembershipProvider
    {
        private string _applicationName;
        private AXLAPIService _axlService;
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CiscoMembershipProvider()
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            //Nombre d'utilisateur en ligne...
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public XUser GetUser(string username)
        {
            XUser user = null;
            try
            {
                GetUserReq gur = new GetUserReq();
                gur.userid = username;
                GetUserRes gurresponse = _axlService.getUser(gur);
                if (gurresponse != null && gurresponse.@return != null && gurresponse.@return.user != null)
                {
                    user = gurresponse.@return.user;
                }
            }
            catch (Exception getUserException)
            {
                log.Error("Impossible de récupérer l'utilisateur Cisco " + username + ": " + getUserException.ToString());
            }
            return user;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                AuthenticateUserReq aur = new AuthenticateUserReq();
                aur.ItemElementName = ItemChoiceType42.password;
                aur.Item = password;
                aur.sequenceSpecified = false;
                aur.userid = username;
                AuthenticateUserResponse response = _axlService.doAuthenticateUser(aur);
                return response.@return.userAuthenticated;
            }
            catch (Exception e)
            {
                log.Error("Impossible d'authentifier l'utilisateur: " + e.Message);
                return false;
            }
        }
    }
}
