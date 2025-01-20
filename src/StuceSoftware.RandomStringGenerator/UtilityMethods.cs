using StuceSoftware.RandomStringGenerator.Exceptions;
#if !NETSTANDARD2_0
using System.Buffers;
#else
using System.Text.RegularExpressions;
#endif

namespace StuceSoftware.RandomStringGenerator;
internal static class UtilityMethods
{
#if !NETSTANDARD2_0
    private static readonly SearchValues<char> SymbolSearcher = SearchValues.Create(DataSource.Symbols);

    internal static void ValidateSymbols(ReadOnlySpan<char> inputSymbols)
    {
        if (inputSymbols.IsEmpty || inputSymbols.ContainsAnyExcept(SymbolSearcher))
        {
            throw new UnsupportedSymbolException($"Input symbols should be a subset of: {DataSource.Symbols}");
        }
    }
#else
    internal static void ValidateSymbols(string inputSymbols)
    {
        if (string.IsNullOrEmpty(inputSymbols) || !Regex.IsMatch(inputSymbols, $@"^[{DataSource.Symbols}]+$"))
        {
            throw new UnsupportedSymbolException($"Input symbols should be a subset of: {DataSource.Symbols}");
        }
    }
#endif
}
