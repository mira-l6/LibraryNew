using LibraryNew.Models;

namespace LibraryNew.Services
{
    public interface IQuoteGenerate
    {
        Task<QuoteSet> GetRandomQuote(IQueryable<QuoteSet> quotes);
    }
}
