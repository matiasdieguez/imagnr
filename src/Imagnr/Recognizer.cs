﻿using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Imagnr
{
    /// <summary>
    /// Recognize cognitive tag catalog search from an image OCR result
    /// </summary>
    public class Recognizer
    {
        /// <summary>
        /// Replace or verify the region.
        ///
        /// You must use the same region in your REST API call as you used to obtain your subscription keys.
        /// For example, if you obtained your subscription keys from the westus region, replace 
        /// "westcentralus" in the URI below with "westus".
        ///
        /// NOTE: Azure Cognitive Services Free trial subscription keys are generated in the westcentralus region, so if you are using
        /// a free trial subscription key, you should not need to change this region.
        /// (Extracted from https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/quickstarts/csharp#RecognizeText)
        /// </summary>
        public string AzureCognitiveServicesApiUrl { get; set; }

        /// <summary>
        /// The Key to acces Azure Cognitive Services
        /// </summary>
        public string AzureCognitiveServicesKey { get; set; }

        /// <summary>
        /// The Catalog of Entities loaded to recognize
        /// </summary>
        public List<Entity> Catalog = new List<Entity>();

        /// <summary>
        /// Constructor for Recognizer.
        /// Recives parameters for Azure Cognitive Services
        /// </summary>
        /// <param name="azureCognitiveServicesKey">Your subscription key for Azure Cognitive Services</param>
        /// <param name="azureCognitiveServicesApiUrl">Your Azure region Cognitive Services API URL</param>
        public Recognizer(string azureCognitiveServicesKey)
        {
            //AzureCognitiveServicesApiUrl = azureCognitiveServicesApiUrl;
            AzureCognitiveServicesKey = azureCognitiveServicesKey;
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns>Result</returns>
        public async Task<Result> Search(byte[] image)
        {
            var result = new Result();

            string condensedText = await AzureCognitiveServicesHandwrittenText(image);

            foreach (var entity in Catalog)
            {
                var entityScore = 0;

                foreach (var tag in entity.Tags)
                {
                    for (var c = 0; c < condensedText.Length; c++)
                    {
                        try
                        {
                            var subSource = condensedText.Substring(c, tag.Value.Length);
                            var similarity = CalculateSimilarity(subSource, tag.Value);
                            if (similarity >= tag.MinimumSimilarity)
                                entityScore++;
                        }
                        catch
                        {
                            break;
                        }
                    }
                }

                result.RecognizedEntities.Add(new RecognizedEntity { Name = entity.Name, Score = entityScore });
            }

            return result;
        }

        /// <summary>
        /// Performs Azure Cognitive Services Handwritten Text Recognition on image
        /// </summary>
        /// <param name="image">Image byte array</param>
        /// <returns>Condensed text string with OCR results</returns>
        private async Task<string> AzureCognitiveServicesHandwrittenText(byte[] image)
        {
            var client = new VisionServiceClient(AzureCognitiveServicesKey);

            HandwritingRecognitionOperation hwResult = null;
            using (var photoStream = new MemoryStream(image))
                hwResult = await client.CreateHandwritingRecognitionOperationAsync(photoStream);

            var r = await client.GetHandwritingRecognitionOperationResultAsync(hwResult);

            var condensedText = string.Empty;

            foreach (var l in r.RecognitionResult.Lines)
                foreach (var w in l.Words)
                    condensedText += w.Text;

            condensedText = condensedText.ToLower();
            return condensedText;
        }

        /// <summary>
        /// Compute Levenshtein Distance between two strings 
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="target">The target string</param>
        /// <returns>Integer representing the Levenshtein Distance result</returns>
        private int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            var sourceWordCount = source.Length;
            var targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            var distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (var i = 0; i <= sourceWordCount; distance[i, 0] = i++)
            {
            }
            for (var j = 0; j <= targetWordCount; distance[0, j] = j++)
            {
            }

            for (var i = 1; i <= sourceWordCount; i++)
            {
                for (var j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    var cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        /// <summary>
        /// Calculates the Similarity percent from the total lenght of the strings
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="target">The target string</param>
        /// <returns>Double value, representing the percentual similarity</returns>
        private double CalculateSimilarity(string source, string target)
        {
            source = source.ToLower();
            target = target.ToLower();

            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            var stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - (stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }
    }
}