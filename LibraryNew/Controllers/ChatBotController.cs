using GroqSharp;
using LibraryNew.Data;
using LibraryNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using GroqSharp.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace LibraryNew.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatBotController : BaseController
    {

        private readonly IGroqClient _groqClient;
        private readonly LibraryDbContext _context;

        public ChatBotController(IGroqClient groqClient, LibraryDbContext context)
        {
            _groqClient = groqClient;
            _context = context;         
        }

        //[HttpPost]
        //public async Task<IActionResult> Index([FromBody] ChatRequest chatRequest)
        //{

            

        //    //ViewBag.Answer = answer;
        //    //return Json(new { answer = answer });

        //}
    }
}
