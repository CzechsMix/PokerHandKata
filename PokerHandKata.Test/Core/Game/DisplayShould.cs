using PokerHandKata.Core.PlayingCards;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.Game;

public class DisplayShould : TestWithErrorOutput
{
	public DisplayShould(ITestOutputHelper output)
		: base(output) { }

	[Theory]
	[InlineData("2♠", "🂢")]
	[InlineData("2♥", "🂲")]
	[InlineData("4♦", "🃄")]
	[InlineData("J♣", "🃛")]
	public void DisplayCardCorrectly(
		string cardString,
		string expectedDisplay)
	{
		var card = PlayingCard.From(cardString, Error);

		card.ShouldNotBeNull();

		var display = DisplayCard.AsUnicodeString(card!);
		display.ShouldBe(expectedDisplay);
	}
}
