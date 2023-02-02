using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public class Pair : PokerHand
{
	private readonly Rank _pairRank;
	private readonly List<Rank> _highCards;
	private Pair(
		Rank pairRank,
		List<Rank> highCards)
		=> (_pairRank, _highCards) = (pairRank, highCards);

	public override bool Beats(PokerHand opponent)
	{
		if (opponent is not Pair opposingPair)
		{
			return BeatsOutright(opponent);
		}

		var winOrLose = WinOrLose(_pairRank, opposingPair._pairRank);

		int highCardIndex = 0;
		while(winOrLose is null && highCardIndex < 3)
		{
			winOrLose = WinOrLose(
				_highCards[highCardIndex],
				opposingPair._highCards[highCardIndex]);
			highCardIndex++;
		}

		return winOrLose ?? false;
	}

	private bool? WinOrLose(Rank mine, Rank opponents)
		=> mine.Beats(opponents)
		? true
		: opponents.Beats(mine)
			? false
			: null;

	public static PokerHand? Check(
		FiveCardHand cards)
	{
		var groups = cards.GroupBy(card =>
			card.Rank);

		var orderedGroupsByCount = groups
			.OrderByDescending(group => group.Count())
			.ThenByDescending(group => group.Key.AceHighValue());
		var orderedGroupCounts = orderedGroupsByCount
			.Select(group => group.Count())
			.ToList();

		return orderedGroupCounts is [2, 1, 1, 1]
			? new Pair(
				pairRank: orderedGroupsByCount.First().Key,
				highCards: orderedGroupsByCount
					.Skip(1)
					.Select(group => group.Key)
					.ToList())
			: null;
	}
}
