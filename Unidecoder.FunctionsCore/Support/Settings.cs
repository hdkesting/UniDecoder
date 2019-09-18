// <copyright file="Settings.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Unidecoder.Functions.Support
{
    /// <summary>
    /// Global settings.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Gets the json serializer settings.
        /// </summary>
        /// <value>
        /// The json serializer settings.
        /// </value>
        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
        };

        /// <summary>
        /// Gets the json formatter.
        /// </summary>
        /// <value>
        /// The json formatter.
        /// </value>
        public static MediaTypeFormatter JsonFormatter => new JsonMediaTypeFormatter
        {
            SerializerSettings = JsonSerializerSettings,
            UseDataContractJsonSerializer = false,
        };
    }
}
