using PokerHandKata.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class FourOfAKindShould : TestWithErrorOutput
{
    public FourOfAKindShould(ITestOutputHelper output)
        : base(output) { }

    [Theory]
    [InlineData("K♥,K♣,K♠,2♥,2♣")]
    [InlineData("8♥,K♥,Q♥,J♥,9♥")]
    [InlineData("9♥,K♣,Q♥,J♦,10♠")]
    [InlineData("K♥,K♣,K♠,2♥,4♣")]
    [InlineData("K♥,K♣,2♠,2♥,4♣")]
    [InlineData("K♥,4♥,Q♣,Q♠,7♣")]
    [InlineData("Q♥,K♣,2♠,3♥,4♣")]
    public void BeatEverythingButAStraightFlush(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", "A♥,A♣,A♠,A♦,10♥".Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(me.Name);
    }

    [Theory]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    public void LoseToAStraightFlush(
        string commaSeperatedCards)
    {
        var me = new PlayerData("Me", "A♥,A♣,A♠,A♦,10♥".Split(','));
        var opponent = new PlayerData("Opponent", commaSeperatedCards.Split(','));
        var winnerName = OneHandGame.Play(me, opponent, Error);

        winnerName.ShouldBe(opponent.Name);
    }

    [Theory]
    [InlineData("2♥,2♣,2♠,2♦,10♥", true)]
    [InlineData("A♥,A♣,A♠,A♦,10♦", false)]
    public void BeatFourOfAKindWithLowerQuads(
        string otherStraightFlush,
        bool expectation)
    {
        var me = new PlayerData("Me", "9♥,9♣,9♠,9♦,10♠".Split(','));
        var opponent = new PlayerData("Opponent", otherStraightFlush.Split(','));
        var winner = OneHandGame.Play(me, opponent, Error);

        var iWon = me.Name == winner;

        iWon.ShouldBe(expectation);
    }
}
