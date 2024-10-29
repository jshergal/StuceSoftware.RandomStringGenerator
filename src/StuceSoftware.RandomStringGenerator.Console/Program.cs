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

using System.Diagnostics;
using StuceSoftware.RandomStringGenerator;
using StuceSoftware.RandomStringGenerator.RandomSourceImplementations;

var randomStringGenerator = new RandomStringGenerator(new SystemRandomSource());

var start = Stopwatch.GetTimestamp();
var randomString = randomStringGenerator.GetString(CharClasses.Lowercase);
var elapsedTicks = ElapsedTicks(start);

Console.WriteLine($"Generated 1 random string, time taken = {ElapsedMicroseconds(elapsedTicks):N} µs");
Console.WriteLine(randomString);

for (var i = 1; i <= 3; i++)
{
    Console.WriteLine("\n");
    start = Stopwatch.GetTimestamp();
    var randomStrings = randomStringGenerator.GetStrings(CharClasses.Numbers, i * 10);
    elapsedTicks = ElapsedTicks(start);
    Console.WriteLine(
        $"Generated a list of {i * 10} random strings of Type Numbers, time taken = {ElapsedMicroseconds(elapsedTicks):N} µs");
    randomStrings.ForEach(str => Console.Write(str + ", "));
}

for (var i = 1; i <= 3; i++)
{
    Console.WriteLine("\n");
    start = Stopwatch.GetTimestamp();
    var randomStrings =
        randomStringGenerator.GetStrings(CharClasses.Numbers | CharClasses.Lowercase | CharClasses.Uppercase,
            i * 10, "+-*/", 20, forceUnique: true);
    elapsedTicks = ElapsedTicks(start);
    Console.WriteLine(
        $"Generated a list of {randomStrings.Count} unique random strings of type AlphanumericMixedCase " +
        $"with custom symbols, time taken = {ElapsedMicroseconds(elapsedTicks):N} µs");
    randomStrings.ForEach(str => Console.Write(str + ", "));
}

for (var i = 1; i <= 3; i++)
{
    Console.WriteLine("\n");
    start = Stopwatch.GetTimestamp();
    var randomStrings = randomStringGenerator.GetStrings(CharClasses.Numbers | CharClasses.Lowercase | CharClasses.Uppercase | CharClasses.Symbols, 
        i * 10, "+-*/", 20,
        forceUnique: true, forceOccurrenceOfEachType: true);
    elapsedTicks = ElapsedTicks(start);
    Console.WriteLine(
        $"Generated a list of {randomStrings.Count} random strings of type AlphanumericMixedCaseWithSymbols with custom symbols " +
        $"and forced occurrence of each type, time taken = {ElapsedMicroseconds(elapsedTicks):N} µs");
    randomStrings.ForEach(str => Console.Write(str + ", "));
}

Console.WriteLine("\n\nGenerating 100000 random string and checking for duplicates 100 times");
var totalElapsedTicks = 0L;
for (var i = 0; i < 100; i++)
{
    start = Stopwatch.GetTimestamp();
    var randomNumbers = randomStringGenerator.GetStrings(CharClasses.Numbers, 100000, 10, false, true);
    totalElapsedTicks += ElapsedTicks(start);

    var anyDuplicate = randomNumbers.GroupBy(x => x).Any(g => g.Count() > 1);
    var allUnique = randomNumbers.GroupBy(x => x).All(g => g.Count() == 1);

    Console.WriteLine($"{i + 1}. duplicate = {anyDuplicate} ; unique = {allUnique}");
}
Console.WriteLine($"Time taken = {ElapsedMilliseconds(totalElapsedTicks):N} ms");

return;

double ElapsedMilliseconds(long ticks) => ticks * (1000.0 / Stopwatch.Frequency);
double ElapsedMicroseconds(long ticks) => ticks * (1000.0 * 1000.0 / Stopwatch.Frequency);
long ElapsedTicks(long begin) => Stopwatch.GetTimestamp() - begin;
