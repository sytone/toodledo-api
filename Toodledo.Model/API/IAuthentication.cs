using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model.API
{
    public interface IAuthentication
    {
        string GetToken(string userid, string appid);
        string GetUserId(string email, string password);
        Account GetAccountInfo();
        string CreateAccount(string email, string password);
        Server GetServerInfo();

    }
}
