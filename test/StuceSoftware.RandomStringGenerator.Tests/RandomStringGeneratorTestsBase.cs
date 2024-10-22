//
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

using FluentAssertions;
using StuceSoftware.RandomStringGenerator.Exceptions;
using System;
using System.ComponentModel;
using System.Linq;

namespace StuceSoftware.RandomStringGenerator.Tests;

public abstract class RandomStringGeneratorTestsBase
{
    private readonly RandomStringGenerator _generator;

    protected RandomStringGeneratorTestsBase(RandomStringGenerator generator) => _generator = generator;

    [Fact]
    public void ValidateSingleRandomString()
    {
        var randomString = _generator.GetString(CharClasses.Lowercase);

        randomString.Should().NotBeNullOrEmpty();
        randomString.Length.Should().BeLessThanOrEqualTo(10);
    }

    [Fact]
    public void ValidateSingleRandomStringWithSymbols()
    {
        var randomString = _generator.GetString(CharClasses.Lowercase, "+*-/");

        randomString.Should().MatchRegex(@"^[a-z+*-/]+$");
    }

    [Fact]
    public void ValidateSingleRandomStringWithBadSymbols()
    {
        var action = () => _generator.GetString(CharClasses.Lowercase, "+*-/₹");

        action.Should().Throw<UnsupportedSymbolException>();
    }

