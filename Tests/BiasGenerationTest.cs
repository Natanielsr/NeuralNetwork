using Domain.Assembly;

namespace Tests;

public class BiasGenerationTest
{

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 0.5)]
    [InlineData(1, 0, 0.5)]
    [InlineData(1, 1, 1)]
    [InlineData(0, -1, -0.5)]
    [InlineData(-1, 0, -0.5)]
    [InlineData(-1, -1, -1)]
    [InlineData(1, -1, 0)]
    [InlineData(-1, 1, 0)]
    public void CrossoverBiasTest(double fatherBias, double motherBias, double expectedResult)
    {
        //act
        var result = BiasGenerator.CrossoverBias(fatherBias, motherBias);

        //assert
        Assert.True(result >= -1 && result <= 1.0);
        Assert.Equal(expectedResult, result);
    }

}
