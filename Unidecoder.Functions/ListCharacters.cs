// <copyright file="ListCharacters.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Functions
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;
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
            TraceWriter log)
        {
            // parse query parameter
            string text = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Equals(q.Key, ParameterName, System.StringComparison.OrdinalIgnoreCase))
                .Value;

            log.Info($"{nameof(ListCharacters)} processing a request for '{text}'.");

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
