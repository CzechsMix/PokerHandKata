using PokerHandKata.Core.Game;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace PokerHandKata.Client.Poker;

public static class PokerService
{
	public delegate Task<string> PlayPoker(
		PlayerForm playerOne,
		PlayerForm playerTwo);

	private record Request(
		PlayerData PlayerOne,
		PlayerData PlayerTwo);

	private record Response(
		string Winner);

	public static PlayPoker CreateServiceCall(HttpClient client)
	{
		return async (playerOne, playerTwo) =>
		{
			var request = CreateRequest(
				url: "https://localhost:7020/poker/onehandgame",
				playerOne,
				playerTwo);

			var response = await client.SendAsync(request);

			return await ParseResponse(response);
		};
	}

	public static PlayerData MapData(
		this PlayerForm form)
		=> new(
			Name: form.Name, 
			CardStrings: form.Cards
				.Select(card => card.DisplayString).ToList());

	public static HttpRequestMessage CreateRequest(
		string url,
		PlayerForm playerOne,
		PlayerForm playerTwo)
	{
		Request model = new(playerOne.MapData(), playerTwo.MapData());
		string json = JsonSerializer.Serialize(model);
		StringContent content = new(
			content: json,
			Encoding.UTF8,
			Application.Json);

		HttpRequestMessage httpRequest = new(HttpMethod.Post, url);
		httpRequest.Content = content;
		return httpRequest;
	}

	public static async Task<string> ParseResponse(
		HttpResponseMessage httpResponse)
	{
		string content = await httpResponse.Content.ReadAsStringAsync();
		if (httpResponse.IsSuccessStatusCode)
		{
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			};
			var model = JsonSerializer.Deserialize<Response>(content, options);
			return model is not null
				? $"Winner: {model.Winner}!"
				: $"Unknown success response: {content}";
		}

		return $"Error: {content}";
	}
}
