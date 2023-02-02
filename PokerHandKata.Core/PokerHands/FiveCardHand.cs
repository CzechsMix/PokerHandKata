using PokerHandKata.Core.PlayingCards;
using System.Collections;

namespace PokerHandKata.Core.PokerHands;

public class FiveCardHand : IEnumerable<PlayingCard>
{
	private readonly PlayingCard[] _cards;
	private FiveCardHand(IEnumerable<PlayingCard> cards)
		=> _cards = cards.ToArray();

	public static FiveCardHand? From(
		IEnumerable<PlayingCard> cards,
		Action<string> error)
	{
		if (cards.Count() != 5)
		{
			error("A five card hand must have exactly 5 cards");
			return null;
		}

		var set = cards.ToHashSet();
		if (set.Count != 5)
		{
			error("Each card in a hand must be unique.");
			return null;
		}

		return new FiveCardHand(set);
	}

	public PlayingCard HighCard
		=> _cards
			.OrderByDescending(card => card.AceHighValue())
			.First();

	public int CountOf(Rank rank)
		=> _cards.Count(card => card.Rank == rank);

	public IEnumerator<PlayingCard> GetEnumerator()
		=> ((IEnumerable<PlayingCard>)_cards).GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
		=> _cards.GetEnumerator();

}
