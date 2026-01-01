
using Application.Assembly.BiasGen;
using Domain.Entities;

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
        BiasGenData biasGenData = BiasGenData.Create(fatherBias, motherBias, 0, 0);
        BiasGenerator biasGen = new(biasGenData);

        //act
        Bias resultBias = biasGen.CrossoverBias();

        //assert
        Assert.True(resultBias.Value >= -1 && resultBias.Value <= 1.0);

        Assert.True(resultBias.Value == expectedResult1 || resultBias.Value == expectedResult2);
    }

}
