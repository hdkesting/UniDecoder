// <copyright file="GetCharactersByType.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace Unidecoder.Functions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;
    using Unidecoder.Functions.Services;

    /// <summary>
    /// Get characters by their block or category.
    /// </summary>
    public static class GetCharactersByType
    {
        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A list of characters.</returns>
        [FunctionName("GetCharactersByType")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string block = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "block", true) == 0)
                .Value;
            string category = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "category", true) == 0)
                .Value;

            var svc = new UnicodeService();
            List<Model.CodepointInfo> list = null;

            if (!string.IsNullOrWhiteSpace(block))
            {
                list = svc.GetCharactersOfBlock(block);
            }
            else if (!string.IsNullOrWhiteSpace(category))
            {
                list = svc.GetCharactersOfCategory(category);
            }

            return list == null || !list.Any()
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a block or category on the query string.")
                : req.CreateResponse(HttpStatusCode.OK, list, Support.Settings.JsonFormatter);
        }
    }
}
