using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public List<BasicInfo> GetAllCharacters()
        {
            var list = this.service.GetAllCharacters();
            return list;
        }

        [HttpGet("version")]
        public string GetVersion()
        {
            return this.service.GetUnicodeVersion().ToString();
        }
    }
}