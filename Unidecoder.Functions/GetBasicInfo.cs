// <copyright file="GetBasicInfo.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Functions
{
    using System.Net;
    using System.Net.Http;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Gets basic information: blocks and categories, character count.
    /// </summary>
    public static class GetBasicInfo
    {
        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A response message.</returns>
        [FunctionName("GetBasicInfo")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req,
            ILogger log)
        {
            log.Log(LogLevel.Information, "C# HTTP trigger function processed a request for basic information.");

            var svc = new Services.UnicodeService();

            var result = new Model.BasicInfo
            {
                Blocks = svc.GetAllBlocks(),
                Categories = svc.GetAllCategories(),
                CharCount = svc.GetTotalCharacterCount(),
                UnicodeVersion = svc.GetUnicodeVersion().ToString(3),
            };

            return req.CreateResponse(HttpStatusCode.OK, result, Support.Settings.JsonFormatter);
        }
    }
}
