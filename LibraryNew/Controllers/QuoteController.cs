using LibraryNew.Services;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using NuGet.Protocol;
using LibraryNew.Models.MappingClasses;
using LibraryNew.Models;
using LibraryNew.Models.MappingClasses;
using LibraryNew.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryNew.Controllers
{
    public class QuoteController : Controller
    {

        private readonly IQuoteGenerate _quote;
        private readonly LibraryDbContext _context;
        public QuoteController(IQuoteGenerate quote, LibraryDbContext context)
        {
            _quote = quote;
            _context = context;
        }
        
        public IActionResult Index()
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/quoteArchive/quotes.csv");
            var quotes = LoadQuotes(filePath);
            
            return View();
        }

        public List<QuoteSet> LoadQuotes(string filePath)
        {
            //за да работи трябва да се конфигурира по-подробно, за да не засича грешки
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,         // първият ред винаги са заглавните имена
                MissingFieldFound = null,       // игнорират се празни места
                HeaderValidated = null,         // игнориране на валидацията за заглавните имена
                Delimiter = ",",                // разделителят да е запетая
                TrimOptions = TrimOptions.Trim, // трие бели празни места
                Quote = '"',                    // винаги двойни кавички
                AllowComments = false,          // не са позволени коментари
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                // Register the custom mapping
                csv.Context.RegisterClassMap<QuoteSetMap>();

                // Read and return the records as a list
                return csv.GetRecords<QuoteSet>().ToList();
            }

        }
    }
}
