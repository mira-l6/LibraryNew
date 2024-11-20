namespace LibraryNew.Services
{
    public class QuoteGenerate : IQuoteGenerate
    {
        public string GetRandomQuote()
        {
            string[] quotes = {};
            Random r = new Random();
            return quotes[r.Next(0, quotes.Length)];
        }
    }
}
