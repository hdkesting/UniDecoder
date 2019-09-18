// <copyright file="ListCharacters.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Unidecoder.Functions.Services;

namespace Unidecoder.Functions
{
    /// <summary>
    /// List characters in the supplied string.
    /// </summary>
    public class ListCharacters
    {
        private const string ParameterName = "text";
        private readonly UnicodeService service;

        public ListCharacters(UnicodeService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A list of character definitions.</returns>
        [FunctionName("ListCharacters")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            // parse query parameter
            string text = req.Query[ParameterName];

            log.LogInformation($"{nameof(ListCharacters)} processing a request for '{text}'.");

            if (string.IsNullOrEmpty(text))
            {
                return new NoContentResult();
            }

            var list = this.service.ListCharacters(text);

            return new JsonResult(list, Support.Settings.JsonSerializerSettings);
        }
    }
}
