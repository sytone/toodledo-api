using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace Toodledo.Client
{
    public interface IItemFactory
    {
        string Name { get; set; }
        IEnumerable<ItemPropertyMapping> Mappings { get; }
        object Create(XElement element);
    }

    public class ItemFactory<ItemType> : IItemFactory where ItemType : new() 
    {
        public ItemFactory(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        private List<ItemPropertyMapping> _mappings = new List<ItemPropertyMapping>();
        public IEnumerable<ItemPropertyMapping> Mappings
        {
            get { return _mappings; }
        }

        public ItemFactory<ItemType> Attribute<AttributeType>(string property, string name)
        {
            _mappings.Add(new ItemPropertyMapping(XmlNodeType.Attribute) { Type = typeof(AttributeType), Name = XName.Get(name,""), Property = typeof(ItemType).GetProperty(property) });
            return this;
        }

        public ItemFactory<ItemType> Value<ValueType>(string property)
        {
            _mappings.Add(new ItemPropertyMapping(XmlNodeType.Text) { Type = typeof(ValueType), Name = XName.Get(Name, ""), Property = typeof(ItemType).GetProperty(property) });
            return this;
        }

        public ItemFactory<ItemType> Element<ElementType>(string property, string name)
        {
            _mappings.Add(new ItemPropertyMapping(XmlNodeType.Element) { Type = typeof(ElementType), Name = XName.Get(name, ""), Property = typeof(ItemType).GetProperty(property) });
            return this;
        }

        public ItemFactory<ItemType> Element<ElementType>(string property, ItemFactory<ElementType> factory) where ElementType : new()
        {
            _mappings.Add(new FactoryItemPropertyMapping<ElementType>(XmlNodeType.Element) { Factory = factory, Name = XName.Get(factory.Name, ""), Property = typeof(ItemType).GetProperty(property) });
            return this;
        }

        public ItemType Create(XElement element)
        {
            var item = new ItemType();
            foreach(var mapping in _mappings)
            {
                string value = string.Empty;
                if (mapping.NodeType == XmlNodeType.Attribute)
                    value = element.Attribute(mapping.Name).Value;
                if (mapping.NodeType == XmlNodeType.Text)
                    value = element.Value;
                if (mapping.NodeType == XmlNodeType.Element && mapping.GetType().Name != "FactoryItemPropertyMapping`1")
                {
                    value = (element.Element(mapping.Name) == null) ? String.Empty : element.Element(mapping.Name).Value;
                }
                if (mapping.NodeType == XmlNodeType.Element && mapping.GetType().Name == "FactoryItemPropertyMapping`1")
                {
                    var getFactory = mapping.GetType().GetProperty("Factory");
                    var factory = getFactory.GetValue(mapping, null) as IItemFactory;
                    if (factory != null && element.Elements(XName.Get(factory.Name)).Count() > 0)
                    {
                        var subItem = factory.Create(element.Element(XName.Get(factory.Name)));
                        mapping.Property.SetValue(item, subItem, null);
                    }
                    else 
                        mapping.Property.SetValue(item, null, null);
                }
                else
                    mapping.Property.SetValue(item, cast(value, mapping.Type), null);
            }
            return item;
        }

        object IItemFactory.Create(XElement element)
        {
            return Create(element);
        }

        private object cast(string value, Type target)
        {
            try
            {
                if (target == typeof(string))
                    return value;

                if (target == typeof(bool))
                    return (!String.IsNullOrEmpty(value)) ?  (int.Parse(value) == 1) : false;

                if (target == typeof(DateTime))
                    return (!String.IsNullOrEmpty(value)) ? DateTime.Parse(value) : DateTime.MinValue;

                if (target.IsEnum)
                    return (!String.IsNullOrEmpty(value)) ? Enum.Parse(target, value) : 0;

                return Convert.ChangeType(value, target);

            }
            catch (Exception ex)
            {
                return value;
            }
        }
    }

    public class ItemPropertyMapping
    {
        public ItemPropertyMapping(XmlNodeType type)
        {
            NodeType = type;
        }

        public XmlNodeType NodeType { get; set; }
        public Type Type { get; set; }
        public XName Name { get; set; }
        public PropertyInfo Property { get; set; }
    }

    public class FactoryItemPropertyMapping<ItemType> : ItemPropertyMapping where ItemType : new()
    {
        public FactoryItemPropertyMapping(XmlNodeType type) : base(type)
        {
            Type = typeof (ItemType);
        }

        public ItemFactory<ItemType> Factory { get; set; }
    }
}
