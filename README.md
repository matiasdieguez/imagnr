# Imagnr
Imagnr is a Cognitive Recognizer of Image Tags

Having an image and a catalog of text tags, Imagnr gives you a library to do a cognitive search based on the text appearing in the image. 
Uses Levenshtein distance algorithm over condensed OCR results to find matches of tags with a configurable similarity level.
##Platform Support
*.NET Portable Class Library

## Dependencies
Imagnr uses Computer Vision from Azure Cognitive Services to extract OCR data from images, so you need a subscription to it.
Currently, the specific service used is Handwriting Recognition (in preview)

## Setup
The simplest way to use the library is to install the following NuGet package 
```csharp
PM> install-package imagnr 
 ```

## How to use
 Example
```csharp
//Create a recognizer:
var recognizer = new Imagnr.Recognizer{ AzureCognitiveServicesKey = "YOUR_AZURE_COGNITIVE_SERVICES_KEY"};

//Create entities with tags:
var heinz = new Imagnr.Entity{Name="Heinz Tomato Ketchup", UseNameAsTags=false, Tags=new string[]{"heinz","tomato","ketchup"}};
var bud = new Imagnr.Entity{Name="Budweisser", UseNameAsTags=false, Tags=new string[]{"budweisser","beer","lager"}};

//Add entity to the recognizer's catalog:
recognizer.Catalog.Add(heinz);
recognizer.Catalog.Add(bud);

//Search 
var imageBytes = GetImageByteArray("sample.jpg");
var results = brandizer.Search(imageBytes);
```

## References used to create this project
* https://docs.microsoft.com/en-us/nuget/guides/create-net-standard-packages-vs2017
* https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/quickstarts/csharp#RecognizeText

#Tags
Computer Vision, Azure, Cognitive Services, OCR, Image Recognition, Brand Recognition, Text Recognition, Tag Recognition
