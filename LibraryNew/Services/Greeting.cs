namespace LibraryNew.Services
{
    public class Greeting : IGreeting
    {
        public string GetGreeted()
        {
            string[] greetings = {
                "Welcome to our website",
                "Thank you for visiting our site",
                "We are pleased to have you here",
                "It’s a pleasure to welcome you to our site",
                "We appreciate your visit",
                "Thank you for joining us today",
                "It’s an honor to welcome you",
                "We’re delighted to have you with us",
                "Welcome aboard",
                "We’re glad you’re here",
                "Thank you for taking the time to visit us",
                "We’re happy to see you on our site",
                "Welcome, and thank you for your visit",
                "We extend a warm welcome to you",
                "We hope you find everything you need here"
            };
            Random r = new Random();
            return greetings[r.Next(0, greetings.Length)];
        }
    }
}
