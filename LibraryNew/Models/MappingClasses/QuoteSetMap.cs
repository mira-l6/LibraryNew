using CsvHelper.Configuration;

namespace LibraryNew.Models.MappingClasses
{
    public class QuoteSetMap : ClassMap<QuoteSet>
    {
        public QuoteSetMap() 
        {
            Map(q => q.Quote).Name("quote");
            Map(q => q.Author).Name("author");
            Map(q => q.Category).Name("category");
        }
    }
}
