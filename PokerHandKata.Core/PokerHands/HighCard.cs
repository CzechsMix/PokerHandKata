using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public class HighCard : PokerHand
{
	private readonly List<PlayingCard> _orderedCards;

	public override bool Beats(PokerHand opponent)
	{
		if (opponent is not HighCard opposingHighCard)
		{
			return false;
		}

		for (int i = 0; i < 5; i++)
		{
			var myCard = _orderedCards[i];
			var opponentsCard = opposingHighCard._orderedCards[i];

			if (myCard.Beats(opponentsCard))
			{
				return true;
			}
		}

		return false;
	}

	public HighCard(FiveCardHand hand)
	{
		_orderedCards = hand
			.OrderByDescending(card => card.AceHighValue())
			.ToList();
	}
}