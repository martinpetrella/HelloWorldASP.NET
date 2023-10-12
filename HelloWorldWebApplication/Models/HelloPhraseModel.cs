namespace HelloWorldWebApplication.Models
{
    /// <summary>
    /// Container class containing a phrase in a specific language
    /// </summary>
    public class HelloPhraseModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public HelloPhraseModel() { }

        /// <summary>
        /// Paramaterized constructor
        /// </summary>
        /// <param name="language">The language</param>
        /// <param name="phrase">The phrase</param>
        public HelloPhraseModel(string language, string phrase)
        {
            Language = language;
            Phrase = phrase;
        }

        /// <summary>
        /// Language for the associated phrase
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Phrase in the associated language
        /// </summary>
        public string Phrase { get; set; }
    }
}