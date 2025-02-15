namespace LibraryNew.Services.AuthorSetHelper
{
    public interface IAuthorHelper
    {
        static string GetFirstName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return string.Empty;
            return fullName.Split(' ').FirstOrDefault() ?? string.Empty;
        }

        // Helper to extract last name
        static string GetLastName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return string.Empty;
            var parts = fullName.Split(' ');
            return parts.Length > 1 ? parts.LastOrDefault() : string.Empty;
        }

        // Helper to parse year from "Born" field (e.g., "1960-01-01" -> 1960)
        static int ParseYear(string bornDate)
        {
            if (DateTime.TryParse(bornDate, out var date))
            {
                return date.Year;
            }
            return 0; // Default to 0 if no valid date
        }
    }
}
