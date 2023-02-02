using static System.Reflection.Metadata.BlobBuilder;

namespace PokerHandKata.Core.PlayingCards;

public static class DisplayCard
{
	public static string AsUnicodeString(PlayingCard card)
	{
		var heartCard = card.Rank.AsSpade();
		var offset = card.Suit.Offset();
		var display = $"{heartCard[0]}{(char)(heartCard[1] + offset)}";
		return display;
	}

	private static int Offset(this Suit suit)
		=> suit.SuitCharacter switch
		{
			'♠' => 0,
			'♥' => 16,
			'♦' => 32,
			'♣' => 48,
			_ => throw new ArgumentException($"Unknown suit: {suit.SuitCharacter}")
		};

	private static string AsSpade(this Rank rank)
		=> rank.ShortString switch
		{
			"2" => "🂢",
			"3" => "🂣",
			"4" => "🂤",
			"5" => "🂥",
			"6" => "🂦",
			"7" => "🂧",
			"8" => "🂨",
			"9" => "🂩",
			"10" => "🂪",
			"J" => "🂫",
			"Q" => "🂭",
			"K" => "🂮",
			"A" => "🂡",
			_ => throw new ArgumentException($"Unknown rank: {rank.ShortString}")
		};
}
