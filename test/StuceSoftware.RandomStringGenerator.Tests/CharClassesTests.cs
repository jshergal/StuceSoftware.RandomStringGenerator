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

using FluentAssertions;

namespace StuceSoftware.RandomStringGenerator.Tests;
public class CharClassesTests
{
    private const string Digits = "0123456789";
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Symbols = @"!#$%&'=()<>*+,-./:;?@[\]\\^_`{|}~""";

    [Fact]
    public void WhenCharClassesNumbersGetStrings_ThenReturnsDigitString()
    {
        var numbersAsString = CharClasses.Numbers.GetStrings();

        numbersAsString.Should().Satisfy(
            s => s == Digits
        );
    }

    [Fact]
    public void WhenCharClassesLowercaseGetStrings_ThenReturnsLowercaseAlphabetString()
    {
        var lowercaseAsString = CharClasses.Lowercase.GetStrings();

        lowercaseAsString.Should().Satisfy(
            s => s == Lowercase
        );
    }

    [Fact]
    public void WhenCharClassesUppercaseGetStrings_ThenReturnsUppercaseAlphabetString()
    {
        var uppercaseAsString = CharClasses.Uppercase.GetStrings();

        uppercaseAsString.Should().Satisfy(
            s => s == Uppercase
        );
    }

    [Fact]
    public void WhenCharClassesSymbolsGetStrings_ThenReturnsSymbolsAlphabetString()
    {
        var symbolsAsString = CharClasses.Symbols.GetStrings();

        symbolsAsString.Should().Satisfy(
            s => s == Symbols
        );
    }

    [Fact]
    public void WhenCharClassesSymbolsAndNumbersGetStrings_ThenReturnsSymbolsAndNumbers()
    {
        const CharClasses numbersAndSymbols = CharClasses.Numbers | CharClasses.Symbols;
        var results = numbersAndSymbols.GetStrings();

        results.Should().HaveCount(2).And
               .OnlyHaveUniqueItems().And
               .IntersectWith(new[] {Digits, Symbols});
    }

    [Fact]
    public void WhenCharClassesLowerAndUppercaseGetStrings_ThenReturnsLowerAndUppercase()
    {
        const CharClasses lowerAndUppercase = CharClasses.Lowercase | CharClasses.Uppercase;
        var results = lowerAndUppercase.GetStrings();

        results.Should().HaveCount(2).And
               .OnlyHaveUniqueItems().And
               .IntersectWith(new[] {Lowercase, Uppercase});
    }

    [Fact]
    public void WhenCharClassesAllGetStrings_ThenReturnsAllClasses()
    {
        const CharClasses allCharClasses = CharClasses.Numbers | CharClasses.Symbols | CharClasses.Lowercase | CharClasses.Uppercase;
        var results = allCharClasses.GetStrings();

        results.Should().HaveCount(4).And
               .OnlyHaveUniqueItems().And
               .IntersectWith(new[] { Lowercase, Uppercase, Digits, Symbols });
    }
}
