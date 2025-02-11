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

using System.ComponentModel;

namespace StuceSoftware.RandomStringGenerator;

/// <summary>
///     Enum <c>CharClasses</c> are the types of categories supported by the RandomString4Net library
/// </summary>
[Flags]
public enum CharClasses
{
    Lowercase = 1 << 0,
    Uppercase = 1 << 1,
    Numbers = 1 << 2,
    Symbols = 1 << 3,
}

public static class CharClassesHelpers
{
    private static readonly string UpperCase = DataSource.Alphabet.ToUpperInvariant();

    /// <summary>
    ///     Returns an array of string corresponding to the enum <c>CharClasses</c>
    /// </summary>
    /// <param name="charClass"><c>CharClasses</c> for whose equivalent string needs to be constructed</param>
    /// <returns>Array of string equivalent of Enum <c>CharClasses</c></returns>
    public static string[] GetStrings(this CharClasses charClass)
    {
        var dest = new string[GetClassCount(charClass)];
        var index = 0;

        if (charClass.HasFlag(CharClasses.Lowercase))
        {
            dest[index++] = DataSource.Alphabet;
        }

        if (charClass.HasFlag(CharClasses.Uppercase))
        {
            dest[index++] = UpperCase;
        }

        if (charClass.HasFlag(CharClasses.Numbers))
        {
            dest[index++] = DataSource.Numbers;
        }

        if (charClass.HasFlag(CharClasses.Symbols))
        {
            dest[index++] = DataSource.Symbols;
        }

        if (index == 0)
            throw new InvalidEnumArgumentException(nameof(charClass));

        return dest;
    }

    public static int GetClassCount(this CharClasses charClass)
    {
#if !NETSTANDARD
        return System.Numerics.BitOperations.PopCount((uint) charClass);
#else
        return SoftwareFallbackPopCount((uint)charClass);
#endif
    }

#if NETSTANDARD
    // Taken from .NET Source code file BitOperations.cs, under the MIT License
    // https://github.com/dotnet/runtime/blob/ec118c7e798862fd69dc7fa6544c0d9849d32488/src/libraries/System.Private.CoreLib/src/System/Numerics/BitOperations.cs#L452
    //
    // Licensed to the .NET Foundation under one or more agreements.
    // The .NET Foundation licenses this file to you under the MIT license.
    //
    private static int SoftwareFallbackPopCount(uint value)
    {
        const uint c1 = 0x_55555555u;
        const uint c2 = 0x_33333333u;
        const uint c3 = 0x_0F0F0F0Fu;
        const uint c4 = 0x_01010101u;

        value -= (value >> 1) & c1;
        value = (value & c2) + ((value >> 2) & c2);
        value = (((value + (value >> 4)) & c3) * c4) >> 24;

        return (int)value;
    }
#endif
}