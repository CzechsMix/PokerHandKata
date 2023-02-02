using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public class FourOfAKind : PokerHand
{
	private readonly Rank _rankOfQuads;
	private FourOfAKind(Rank rankOfQuads)
		=> _rankOfQuads = rankOfQuads;

	public override bool Beats(PokerHand opponent)
		=> opponent is FourOfAKind opposingQuads
		? _rankOfQuads.Beats(opposingQuads._rankOfQuads)
		: BeatsOutright(opponent);

	public static PokerHand? Check(
		FiveCardHand cards)
	{
		var first = cards.First();
		var sameRankAsFirst = Count(cards, first.Rank);
		if (sameRankAsFirst == 4)
		{
			return new FourOfAKind(first.Rank);
		}

		var second = cards.Skip(1).First();
		var sameRankAsSecond = Count(cards, second.Rank);
		if (sameRankAsSecond == 4)
		{
			return new FourOfAKind(second.Rank);
		}

		return null;
	}

	private static int Count(
		FiveCardHand cards,
		Rank rank)
		=> cards.Count(card => card.Rank == rank);
}
