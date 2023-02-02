using PokerHandKata.Core.Game;
using PokerHandKata.Test.Core;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.Game;

public class OneHandGameShould : TestWithErrorOutput
{
	public OneHandGameShould(ITestOutputHelper output)
		: base(output) { }

	[Fact]
	public void ShouldEnsureNoDuplicates()
	{
		var errorWrittenToo = false;
		Action<string> error = msg =>
		{
			errorWrittenToo = true;
			Error(msg);
		};

		var handOne = "A♥,K♥,Q♥,J♥,9♥".Split(',');
		var handTwo = "K♣,4♥,A♣,A♥,7♣".Split(',');

		var playerOne = new PlayerData("One", handOne);
		var playerTwo = new PlayerData("Two", handTwo);

		var winner = OneHandGame.Play(
			playerOne,
			playerTwo,
			error);

		winner.ShouldBeNull();
		errorWrittenToo.ShouldBeTrue();
	}
}
