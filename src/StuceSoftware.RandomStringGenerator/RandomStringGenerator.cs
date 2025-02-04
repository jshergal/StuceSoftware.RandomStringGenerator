﻿//
// StuceSoftware.RandomStringGenerator - .NET library for random string generation
//
// Copyright 2020-2024 - Jeff Shergalis; Lakhya Nath
//
// Licensed under the MIT License - http://www.opensource.org/licenses/mit-license.php
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE
// AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES
// OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT
// OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// project: https://github.com/jshergal/StuceSoftware.RandomStringGenerator
//

using System.Text;
using StuceSoftware.RandomStringGenerator.Exceptions;

namespace StuceSoftware.RandomStringGenerator;

/// <summary>
///     Main class of the library containing the publicly exposed methods and internal logic for random number generation
/// </summary>
public sealed class RandomStringGenerator
{
    private const CharClasses SymbolMask = ~CharClasses.Symbols;

    private readonly IRandomSource _randomSource;

    /// <summary>
    ///   Initializes a new instance of the <c>RandomStringGenerator</c> class with the specified <c>IRandomSource</c>
    /// </summary>
    /// <param name="randomSource">Random source object used in random string generation</param>
    public RandomStringGenerator(IRandomSource randomSource) => _randomSource = randomSource;

    /// <summary>
    ///     Generates a random string of input type <c>charClasses</c> having a maximum string length of <c>maxLength</c>
    /// </summary>
    /// <param name="charClasses">Type of RandomString4Net.CharClasses is the type of input string for random string generation</param>
    /// <param name="maxLength">Maximum length of a random string to be generated; default is 10</param>
    /// <param name="randomLength">Boolean choice if the length of the generated random string should be random as well</param>
    /// <param name="forceOccurrenceOfEachType">
    ///     Boolean choice to indicate if string of each subtype should present in the
    ///     generated random string
    /// </param>
    /// <returns>A newly generated random string</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <c>count</c> or <c>maxLength</c> is less then or equal to 0</exception>
    public string GetString(CharClasses charClasses, int maxLength = 10, bool randomLength = false,
        bool forceOccurrenceOfEachType = false) => GetRandomStrings(charClasses.GetStrings(), 1, maxLength, randomLength, false,
        forceOccurrenceOfEachType)[0];

    /// <summary>
    ///     Generates a random string of input type <c>charClasses</c> having a maximum string length of <c>maxLength</c> including
    ///     symbols specified as <c>symbolsToInclude</c>
    /// </summary>
    /// <param name="charClasses">Type of RandomString4Net.CharClasses is the type of input string for random string generation</param>
    /// <param name="symbolsToInclude">Subset of symbols from the list of supported symbols, specified as a string</param>
    /// <param name="maxLength">Maximum length of a random string to be generated; default is 10</param>
    /// <param name="randomLength">Boolean choice if the length of the generated random string should be random as well</param>
    /// <param name="forceOccurrenceOfEachType">
    ///     Boolean choice to indicate if string of each subtype should present in the
    ///     generated random string
    /// </param>
    /// <returns>A newly generated random string</returns>
    /// <exception cref="UnsupportedSymbolException">
    ///     Thrown when the input subset of string is not present in the list of
    ///     supported symbols
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <c>count</c> or <c>maxLength</c> is less then or equal to 0</exception>
    public string GetString(CharClasses charClasses, string symbolsToInclude, int maxLength = 10, bool randomLength = false,
        bool forceOccurrenceOfEachType = false)
    {
        UtilityMethods.ValidateSymbols(symbolsToInclude);

        // excluding symbols as custom symbols are specified
        charClasses &= SymbolMask;

        var classCount = charClasses.GetClassCount();
        var inputStrings = new string[classCount + 1];
        Array.Copy(charClasses.GetStrings(), inputStrings, classCount);

        // classCount is the last element since array size is classCount + 1
        // if we drop netstandard2.0 support, we can use the indexer ^1
        inputStrings[classCount] = symbolsToInclude;

        return GetRandomStrings(inputStrings, 1, maxLength, randomLength, false,
            forceOccurrenceOfEachType)[0];
    }

    /// <summary>
    ///     Generates a list of random strings of input type <c>charClasses</c> having a maximum string length of <c>maxLength</c>
    /// </summary>
    /// <param name="charClasses">Type of RandomString4Net.CharClasses is the type of input string for random string generation</param>
    /// <param name="count">Number of random strings to be generated</param>
    /// <param name="maxLength">Maximum length of a random string to be generated; default is 10</param>
    /// <param name="randomLength">Boolean choice if the length of the generated random string should be random as well</param>
    /// <param name="forceUnique">
    ///     Boolean choice to force generation of only unique numbers, count may not be met if this is
    ///     set to true
    /// </param>
    /// <param name="forceOccurrenceOfEachType">
    ///     Boolean choice to indicate if string of each subtype should present in the
    ///     generated random string
    /// </param>
    /// <returns>A list of random strings</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <c>count</c> or <c>maxLength</c> is less then or equal to 0</exception>
    public List<string> GetStrings(CharClasses charClasses, int count, int maxLength = 10, bool randomLength = false,
        bool forceUnique = false, bool forceOccurrenceOfEachType = false) => GetRandomStrings(charClasses.GetStrings(), count,
        maxLength, randomLength, forceUnique, forceOccurrenceOfEachType);

