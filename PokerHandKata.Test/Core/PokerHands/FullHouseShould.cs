using PokerHandKata.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class FullHouseShould : TestWithErrorOutput
{
    private const string _middleFullHouse = "7♥,7♣,7♠,6♥,6♣";

    public FullHouseShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("A♥,K♥,Q♥,J♥,9♥")]
    [InlineData("A♥,K♣,Q♥,J♦,10♠")]
    [InlineData("A♥,A♣,A♠,2♥,4♣")]
    [InlineData("A♥,A♣,2♠,2♥,4♣")]
    [InlineData("K♥,4♥,A♣,A♠,8♣")]
    [InlineData("A♥,K♣,2♠,3♥,4♣")]
    public void BeatWorseHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleFullHouse.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    [InlineData("A♥,A♣,A♠,A♦,2♣")]
    public void LoseToBetterHands(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleFullHouse.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("8♥,8♣,8♠,A♥,A♣")]
    [InlineData("A♥,A♣,A♠,2♥,2♣")]
    public void LoseToBetterTrips(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleFullHouse.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("5♥,5♣,5♠,A♥,A♣")]
    [InlineData("3♥,3♣,3♠,2♥,2♣")]
    public void BeatWorseTrips(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", _middleFullHouse.Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }
}
