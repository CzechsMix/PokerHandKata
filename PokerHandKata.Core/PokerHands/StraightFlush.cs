using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public class StraightFlush : PokerHand
{
	private readonly PlayingCard _highCard;

	private StraightFlush(PlayingCard highCard)
		=> _highCard = highCard;

	public override bool Beats(PokerHand opponent)
		=> opponent is StraightFlush opposingStraightFlush
		? _highCard.Beats(opposingStraightFlush._highCard)
		: BeatsOutright(opponent);

	internal static PokerHand? Check(
		FiveCardHand cards)
	{
		var checkSuit = cards.First().Suit;
		var sameSuit = cards.All(card => card.Suit == checkSuit);

		if (sameSuit is false)
		{
			return null;
		}

		var distinctOrderedCards = cards
			.DistinctBy(card => card.Rank)
			.OrderByDescending(card => card.AceHighValue())
			.ToList();
		var count = distinctOrderedCards.Count();
		var firstValue = distinctOrderedCards.First().AceHighValue();
		var lastValue = distinctOrderedCards.Last().AceHighValue();
		var inSequence = count == 5 && firstValue - lastValue == 4;

		return inSequence
			? new StraightFlush(distinctOrderedCards.First())
			: null;
	}
}
