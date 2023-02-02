using PokerHandKata.Core.PokerHands;
using Xunit.Abstractions;

namespace PokerHandKata.Test.Core.PokerHands;

public class PokerHandShould : TestWithErrorOutput
{
    public PokerHandShould(ITestOutputHelper output)
        : base(output) { }

    [Fact]
    public void ParseFromFiveUniqueCards()
    {
        var cards = new[] { "A♥", "K♥", "Q♥", "J♥", "10♥" };
        var hand = PokerHand.From(cards, Error);
        hand.ShouldNotBeNull();
    }

    [Fact]
    public void RejectHandsWithMoreThanFiveCardsButOnlyFiveUniqueCards()
    {
        var cards = new[] { "A♥", "K♥", "Q♥", "J♥", "10♥", "10♥" };
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeNull();
    }

    [Fact]
    public void RejectHandsWithFiveCardsButThatContainDuplicates()
    {
        var cards = new[] { "A♥", "K♥", "Q♥", "10♥", "10♥" };
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeNull();
    }

    [Theory]
    [InlineData("A♥,K♥,Q♥,J♥,10♥")]
    [InlineData("K♣,Q♣,J♣,10♣,9♣")]
    [InlineData("2♦,4♦,6♦,3♦,5♦")]
    public void RecognizeAStraightFlush(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<StraightFlush>();
    }

    [Theory]
    [InlineData("A♥,A♣,A♠,A♦,10♥")]
    [InlineData("10♥,A♣,A♠,A♦,A♥")]
    [InlineData("2♥,2♣,10♥,2♠,2♦")]
    public void RecognizeFourOfAKind(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<FourOfAKind>();
    }

    [Theory]
    [InlineData("A♥,A♣,A♠,2♥,2♣")]
    [InlineData("2♥,2♣,A♥,A♣,A♠")]
    [InlineData("2♥,A♥,A♣,A♠,2♣")]
    [InlineData("2♥,A♥,2♣,A♣,A♠")]
    public void RecognizeAFullHouse(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<FullHouse>();
    }

    [Theory]
    [InlineData("A♥,K♥,Q♥,J♥,9♥")]
    [InlineData("4♥,Q♥,J♥,10♥,9♥")]
    [InlineData("2♥,K♥,6♥,9♥,J♥")]
    public void RecognizeAFlush(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<Flush>();
    }

    [Theory]
    [InlineData("A♥,K♣,Q♥,J♦,10♠")]
    [InlineData("K♥,Q♣,J♠,10♥,9♥")]
    [InlineData("2♦,4♥,6♣,3♠,5♠")]
    public void RecognizeAStraight(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<Straight>();
    }

    [Theory]
    [InlineData("A♥,A♣,A♠,2♥,4♣")]
    [InlineData("J♥,Q♣,A♥,A♣,A♠")]
    [InlineData("K♥,A♥,A♣,A♠,7♣")]
    [InlineData("8♥,2♥,4♣,2♣,2♠")]
    public void RecognizeThreeOfAKind(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<ThreeOfAKind>();
    }

    [Theory]
    [InlineData("A♥,A♣,2♠,2♥,4♣")]
    [InlineData("J♥,Q♣,A♥,A♣,J♠")]
    [InlineData("K♥,7♥,A♣,A♠,7♣")]
    [InlineData("8♥,2♥,4♣,2♣,8♠")]
    public void RecognizeTwoPair(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<TwoPair>();
    }

    [Theory]
    [InlineData("A♥,K♣,2♠,2♥,4♣")]
    [InlineData("10♥,Q♣,A♥,A♣,J♠")]
    [InlineData("K♥,4♥,A♣,A♠,7♣")]
    [InlineData("8♥,A♥,4♣,2♣,8♠")]
    public void RecognizeAPair(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<Pair>();
    }

    [Theory]
    [InlineData("A♥,K♣,2♠,3♥,4♣")]
    [InlineData("10♥,Q♣,A♥,2♣,J♠")]
    [InlineData("K♥,4♥,Q♣,A♠,7♣")]
    [InlineData("8♥,A♥,4♣,2♣,9♠")]
    public void DefaultToHighCard(string commaSeperatedCards)
    {
        var cards = commaSeperatedCards.Split(',');
        var hand = PokerHand.From(cards, Error);
        hand.ShouldBeOfType<HighCard>();
    }
}
