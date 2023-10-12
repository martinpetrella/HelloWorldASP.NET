using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HelloWorldWebApplication.Models
{
    /// <summary>
    /// Model to retrieve and maintain a list of phrases in various languages
    /// </summary>
    public class HelloPhrasesModel
    {
        static string _defaultLanguage = "English";

        /// <summary>
        /// Initial list of phrases in various languages
        /// </summary>
        private static Dictionary<string, string> phrases = new Dictionary<string, string>()
        {
            { "English", "Hello World!" },
            { "Spanish", "¡Hola Mundo!" },
            { "French", "Bonjour Le Monde!" },
            { "Italian", "Ciao Mondo!" },
            { "German",  "Hallo Welt!" }
        };

        /// <summary>
        /// Get or Set the default language
        /// </summary>
        public string DefaultLanguage
        {
            get
            {
                return _defaultLanguage;
            }

            set
            {
                _defaultLanguage = value;
            }
        }

        /// <summary>
        /// Get or Set the default phrase for the default language
        /// </summary>
        public string DefaultPhrase
        {
            get
            {
                string result = string.Empty;

                if (string.IsNullOrEmpty(DefaultLanguage))
                {
                    result = "No default language specified.";
                }
                else
                {
                    if (phrases.Keys.Contains(DefaultLanguage))
                    {
                        result = phrases[DefaultLanguage];
                    }
                    else
                    {
                        result = string.Format("Language '{0}' is not supported.", DefaultLanguage);
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Get phrases for all supported languages
        /// </summary>
        public Dictionary<string, string> Phrases
        {
            get
            {
                return phrases;
            }
        }

        /// <summary>
        /// Get a list of phrases in the format language':phrase
        /// </summary>
        public IEnumerable<SelectListItem> PhrasesList
        {
            get 
            {
                SelectList phrases = new SelectList(Phrases);
                SelectListItem[] phrasesArray = phrases.ToArray();
                
                foreach(SelectListItem item in phrasesArray)
                {
                    item.Text = item.Text.Replace("[", string.Empty).Replace(",", ":").Replace("]", string.Empty);
                }

                return phrasesArray;
            }
        }

        /// <summary>
        /// Get phrase for the specified language
        /// </summary>
        /// <param name="language">The language</param>
        /// <returns>The phrase</returns>
        public string Phrase(string language)
        {
            string result = string.Empty;

            if (phrases.ContainsKey(language))
            {
                result = phrases[language];
            }
            else
            {
                result = string.Format("Language '{0}' is not supported.", language);
            }

            return result;
        }

        /// <summary>
        /// Add a phrase for a specific language
        /// </summary>
        /// <param name="phraseInfo"></param>
        /// <returns></returns>
        public string AddPhrase(HelloPhraseModel phraseInfo)
        {
            string result = string.Empty;

            if (phraseInfo == null || string.IsNullOrEmpty(phraseInfo.Language) || string.IsNullOrEmpty(phraseInfo.Phrase))
            {
                string message = "Invalid phrase specification.";
                string reason = "Language and Phrase are required.";
                result = string.Format("{0} {1}", message, reason);
            }
            else if (phrases.Keys.Contains(phraseInfo.Language))
            {
                string message = "Duplicate language entry.";
                string reason = string.Format("An entry for language '{0}' already exists.", phraseInfo.Language);
                result = string.Format("{0} {1}", message, reason);
            }
            else
            {
                phrases.Add(phraseInfo.Language, phraseInfo.Phrase);
                result = string.Format("Phrase '{0}' added for language '{1}'.", phraseInfo.Phrase, phraseInfo.Language);

                if(phrases.Count == 1)
                {
                    DefaultLanguage = phraseInfo.Language;
                }
            }

            return result;
        }

        /// <summary>
        /// Delete a phrase for a specific language
        /// </summary>
        /// <param name="language">The language of the phrase to delete</param>
        /// <returns>Status message</returns>
        public string DeletePhrase(string language)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(language))
            {
                string message = "Invalid language specification.";
                string reason = "Language can not be null or empty";
                result = string.Format("{0} {1}", message, reason);
            }
            else if (!phrases.Keys.Contains(language))
            {
                string message = "Invalid language specification.";
                string reason = string.Format("An entry for language '{0}' does not exists.", language);
                result = string.Format("{0} {1}", message, reason);
            }
            else
            {
                phrases.Remove(language);
                result = string.Format("Phrase deleted for language '{0}'.", language);

                if(phrases.Count == 0)
                {
                    DefaultLanguage = null;
                }
                else
                {
                    DefaultLanguage = phrases.Keys.ElementAt(0);
                }
            }

            return result;
        }

        /// <summary>
        /// Helper property for adding a phrase
        /// </summary>
        [Required(ErrorMessage = "Language is required")]
        public string LanguageToAdd { get; set; }

        /// <summary>
        /// Helper property for adding a phrase
        /// </summary>
        [Required(ErrorMessage = "Phrase is required")]
        public string PhraseToAdd { get; set; }

        /// <summary>
        /// Helper property for deleting a phrase
        /// </summary>
        public string LanguageToDelete {  get; set; }

        /// <summary>
        /// Status message
        /// </summary>
        public string StatusMessage { get; set; } = "* Required";
    }
}