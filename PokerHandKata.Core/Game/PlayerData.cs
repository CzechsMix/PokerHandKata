namespace PokerHandKata.Core.Game;

public record PlayerData(
	string Name,
	IEnumerable<string> CardStrings);
