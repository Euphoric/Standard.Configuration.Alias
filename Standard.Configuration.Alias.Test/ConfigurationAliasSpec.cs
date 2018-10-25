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

      [Fact]
      public void Can_retrieve_through_section()
      {
         var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               {"section1:A", "3"}
            })
            .AddAlias(new Dictionary<string, string>())
            .Build();

         Assert.Equal("3", config.GetSection("section1")["A"]);
         Assert.Null(config.GetSection("section1")["B"]);
      }

      [Fact]
      public void Can_retrieve_aliased_setting_through_section()
      {
         var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               {"section1:A", "7"}
            })
            .AddAlias(new Dictionary<string, string>
            {
               { "section1:B", "section1:A" }
            })
            .Build();

         Assert.Equal("7", config.GetSection("section1")["B"]);
         Assert.Null(config.GetSection("section1")["C"]);
      }

      [Fact]
      public void Alias_not_used_when_value_exists_through_section()
      {
         var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               {"section1:A", "9"},
               {"section1:B", "8"}
            })
            .AddAlias(new Dictionary<string, string>
            {
               { "section1:B", "section1:A" }
            })
            .Build();

         Assert.Equal("8", config.GetSection("section1")["B"]);
         Assert.Null(config.GetSection("section1")["C"]);
      }

      [Fact]
      public void Alias_works_across_sections()
      {
         var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
               {"section1:A", "17"}
            })
            .AddAlias(new Dictionary<string, string>
            {
               { "section2:B", "section1:A" }
            })
            .Build();

         Assert.Equal("17", config.GetSection("section2")["B"]);
      }
   }
}
