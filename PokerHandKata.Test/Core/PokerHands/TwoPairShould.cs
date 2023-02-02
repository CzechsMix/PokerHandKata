using PokerHandKata.Core.Game;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class TwoPairShould : TestWithErrorOutput
{
    private const string _middleTwoPair = "6♥,6♣,3♠,3♥,4♣";

    public TwoPairShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("K♥,4♥,A♣,A♠,7♣")]
    [InlineData("A♥,K♣,2♠,3♣,4♥")]
    public void BeatWorseHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTwoPair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("5♥,7♣,7♠,7♥,9♣")]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    [InlineData("A♥,A♣,A♠,A♦,2♣")]
    [InlineData("A♥,K♥,Q♥,J♥,9♥")]
    [InlineData("7♥,7♣,7♠,6♦,6♠")]
    [InlineData("5♥,7♣,8♠,6♦,9♣")]
    public void LoseToBetterHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTwoPair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("7♥,7♣,4♠,4♥,2♣")]
    [InlineData("A♥,A♣,2♠,2♥,4♠")]
    public void LoseToHigherHighPair(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTwoPair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("6♦,6♠,5♠,5♥,4♠")]
    [InlineData("6♦,6♠,4♠,4♥,2♣")]
    public void LoseToHigherLowPair(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTwoPair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("5♥,5♣,4♠,4♥,2♣")]
    [InlineData("4♥,4♦,2♠,2♥,9♣")]
    public void BeatLowerHighPair(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTwoPair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("6♦,6♠,2♠,2♥,4♦")]
    public void BeatLowerLowPair(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTwoPair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("6♦,6♠,3♣,3♦,2♣")]
    public void BeatLowerHighCard(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTwoPair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }
}
