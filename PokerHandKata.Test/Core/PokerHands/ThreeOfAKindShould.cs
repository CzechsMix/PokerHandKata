using PokerHandKata.Core.Game;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class ThreeOfAKindShould : TestWithErrorOutput
{
    private const string _middleTrips = "5♥,7♣,7♠,7♥,9♣";

    public ThreeOfAKindShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("A♥,A♣,2♠,2♥,4♣")]
    [InlineData("K♥,4♥,A♣,A♠,7♦")]
    [InlineData("A♥,K♣,2♠,3♥,4♣")]
    public void BeatWorseHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTrips.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    [InlineData("A♥,A♣,A♠,A♦,2♣")]
    [InlineData("A♥,K♥,Q♥,J♥,9♥")]
    [InlineData("8♥,8♣,8♠,6♥,6♣")]
    [InlineData("5♦,7♦,8♠,6♥,9♦")]
    public void LoseToBetterHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTrips.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("5♦,8♣,8♠,8♥,9♦")]
    [InlineData("5♦,A♣,A♠,A♥,9♦")]
    public void LoseToHigherTrips(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTrips.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("5♦,6♣,6♠,6♥,9♦")]
    [InlineData("5♦,2♣,2♠,2♥,9♦")]
    public void BeatLowerTrips(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleTrips.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }
}
