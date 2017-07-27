using Imagnr;
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
        /// This test loads the recognizer's catalog with two entities 
        /// and takes the images in the specified as input
        /// </summary>
        [TestMethod]
        public void TestWithImagesFolder()
        {
            //Create a recognizer:
            var imagnr = new Recognizer(TestConfig.AzureCognitiveServicesKey);

            //Add Heinz to the recognizer's catalog:
            imagnr.Catalog.Add(new Entity
            {
                Name = "Heinz Ketchup",
                Tags = new List<Tag>
                {
                    new Tag { Value = "Heinz", MinimumSimilarity = 0.8, Required = true, Score = 2 },
                    new Tag { Value = "Tomato", MinimumSimilarity = 0.5 },
                    new Tag { Value = "Ketchup", MinimumSimilarity = 0.8, Required = true }
                }
            });

            //Add Natura to the recognizer's catalog:
            imagnr.Catalog.Add(new Entity
            {
                Name = "Mayonesa Natura",
                Tags = new List<Tag>
                {
                    new Tag { Value = "Mayonesa", MinimumSimilarity = 0.8, Required = true },
                    new Tag { Value = "Natura", MinimumSimilarity = 0.8, Required = true },
                }
            });

            //Search in Heinz folder
            foreach (var image in Directory.GetFiles("images\\heinzketchup", "*.jpg"))
            {
                var results = imagnr.Search(GetImageAsByteArray(image)).Result;
                Assert.AreEqual(results.RecognizedEntities[0].Name, imagnr.Catalog[0].Name);
            }

            //Search in Natura folder
            foreach (var image in Directory.GetFiles("images\\natura", "*.jpg"))
            {
                var results = imagnr.Search(GetImageAsByteArray(image)).Result;
                Assert.AreEqual(results.RecognizedEntities[0].Name, imagnr.Catalog[1].Name);
            }
        }

        /// <summary>
        /// This test loads the recognizer's catalog with 3 Heinz products
        /// is expected to return in first place the best match
        /// </summary>
        [TestMethod]
        public void TestMethod()
        {
            //Create a recognizer:
            var imagnr = new Recognizer(TestConfig.AzureCognitiveServicesKey);

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
            var results0 = imagnr.Search(GetImageAsByteArray("images\\heinz\\ketchup.jpg")).Result;
            Assert.AreEqual(results0.RecognizedEntities[0].Name, imagnr.Catalog[0].Name);

            var results1 = imagnr.Search(GetImageAsByteArray("images\\heinz\\mustard.jpg")).Result;
            Assert.AreEqual(results1.RecognizedEntities[0].Name, imagnr.Catalog[1].Name);

            var results2 = imagnr.Search(GetImageAsByteArray("images\\heinz\\relish.jpg")).Result;
            Assert.AreEqual(results2.RecognizedEntities[0].Name, imagnr.Catalog[2].Name);
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
    }
}
