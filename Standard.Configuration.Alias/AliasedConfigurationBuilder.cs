using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.Configuration.Alias
{
   public class AliasedConfigurationBuilder : IConfigurationBuilder
   {
      private readonly IConfigurationBuilder _builder;
      private readonly Dictionary<string, string> _aliases;

      public AliasedConfigurationBuilder(IConfigurationBuilder builder, Dictionary<string, string> aliases)
      {
         _builder = builder;
         _aliases = aliases;
      }

      public IConfigurationBuilder Add(IConfigurationSource source)
      {
         throw new NotSupportedException("Alias should be last source.");
      }

      public IConfigurationRoot Build()
      {
         return new ConfigurationRootAlias(_builder.Build(), _aliases);
      }

      public IDictionary<string, object> Properties => _builder.Properties;
      public IList<IConfigurationSource> Sources => _builder.Sources;
   }
}