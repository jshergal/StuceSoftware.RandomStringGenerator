using FluentAssertions;
using StuceSoftware.RandomStringGenerator.Exceptions;
using System;
using System.Linq;

namespace StuceSoftware.RandomStringGenerator.Tests;

public class RandomStringGeneratorTests
{
    [Fact]
    public void ValidateTypeGetString()
    {
        var numbersAsString = Types.NUMBERS.GetString();

        numbersAsString.Should().Satisfy(
            s => s == "0123456789"
        );
    }

    [Fact]
    public void ValidateSingleRandomString()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_LOWERCASE);

        randomString.Should().NotBeNullOrEmpty();
        randomString.Length.Should().BeLessThanOrEqualTo(10);
    }

    [Fact]
    public void ValidateSingleRandomStringWithSymbols()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_LOWERCASE, "+*-/");

        randomString.Should().MatchRegex(@"^[a-z+*-/]+$");
    }

    [Fact]
    public void ValidateSingleRandomStringWithBadSymbols()
    {
        var action = () => RandomStringGenerator.GetString(Types.ALPHABET_LOWERCASE, "+*-/₹");

        action.Should().Throw<UnsupportedSymbolException>();
    }

    [Fact]
    public void ValidateSingleRandomStringOfInvalidLength()
    {
        var action = () => RandomStringGenerator.GetString(Types.ALPHABET_LOWERCASE, 0);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ValidateMultipleRandomStrings()
    {
        var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHABET_LOWERCASE, 5);

        randomStrings.Should().HaveCount(5);
    }

    [Fact]
    public void ValidateMultipleRandomStringsOfBadCount()
    {
        var action = () => RandomStringGenerator.GetStrings(Types.ALPHABET_LOWERCASE, 0);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ValidateMultipleRandomStringsWithSymbols()
    {
        var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHABET_UPPERCASE, 2, "+*-/");

        randomStrings.Should().AllSatisfy(s => s.Should().MatchRegex(@"^[A-Z+*-/]+$"));
    }

    [Fact]
    public void ValidateLowercaseAlphabet()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_LOWERCASE);

        randomString.Should().MatchRegex(@"^[a-z]+$");
    }

    [Fact]
    public void ValidateUppercaseAlphabet()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_UPPERCASE);

        randomString.Should().MatchRegex(@"^[A-Z]+$");
    }

    [Fact]
    public void ValidateMixedCaseAlphabet()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_MIXEDCASE);

        randomString.Should().MatchRegex(@"^[A-Za-z]+$");
    }

    [Fact]
    public void ValidateLowercaseAlphabetWithCustomSymbols()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_LOWERCASE, "*&^%$#@!", maxLength: 100);

        randomString.Should().MatchRegex(@"^[a-z*&^%$#@!]+$");
    }

    [Fact]
    public void ValidateUppercaseAlphabetWithCustomSymbols()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_UPPERCASE, "*&^%$#@!", maxLength: 100);

        randomString.Should().MatchRegex(@"^[A-Z*&^%$#@!]+$");
    }

    [Fact]
    public void ValidateMixedCaseAlphabetWithCustomSymbols()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_MIXEDCASE, "*&^%$#@!", maxLength: 100);

        randomString.Should().MatchRegex(@"^[a-zA-Z*&^%$#@!]+$");
    }

    [Fact]
    public void ValidateLowercaseAlphaNumericWithSymbols()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHANUMERIC_LOWERCASE_WITH_SYMBOLS, maxLength: 100);

        randomString.Should().MatchRegex(@"^[0-9a-z!#$%&'()*+,-./:;<=>?@[\]\\^_`{|}~""]+$");
    }

    [Fact]
    public void ValidateUppercaseAlphaNumericWithSymbols()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHANUMERIC_UPPERCASE_WITH_SYMBOLS, maxLength: 100);

        randomString.Should().MatchRegex(@"^[0-9A-Z!#$%&'()*+,-./:;<=>?@[\]\\^_`{|}~""]+$");
    }

    [Fact]
    public void ValidateMixedCaseAlphaNumericWithSymbols()
    {
        var randomString = RandomStringGenerator.GetString(Types.ALPHABET_MIXEDCASE_WITH_SYMBOLS, maxLength: 100);

        randomString.Should().MatchRegex(@"^[0-9a-zA-Z!#$%&'()*+,-./:;<=>?@[\]\\^_`{|}~""]+$");
    }

    [Fact]
    public void ValidateRandomNumbers()
    {
        var randomStrings = RandomStringGenerator.GetStrings(Types.NUMBERS, 10);

        randomStrings.Should().AllSatisfy(s => s.Should().MatchRegex("^[0-9]+$"));
    }

    [Fact]
    public void ValidateRandomNumbersOfMaxLength()
    {
        var randomStrings = RandomStringGenerator.GetStrings(Types.NUMBERS, 10, 20);

        randomStrings.Should().AllSatisfy(s =>
            s.Should().HaveLength(20).And
             .MatchRegex(@"^[0-9]+$")
        );
    }

    [Fact]
    public void ValidateRandomNumbersOfRandomLength()
    {
        var randomStrings = RandomStringGenerator.GetStrings(Types.NUMBERS, 10, 15, true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.NUMBERS, count: 2000, maxLength: 3, randomLength: false, forceUnique: true);

            randomStrings.Should()
                         .HaveCountLessThan(1000).And
                         .OnlyHaveUniqueItems();
        }
    }

    [Fact]
    public void ValidateDuplicatesWhenNoForceUnique()
    {
        var randomStrings = RandomStringGenerator.GetStrings(Types.NUMBERS, count: 2000, maxLength: 3, randomLength: false, forceUnique: false);

        randomStrings.Should().HaveCount(2000);
        randomStrings.Distinct().Count().Should().BeLessThanOrEqualTo(2000);
    }

    [Fact]
    public void ValidateForceOccurrenceForAlphabetWithCustomSymbols()
    {
        for (var i = 0; i < 100; i++)
        {
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHABET_LOWERCASE_WITH_SYMBOLS, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHABET_UPPERCASE_WITH_SYMBOLS, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHANUMERIC_LOWERCASE, count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHANUMERIC_LOWERCASE_WITH_SYMBOLS, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHANUMERIC_UPPERCASE, count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHANUMERIC_UPPERCASE_WITH_SYMBOLS, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHANUMERIC_MIXEDCASE, count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHANUMERIC_MIXEDCASE_WITH_SYMBOLS, symbolsToInclude: "*&^%$#@!", count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

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
            var randomStrings = RandomStringGenerator.GetStrings(Types.ALPHANUMERIC_MIXEDCASE_WITH_SYMBOLS, count: 1000, maxLength: 20, forceUnique: false, forceOccurrenceOfEachType: true);

            randomStrings.Should().AllSatisfy(s =>
                s.Should().HaveLength(20).And
                 .MatchRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!#$%&'()*+,-.:;<=>?@[\]/\\|^_`{}~""])[a-zA-Z0-9!#$%&'()*+,-.:;<=>?@[\]/\\|^_`{}~""]{20}$")
                 );
        }
    }

    [Fact]
    public void ValidateGetStringForceOccurrenceForTypesThrowsExceptionWhenMaxLengthLessThanTypes()
    {
        var action = () => RandomStringGenerator.GetString(Types.ALPHANUMERIC_MIXEDCASE_WITH_SYMBOLS, maxLength: 3, forceOccuranceOfEachType: true);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ValidateGetStringsForceOccurrenceForTypesThrowsExceptionWhenMaxLengthLessThanTypes()
    {
        var action = () => RandomStringGenerator.GetStrings(Types.ALPHANUMERIC_MIXEDCASE_WITH_SYMBOLS, count: 10, maxLength: 3, forceOccurrenceOfEachType: true );

        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}