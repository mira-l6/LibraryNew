using CsvHelper.Configuration;

namespace LibraryNew.Models.MappingClasses
{
    public class CsvAuthorMap : ClassMap<CsvAuthor>
    {
        public CsvAuthorMap()
        {
            Map(a => a.Name).Name("name");
            Map(a => a.Born).Name("born");
            Map(a => a.FanCount).Name("fan_count");
        }
    }
}
