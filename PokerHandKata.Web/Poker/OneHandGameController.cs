using Microsoft.AspNetCore.Mvc;
using PokerHandKata.Core.Game;

namespace PokerHandKata.Web.Poker;


[Route("/Poker/OneHandGame")]
public class OneHandGameController : ControllerBase
{
	public new record Request(
		PlayerData PlayerOne,
		PlayerData PlayerTwo);

	public new record Response(
		string Winner);

	[HttpPost]
	public ActionResult<Response> Play(
		[FromBody] Request request)
	{
		List<string> errors = new();

		string? winner = OneHandGame.Play(
			request.PlayerOne,
			request.PlayerTwo,
			errors.Add);

		if (winner is null)
		{
			return BadRequest(errors);
		}

		Response response = new(winner);

		return Ok(response);
	}
}
