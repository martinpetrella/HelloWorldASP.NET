using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelloWorldWebApplication.Models;

namespace HelloWorldWebApplication.Controllers
{
    /// <summary>
    /// HelloWorld - Display "Hello World!" phrases for various languages. Phrases can be added and deleted.
    /// </summary>
    public class HelloWorldController : ApiController
    {
        HelloPhrasesModel _model = new HelloPhrasesModel();

        /// <summary>
        /// Get phrase for default language
        /// </summary>
        /// <returns>Phrase in default language</returns>
        [HttpGet]
        public string Index()
        {
            string phrase = _model.DefaultPhrase;
            return phrase;
        }

        /// <summary>
        /// Set default language
        /// </summary>
        /// <param name="phraseInfo">The default language</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("api/HelloWorld/International/DefaultLanguage")]
        public HttpResponseMessage SetDefaultLanguage([FromBody] HelloPhraseModel phraseInfo)
        {
            HttpResponseMessage responseMessage;

            try
            {
                if (_model.Phrases.ContainsKey(phraseInfo.Language))
                {
                    _model.DefaultLanguage = phraseInfo.Language;

                    responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(string.Format("Default language set to '{0}'.", phraseInfo.Language))
                    };
                }
                else
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(string.Format("Language '{0}' is not supported.", phraseInfo))
                    };
                }

            }
            catch (Exception ex)
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }

            return responseMessage;
        }

        /// <summary>
        /// Get phrase for each supported language
        /// </summary>
        /// <returns>Dictionary containing phrases for various languages</returns>
        [HttpGet]
        [Route("api/HelloWorld/International")]
        public Dictionary<string, string> Phrases()
        {
            return _model.Phrases;
        }

        /// <summary>
        /// Get phrase for specified language
        /// </summary>
        /// <param name="language">The language</param>
        /// <returns>Phrase in the specified language</returns>
        [HttpGet]
        [Route("api/HelloWorld/International")]
        public string GetPhrase(string language)
        {
            return _model.Phrase(language);
        }

        /// <summary>
        /// Add phrase for specified language
        /// </summary>
        /// <param name="phraseInfo">The phrase to add for a specified language</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("api/HelloWorld/International")]
        public HttpResponseMessage AddPhrase([FromBody] HelloPhraseModel phraseInfo)
        {
            HttpResponseMessage responseMessage;

            try
            {
                if (phraseInfo == null || string.IsNullOrEmpty(phraseInfo.Language) || string.IsNullOrEmpty(phraseInfo.Phrase))
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Language and phrase are required.")
                    };
                }
                else
                {
                    string result = _model.AddPhrase(phraseInfo);

                    responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(result)
                    };
                }
            }
            catch (Exception ex)
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }

            return responseMessage;
        }

        /// <summary>
        /// Delete phrase for specified language
        /// </summary>
        /// <param name="language">The language of the phrase to delete</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpDelete]
        [Route("api/HelloWorld/International")]
        public HttpResponseMessage DeletePhrase([FromBody] string language)
        {
            HttpResponseMessage responseMessage;

            try
            {
                string result = _model.DeletePhrase(language);

                responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(result)
                };
            }
            catch (Exception ex)
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }

            return responseMessage;
        }
    }
}