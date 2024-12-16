using LibraryNew.Data;
using LibraryNew.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryNew.Services
{
    public class QuoteGenerate : IQuoteGenerate
    {
        //private readonly LibraryDbContext _context;
        private static readonly Random _random = new Random();

        public async Task<QuoteSet> GetRandomQuote(IQueryable<QuoteSet> quotes)
        {
            var filteredQuotes = quotes.Where(q => q.Quote.Length <= 200);
            var count = await filteredQuotes.CountAsync();
            var randomIndex = _random.Next(0, count);
            if (count == 0)
            {
                filteredQuotes = quotes.Where(q => q.Quote.Length <= 250);
                count = await filteredQuotes.CountAsync();
            }
            return await filteredQuotes.Skip(randomIndex).Take(1).FirstOrDefaultAsync();
        }
    }
}
