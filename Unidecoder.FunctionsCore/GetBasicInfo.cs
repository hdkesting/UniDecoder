// <copyright file="GetBasicInfo.cs" company="Hans Kesting">
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
    /// Gets basic information: blocks and categories, character count.
    /// </summary>
    public class GetBasicInfo
    {
        private readonly UnicodeService service;

        public GetBasicInfo(UnicodeService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A response message.</returns>
        [FunctionName("GetBasicInfo")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request for basic information.");

            var result = new Model.BasicInfo
            {
                Blocks = this.service.GetAllBlocks(),
                Categories = this.service.GetAllCategories(),
                CharCount = this.service.GetTotalCharacterCount(),
                UnicodeVersion = this.service.GetUnicodeVersion().ToString(3),
            };

            return new JsonResult(result, Support.Settings.JsonSerializerSettings);
        }
    }
}
