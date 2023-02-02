using PokerHandKata.Core.Game;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class HighCardShould : TestWithErrorOutput
{
    private const string _middleHighCard = "7♥,5♣,3♠,J♥,9♣";

    public HighCardShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("7♦,4♥,A♣,A♠,7♣")]
    [InlineData("5♥,7♣,7♠,7♦,9♦")]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    [InlineData("A♥,A♣,A♠,A♦,2♣")]
    [InlineData("A♦,K♦,Q♦,J♦,9♦")]
    [InlineData("7♦,7♣,7♠,6♥,6♣")]
    [InlineData("5♥,7♣,8♠,6♥,9♦")]
    [InlineData("6♥,6♣,3♦,J♦,4♣")]
    public void LoseToBetterHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleHighCard.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("7♣,5♠,3♣,Q♣,9♠")]
    [InlineData("7♣,5♠,3♣,J♣,10♠")]
    [InlineData("8♣,5♠,3♣,J♣,9♠")]
    [InlineData("7♣,6♣,3♣,J♣,9♠")]
    [InlineData("7♣,5♠,4♣,J♣,9♠")]
    public void LoseToHigherCards(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleHighCard.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("7♣,5♠,3♣,10♣,9♠")]
    [InlineData("7♣,5♠,3♣,J♣,8♠")]
    [InlineData("6♣,5♠,3♣,J♣,9♠")]
    [InlineData("7♣,4♣,3♣,J♣,9♠")]
    [InlineData("7♣,5♠,2♣,J♣,9♠")]
    public void BeatLowerCards(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleHighCard.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }
}
