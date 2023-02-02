using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public class Straight : PokerHand
{
	private readonly PlayingCard _highCard;
	private Straight(PlayingCard highCard)
		=> _highCard = highCard;

	public override bool Beats(PokerHand opponent)
		=> opponent is Straight opposingStraight
		? _highCard.Beats(opposingStraight._highCard)
		: BeatsOutright(opponent);

	public static PokerHand? Check(
		FiveCardHand cards)
	{
		var distinctOrderedCards = cards
			.DistinctBy(card => card.Rank)
			.OrderByDescending(card => card.AceHighValue())
			.ToList();

		var count = distinctOrderedCards.Count();
		var firstValue = distinctOrderedCards.First().AceHighValue();
		var lastValue = distinctOrderedCards.Last().AceHighValue();

		return count == 5 && firstValue - lastValue == 4
			? new Straight(distinctOrderedCards.First())
			: null;
	}
}
