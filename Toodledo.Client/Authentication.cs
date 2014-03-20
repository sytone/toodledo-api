using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Toodledo.Model;
using Toodledo.Model.API;

namespace Toodledo.Client
{
	public partial class Session : IAuthentication
	{
        #region Implementation of IAuthentication

        string IAuthentication.GetToken(string userid, string appid)
        {
            var parameters = String.Format("userid={0}{1}", userid, (String.IsNullOrEmpty(appid) ? String.Empty : ";appid=" + appid));
            return ReadElement<String>(SendRequest("getToken", parameters));
        }

        string IAuthentication.GetUserId(string email, string password)
        {
            return GetUserId(email, password);
        }

        Account IAuthentication.GetAccountInfo()
        {
            var results = SendRequest("getAccountInfo");
            return new Account
                              {
                                  Alias = ReadElement<string>(results, "alias"),
                                  DateFormat = (DateFormat) ReadElement<int>(results, "dateformat"),
                                  HideMonths = ReadElement<int>(results, "hidemonths"),
                                  HotListDueDate = ReadElement<int>(results, "hotlistduedate"),
                                  HotListPriority = (Priority) ReadElement<int>(results, "hotlistpriority"),
                                  IsPro = (ReadElement<int>(results, "pro") != 0),
                                  LastAddEdit = DateTime.Parse(ReadElement<string>(results, "lastaddedit")),
                                  LastContextEdit = DateTime.Parse(ReadElement<string>(results, "lastcontextedit")),
                                  LastDelete = DateTime.Parse(ReadElement<string>(results, "lastdelete")),
                                  LastFolderEdit = DateTime.Parse(ReadElement<string>(results, "lastfolderedit")),
                                  LastGoalEdit = DateTime.Parse(ReadElement<string>(results, "lastgoaledit")),
                                  TimeZone = ReadElement<int>(results, "timezone"),
                                  UserId = ReadElement<string>(results, "userid")
                              };
        }

        string IAuthentication.CreateAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        Server IAuthentication.GetServerInfo()
        {
            var results = SendRequest("getServerInfo");
            return new Server()
                       {
                           Timestamp = DateTime.Parse(ReadElement<string>(results, "date")),
                           TokenExpires = ReadElement<float>(results, "tokenexpires")
                       };
        }

        #endregion Implementation of IAuthentication

        public void 
	}
}
