using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Toodledo.Model;
using Toodledo.Model.API;

namespace Toodledo.Client
{
	public partial class Session
	{
		public static Session Create(string user, string password, string appid)
		{
			var userid = (user.Contains("@") ? GetUserId(user, password) : user);

            if (Cache.Contains<Session>(userid))
                return Cache.Get<Session>(userid);
			return (Cache.Set<Session>(new Session(userid, password, appid), userid));
		}

        public static Session Create(string user, string password)
        {
            return Create(user, password, null);
        }

        public static string GetUserId(string user, string password)
		{
			var query = @"http://api.toodledo.com/api.php?method=getUserid;email={0};pass={1}";

			var results = XDocument.Load(String.Format(query, user, password));
			return results.Element(XName.Get("userid", "")).Value;
		}

        public Session() { }

	    private Session(string userid, string password, string appid)
		{
			UserId = userid;
			Password = password;
		    Token = ((IAuthentication) this).GetToken(userid, appid);
		    Account = ((IAuthentication) this).GetAccountInfo();
		}

        public string AppId { get; set; }
		public string Token { get; set; }
		public string UserId { get; set; }
        public string Password { get; set; }

		public string Key
		{
			get
			{
				if (String.IsNullOrEmpty(Token))
					throw new ArgumentNullException("Token is missing.");
				
				// key = md5( md5(password)+token+myuserid )

				return MD5(MD5(Password) + Token + UserId);

			}
		}

		private string MD5(string input)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
			bs = x.ComputeHash(bs);
			System.Text.StringBuilder s = new System.Text.StringBuilder();
			foreach (byte b in bs)
			{
				s.Append(b.ToString("x2").ToLower());
			}
			string password = s.ToString();
			return password;
		}

		private static Session _currentSession = null;
		public static Session CurrentSession
		{
			get { return _currentSession; }
        }

        public Account Account { get; set; }

	    public Server Server
	    {
	        get { return ((IAuthentication) this).GetServerInfo(); }
	    }

        #region Communications

	    private static bool _isRevalidating = false;
        internal XDocument SendRequest(string method, string parameters)
        {
            string key = String.Empty;
            switch(method)
            {
                case "getToken":
                case "getUserId":
                    key = String.Empty;
                    break;
                default:
                    key = String.Format(";key={0}", this.Key);
                    break;
            }
            var url = String.Format("http://api.toodledo.com/api.php?method={0}{1};{2}", method, key, parameters);
            var results = XDocument.Load(url);
            if (results.Root == null)
                throw new ApplicationException("Invalid XML response from Toodledo");
            if (results.Root.Name == "error")
            {
                var message = ReadElement<string>(results);
                if (message == "key did not validate")
                {
                    if (!_isRevalidating)
                    {
                        _isRevalidating = true;
                        // Our session has expired, so get a new token.
                        Token = ((IAuthentication) this).GetToken(UserId, AppId);
                        Cache.Set<Session>(this, UserId);
                        results = SendRequest(method, parameters);
                        _isRevalidating = false;
                        return results;
                    }
                }
                throw new ApplicationException(String.Format("Error \"{0}\"", message));
            }
            return results;
        }

        internal XDocument SendRequest(string method)
        {
            return SendRequest(method, String.Empty);
        }

        internal ItemType ReadElement<ItemType>(XDocument document)
        {
            return ReadElement<ItemType>(document.Root);
        }

        internal ItemType ReadElement<ItemType>(XDocument document, string name)
        {
            if (document.Root == null)
                return default(ItemType);
            return ReadElement<ItemType>(document.Root, name);
        }

        internal ItemType ReadElement<ItemType>(XElement element, string name)
        {
            return ReadElement<ItemType>(element.Element(XName.Get(name, String.Empty)));
        }

        internal ItemType ReadElement<ItemType>(XElement element)
        {
            var value = (element == null) ? String.Empty : element.Value;
            return (ItemType)Convert.ChangeType(value, typeof(ItemType));
        }

        internal ItemType ReadAttribute<ItemType>(XDocument document, string name)
        {
            if (document.Root == null)
                return default(ItemType);
            return ReadAttribute<ItemType>(document.Root, name);
        }

        internal ItemType ReadAttribute<ItemType>(XElement element, string name)
        {
            var attribute = element.Attribute(XName.Get(name, String.Empty));
            var value = (attribute == null) ? String.Empty : attribute.Value;
            return (ItemType)Convert.ChangeType(value, typeof(ItemType));
        }

        private string formatArgument(bool value, string name)
        {
            return String.Format("{0}={1};", name, (value) ? 1 : 0);
        }

        private string formatArgument(int? value, string name)
        {
            return (!value.HasValue) ? String.Empty : String.Format("{0}={1};", name, value);
        }

        private string formatArgument(int value, string name)
        {
            return String.Format("{0}={1};", name, value);
        }

        private string formatArgument(string value, string name)
        {
            return (String.IsNullOrEmpty(value)) ? String.Empty : String.Format("{0}={1};", name, encodeString(value));
        }

        private string formatArgument(DateTime? value, string name)
        {
            return (!value.HasValue) ? String.Empty : String.Format("{0}={1};", name, value.Value);
        }

        private string formatArgument(DateTime value, string name)
        {
            return (value == DateTime.MinValue) ? String.Empty : String.Format("{0}={1};", name, value);
        }

        private string formatArgument(TimeSpan value, string name)
        {
            return (value == TimeSpan.Zero) ? String.Empty : String.Format("{0}={1};", name, value);
        }

        private string formatArgument(Item value, string name)
        {
            return (value == null) ? String.Empty : String.Format("{0}={1};", name, value.Id);
        }

        #endregion Communications
	}
}
