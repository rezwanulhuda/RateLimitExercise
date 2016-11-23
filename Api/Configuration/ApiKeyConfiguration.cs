using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Api.Configuration
{
    public class ApiKeysSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public ApiKeyCollection Instances
        {
            get { return (ApiKeyCollection)this[""]; }
            set { this[""] = value; }
        }
    }

    public class ApiKeyCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ApiKeyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApiKeyElement)element).Key;
        }

        public new ApiKeyElement this[string elementName]
        {
            get
            {
                return this.OfType<ApiKeyElement>().FirstOrDefault(item => item.Key == elementName);
            }
        }
    }

    public class ApiKeyElement : ConfigurationElement
    {
        [ConfigurationProperty("Key", IsKey = true, IsRequired = true)]
        public string Key
        {
            get { return (string)base["Key"]; }
            set { base["Key"] = value; }
        }

        [ConfigurationProperty("RequestLimit", IsRequired = true)]
        public int RequestLimits
        {
            get { return (int)base["RequestLimit"]; }
            set { base["RequestLimit"] = value; }
        }
    }
}