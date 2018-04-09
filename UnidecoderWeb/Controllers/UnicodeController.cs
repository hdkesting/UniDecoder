using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using UnidecoderWeb.Services;
using Microsoft.AspNetCore.Hosting;

namespace UnidecoderWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/Unicode")]
    public class UnicodeController : Controller
    {
        private static readonly object padlock = new object();

        private readonly UnicodeService service;
        private readonly IHostingEnvironment environment;

        public UnicodeController(UnicodeService service, IHostingEnvironment environment)
        {
            this.service = service;
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
            const string charfile = "characters.json";
            var path = System.IO.Path.Combine(this.environment.WebRootPath, charfile);

            lock (padlock)
            {
                if (!System.IO.File.Exists(path))
                {
                    var list = this.service.GetAllCharacters();

                    var catlist = new Dictionary<int, string>();
                    var blocklist = new List<string>();

                    JObject charlist = new JObject();
                    // filtering out "Private Use"characters goes from 40 MB to 22 MB
                    // category and blocks to separte list reduces to 12 MB (not indented)
                    foreach (var c in list.Where(it => it.Category.IndexOf("Private Use") == -1 && it.Name.IndexOf("Surrogate-") == -1))
                    {
                        var cp = c.Codepoint;
                        if (!catlist.ContainsKey(c.CategoryId))
                        {
                            catlist.Add(c.CategoryId, c.Category);
                        }
                        int blockindex = GetIndex(blocklist, c.Block);

                        var obj = new JObject
                                        {
                                            new JProperty("name", c.Name),
                                            new JProperty("category", c.CategoryId),
                                            new JProperty("block", blockindex),
                                            new JProperty("hex", c.CodepointHex),
                                        };
                        charlist.Add(new JProperty(cp.ToString(), obj));
                    }

                    JObject result = new JObject
                    {
                        new JProperty("characters", charlist),
                        new JProperty("categories", GetCategoryList(catlist)),
                        new JProperty("blocks", new JArray(blocklist))
                    };

                    //return this.Content(result.ToString(), "application/json");
                    System.IO.File.WriteAllText(path, result.ToString(Newtonsoft.Json.Formatting.None));
                }
            }

            return this.RedirectPermanent("/" + charfile);
        }

        [HttpGet("version")]
        public string GetVersion()
        {
            return this.service.GetUnicodeVersion().ToString();
        }

        private int GetIndex(List<string> list, string name)
        {
            var idx = list.IndexOf(name);
            if (idx < 0)
            {
                list.Add(name);
                idx = list.Count - 1;
            }

            return idx;
        }

        private JObject GetCategoryList(IDictionary<int, string> catlist)
        {
            var res = new JObject();
            foreach (var kvp in catlist)
            {
                res.Add(new JProperty(kvp.Key.ToString(), kvp.Value));
            }

            return res;
        }
    }
}