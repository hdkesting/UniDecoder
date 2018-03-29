using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using UnidecoderWeb.Services;
using UniDecoderWeb.Models;

namespace UnidecoderWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Unicode")]
    public class UnicodeController : Controller
    {
        private readonly UnicodeService service;

        public UnicodeController(UnicodeService service)
        {
            this.service = service;
        }

        [HttpGet("blocks")]
        public List<string> GetUnicodeBlocks()
        {
            var list = this.service.GetUnicodeBlockNames();
            return list;
        }

        [HttpGet("characters")]
        public JObject GetAllCharacters()
        {
            var list = this.service.GetAllCharacters();

            var result = new JObject();
            foreach (var c in list)
            {
                var cp = c.Codepoint;
                var obj = new JObject
                {
                    new JProperty("name", c.Name),
                    new JProperty("category", c.Category),
                    new JProperty("block", c.Block),
                    new JProperty("hex", c.CodepointHex),
                };
                result.Add(new JProperty(cp.ToString(), obj));
            }

            return result;
        }

        [HttpGet("version")]
        public string GetVersion()
        {
            return this.service.GetUnicodeVersion().ToString();
        }
    }
}