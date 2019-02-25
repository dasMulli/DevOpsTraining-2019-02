## Azure DevOps training material

This repository contains a few training materials and step-by-step tutorials to set up an Azure DevOps CI/CD pipeline for an ASP.NET Core application.

The following materials are avilable
* [Lab 1](01-Setup-AzureDevOps.md) Set up Azure DevOps and initialize a repository with code
* [Lab 2](02-Setup-CI.md) Set up continuous integration for the repsoitory, add branch policies and use a pull request to add tests.
* [Lab 3](03-Setup-CD.md) Set up continuous deployment to an Azure Web App.

Where to go from here?

Upload the file in `Additional_BookNameTokens` to an Azure Storage Account using Blob Storage and consume it from the application.
Then use a load test to find bottlenecks in your application, re-evaluate how caching and cache invalidation should work in your app to work around network failures.
