namespace PokerHandKata.Core.PlayingCards;

public record PlayingCard(
	Rank Rank,
	Suit Suit)
{
	public string DisplayString => $"{Rank.ShortString}{Suit.SuitCharacter}";

	public static PlayingCard? From(
		string cardString,
		Action<string> error)
	{
		var rankShortString = cardString[..^1];
		var suitCharacter = cardString.Last();

		var rank = Rank.From(rankShortString, error);
		var suit = Suit.From(suitCharacter, error);

		if (rank is null || suit is null)
		{
			error($"{cardString} is not a valid card string");
			return null;
		}

		return new PlayingCard(rank, suit);
	}
}
