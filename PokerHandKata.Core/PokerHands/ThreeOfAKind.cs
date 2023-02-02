using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public class ThreeOfAKind : PokerHand
{
	private readonly Rank _tripsRank;
	private ThreeOfAKind(Rank tripsRank)
		=> _tripsRank = tripsRank;

	public override bool Beats(PokerHand opponent)
		=> opponent is ThreeOfAKind opposingTrips
		? _tripsRank.Beats(opposingTrips._tripsRank)
		: BeatsOutright(opponent);

	public static PokerHand? Check(
		FiveCardHand cards)
	{
		var oneOfTheTrips = cards
			.Take(3)
			.FirstOrDefault(card => cards.CountOf(card.Rank) == 3);

		return oneOfTheTrips is not null
			? new ThreeOfAKind(oneOfTheTrips.Rank)
			: null;
	}
}
