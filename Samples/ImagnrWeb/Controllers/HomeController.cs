using Imagnr;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImagnrWeb.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            //Create a recognizer:
            var imagnr = new Recognizer(ConfigurationManager.AppSettings["AzureCognitiveServicesApiKey"]);

            //Add some entities to the recognizer's catalog:
            imagnr.Catalog.Add(new Entity
            {
                Name = "Heinz Ketchup",
                Tags = new List<Tag>
                {
                    new Tag { Value = "Heinz", MinimumSimilarity = 0.8 },
                    new Tag { Value = "Tomato", MinimumSimilarity = 0.5 },
                    new Tag { Value = "Ketchup", MinimumSimilarity = 0.8 , Score=10}
                }
            });

            imagnr.Catalog.Add(new Entity
            {
                Name = "Heinz Yellow Mustard",
                Tags = new List<Tag>
                {
                    new Tag { Value = "Heinz", MinimumSimilarity = 0.8},
                    new Tag { Value = "Yellow", MinimumSimilarity = 0.5 },
                    new Tag { Value = "Mustard", MinimumSimilarity = 0.8, Score=10}
                }
            });

            imagnr.Catalog.Add(new Entity
            {
                Name = "Heinz Sweet Relish",
                Tags = new List<Tag>
                {
                    new Tag { Value = "Heinz", MinimumSimilarity = 0.8},
                    new Tag { Value = "Sweet", MinimumSimilarity = 0.5 },
                    new Tag { Value = "Relish", MinimumSimilarity = 0.8, Score=10}
                }
            });

            //Search for entity tags in image
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            //var results0 = await imagnr.Search(GetImageAsByteArray(path + "\\images\\heinz\\ketchup.jpg"));

            //var results1 = await imagnr.Search(GetImageAsByteArray(path + "\\images\\heinz\\mustard.jpg"));

            //var results2 = await imagnr.Search(GetImageAsByteArray(path + "\\images\\heinz\\relish.jpg"));

            return View();
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (var fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            using (var binaryReader = new BinaryReader(fileStream))
                return binaryReader.ReadBytes((int)fileStream.Length);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}