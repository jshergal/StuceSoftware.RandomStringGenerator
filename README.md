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
StuceSoftware.RandomStringGenerator is a library developed in C# to generate random strings of from various categories. It is fast and suports string generation of various length.  
It is parameterized to generate both a single or a list of random strings.  
Random strings can be of types alphabet and alphanumeric supporting all the cases viz. lower, upper and mixed.  
It also supports symbol during the random string generation process. Following are the list of supported symbols:
!#$%&'()*+,-./:;<=>?@[]\^_`{|}~"

It also allows you to generate random string with only a subset of symbols from the above supported list. It is an ideal use for projects such as:  
* password generator
* mobile number generator
* unique-id generator
* unique filename generator
* raw data generator for data processing/parsing projects
* fake name generator
etc. etc.

## Features
* Supports single or multiple random string generation 
* Supports random string generation from alphabet, numbers, symbols or a combination of them
* Supports customized length of randon string
* Supports random length of generated strings with a fixed max length
* Supports true unique random number generation
* Support force inclusion of strings of each type
* Supports .NET Standard 2.0 & 2.1, .NET 6.0, 8.0 & 9.0

## Supported Types
* **Numbers** : *0123456789*
* **Symbols** : *!#$%&'()\*+,-./:;<=>?@[]\^_`{|}~"*
* **AlphabetLowercase** : *abcdefghijklmnopqrstuvwxyz*
* **AlphabetUppercase** : *ABCDEFGHIJKLMNOPQRSTUVWXYZ*
* **AlphabetMixedCase** : *abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ*
* **ALPHANUMERICLowercase** : *abcdefghijklmnopqrstuvwxyz0123456789*
* **ALPHANUMERICUppercase** : *ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789*
* **ALPHANUMERICMixedCase** : *abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789*
* **AlphabetLowercaseWithSymbols** : *abcdefghijklmnopqrstuvwxyz!#$%&'()\*+,-./:;<=>?@[]\^_`{|}~"*
* **AlphabetUppercaseWithSymbols** : *ABCDEFGHIJKLMNOPQRSTUVWXYZ!#$%&'()\*+,-./:;<=>?@[]\^_`{|}~"*
* **AlphabetMixedCaseWithSymbols** : *abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$%&'()\*+,-./:;<=>?@[]\^_`{|}~"*
* **AlphanumericLowercaseWithSymbols** : *abcdefghijklmnopqrstuvwxyz0123456789!#$%&'()\*+,-./:;<=>?@[]\^_`{|}~"*
* **AlphanumericUppercaseWithSymbols** : *ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&'()\*+,-./:;<=>?@[]\^_`{|}~"*
* **AlphanumericMixedCaseWithSymbols** : *abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" !#$%&'()\*+,-./:;<=>?@[]\^_`{|}~"*


## Get Started
### 1. Install Package
```ini
PM> Install-Package StuceSoftware.RandomStringGenerator
```
### 2. Add reference in your project

### 3. Call the methods in your program
```csharp
using StuceSoftware.RandomStringGenerator;

// generating one random string from lowercase alphabets
string randomString = RandomString.GetString(Types.AlphabetLowercase);
System.Console.WriteLine(randomString);

// generating 100 random string from all mixedcase alphabet, numbers and all supported symbols
List<string> randomAlphaNumbericStrings = RandomString.GetStrings(Types.AlphanumericMixedCaseWithSymbols, 100);
foreach (string s in randomAlphaNumbericStrings)
    System.Console.WriteLine(s);

// generating 200 random string from uppercase alphabet with custom symbols
List<string> randomAlphabetWithCustomSymbols = RandomString.GetStrings(Types.AlphabetUppercase, 200, "/+*-");
foreach (string s in randomAlphabetWithCustomSymbols)
    System.Console.WriteLine(s);

// generating 1000 true random strings of length 20 from uppercase alphabet with custom symbols
List<string> trueUniqueRandomStrings = RandomString.GetStrings(Types.AlphabetUppercase, 1000, 20, false, true);
foreach (string s in trueUniqueRandomStrings)
    System.Console.WriteLine(s);

// generating 100 random string of mixedcase alphanummeric with custom symbols
List<string> randomAlphabetWithCustomSymbols = RandomString.GetStrings(Types.AlphanumericMixedCaseWithSymbols, 100, "/+*-", forceOccuranceOfEachType: true);
foreach (string s in randomAlphabetWithCustomSymbols)
    System.Console.WriteLine(s);
```
    
## Give a Star! ⭐️

If you find this repository useful, please give it a star.
Thanks in advance !

## License

Copyright © 2024 [Stuce Software Solutions](https://stucesoftware.com/) under the [MIT License](https://github.com/jshergal/StuceSoftware.RandomStringGenerator/blob/main/LICENSE).
