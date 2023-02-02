using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public class Flush : PokerHand
{
	private readonly PlayingCard _highCard;

	private Flush(PlayingCard highCard)
		=> _highCard = highCard;

	public override bool Beats(PokerHand opponent)
		=> opponent is Flush opposingFlush
			? _highCard.Beats(opposingFlush._highCard)
			: BeatsOutright(opponent);

	public static PokerHand? Check(
		FiveCardHand cards)
	{
		var checkSuit = cards.First().Suit;
		bool isFlush = cards.All(card => card.Suit == checkSuit);

		return isFlush
			? new Flush(cards.HighCard)
			: null;
	}
}
