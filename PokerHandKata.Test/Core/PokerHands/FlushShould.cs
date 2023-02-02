using PokerHandKata.Core.Game;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class FlushShould : TestWithErrorOutput
{
    private const string _middleFlush = "7♥,6♥,8♥,2♥,3♥";

    public FlushShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("A♥,K♣,Q♥,J♦,10♠")]
    [InlineData("A♥,A♣,A♠,2♣,4♣")]
    [InlineData("A♥,A♣,2♠,2♣,4♣")]
    [InlineData("K♥,4♥,A♣,A♠,7♣")]
    [InlineData("A♥,K♣,2♠,3♣,4♣")]
    public void BeatWorseHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleFlush.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    [InlineData("A♥,A♣,A♠,A♦,2♣")]
    [InlineData("7♦,7♣,7♠,6♦,6♣")]
    public void LoseToBetterHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleFlush.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("7♣,6♣,9♣,2♣,3♣")]
    [InlineData("7♣,6♣,10♣,2♣,3♣")]
    [InlineData("7♣,6♣,J♣,2♣,3♣")]
    [InlineData("7♣,6♣,Q♣,2♣,3♣")]
    [InlineData("7♣,6♣,K♣,2♣,3♣")]
    [InlineData("7♣,6♣,A♣,2♣,3♣")]
    public void LoseToFlushesWithHigherHighCard(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleFlush.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("7♣,6♣,4♣,2♣,3♣")]
    public void BeatFlushesWithLowerHighCard(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleFlush.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }
}
