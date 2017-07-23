using Imagnr.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    /// <summary>
    /// Imagnr UnitTest
    /// </summary>
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// TestMethod
        /// </summary>
        [TestMethod]
        public void TestMethod()
        {
            //Create a recognizer:
            var imagnr = new Imagnr.Recognizer(TestConfig.AzureCognitiveServicesKey);

            //Create entities with tags:
            var heinz = new Imagnr.Entity
            {
                Name = "Heinz Tomato Ketchup",
                Tags = new List<Imagnr.Tag>
                {
                    new Imagnr.Tag { Value = "Heinz", MinimumSimilarity = 0.8, Required = true, Score = 2 },
                    new Imagnr.Tag { Value = "Tomato", MinimumSimilarity = 0.5 },
                    new Imagnr.Tag { Value = "Ketchup", MinimumSimilarity = 0.8, Required = true }
                }
            };

            var natura = new Imagnr.Entity
            {
                Name = "Mayonesa Natura",
                Tags = new List<Imagnr.Tag>
                {
                    new Imagnr.Tag { Value = "Mayonesa", MinimumSimilarity = 0.8, Required = true },
                    new Imagnr.Tag { Value = "Natura", MinimumSimilarity = 0.8, Required = true },
                }
            };

            //Add entity to the recognizer's catalog:
            imagnr.Catalog.Add(heinz);
            imagnr.Catalog.Add(natura);

            //Search 
            var results1 = imagnr.Search(GetImageAsByteArray("images\\heinz\\heinz3.jpg")).Result;
            Assert.AreEqual(results1.RecognizedEntities[0].Name, heinz.Name);

            //var results2 = imagnr.Search(GetImageAsByteArray("images\\natura\\naturamaxresdefault.jpg")).Result;
            //Assert.AreEqual(results2.RecognizedEntities[0].Name, natura.Name);
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}
