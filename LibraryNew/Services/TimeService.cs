namespace LibraryNew.Services
{
    public class TimeService : ITimeNow
    {
        public string GetTimeNow()
        {
            return DateTime.Now.ToString("HH::mm::ss tt");
        }
    }
}
