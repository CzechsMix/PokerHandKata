using PokerHandKata.Core.Game;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class PairShould : TestWithErrorOutput
{
    private const string _middlePair = "6♥,6♣,3♠,J♥,4♣";

    public PairShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("A♥,K♣,2♠,3♥,4♠")]
    public void BeatWorseHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middlePair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("7♥,4♥,A♣,A♠,7♣")]
    [InlineData("5♥,7♣,7♠,7♥,9♣")]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    [InlineData("A♥,A♣,A♠,A♦,2♣")]
    [InlineData("A♦,K♦,Q♦,J♦,9♦")]
    [InlineData("7♥,7♣,7♠,6♠,6♦")]
    [InlineData("5♥,7♣,8♠,6♠,9♣")]
    public void LoseToBetterHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middlePair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("7♥,7♣,4♠,3♥,2♣")]
    [InlineData("A♥,A♣,2♠,3♥,4♠")]
    public void LoseToHigherPair(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middlePair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("5♥,5♣,4♥,3♥,2♣")]
    [InlineData("4♥,4♠,2♠,3♥,9♣")]
    public void BeatLowerPair(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middlePair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("6♦,6♠,3♥,4♥,2♣")]
    public void BeatLowerHighCards(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middlePair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("6♦,6♠,3♥,A♥,2♣")]
    public void LoseToHigherHighCards(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middlePair.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }
}
