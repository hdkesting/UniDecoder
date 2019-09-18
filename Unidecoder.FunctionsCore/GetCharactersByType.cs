// <copyright file="GetCharactersByType.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Unidecoder.Functions.Services;

namespace Unidecoder.Functions
{
    /// <summary>
    /// Get characters by their block or category.
    /// </summary>
    public class GetCharactersByType
    {
        private readonly UnicodeService service;

        public GetCharactersByType(UnicodeService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Runs the specified request.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The log.</param>
        /// <returns>A list of characters.</returns>
        [FunctionName("GetCharactersByType")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // parse query parameter
            string block = req.Query["block"];
            string category = req.Query["category"];

            List<Model.CodepointInfo> list = null;

            if (!string.IsNullOrWhiteSpace(block))
            {
                list = this.service.GetCharactersOfBlock(block);
            }
            else if (!string.IsNullOrWhiteSpace(category))
            {
                list = this.service.GetCharactersOfCategory(category);
            }

            if (list == null || !list.Any())
            {
                return new BadRequestObjectResult("Please pass a block or category on the query string.");
            }

            return new JsonResult(list, Support.Settings.JsonSerializerSettings);
        }
    }
}
