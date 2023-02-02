namespace PokerHandKata.Core.LanguageExtensions;

public static class EnumerableExtensions
{
	public static IEnumerable<Item> NotNull<Item>(
		this IEnumerable<Item?> possiblyNullItems)
		where Item : notnull
	{
		foreach(var item in possiblyNullItems)
		{
			if (item is not null)
			{
				yield return item;
			}
		}
	}

	public static Output? Choose<Input, Output>(
		this Input input,
		params Func<Input, Output?>[] factories)
	{
		Output? output = default;
		foreach(var factory in factories)
		{
			output = factory(input);
			if (output is not null)
			{
				break;
			}
		}

		return output;
	}
}
