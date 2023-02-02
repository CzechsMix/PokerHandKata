using PokerHandKata.Core.PlayingCards;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PlayingCards;

public class RanksShould : TestWithErrorOutput
{
    public RanksShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    [InlineData("6")]
    [InlineData("7")]
    [InlineData("8")]
    [InlineData("9")]
    [InlineData("10")]
    [InlineData("J")]
    [InlineData("Q")]
    [InlineData("K")]
    [InlineData("A")]
    public void ParseFromValidShortStrings(string rankShortString)
    {
        var rank = Rank.From(rankShortString, Error);
        rank.ShouldNotBeNull();
    }

    [Theory]
    [InlineData("BCDEFGHILMNOPRSTUVWXYZ")]
    public void FailToParseFromInvalidCharacters(string invalidCharacters)
    {
        foreach (var invalidCharacter in invalidCharacters)
        {
            var rankShortString = "" + invalidCharacter;
            var rank = Rank.From(rankShortString, Error);
            rank.ShouldBeNull();
        }
    }
}
