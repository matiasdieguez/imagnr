# ![Logo](https://raw.githubusercontent.com/matiasdieguez/imagnr/master/icon.png) Imagnr
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
See samples in the Test Methods of https://github.com/matiasdieguez/imagnr/blob/master/Tests/UnitTest.cs

 Add using 
```csharp
 using Imagnr;
 ```

 Sample
```csharp
public async void TestMethod()
{
    //Create a recognizer:
    var imagnr = new Recognizer("YOUR_AZURE_COGNITIVE_SERVICES_KEY");

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

    //Search for entity tags in images
    var results0 = await imagnr.Search(GetImageAsByteArray("images\\heinz\\ketchup.jpg"));
    var results1 = await imagnr.Search(GetImageAsByteArray("images\\heinz\\mustard.jpg"));
    var results2 = await imagnr.Search(GetImageAsByteArray("images\\heinz\\relish.jpg"));
}

private static byte[] GetImageAsByteArray(string imageFilePath)
{
    using (var fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
    using (var binaryReader = new BinaryReader(fileStream))
        return binaryReader.ReadBytes((int)fileStream.Length);
}

```

IMPORTANT: You must set your valid api key and image paths 

## References used to create this project
* https://docs.microsoft.com/en-us/nuget/guides/create-net-standard-packages-vs2017
* https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/quickstarts/csharp#RecognizeText

##Tags
Computer Vision, Azure, Cognitive Services, OCR, Image Recognition, Brand Recognition, Text Recognition, Tag Recognition

##Resources
Icons made by http://www.freepik.com from https://www.flaticon.com/ licensed by http://creativecommons.org/licenses/by/3.0/ 