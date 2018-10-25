using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Alias;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration
{
   /// <summary>
   /// Adds aliases to configuration.
   /// </summary>
   public static class AliasedConfigurationBuilderExtension
   {
      public static IConfigurationBuilder AddAlias(
         this IConfigurationBuilder builder,
         Dictionary<string, string> aliases)
      {
         return new AliasedConfigurationBuilder(builder, aliases);
      }
   }
}