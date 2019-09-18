// <copyright file="FindCharacters.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Unidecoder.Functions.Services;

namespace Unidecoder.Functions
{
    /// <summary>
    /// Find characters by id or name.
    /// </summary>
    public class FindCharacters
    {
        private const string ParameterName = "search";
        private readonly UnicodeService service;

        public FindCharacters(UnicodeService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A list of characters.</returns>
        [FunctionName("FindCharacters")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            // parse query parameter
            string searchText = req.Query[ParameterName];

            log.LogInformation($"C# HTTP trigger function processing a request to find '{searchText}'.");

            if (string.IsNullOrWhiteSpace(searchText))
            {
                return new NoContentResult();
            }

            List<Model.CodepointInfo> list;

            // try and parse the search value as an integer or hex value.
            int? codepoint = ParseAsDecimal(searchText) ?? ParseAsHex(searchText);

            if (codepoint.HasValue)
            {
                list = this.service.FindAroundValue(codepoint.Value);
            }
            else
            {
                // regular search
                list = this.service.FindByName(searchText);
            }

            return new JsonResult(list, Support.Settings.JsonSerializerSettings);
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
