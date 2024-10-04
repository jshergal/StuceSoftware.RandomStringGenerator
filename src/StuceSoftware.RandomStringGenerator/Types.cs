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

namespace StuceSoftware.RandomStringGenerator;

/// <summary>
///     Enum <c>Types</c> are the types of categories supported by the RandomString4Net library
/// </summary>
public enum Types
{
    AlphabetLowercase,
    AlphabetLowercaseWithSymbols,
    AlphabetUppercase,
    AlphabetUppercaseWithSymbols,
    AlphabetMixedCase,
    AlphabetMixedCaseWithSymbols,
    AlphanumericLowercase,
    AlphanumericLowercaseWithSymbols,
    AlphanumericUppercase,
    AlphanumericUppercaseWithSymbols,
    AlphanumericMixedCase,
    AlphanumericMixedCaseWithSymbols,
    Numbers
}

public static class TypesMethods
{
    /// <summary>
    ///     Returns an array of string corresponding to the enum <c>Types</c>
    /// </summary>
    /// <param name="types"><c>Types</c> for whose equivalent string needs to be constructed</param>
    /// <returns>Array of string equivalent of Enum <c>Types</c></returns>
    public static string[] GetString(this Types types)
    {
        switch (types)
        {
            case Types.AlphabetLowercase:
                return new[]
                {
                    DataSource.Alphabet
                };

            case Types.AlphabetLowercaseWithSymbols:
                return new[]
                {
                    DataSource.Alphabet,
                    DataSource.Symbols
                };

            case Types.AlphabetUppercase:
                return new[]
                {
                    DataSource.Alphabet.ToUpperInvariant()
                };

            case Types.AlphabetUppercaseWithSymbols:
                return new[]
                {
                    DataSource.Alphabet.ToUpperInvariant(),
                    DataSource.Symbols
                };

            case Types.AlphabetMixedCase:
                return new[]
                {
                    DataSource.Alphabet,
                    DataSource.Alphabet.ToUpperInvariant()
                };

            case Types.AlphabetMixedCaseWithSymbols:
                return new[]
                {
                    DataSource.Alphabet,
                    DataSource.Alphabet.ToUpperInvariant(),
                    DataSource.Symbols
                };

            case Types.AlphanumericLowercase:
                return new[]
                {
                    DataSource.Alphabet,
                    DataSource.Numbers
                };

            case Types.AlphanumericLowercaseWithSymbols:
                return new[]
                {
                    DataSource.Alphabet,
                    DataSource.Numbers,
                    DataSource.Symbols
                };

            case Types.AlphanumericUppercase:
                return new[]
                {
                    DataSource.Alphabet.ToUpperInvariant(),
                    DataSource.Numbers
                };

            case Types.AlphanumericUppercaseWithSymbols:
                return new[]
                {
                    DataSource.Alphabet.ToUpperInvariant(),
                    DataSource.Numbers,
                    DataSource.Symbols
                };

            case Types.AlphanumericMixedCase:
                return new[]
                {
                    DataSource.Alphabet,
                    DataSource.Alphabet.ToUpperInvariant(),
                    DataSource.Numbers
                };

            case Types.AlphanumericMixedCaseWithSymbols:
                return new[]
                {
                    DataSource.Alphabet,
                    DataSource.Alphabet.ToUpperInvariant(),
                    DataSource.Numbers,
                    DataSource.Symbols
                };

            case Types.Numbers:
                return new[]
                {
                    DataSource.Numbers
                };
        }

        return new[] {string.Empty};
    }
}