using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.IO;

namespace Toodledo.Client
{
    public static class Cache
    {
        private static IsolatedStorageFile _store;

        static Cache()
        {
            _store = IsolatedStorageFile.GetUserStoreForAssembly();
        }

        private const string FILESPEC_FORMAT = "{0}\\{1}.xml";

        public static bool Contains<ItemType>(string key) where ItemType : class 
        {
            assureDirectoryExists<ItemType>();
            return _store.GetFileNames(getItemPath<ItemType>(key)).Length > 0;
        }

        public static ItemType Get<ItemType>(string key) where ItemType : class 
        {
            if (!Contains<ItemType>(key))
                throw new ArgumentOutOfRangeException(String.Format("There is no {0} with a key of \"{1}\" in the cache.",
                                                                    typeof (ItemType).Name, key));
            using (var stream = new IsolatedStorageFileStream(getItemPath<ItemType>(key), FileMode.Open, _store))
            {
                var serializer = new XmlSerializer(typeof(ItemType));
                return serializer.Deserialize(stream) as ItemType;
            }
        }

        public static ItemType Set<ItemType>(ItemType item, string key) where ItemType : class 
        {
            assureDirectoryExists<ItemType>();
            var serializer = new XmlSerializer(item.GetType());
            var stream = new IsolatedStorageFileStream(getItemPath<ItemType>(key), FileMode.Create, _store);
            serializer.Serialize(stream, item);
            stream.Close();
            return item;
        }

        private static string getItemPath<ItemType>(string key) where ItemType : class 
        {
            assureDirectoryExists<ItemType>();
            return String.Format(FILESPEC_FORMAT, typeof (ItemType).Name, key);
        }

        private static void assureDirectoryExists<ItemType>() where ItemType : class 
        {
            if (_store.GetDirectoryNames(typeof(ItemType).Name).Length == 0)
                _store.CreateDirectory(typeof(ItemType).Name);
        }
    }
}
