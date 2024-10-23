# StuceSoftware.RandomStringGenerator

Authors :
Jeff Shergalis   
Lakhya Jyoti Nath (ljnath)

Date : September 2020 - October 2024  

![GitHub last commit](https://img.shields.io/github/last-commit/jshergal/StuceSoftware.RandomStringGenerator)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/jshergal/StuceSoftware.RandomStringGenerator/dotnet-CI-workflow.yaml)
[![codecov](https://codecov.io/gh/jshergal/StuceSoftware.RandomStringGenerator/branch/main/graph/badge.svg?token=840K6JYMT1)](https://codecov.io/gh/jshergal/StuceSoftware.RandomStringGenerator)
![NuGet Version](https://img.shields.io/nuget/v/StuceSoftware.RandomStringGenerator)
![NuGet Downloads](https://img.shields.io/nuget/dt/StuceSoftware.RandomStringGenerator)


## Introduction
StuceSoftware.RandomStringGenerator is a library developed in C# to generate random strings of from various categories.
It is fast and suports string generation of various length. It is parameterized to generate both a single or a list of random strings.  
Random strings can be of types lowercase, uppercase, numbers and symbols. It also allows you to generate random string with only a
subset of symbols from the supported list. It is an ideal library for use in projects such as:  
* password generator
* unique-id generator
* unique filename generator
* raw data generator for data processing/parsing projects
* etc. etc.

## Features
* Supports single or multiple random string generation 
* Supports random string generation from alphabet, numbers, symbols or a combination of them
* Supports customized length of random string
* Supports random length of generated strings with a fixed max length
* Supports true unique random number generation
* Support force inclusion of strings of each type
* Supports .NET Standard 2.0 & 2.1, .NET 6.0, 8.0 & 9.0

## Supported Types
* **Lowercase** : *abcdefghijklmnopqrstuvwxyz*
* **Uppercase** : *ABCDEFGHIJKLMNOPQRSTUVWXYZ*
* **Numbers** : *0123456789*
* **Symbols** : *!#$%&'()\*+,-./:;<=>?@[]\^_`{|}~"*

These types are defined by an enum `CharClasses` which has the `[Flags]` attribue, making it easy to combine the different classes.

**Breaking Changes :rotating_light:**  

#### v2.0
Switched to a `[Flags]` enum for `CharClasses`

### v3.0 (in pre-release)
`RandomStringGenerator` class is now non-static. The constructor takes an `IRandomSource` object, which is used internally to generate random values.  
There are two implementations of `IRandomSource` provided with the library: `SystemRandomSource` and `SecureRandomSource`.

## Download and Install
**NuGet Package [StuceSoftware.RandomStringGenerator](https://www.nuget.org/packages/StuceSoftware.RandomStringGenerator/)**

```powershell
dotnet add package StuceSoftware.RandomStringGenerator
```

### Sample Usage
```cs
using StuceSoftware.RandomStringGenerator;
using StuceSoftware.RandomStringGenerator.RandomSourceImplementations;

var randomStringGenerator = new RandomStringGenerator(new SystemRandomSource());

// generating one random string from lowercase alphabets
var randomString = randomStringGenerator.GetString(CharClasses.Lowercase);
System.Console.WriteLine(randomString);

// generating 100 random strings using both upper and lower case alphabet, numbers and all supported symbols
var randomAlphaNumbericStrings = randomStringGenerator.GetStrings(
    CharClasses.Lowercase | CharClasses.Uppercase | CharClasses.Numbers | CharClasses.Symbols, 100);
foreach (string s in randomAlphaNumbericStrings)
    System.Console.WriteLine(s);

// generating 200 random string using uppercase alphabet and custom symbols
var randomAlphabetWithCustomSymbols = randomStringGenerator.GetStrings(CharClasses.Uppercase, 200, "/+*-");
foreach (string s in randomAlphabetWithCustomSymbols)
    System.Console.WriteLine(s);

// generating 1000 true random strings of length 20 using uppercase alphabet with custom symbols
var trueUniqueRandomStrings = randomStringGenerator.GetStrings(CharClasses.Uppercase, 1000, 20, "/+*-", false, true);
foreach (string s in trueUniqueRandomStrings)
    System.Console.WriteLine(s);

// generating 100 random strings using both upper and lowercase alphabet, numbers and custom symbols
var randomAlphabetWithCustomSymbols = randomStringGenerator.GetStrings(CharClasses.Lowercase | CharClasses.Uppercase | CharClasses.Numbers, 100, "/+*-", forceOccuranceOfEachType: true);
foreach (string s in randomAlphabetWithCustomSymbols)
    System.Console.WriteLine(s);
```
**Note :eyes:**  
As of version 2.0, if custom symbols are defined, there is no need to specify the symbol character class (`CharClasses.Symbols`),
this is assumed when custom symbols are provided
    
## Give a Star! :star:

If you find this library helpful, please give it a star.
Thanks in advance!

## License

Copyright © 2024 [Stuce Software Solutions](https://stucesoftware.com/) under the [MIT License](https://github.com/jshergal/StuceSoftware.RandomStringGenerator/blob/main/LICENSE).
