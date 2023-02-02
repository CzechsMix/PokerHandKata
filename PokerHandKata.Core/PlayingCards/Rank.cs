
using System.Globalization;

namespace PokerHandKata.Core.PlayingCards;

public class Rank
{
	public static readonly Rank Two = new Rank("2", "Two");
	public static readonly Rank Three = new Rank("3", "Three");
	public static readonly Rank Four = new Rank("4", "Four");
	public static readonly Rank Five = new Rank("5", "Five");
	public static readonly Rank Six = new Rank("6", "Six");
	public static readonly Rank Seven = new Rank("7", "Seven");
	public static readonly Rank Eight = new Rank("8", "Eight");
	public static readonly Rank Nine = new Rank("9", "Nine");
	public static readonly Rank Ten = new Rank("10", "Ten");
	public static readonly Rank Jack = new Rank("J", "Jack");
	public static readonly Rank Queen = new Rank("Q", "Queen");
	public static readonly Rank King = new Rank("K", "King");
	public static readonly Rank Ace = new Rank("A", "Ace");

	public static IEnumerable<Rank> All()
	{
		yield return Two;
		yield return Three;
		yield return Four;
		yield return Five;
		yield return Six;
		yield return Seven;
		yield return Eight;
		yield return Nine;
		yield return Ten;
		yield return Jack;
		yield return Queen;
		yield return King;
		yield return Ace;
	}

	public string ShortString { get; }
	public string DisplayString { get; }

	private Rank(string shortString, string displayString)
		=> (ShortString, DisplayString) = (shortString, displayString);

	public static Rank? From(
		string rankShortString,
		Action<string> error)
	{
		var rank = rankShortString switch
		{
			"2" => Two,
			"3" => Three,
			"4" => Four,
			"5" => Five,
			"6" => Six,
			"7" => Seven,
			"8" => Eight,
			"9" => Nine,
			"10" => Ten,
			"J" => Jack,
			"Q" => Queen,
			"K" => King,
			"A" => Ace,
			_ => null
		};
		
		if (rank is null)
		{
			error($"{rankShortString} is not a valid Rank");
		}

		return rank;
	}
}
