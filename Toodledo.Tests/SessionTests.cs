using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toodledo.Client;
using Toodledo.Model;

using Toodledo.Model.API;
using Toodledo.Tests.Internal;
using Toodledo.Tests.Properties;

namespace Toodledo.Tests
{
    [TestClass]
    public class SessionTests : TestsBase
    {
        [TestMethod]
        public void Can_provide_a_session()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(this.Session.Token));
        }

        [TestMethod]
        public void Can_authenticate_with_userid()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(this.Session.Token));
            Assert.IsTrue(this.Session.Token.StartsWith("td"));
        }

        [TestMethod]
        public void Can_authenticate_with_email()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(this.Session.Token));
            Assert.IsTrue(this.Session.Token.StartsWith("td"));
        }

        [TestMethod]
        public void Can_Get_AccountInfo()
        {
            var account = this.Session.Account;

            Assert.IsNotNull(account);
            Assert.AreEqual(Settings.Default.USERID, account.UserId);
        }

        [TestMethod]
        public void Can_Get_ServerInfo()
        {
            var server = this.Session.Server;

            Assert.IsNotNull(server);
            ExtAssert.Greater(server.TokenExpires, 0);
        }
    }
}
