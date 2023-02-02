using PokerHandKata.Core.LanguageExtensions;
using PokerHandKata.Core.PlayingCards;
using PokerHandKata.Core.PokerHands;
using System.Reflection.Emit;

namespace PokerHandKata.Core.Game;

public static partial class OneHandGame
{
	public static string? Play(
		PlayerData playerOneData,
		PlayerData playerTwoData,
		Action<string> error)
	{
		if (NonUniqueCards(
			playerOneData.CardStrings,
			playerTwoData.CardStrings,
			error))
		{
			return null;
		}

		var playerOne = Player.From(playerOneData, error);
		var playerTwo = Player.From(playerTwoData, error);

		if (playerOne is null
			|| playerTwo is null)
		{
			return null;
		}

		if (playerOne.Hand.Beats(playerTwo.Hand))
		{
			return playerOne.Name;
		}

		if (playerTwo.Hand.Beats(playerOne.Hand))
		{
			return playerTwo.Name;
		}

		return "Tie";
	}

	private static bool NonUniqueCards(
		IEnumerable<string> playerOneCardStrings,
		IEnumerable<string> playerTwoCardStrings,
		Action<string> error)
	{
		var playerOneCards = playerOneCardStrings
			.Select(cardString => PlayingCard.From(cardString, error))
			.NotNull();

		var playerTwoCards = playerTwoCardStrings
			.Select(cardString => PlayingCard.From(cardString, error))
			.NotNull();

		var allCards = playerOneCards.Concat(playerTwoCards);
		var uniqueCards = allCards.Distinct();

		if (allCards.Count() != uniqueCards.Count())
		{
			error("Each players hand must be unique (Play with a single deck).");
			return true;
		}

		return false;
	}
}
