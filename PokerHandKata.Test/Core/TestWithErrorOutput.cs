using Xunit.Abstractions;

namespace PokerHandKata.Test.Core;

public abstract class TestWithErrorOutput
{
    private readonly ITestOutputHelper _output;

    protected void Error(string message)
        => _output.WriteLine(message);

    protected TestWithErrorOutput(ITestOutputHelper output)
        => _output = output;
}
