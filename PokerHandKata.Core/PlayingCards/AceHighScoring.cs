using PokerHandKata.Core.PokerHands;

namespace PokerHandKata.Core.PlayingCards;

public static class AceHighScoring
{
	private static readonly List<Rank> _aceHighOrder = new()
	{
		Rank.Two,
		Rank.Three,
		Rank.Four,
		Rank.Five,
		Rank.Six,
		Rank.Seven,
		Rank.Eight,
		Rank.Nine,
		Rank.Ten,
		Rank.Jack,
		Rank.Queen,
		Rank.King,
		Rank.Ace
	};

	public static int AceHighValue(
		this Rank rank)
		=> _aceHighOrder.IndexOf(rank);

	public static int AceHighValue(
		this PlayingCard card)
		=> card.Rank.AceHighValue();

	public static bool Beats(
		this PlayingCard left,
		PlayingCard right)
		=> left.AceHighValue() > right.AceHighValue();

	public static bool Beats(
		this Rank left,
		Rank right)
		=> _aceHighOrder.IndexOf(left) > _aceHighOrder.IndexOf(right);
}
