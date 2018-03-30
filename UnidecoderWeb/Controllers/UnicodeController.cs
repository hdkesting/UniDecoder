using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache memoryCache;

        public UnicodeController(UnicodeService service, IMemoryCache memoryCache)
        {
            this.service = service;
            this.memoryCache = memoryCache;
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
            lock (memoryCache)
            {
                JObject result;

                result = this.memoryCache.Get<JObject>(nameof(GetAllCharacters));

                if (result != null)
                {
                    return result;
                }

                var list = this.service.GetAllCharacters();

                result = new JObject();
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

                this.memoryCache.Set(nameof(GetAllCharacters), result);

                return result;
            }
        }

        [HttpGet("version")]
        public string GetVersion()
        {
            return this.service.GetUnicodeVersion().ToString();
        }
    }
}