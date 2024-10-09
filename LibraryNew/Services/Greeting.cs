namespace LibraryNew.Services
{
    public class Greeting : IGreeting
    {
        public string GetGreeted()
        {
            string[] greetings = { "Welcome!", "Hello!", "Good to see you!"};
            Random r = new Random();
            return greetings[r.Next(0, greetings.Length)];
        }
    }
}
