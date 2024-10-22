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

namespace StuceSoftware.RandomStringGenerator;

public sealed class SystemRandomSource : IRandomSource
{
#if NET6_0_OR_GREATER
    private readonly Random _rand = Random.Shared;
#else
    private readonly Random _rand = new();
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Next(int maxValue) => _rand.Next(maxValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Next(int minValue, int maxValue) => _rand.Next(minValue, maxValue);
}