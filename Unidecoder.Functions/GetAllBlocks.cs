// <copyright file="GetAllBlocks.cs" company="Hans Kesting">
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
    /// Get all unicode block names.
    /// </summary>
    public static class GetAllBlocks
    {
        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A list of names and indexes.</returns>
        [FunctionName("GetAllBlocks")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"{nameof(GetAllBlocks)} processing a request.");

            var svc = new UnicodeService();
            var result = svc.GetAllBlocks();

            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
