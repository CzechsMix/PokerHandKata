using PokerHandKata.Core.PlayingCards;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PlayingCards;

public class PlayingCardsShould : TestWithErrorOutput
{
    public PlayingCardsShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("A♥", true)]
    [InlineData("1♥", false)]
    [InlineData("AP", false)]
    [InlineData("1P", false)]
    public void ParseFromValidStringsOnly(
        string cardString,
        bool expectedValidity)
    {
        var card = PlayingCard.From(cardString, Error);
        var actualValidity = card is not null;
        actualValidity.ShouldBe(expectedValidity);
    }

    [Theory]
    [InlineData("A♥", "J♥", true)]
    [InlineData("A♥", "A♦", false)]
    [InlineData("K♥", "A♥", false)]
    [InlineData("10♠", "3♣", true)]
    public void DeterminIfOneCardBeatsAnother(
        string oneCardString,
        string anotherCardString,
        bool expectedOneBeatsAnother)
    {
        var oneCard = PlayingCard.From(oneCardString, Error)!;
        var anotherCard = PlayingCard.From(anotherCardString, Error)!;

        var actualOneBeatsAnother = oneCard.Beats(anotherCard);
        actualOneBeatsAnother.ShouldBe(expectedOneBeatsAnother);
    }

}
