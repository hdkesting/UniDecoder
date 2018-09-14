// <copyright file="GetAllCategories.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Functions
{
    using System.Net;
    using System.Net.Http;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;
    using Unidecoder.Functions.Services;

    /// <summary>
    /// Gets all block names and ids.
    /// </summary>
    public static class GetAllCategories
    {
        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="log">The log.</param>
        /// <returns>A list of names and indexes.</returns>
        [FunctionName("GetAllCategories")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"{nameof(GetAllCategories)} processing a request.");

            var svc = new UnicodeService();
            var result = svc.GetAllCategories();

            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
