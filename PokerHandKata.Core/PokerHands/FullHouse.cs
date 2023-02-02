using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;
public class FullHouse : PokerHand
{
	private readonly Rank _tripRank;
	private FullHouse(Rank tripRank)
		=> _tripRank = tripRank;

	public override bool Beats(PokerHand opponent)
		=> opponent is FullHouse opposingFullHouse
		? _tripRank.Beats(opposingFullHouse._tripRank)
		: BeatsOutright(opponent);

	public static PokerHand? Check(
		FiveCardHand cards)
	{
		var orderedCards = cards
			.OrderByDescending(card => card.AceHighValue())
			.ToList();
		var first = orderedCards.First();
		var last = orderedCards.Last();

		var countOfFirst = orderedCards.Count(card => card.Rank == first.Rank);
		var countOfLast = orderedCards.Count(card => card.Rank == last.Rank);

		return (countOfFirst, countOfLast) switch
		{
			(3, 2) => new FullHouse(orderedCards.First().Rank),
			(2, 3) => new FullHouse(orderedCards.Last().Rank),
			_ => null
		};
	}
}
