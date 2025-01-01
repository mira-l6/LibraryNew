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
    public class ChatBotController : Controller
    {

        //private readonly IGroqClient _groqClient;
        private readonly HttpClient _httpClient;
        private readonly LibraryDbContext _context;

        public ChatBotController(LibraryDbContext context, HttpClient httpClient)
        {
            //_groqClient = groqClient;
            _context = context;         
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ChatRequest chatRequest)
        {
            var aiService = new GeminiService(_httpClient);
            string response = await aiService.GenerateResponse(chatRequest.Question, "AIzaSyBBs01z2sFbCgMAubIirXrW6D5REM6-SsA");
            Console.WriteLine(response);

            return Json(new { response });
        }

        //[HttpPost]
        //public async Task<IActionResult> Index([FromBody] ChatRequest chatRequest)
        //{



        //    //ViewBag.Answer = answer;
        //    //return Json(new { answer = answer });

        //}
    }
}
