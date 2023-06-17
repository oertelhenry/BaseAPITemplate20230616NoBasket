using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Core.Serialization
{
    public static class JsonConfiguration
    {
        public static JsonMergeSettings DefaultMergeSettings => new JsonMergeSettings
        {
            MergeArrayHandling = MergeArrayHandling.Union,
            MergeNullValueHandling = MergeNullValueHandling.Ignore,
            PropertyNameComparison = StringComparison.InvariantCultureIgnoreCase
        };

        public static JsonSerializerSettings DefaultSerializerSettings => GetStandardSerializerSettings(true);

        public static JsonMergeSettings IncludeNullMergeSettings => new JsonMergeSettings
        {
            MergeArrayHandling = MergeArrayHandling.Union,
            MergeNullValueHandling = MergeNullValueHandling.Merge,
            PropertyNameComparison = StringComparison.InvariantCultureIgnoreCase
        };

        public static Action<MvcNewtonsoftJsonOptions> JsonOptionsSetup => (options) =>
                                {
                                    JsonConvert.DefaultSettings = () => GetStandardSerializerSettings(true);

                                    options.UseCamelCasing(true);

                                    //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                                    var resolver = new CamelCasePropertyNamesContractResolver();

                                    resolver.NamingStrategy.ProcessDictionaryKeys = true;

                                    options.SerializerSettings.ContractResolver = resolver;

                                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                                    options.SerializerSettings.Converters.Add(new JsonInt32Converter());

                                    options.AllowInputFormatterExceptionMessages = true;
                                };

        public static JsonSerializerSettings GetExpandObjectSerializerSettings(bool camel = true)
        {
            var resolver = camel ? new CamelCasePropertyNamesContractResolver() : new DefaultContractResolver();
            resolver.NamingStrategy.ProcessDictionaryKeys = true;

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = resolver,
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(),
                    new ExpandoObjectConverter(),
                    new JsonInt32Converter()
                }
            };

            return settings;
        }

        public static JsonSerializerSettings GetNoIgnoreSerializerSettings(bool camel = true)
        {
            DefaultContractResolver resolver = camel ? (DefaultContractResolver)new CamelCaseNoJsonIgnoreContractResolver() : new NoJsonIgnoreContractResolver();
            resolver.NamingStrategy.ProcessDictionaryKeys = true;

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = resolver,
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(),
                    new JsonInt32Converter()
                }
            };

            return settings;
        }

        public static JsonSerializerSettings GetPopulationSerializerSettings(bool camel = true)
        {
            DefaultContractResolver resolver = camel ? (DefaultContractResolver)new CamelCasePopulationJsonIgnoreContractResolver() : new PopulationJsonIgnoreContractResolver();
            resolver.NamingStrategy.ProcessDictionaryKeys = true;

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = resolver,
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(),
                    new JsonInt32Converter()
                }
            };

            return settings;
        }

        public static JsonSerializerSettings GetStandardSerializerSettings(bool camel = true)
        {
            var resolver = camel ? new CamelCasePropertyNamesContractResolver() : new DefaultContractResolver();
            resolver.NamingStrategy.ProcessDictionaryKeys = true;

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = resolver,
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(),
                    new JsonInt32Converter()
                }
            };

            return settings;
        }
    }
}