    [Fact]
    public void ValidateSingleRandomStringOfInvalidLength()
    {
        var action = () => _generator.GetString(CharClasses.Lowercase, 0);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ValidateMultipleRandomStrings()
    {
        var randomStrings = _generator.GetStrings(CharClasses.Lowercase, 5);

        randomStrings.Should().HaveCount(5);
    }

    [Fact]
    public void ValidateMultipleRandomStringsOfBadCount()
    {
        var action = () => _generator.GetStrings(CharClasses.Lowercase, 0);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ValidateMultipleRandomStringsWithSymbols()
    {
        var randomStrings = _generator.GetStrings(CharClasses.Uppercase, 2, "+*-/");

        randomStrings.Should().AllSatisfy(s => s.Should().MatchRegex(@"^[A-Z+*-/]+$"));
    }

    [Fact]
    public void ValidateLowercaseAlphabet()
    {
        var randomString = _generator.GetString(CharClasses.Lowercase);

        randomString.Should().MatchRegex(@"^[a-z]+$");
    }

    [Fact]
    public void ValidateUppercaseAlphabet()
    {
        var randomString = _generator.GetString(CharClasses.Uppercase);

        randomString.Should().MatchRegex(@"^[A-Z]+$");
    }

    [Fact]
    public void ValidateMixedCaseAlphabet()
    {
        var randomString = _generator.GetString(CharClasses.Uppercase | CharClasses.Lowercase);

        randomString.Should().MatchRegex(@"^[A-Za-z]+$");
    }

    [Fact]
    public void ValidateLowercaseAlphabetWithCustomSymbols()
    {
        var randomString = _generator.GetString(CharClasses.Lowercase, "*&^%$#@!", maxLength: 100);

        randomString.Should().MatchRegex(@"^[a-z*&^%$#@!]+$");
    }

    [Fact]
    public void ValidateUppercaseAlphabetWithCustomSymbols()
    {
        var randomString = _generator.GetString(CharClasses.Uppercase, "*&^%$#@!", maxLength: 100);

        randomString.Should().MatchRegex(@"^[A-Z*&^%$#@!]+$");
    }

    [Fact]
    public void ValidateMixedCaseAlphabetWithCustomSymbols()
    {
        var randomString = _generator.GetString(CharClasses.Uppercase | CharClasses.Lowercase, "*&^%$#@!", maxLength: 100);

        randomString.Should().MatchRegex(@"^[a-zA-Z*&^%$#@!]+$");
    }

    [Fact]
    public void ValidateLowercaseAlphaNumericWithSymbols()
    {
        var randomString = _generator.GetString(CharClasses.Lowercase | CharClasses.Numbers | CharClasses.Symbols, maxLength: 100);

        randomString.Should().MatchRegex(@"^[0-9a-z!#$%&'()*+,-./:;<=>?@[\]\\^_`{|}~""]+$");
    }

    [Fact]
    public void ValidateUppercaseAlphaNumericWithSymbols()
    {
        var randomString = _generator.GetString(CharClasses.Uppercase | CharClasses.Numbers | CharClasses.Symbols, maxLength: 100);

        randomString.Should().MatchRegex(@"^[0-9A-Z!#$%&'()*+,-./:;<=>?@[\]\\^_`{|}~""]+$");
    }

    [Fact]
    public void ValidateMixedCaseAlphaNumericWithSymbols()
    {
        var randomString = _generator.GetString(CharClasses.Uppercase | CharClasses.Lowercase | CharClasses.Numbers | CharClasses.Symbols, maxLength: 100);

        randomString.Should().MatchRegex(@"^[0-9a-zA-Z!#$%&'()*+,-./:;<=>?@[\]\\^_`{|}~""]+$");
    }

    [Fact]
    public void ValidateRandomNumbers()
    {
        var randomStrings = _generator.GetStrings(CharClasses.Numbers, 10);

        randomStrings.Should().AllSatisfy(s => s.Should().MatchRegex("^[0-9]+$"));
    }

    [Fact]
    public void ValidateRandomNumbersOfMaxLength()
    {
        var randomStrings = _generator.GetStrings(CharClasses.Numbers, 10, 20);

        randomStrings.Should().AllSatisfy(s =>
            s.Should().HaveLength(20).And
             .MatchRegex(@"^[0-9]+$")
        );
    }

    [Fact]
    public void ValidateRandomNumbersOfRandomLength()
    {
        var randomStrings = _generator.GetStrings(CharClasses.Numbers, 10, 15, true);

        randomStrings.Should().AllSatisfy(s =>
        {
            s.Length.Should().BeInRange(1, 15);
            s.Should().MatchRegex(@"^[0-9]+$");
        });
    }

    [Fact]
    public void ValidateForceUnique()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Numbers, count: 2000, maxLength: 3, randomLength: false, forceUnique: true);

            randomStrings.Should()
                         .HaveCountLessThan(1000).And
                         .OnlyHaveUniqueItems();
        }
    }

    [Fact]
    public void ValidateDuplicatesWhenNoForceUnique()
    {
        var randomStrings = _generator.GetStrings(CharClasses.Numbers, count: 2000, maxLength: 3, randomLength: false, forceUnique: false);

        randomStrings.Should().HaveCount(2000);
        randomStrings.Distinct().Count().Should().BeLessThanOrEqualTo(2000);
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphabetWithCustomSymbols()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Lowercase | CharClasses.Symbols, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[a-z])(?=.*[*&^%$#@!])[a-z*&^%$#@!]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphabetUppercaseWithCustomSymbols()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Uppercase | CharClasses.Symbols, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[A-Z])(?=.*[*&^%$#@!])[A-Z*&^%$#@!]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphaNumericLowercase()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Lowercase | CharClasses.Numbers, count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[a-z])(?=.*[0-9])[a-z0-9]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphaNumericLowercaseWithCustomSymbols()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Lowercase | CharClasses.Numbers, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[a-z])(?=.*[0-9])(?=.*[*&^%$#@!])[a-z0-9*&^%$#@!]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphaNumericUppercase()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Uppercase | CharClasses.Numbers, count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[A-Z])(?=.*[0-9])[A-Z0-9]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphaNumericUppercaseWithCustomSymbols()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Uppercase | CharClasses.Numbers | CharClasses.Symbols, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[*&^%$#@!])[A-Z0-9*&^%$#@!]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphaNumericMixedCase()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Lowercase | CharClasses.Uppercase | CharClasses.Numbers, count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphaNumericMixedCaseWithCustomSymbols()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Lowercase | CharClasses.Uppercase | CharClasses.Numbers | CharClasses.Symbols, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[*&^%$#@!])[a-zA-Z0-9*&^%$#@!]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphaNumericMixedCaseWithoutCustomSymbols()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = _generator.GetStrings(CharClasses.Lowercase | CharClasses.Uppercase | CharClasses.Numbers | CharClasses.Symbols, count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!#$%&'()*+,-.:;<=>?@[\]/\\|^_`{}~""])[a-zA-Z0-9!#$%&'()*+,-.:;<=>?@[\]/\\|^_`{}~""]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateGetStringForceOccurrenceForTypesThrowsExceptionWhenMaxLengthLessThanTypes()
    {
        var action = () => _generator.GetString(CharClasses.Lowercase | CharClasses.Uppercase | CharClasses.Numbers | CharClasses.Symbols, maxLength: 3, forceOccurrenceOfEachType: true);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ValidateGetStringsForceOccurrenceForTypesThrowsExceptionWhenMaxLengthLessThanTypes()
    {
        var action = () => _generator.GetStrings(CharClasses.Lowercase | CharClasses.Uppercase | CharClasses.Numbers | CharClasses.Symbols, count: 10, maxLength: 3, forceOccurrenceOfEachType: true );

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ValidateGetStringWithInvalidCharClassThrowsException()
    {
        var action = () => _generator.GetString(charClasses: 0);

        action.Should().Throw<InvalidEnumArgumentException>();
    }
}