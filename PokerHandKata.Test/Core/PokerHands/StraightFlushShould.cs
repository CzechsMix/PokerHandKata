using Xunit.Abstractions;
using PokerHandKata.Core.Game;

namespace PokerHandKata.Test.Core.PokerHands;

public class StraightFlushShould : TestWithErrorOutput
{
    public StraightFlushShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("A♥,A♣,A♠,A♦,10♥")]
    [InlineData("A♥,A♣,A♠,2♥,2♣")]
    [InlineData("A♥,K♥,Q♥,J♥,9♥")]
    [InlineData("A♥,K♣,Q♥,J♦,10♠")]
    [InlineData("A♥,A♣,A♠,2♥,4♣")]
    [InlineData("A♥,A♣,2♠,2♥,4♣")]
    [InlineData("K♥,4♥,A♣,A♠,7♣")]
    [InlineData("A♥,K♣,2♠,3♥,4♣")]
    public void BeatEveryNonStraightFlush(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", "2♦,4♦,6♦,3♦,5♦".Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("2♠,4♠,6♠,3♠,5♠", true)]
    [InlineData("A♦,K♦,Q♦,J♦,10♦", false)]
    public void BeatStraightFlushesWithALowerHighCard(
        string otherStraightFlush,
        bool expectation)
    {
        var me = new PlayerData("Me", "7♦,9♦,6♦,8♦,5♦".Split(','));
        var opponent = new PlayerData("Opponent", otherStraightFlush.Split(','));
        var winner = OneHandGame.Play(me, opponent, Error);

        var iWon = me.Name == winner;

        iWon.ShouldBe(expectation);
    }
}
