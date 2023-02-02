using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public class TwoPair : PokerHand
{
	private readonly Rank _highPairRank;
	private readonly Rank _lowPairRank;
	private readonly Rank _nonPairRank;

	private TwoPair(
		Rank highPairRank,
		Rank lowPairRank,
		Rank nonPairRank)
	{
		_highPairRank = highPairRank;
		_lowPairRank = lowPairRank;
		_nonPairRank = nonPairRank;
	}

	public override bool Beats(PokerHand opponent)
	{
		if (opponent is not TwoPair opposingTwoPair)
		{
			return BeatsOutright(opponent);
		}

		return WinOrLose(_highPairRank, opposingTwoPair._highPairRank)
			?? WinOrLose(_lowPairRank, opposingTwoPair._lowPairRank)
			?? _nonPairRank.Beats(opposingTwoPair._nonPairRank);
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
		var groups = cards.GroupBy(card => card.Rank);

		var orderedGroups = groups
			.OrderByDescending(group => group.Count())
			.ThenByDescending(group => group.Key.AceHighValue());

		var orderedGroupCount = orderedGroups
			.Select(group => group.Count())
			.ToList();

		return orderedGroupCount is [2, 2, 1]
			? new TwoPair(
				highPairRank: orderedGroups.First().Key,
				lowPairRank: orderedGroups.Skip(1).First().Key,
				nonPairRank: orderedGroups.Last().Key)
			: null;
	}

}
