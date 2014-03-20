using Toodledo.Client;
using Toodledo.Model.API;
using Toodledo.Tests.Properties;

namespace Toodledo.Tests.Internal
{
    public class TestsBase
    {
        private Session _session = null;
        public Session Session
        {
            get { return _session ?? Session.Create(Settings.Default.USERID, Settings.Default.PASSWORD, Settings.Default.APPID); }
        }

        public IGeneral General
        {
            get { return (IGeneral)this.Session; }
        }

        public ITasks Tasks
        {
            get { return (ITasks) this.Session; }
        }

        public INotebook Notebook
        {
            get { return (INotebook) this.Session; }
        }
    }
}