    /// <summary>
    ///     Generates a list of random strings of input type <c>charClasses</c> having a maximum string length of <c>maxLength</c>
    /// </summary>
    /// <param name="charClasses">Type of RandomString4Net.CharClasses is the type of input string for random string generation</param>
    /// <param name="count">Number of random strings to be generated</param>
    /// <param name="symbolsToInclude">Subset of symbols from the list of supported symbols, specified as a string</param>
    /// <param name="maxLength">Maximum length of a random string to be generated; default is 10</param>
    /// <param name="randomLength">Boolean choice if the length of the generated random string should be random as well</param>
    /// <param name="forceUnique">
    ///     Boolean choice to force generation of only unique numbers, count may not be met if this is
    ///     set to true
    /// </param>
    /// <param name="forceOccurrenceOfEachType">
    ///     Boolean choice to indicate if string of each subtype should present in the
    ///     generated random string
    /// </param>
    /// <returns>A list of random strings</returns>
    /// <exception cref="UnsupportedSymbolException">
    ///     Thrown when the input subset of string is not present in the list of
    ///     supported symbols
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <c>count</c> or <c>maxLength</c> is less then or equal to 0</exception>
    public List<string> GetStrings(CharClasses charClasses, int count, string symbolsToInclude, int maxLength = 10,
        bool randomLength = false, bool forceUnique = false, bool forceOccurrenceOfEachType = false)
    {
        UtilityMethods.ValidateSymbols(symbolsToInclude);

        // excluding symbols as custom symbols are specified
        charClasses &= SymbolMask;

        var classCount = charClasses.GetClassCount();
        var inputStrings = new string[classCount + 1];
        Array.Copy(charClasses.GetStrings(), inputStrings, classCount);

        // classCount is the last element since array size is classCount + 1
        // if we drop netstandard2.0 support, we can use the indexer ^1
        inputStrings[classCount] = symbolsToInclude;

        return GetRandomStrings(inputStrings, count, maxLength, randomLength, forceUnique,
            forceOccurrenceOfEachType);
    }

    /// <summary>
    ///     Method responsible for generating random strings
    /// </summary>
    /// <param name="inputStrings">Strings whose characters are to be used for generating random strings</param>
    /// <param name="count">Number of random string to generate</param>
    /// <param name="maxLength">Maximum length of random string</param>
    /// <param name="randomLength">Boolean choice if the length of the generated random string should be random as well</param>
    /// <param name="forceUnique">
    ///     Boolean choice to force generation of only unique numbers, count may not be met if this is
    ///     set to true
    /// </param>
    /// <param name="forceOccurrenceOfEachType">
    ///     Boolean choice to indicate if string of each subtype should present in the
    ///     generated random string
    /// </param>
    /// <returns>A list of random strings</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <c>count</c> or <c>maxLength</c> is less then or equal to 0</exception>
    private List<string> GetRandomStrings(string[] inputStrings, int count, int maxLength, bool randomLength,
        bool forceUnique, bool forceOccurrenceOfEachType)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero");
        }

        if (maxLength <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength), "Length must be greater than zero");
        }

        return GetRandomStringsInternal(inputStrings, count, maxLength, randomLength, forceUnique,
            forceOccurrenceOfEachType);
    }

    private List<string> GetRandomStringsInternal(string[] inputStrings, int count, int maxLength,
        bool randomLength, bool forceUnique, bool forceOccurrence)
    {
        if (maxLength < inputStrings.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength),
                "Length must be at least equal to the specified character classes");
        }

        var results = new List<string>();
        var uniqueStrings = new HashSet<string>();

        if (forceOccurrence)
            maxLength -= inputStrings.Length;

        var source = string.Join("", inputStrings);

        var currentRandomString = new StringBuilder();
        for (var i = 0; i < count; i++)
        {
            var outputStringLength = randomLength ? _randomSource.Next(1, maxLength) : maxLength;
            GenerateRandomString(source, outputStringLength, currentRandomString);

            if (forceOccurrence)
            {
                // Ensure at least one character from each character class
                foreach (var input in inputStrings)
                {
                    var index = _randomSource.Next(currentRandomString.Length);
                    currentRandomString.Insert(index, input[_randomSource.Next(input.Length)]);
                }
            }

            var randomString = currentRandomString.ToString();
            if (!forceUnique || uniqueStrings.Add(randomString))
            {
                results.Add(randomString);
            }

            currentRandomString.Clear();
        }

        return results;
    }

    private void GenerateRandomString(string source, int length, StringBuilder dest)
    {
        for (var i = 0; i < length; ++i)
        {
            dest.Append(source[_randomSource.Next(source.Length)]);
        }
    }
}