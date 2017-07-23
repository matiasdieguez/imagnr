# Imagnr
Imaginr is a Image Tags Cognitive Recognizer

# What it does
Having an image and a catalog of text tags, Imagnr gives you a .NET Standard library to do a cognitive catalog search 
based on the text appearing in the image. Uses Levenshtein Distance Algorithm to find matches of tags with a configurable similarity level.

# Technology
Imaginr uses Computer Vision from Azure Cognitive Services to extract OCR data from images

# How to use
The simplest way to use the library is to install a NuGet package 

nuget> install-package imagnr 

Create a recognizer:
var recognizer = new imagnr.Recognizer{ TagSimilarity=0.8d, AzureCognitiveServicesApiKey = ""};

Create entities with tags:
var heinz = new Imagnr.Entity{Name="Heinz Tomato Ketchup", UseNameAsTags=false, Tags=new string[]{"heinz","tomato","ketchup"}};
var bud = new Imagnr.Entity{Name="Budweisser", UseNameAsTags=false, Tags=new string[]{"budweisser","beer","lager"}};

Add entity to the recognizer's catalog:
recognizer.Catalog.Add(entity);

var imageBytes = GetImageByteArray("sample.jpg");

var results = brandizer.Search(imageBytes);


# References used to create this project
https://docs.microsoft.com/en-us/nuget/guides/create-net-standard-packages-vs2017

