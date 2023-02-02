using PokerHandKata.Core.PlayingCards;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PlayingCards;

public class SuitsShould : TestWithErrorOutput
{
    public SuitsShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData('♠')]
    [InlineData('♣')]
    [InlineData('♥')]
    [InlineData('♦')]
    public void ParseValidValues(char suitCharacter)
    {
        var suit = Suit.From(suitCharacter, _ => { });
        suit.ShouldNotBeNull();
    }

    [Fact]
    public void FailToParseInvalidValues()
    {
        // Test some common printable characters
        for (char candidate = ' '; candidate <= '~'; candidate++)
        {
            // Skip the valid values
            if ("♠♣♥♦".Contains(candidate))
            {
                continue;
            }

            var suit = Suit.From(candidate, Error);
            suit.ShouldBeNull();
        }
    }

    [Theory]
    [InlineData('♠')]
    [InlineData('♣')]
    [InlineData('♥')]
    [InlineData('♦')]
    public void HaveValueEquality(char suitCharacter)
    {
        var oneSuit = Suit.From(suitCharacter, Error);
        var theSameSuit = Suit.From(suitCharacter, Error);

        oneSuit.ShouldBe(theSameSuit);
    }
}
