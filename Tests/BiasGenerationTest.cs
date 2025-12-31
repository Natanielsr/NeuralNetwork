
using Application.Assembly.Bias;

namespace Tests;

public class BiasGenerationTest
{

    [Theory]
    [InlineData(-1, -1, -1, -1)]
    [InlineData(-1, 0, -1, 0)]
    [InlineData(-1, 1, -1, 1)]
    [InlineData(0, -1, 0, -1)]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 1, 0, 1)]
    [InlineData(1, -1, 1, -1)]
    [InlineData(1, 0, 1, 0)]
    [InlineData(1, 1, 1, 1)]
    public void CrossoverBiasTest(
        double fatherBias, double motherBias, double expectedResult1, double expectedResult2)
    {
        //arrange
        BiasGenData biasGenData = new(fatherBias, motherBias, 0, 0);
        BiasGenerator biasGen = new(biasGenData);

        //act
        var result = biasGen.CrossoverBias();

        //assert
        Assert.True(result >= -1 && result <= 1.0);

        Assert.True(result == expectedResult1 || result == expectedResult2);
    }

}
