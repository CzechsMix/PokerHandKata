using PokerHandKata.Core.PokerHands;
using System.Numerics;

namespace PokerHandKata.Core.Game;

internal class Player
{
	public string Name { get; }
	public PokerHand Hand { get; }
	private Player(string name, PokerHand hand)
		=> (Name, Hand) = (name, hand);

	public static Player? From(
		PlayerData playerData,
		Action<string> error)
	{
		var hand = PokerHand.From(
			playerData.CardStrings,
			error);

		return hand is not null
			? new Player(playerData.Name, hand)
			: null;
	}
};