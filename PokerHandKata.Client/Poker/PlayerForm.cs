using PokerHandKata.Core.PlayingCards;

namespace PokerHandKata.Client.Poker;

public class PlayerForm
{	public string Name { get; set; } = string.Empty;
	public List<PlayingCard> Cards { get; set; } = new();
}
