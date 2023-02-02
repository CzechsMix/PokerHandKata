using PokerHandKata.Core.Game;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class StraightShould : TestWithErrorOutput
{
    private const string _middleStraight = "5♥,7♣,8♠,6♥,9♣";

    public StraightShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("A♥,A♣,A♠,2♥,4♣")]
    [InlineData("A♥,A♣,2♠,2♥,4♣")]
    [InlineData("K♥,4♥,A♣,A♠,7♠")]
    [InlineData("A♥,K♣,2♠,3♥,4♣")]
    public void BeatWorseHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleStraight.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    [InlineData("A♥,A♣,A♠,A♦,2♣")]
    [InlineData("A♥,K♥,Q♥,J♥,9♥")]
    [InlineData("7♥,7♦,7♠,6♦,6♣")]
    public void LoseToBetterHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleStraight.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("10♦,7♠,8♣,6♣,9♥")]
    [InlineData("10♦,Q♠,K♣,J♣,A♥")]
    public void LoseToHigherHighCard(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleStraight.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("5♦,7♠,8♣,6♣,4♥")]
    [InlineData("2♦,4♠,5♣,3♣,A♥")]
    public void BeatLowerHighCard(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleStraight.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }
}
