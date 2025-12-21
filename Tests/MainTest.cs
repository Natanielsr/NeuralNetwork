using Domain;

namespace Tests;

public class MainTest
{
    [Fact]
    public void MainStartTest()
    {
        var main = new Main();
        var r = main.Start();

        Assert.Equal(0, r);

    }
}