#if NET8_0_OR_GREATER
using System.Buffers;
#else
using System.Text.RegularExpressions;
#endif
using StuceSoftware.RandomStringGenerator.Exceptions;

namespace StuceSoftware.RandomStringGenerator;

internal static class UtilityMethods
{
#if NET8_0_OR_GREATER
    private static readonly SearchValues<char> SymbolSearcher = SearchValues.Create(DataSource.Symbols);

    internal static void ValidateSymbols(ReadOnlySpan<char> inputSymbols)
    {
        if (inputSymbols.IsEmpty || inputSymbols.ContainsAnyExcept(SymbolSearcher))
        {
            throw new UnsupportedSymbolException($"Input symbols should be a subset of: {DataSource.Symbols}");
        }
    }
#else
    private static readonly Regex SymbolValidator = new($@"^[{DataSource.Symbols}]+$", RegexOptions.Compiled);

    internal static void ValidateSymbols(string inputSymbols)
    {
        if (string.IsNullOrEmpty(inputSymbols) || !SymbolValidator.IsMatch(inputSymbols))
        {
            throw new UnsupportedSymbolException($"Input symbols should be a subset of: {DataSource.Symbols}");
        }
    }
#endif

#if !NET9_0_OR_GREATER
    public static unsafe string Concat(ReadOnlySpan<string?> strings)
    {
        if (strings.Length <= 1)
        {
            return strings.IsEmpty ? string.Empty : strings[0] ?? string.Empty;
        }

        var finalCharacters = stackalloc char[DataSource.MaxLength];
        var tempSpan = new Span<char>(finalCharacters, DataSource.MaxLength);

        var finalLength = 0;
        foreach (var s in strings)
        {
            var current = s.AsSpan();
            current.CopyTo(tempSpan);
            finalLength += current.Length;
            tempSpan = tempSpan[current.Length..];
        }

        return new string(finalCharacters, 0, finalLength);
    }
#endif
}