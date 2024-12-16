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
using EFCore.BulkExtensions;

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

        [HttpGet]
        public async Task<IActionResult> GetQuote()
        {
            IQueryable<QuoteSet> quotes = _context.QuoteSets;

            var randomQuote = await _quote.GetRandomQuote(quotes);

            if (randomQuote == null)
            {
                return Json(new { message = "No quotes found" });
            }

            //ViewBag.Quote = randomQuote.Quote;
            //ViewBag.Author = randomQuote.Author;

            return Json(new { randomQuote });

            //PROCESS OF ADDING EVERY QUOTE TO THE DATABASE (нямам нужда от него тъй като вече са добавени и
            //няма нужда при всяко пускане на приложението да се инициализират тези функции)

            //public IActionResult Index()
            //{

            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/quoteArchive/quotes.csv");
            //    var quotes = LoadQuotes(filePath);
            //    SaveQuotesToDatabaseBulk(quotes);

            //    return View();
            //}

            //public async Task SaveQuotesToDatabaseBulk(List<QuoteSet> quotes)
            //{
            //    const int batchSize = 1000; // Number of records to process in each batch
            //    int alreadyInsertedCount = _context.QuoteSets.Count(); // Check existing records

            //    for (int i = alreadyInsertedCount; i < quotes.Count; i += batchSize)
            //    {
            //        var batch = quotes.Skip(i).Take(batchSize).ToList();
            //        await _context.BulkInsertAsync(batch);
            //    }
            //}

            //public List<QuoteSet> LoadQuotes(string filePath)
            //{
            //    //за да работи трябва да се конфигурира по-подробно, за да не засича грешки
            //    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            //    {
            //        HasHeaderRecord = true,         // първият ред винаги са заглавните имена
            //        MissingFieldFound = null,       // игнорират се празни места
            //        HeaderValidated = null,         // игнориране на валидацията за заглавните имена
            //        Delimiter = ",",                // разделителят да е запетая
            //        TrimOptions = TrimOptions.Trim, // трие бели празни места
            //        Quote = '"',                    // винаги двойни кавички
            //        AllowComments = false,          // не са позволени коментари
            //    };

            //    using (var reader = new StreamReader(filePath))
            //    using (var csv = new CsvReader(reader, config))
            //    {
            //        // Register the custom mapping
            //        csv.Context.RegisterClassMap<QuoteSetMap>();

            //        // Read and return the records as a list
            //        return csv.GetRecords<QuoteSet>().ToList();
            //    }

            //}
        }
    }
}
