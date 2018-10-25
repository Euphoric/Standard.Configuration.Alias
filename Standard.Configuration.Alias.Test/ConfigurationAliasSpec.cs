using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Standard.Configuration.Alias.Test
{
   public class ConfigurationAliasSpec
   {
      [Fact]
      public void Can_retrieve_normal_setting()
      {
         var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               {"A", "3"}
            })
            .AddAlias(new Dictionary<string, string>())
            .Build();

         Assert.Equal("3", config["A"]);
         Assert.Null(config["B"]);
      }

      [Fact]
      public void Can_retrieve_aliased_setting()
      {
         var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               {"A", "7"}
            })
            .AddAlias(new Dictionary<string, string>
            {
               { "B", "A" }
            })
            .Build();

         Assert.Equal("7", config["B"]);
         Assert.Null(config["C"]);
      }

      [Fact]
      public void Alias_not_used_when_value_exists()
      {
         var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               {"A", "9"},
               {"B", "8"}
            })
            .AddAlias(new Dictionary<string, string>
            {
               { "B", "A" }
            })
            .Build();

         Assert.Equal("8", config["B"]);
         Assert.Null(config["C"]);
      }
   }
}
