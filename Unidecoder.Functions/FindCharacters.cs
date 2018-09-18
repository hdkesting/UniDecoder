// <copyright file="FindCharacters.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Functions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;
    using Unidecoder.Functions.Services;

    /// <summary>
    /// Find characters by id or name.
    /// </summary>
    public static class FindCharacters
    {
        private const string ParameterName = "search";

        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A list of characters.</returns>
        [FunctionName("FindCharacters")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req,
            TraceWriter log)
        {
            // parse query parameter
            string searchText = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Equals(q.Key, ParameterName, System.StringComparison.OrdinalIgnoreCase))
                .Value;

            log.Info($"C# HTTP trigger function processing a request to find '{searchText}'.");

            if (string.IsNullOrWhiteSpace(searchText))
            {
                return req.CreateResponse(HttpStatusCode.NoContent);
            }

            var svc = new UnicodeService();
            List<Model.CodepointInfo> list;

            // try and parse the search value as an integer or hex value.
            int? codepoint = ParseAsDecimal(searchText) ?? ParseAsHex(searchText);

            if (codepoint.HasValue)
            {
                list = svc.FindAroundValue(codepoint.Value);
            }
            else
            {
                // regular search
                list = svc.FindByName(searchText);
            }

            return req.CreateResponse(HttpStatusCode.OK, list, Support.Settings.JsonFormatter);
        }

        private static int? ParseAsDecimal(string value)
        {
            return int.TryParse(value, out int res) ? res : default(int?);
        }

        private static int? ParseAsHex(string value)
        {
            // ignore some usual prefixes
            if (value.StartsWith("0x", System.StringComparison.OrdinalIgnoreCase) || value.StartsWith("U+", System.StringComparison.OrdinalIgnoreCase))
            {
                value = value.Substring(2);
            }

            return int.TryParse(
                    value,
                    System.Globalization.NumberStyles.HexNumber,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out int code)
                ? code
                : default(int?);
        }
    }
}
