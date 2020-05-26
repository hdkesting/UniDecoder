// <copyright file="ListCharacters.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Functions
{
    using System.Net;
    using System.Net.Http;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    using Unidecoder.Functions.Services;

    /// <summary>
    /// List characters in the supplied string.
    /// </summary>
    public static class ListCharacters
    {
        private const string ParameterName = "text";

        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A list of character definitions.</returns>
        [FunctionName("ListCharacters")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req,
            ILogger log)
        {
            // parse query parameter
            var qs = req.RequestUri.ParseQueryString();
            string text = qs.Get(ParameterName);

            log.LogInformation($"{nameof(ListCharacters)} processing a request for '{{text}}'.", text);

            if (string.IsNullOrEmpty(text))
            {
                return req.CreateResponse(HttpStatusCode.NoContent);
            }

            var svc = new UnicodeService();
            var list = svc.ListCharacters(text);

            return req.CreateResponse(HttpStatusCode.OK, list, Support.Settings.JsonFormatter);
        }
    }
}
