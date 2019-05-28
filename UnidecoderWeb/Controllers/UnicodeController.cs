// <copyright file="UnicodeController.cs" company="Hans Kesting">
// Copyright (c) Hans Kesting. All rights reserved.
// </copyright>

namespace UnidecoderWeb.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using UnidecoderWeb.Services;

    /// <summary>
    /// Controller for unicode related info.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Unicode")]
    public class UnicodeController : Controller
    {
        private static readonly object Padlock = new object();

        private readonly UnicodeService service;
        private readonly IHostingEnvironment environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="environment">The environment.</param>
        public UnicodeController(UnicodeService service, IHostingEnvironment environment)
        {
            this.service = service;
            this.environment = environment;
        }

        /// <summary>
        /// Gets all unicode blocks.
        /// </summary>
        /// <returns>A list of block names.</returns>
        [HttpGet("blocks")]
        public List<string> GetUnicodeBlocks()
        {
            var list = this.service.GetUnicodeBlockNames();
            return list;
        }

        /// <summary>
        /// Gets all characters.
        /// </summary>
        /// <param name="v">The unicode version.</param>
        /// <returns>
        /// A redirect to the file containing the characters.
        /// </returns>
        [HttpGet("characters")]
        public IActionResult GetAllCharacters(string v = null)
        {
            string version = v;

            const string charfile = "characters.json";
            var path = Path.Combine(this.environment.WebRootPath, charfile);

            lock (Padlock)
            {
                if (!System.IO.File.Exists(path))
                {
                    var list = this.service.GetAllCharacters();

                    var catlist = new Dictionary<int, string>();
                    var blocklist = new List<string>();

                    JObject charlist = new JObject();

                    /* filtering out "Private Use" characters reduces output from 40 MB to 22 MB
                     * category and blocks to separate list reduces to 12 MB (not indented)
                     */
                    foreach (var c in list.Where(it =>
                        it.Codepoint >= 32 &&
                        it.Category != "Surrogate" &&
                        it.Category.IndexOf("Private Use") == -1))
                    {
                        var cp = c.Codepoint;
                        if (!catlist.ContainsKey(c.CategoryId))
                        {
                            catlist.Add(c.CategoryId, c.Category);
                        }

                        int blockindex = this.GetIndex(blocklist, c.Block);

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
                        new JProperty("categories", this.GetCategoryList(catlist)),
                        new JProperty("blocks", new JArray(blocklist)),
                    };

                    // write both as plain (JSON) text and as gzipped version
                    var formattedJson = result.ToString(Newtonsoft.Json.Formatting.None);
                    System.IO.File.WriteAllText(path, formattedJson);
                    using (var stream = System.IO.File.OpenWrite(path + ".gz"))
                    using (var zip = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionLevel.Optimal))
                    using (var sw = new StreamWriter(zip))
                    {
                        sw.Write(formattedJson);
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(version))
            {
                return this.RedirectPermanent("/" + charfile);
            }

            return this.RedirectPermanent("/" + charfile + "?v=" + version);
        }

        /// <summary>
        /// Gets the current unicode version.
        /// </summary>
        /// <returns>The current unicode version.</returns>
        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            return this.Content(this.service.GetUnicodeVersion().ToString());
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