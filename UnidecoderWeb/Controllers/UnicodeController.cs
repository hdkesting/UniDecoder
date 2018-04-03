using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using UnidecoderWeb.Services;
using UniDecoderWeb.Models;
using Microsoft.AspNetCore.Hosting;

namespace UnidecoderWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Unicode")]
    public class UnicodeController : Controller
    {
        private readonly UnicodeService service;
        private readonly IMemoryCache memoryCache;
        private readonly IHostingEnvironment environment;

        public UnicodeController(UnicodeService service, IMemoryCache memoryCache, IHostingEnvironment environment)
        {
            this.service = service;
            this.memoryCache = memoryCache;
            this.environment = environment;
        }

        [HttpGet("blocks")]
        public List<string> GetUnicodeBlocks()
        {
            var list = this.service.GetUnicodeBlockNames();
            return list;
        }

        [HttpGet("characters")]
        public IActionResult GetAllCharacters()
        {
            var path = System.IO.Path.Combine(this.environment.WebRootPath, "characters.json");

            if (System.IO.File.Exists(path))
            {
                this.Redirect("/characters.json");
            }

            lock (memoryCache)
            {
                JObject result;

                result = this.memoryCache.Get<JObject>(nameof(GetAllCharacters));

                if (result == null)
                {
                    var list = this.service.GetAllCharacters();

                    result = new JObject();
                    foreach (var c in list.Where(it => it.Category.IndexOf("Private Use") == -1))
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
                }

                return this.Content(result.ToString(), "application/json");
            }
        }

        [HttpGet("version")]
        public string GetVersion()
        {
            return this.service.GetUnicodeVersion().ToString();
        }
    }
}