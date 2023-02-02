namespace PokerHandKata.Core.PlayingCards;

public class Suit
{
	public static readonly Suit Hearts = new Suit('♥', isRed: true);
	public static readonly Suit Diamonds = new Suit('♦', isRed: true);
	public static readonly Suit Spades = new Suit('♠', isRed: false);
	public static readonly Suit Clubs = new Suit('♣', isRed: false);

	public static IEnumerable<Suit> All()
	{
		yield return Hearts;
		yield return Diamonds;
		yield return Spades;
		yield return Clubs;
	}

	public char SuitCharacter { get; }
	public bool IsRed { get; }
	public bool isBlack => IsRed is false;

	private Suit(char suitCharacter, bool isRed)
		=> (SuitCharacter, IsRed) = (suitCharacter, isRed);

	public static Suit? From(
		char suitCharacter,
		Action<string> error)
	{
		var suit = suitCharacter switch
		{
			'♥' => Hearts,
			'♦' => Diamonds,
			'♠' => Spades,
			'♣' => Clubs,
			_ => null
		};

		if (suit is null)
		{
			error($"{suitCharacter} is not a valid Suit");
		}

		return suit;
	}
}
