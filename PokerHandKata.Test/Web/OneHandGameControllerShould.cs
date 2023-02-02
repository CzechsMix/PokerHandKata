using Microsoft.AspNetCore.Mvc;
using PokerHandKata.Core.Game;
using PokerHandKata.Web.Poker;

namespace PokerHandKata.Test.Web;

public class OneHandGameControllerShould
{
	[Theory]
	[InlineData("One", "2♦,4♦,6♦,3♦,5♦", "Two", "A♥,A♣,A♠,A♦,10♥", "One")]
	[InlineData("One", "2♦,4♦,6♦,3♦,5♥", "Two", "A♥,A♣,A♠,A♦,10♥", "Two")]
	[InlineData("One", "2♦,4♦,6♦,3♦,5♦", "Two", "2♥,4♥,6♥,3♥,5♥", "Tie")]
	public void PlayGamesCorrectly(
		string playerOneName,
		string playerOneCards,
		string playerTwoName,
		string playerTwoCards,
		string expectedWinner)
	{
		var sut = new OneHandGameController();
		var request = new OneHandGameController.Request(
			new PlayerData(playerOneName, playerOneCards.Split(',')),
			new PlayerData(playerTwoName, playerTwoCards.Split(',')));

		var result = sut.Play(request);
		var response = ((OkObjectResult)result.Result!).Value as OneHandGameController.Response;
		response!.Winner.ShouldBe(expectedWinner);
	}

	[Theory]
	[InlineData("One", "A♦,4♦,6♦,3♦,5♦", "Two", "A♥,A♣,A♠,A♦,10♥")]
	[InlineData("One", "2♦,4♦,6♦,3♦,5♥", "Two", "A♥,A♣,A♠,A♦,10♥,9♥")]
	[InlineData("One", "2♦,4♦,6♦,3♦,5♦", "Two", "2♥,4♥,6♥,3♥,5H")]
	public void CommunicateBadRequest(
		string playerOneName,
		string playerOneCards,
		string playerTwoName,
		string playerTwoCards)
	{
		var sut = new OneHandGameController();
		var request = new OneHandGameController.Request(
			new PlayerData(playerOneName, playerOneCards.Split(',')),
			new PlayerData(playerTwoName, playerTwoCards.Split(',')));

		var result = sut.Play(request);
		var response = ((BadRequestObjectResult)result.Result!).Value as List<string>;
		response.ShouldNotBeNull();
		response!.Any().ShouldBeTrue();
	}
}
