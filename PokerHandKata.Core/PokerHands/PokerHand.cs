using PokerHandKata.Core.LanguageExtensions;
using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Core.PokerHands;

public abstract class PokerHand
{
	private readonly List<Type> handRankings = new()
	{
		typeof(HighCard),
		typeof(Pair),
		typeof(TwoPair),
		typeof(ThreeOfAKind),
		typeof(Straight),
		typeof(Flush),
		typeof(FullHouse),
		typeof(FourOfAKind),
		typeof(StraightFlush)
	};

	protected bool BeatsOutright(PokerHand opponent)
	{
		int myRank = handRankings.IndexOf(GetType());
		if (myRank < 0)
		{
			throw new ArgumentException($"Unknown Poker Hand {this.GetType().FullName}");
		}

		int opponentRank = handRankings.IndexOf(opponent.GetType());
		if (opponentRank < 0)
		{
			throw new ArgumentException($"Unknown Poker Hand {opponent.GetType().FullName}");
		}

		return myRank > opponentRank;
	}

	public abstract bool Beats(PokerHand opponent);

	public static PokerHand? From(
		IEnumerable<string> cardStrings,
		Action<string> error)
	{
		var cards = cardStrings.Select(cardString
			=> PlayingCard.From(cardString, error));

		if (cards.Any(card => card is null))
		{
			return null;
		}

		var nonNullCards = cards.NotNull();

		var fiveCardHand = FiveCardHand.From(nonNullCards, error);

		if (fiveCardHand is null)
		{
			return null;
		}

		return fiveCardHand.Choose(
			StraightFlush.Check,
			FourOfAKind.Check,
			FullHouse.Check,
			Flush.Check,
			Straight.Check,
			ThreeOfAKind.Check,
			TwoPair.Check,
			Pair.Check
			) ?? new HighCard(fiveCardHand);
	}
}
