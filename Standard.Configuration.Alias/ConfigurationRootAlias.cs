using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Microsoft.Extensions.Configuration.Alias
{
   class ConfigurationRootAlias : IConfigurationRoot
   {
      private readonly IConfigurationRoot _configurationRoot;
      private readonly Dictionary<string, string> _aliases;

      public ConfigurationRootAlias(IConfigurationRoot configurationRoot, Dictionary<string, string> aliases)
      {
         _configurationRoot = configurationRoot;
         _aliases = aliases;
      }

      public IConfigurationSection GetSection(string key) => new ConfigurationSection(this, key);

      public IEnumerable<IConfigurationSection> GetChildren()
      {
         return GetChildrenImplementation(null);
      }

      public IChangeToken GetReloadToken()
      {
         return _configurationRoot.GetReloadToken();
      }

      public string this[string key]
      {
         get => GetValue(key);
         set => _configurationRoot[key] = value;
      }

      private string GetValue(string key)
      {
         var val = _configurationRoot[key];
         if (val != null)
            return val;

         if (_aliases.TryGetValue(key, out var aliasedKey))
         {
            return _configurationRoot[aliasedKey];
         }

         return null;
      }
      
      public void Reload()
      {
         _configurationRoot.Reload();
      }

      public IEnumerable<IConfigurationProvider> Providers => _configurationRoot.Providers;

      internal IEnumerable<IConfigurationSection> GetChildrenImplementation(string path)
      {
         throw new NotImplementedException();
      }
   }
}