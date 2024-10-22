//
// StuceSoftware.RandomStringGenerator - .NET library for random string generation
//
// Copyright 2024 - Jeff Shergalis;
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

using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace StuceSoftware.RandomStringGenerator;

public sealed class SecureRandomSource : IRandomSource
{
    #if NETSTANDARD2_0
    private static readonly RandomNumberGenerator Rand = RandomNumberGenerator.Create();
    #endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Next(int maxValue) => GetInt32(maxValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Next(int minValue, int maxValue) => GetInt32(minValue, maxValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetInt32(int toExclusive)
    {
        if (toExclusive <= 0)
            throw new ArgumentOutOfRangeException(nameof(toExclusive), "Value must be greater than 0");
 
        return GetInt32(0, toExclusive);
    }

    #if NETSTANDARD2_0
    // Code taken from .NET source to support older .netstandard 2.0
    // Used under the MIT license 
    // https://github.com/dotnet/runtime/blob/741390a68500e54b11dc8958573ca900e76fd80d/src/libraries/System.Security.Cryptography/src/System/Security/Cryptography/RandomNumberGenerator.cs
    private static int GetInt32(int fromInclusive, int toExclusive)
    {
        if (fromInclusive >= toExclusive)
            throw new ArgumentException("Range of random number does not contain at least one possibility.");
 
        // The total possible range is [0, 4,294,967,295).
        // Subtract one to account for zero being an actual possibility.
        var range = (uint)toExclusive - (uint)fromInclusive - 1;
 
        // If there is only one possible choice, nothing random will actually happen, so return
        // the only possibility.
        if (range == 0)
        {
            return fromInclusive;
        }
 
        // Create a mask for the bits that we care about for the range. The other bits will be
        // masked away.
        var mask = range;
        mask |= mask >> 1;
        mask |= mask >> 2;
        mask |= mask >> 4;
        mask |= mask >> 8;
        mask |= mask >> 16;
 
        var oneUintBytes = new byte[sizeof(uint)];
        uint result;
        do
        {
            Rand.GetBytes(oneUintBytes);
            var oneUint = BitConverter.ToUInt32(oneUintBytes, 0);
            result = mask & oneUint;
        }
        while (result > range);
 
        return (int)result + fromInclusive;
    }
    #else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetInt32(int fromInclusive, int toExclusive) => RandomNumberGenerator.GetInt32(fromInclusive, toExclusive);
    #endif
}