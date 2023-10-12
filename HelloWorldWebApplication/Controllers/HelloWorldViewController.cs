using System.Web.Mvc;
using HelloWorldWebApplication.Models;

namespace HelloWorldWebApplication.Controllers
{
    /// <summary>
    /// The HelloWorld view controller
    /// </summary>
    public class HelloWorldViewController : Controller
    {
        /// <summary>
        /// The HelloWorld view
        /// </summary>
        /// <param name="submit">Submit button text</param>
        /// <param name="helloPhrasesModel">HelloPhrasesModel</param>
        /// <returns></returns>
        public ActionResult HelloWorldView(string submit, HelloPhrasesModel helloPhrasesModel)
        {
            if (submit == "Add Phrase")
            {
                helloPhrasesModel.StatusMessage = helloPhrasesModel.AddPhrase(new HelloPhraseModel(helloPhrasesModel.LanguageToAdd, helloPhrasesModel.PhraseToAdd));
            }
            else if(submit == "Delete Phrase")
            {
                string language = helloPhrasesModel.LanguageToDelete.Split(':')[0];
                helloPhrasesModel.DeletePhrase(language);
            }
            else if (submit == "Set As Default")
            {
                string language = helloPhrasesModel.LanguageToDelete.Split(':')[0];
                helloPhrasesModel.DefaultLanguage = language;
            }

            return View(helloPhrasesModel);
        }
    }
}