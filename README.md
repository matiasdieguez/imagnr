# Imagnr
Imagnr is a Cognitive Recognizer of Image Tags

Having an image and a catalog of text tags, Imagnr gives you a library to do a cognitive search based on the text appearing in the image. 
Uses Levenshtein distance algorithm over condensed OCR results to find matches of tags with a configurable similarity level.

##Platform Support
.NET Portable Class Library

## Dependencies
Imagnr uses Computer Vision from Azure Cognitive Services to extract OCR data from images, so you need a subscription to it.
Currently, the specific service used is Handwriting Recognition (in preview)

## Setup
The simplest way to use the library is to install the following NuGet package 
```csharp
PM> install-package imagnr 
 ```

## How to use
 Add using 
```csharp
 using Imagnr;
 ```

 Sample
```csharp
//Create a recognizer:
var imagnr = new Recognizer("YOUR_AZURE_COGNITIVE_SERVICES_API_KEY");

//Add some entities to the recognizer's catalog:
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

//load image into byte[]
byte[] image = null;
using (var fileStream = new FileStream("yourtestimage.jpg", FileMode.Open, FileAccess.Read))
using (var binaryReader = new BinaryReader(fileStream))
    image = binaryReader.ReadBytes((int)fileStream.Length);

//Search for entity tags in image (async)
var results = await imagnr.Search(image);

//Search for entity tags in image (sync)
var results = imagnr.Search(image).Result;

```

## References used to create this project
* https://docs.microsoft.com/en-us/nuget/guides/create-net-standard-packages-vs2017
* https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/quickstarts/csharp#RecognizeText

#Tags
Computer Vision, Azure, Cognitive Services, OCR, Image Recognition, Brand Recognition, Text Recognition, Tag Recognition
