using Application.Assembly.WeightGen;
using Domain.Entities;
using Xunit.Abstractions;

namespace Tests;

public class WeightGeneratorTest
{
    private readonly ITestOutputHelper _output;

    public WeightGeneratorTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ChildWeightTest()
    {
        //arrange
        double[] p1 = [0.0, 0.1];
        double[] p2 = [0.2, 0.3];

        var data = WeightGenData.Create(p1, p2, 0, 0);
        var weightGenerator = new WeightGenerator(data);

        //act
        for (int i = 0; i < 1000; i++)
        {
            Weight[] resultWeights = weightGenerator.GenerateWeights();
            Assert.NotNull(resultWeights);
            Assert.Equal(2, resultWeights.Length);

            Assert.True(resultWeights[0].Value == p1[0] || resultWeights[0].Value == p2[0]);
            Assert.True(resultWeights[1].Value == p1[1] || resultWeights[1].Value == p2[1]);
        }

    }

    [Fact]
    public void MutateWeightTest()
    {
        //arrange
        var p1 = new double[] { 0.0 };
        var p2 = new double[] { 0.0 };
        Weight weight = new(0);
        var mutationRate = 1;
        var mutationStrength = 1;

        var data = WeightGenData.Create(p1, p2, mutationRate, mutationStrength);
        var weightGenerator = new WeightGenerator(data);

        Weight aux = new(0);
        //act
        for (int i = 0; i < 100; i++)
        {
            Weight resultWeight = weightGenerator.MutateWeight(weight);
            Assert.True(resultWeight.Value >= -1.0 || resultWeight.Value <= 1.0);
            Assert.NotEqual(aux, resultWeight);
            aux = resultWeight;
        }

    }

    [Fact]
    public void GenerateWeightTest1()
    {
        //arrange
        var p1 = new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 };
        var p2 = new double[] { 0.6, 0.7, 0.8, 0.9, 1.0 };

        var data = WeightGenData.Create(p1, p2, 0, 0);
        var weightGen = new WeightGenerator(data);
        //act
        Weight[] resultWeights = weightGen.GenerateWeights();

        //assert
        Assert.NotNull(resultWeights);
        Assert.Equal(p1.Length, resultWeights.Length);
        Assert.Equal(p2.Length, resultWeights.Length);
        for (int i = 0; i < resultWeights.Length; i++)
        {
            Assert.True(resultWeights[i].Value == p1[i] || resultWeights[i].Value == p2[i]);
        }
    }

    [Fact]
    public void GenerateWeightTest2()
    {
        //arrange
        Weight[] p1 = Weight.Create([0.1, 0.2]);
        Weight[] p2 = Weight.Create([0.3, 0.4]);

        var data = WeightGenData.Create(p1, p2, 0, 0);
        var weightGen = new WeightGenerator(data);
        //act
        Weight[] resultWeights = weightGen.GenerateWeights();

        //assert
        Assert.NotNull(resultWeights);
        Assert.Equal(p1.Length, resultWeights.Length);
        Assert.Equal(p2.Length, resultWeights.Length);

        if (resultWeights[0].Equals(p1[0]))
        {
            Assert.Equal(resultWeights[1], p2[1]);
        }
        else if (resultWeights[0].Equals(p2[0]))
        {
            Assert.Equal(resultWeights[1], p1[1]);
        }
        else
        {
            throw new Exception("results wrong");
        }
    }

    [Fact]
    public void GenerateChildOneWeightTest()
    {
        //arrange
        double[] p1 = [0.1];
        double[] p2 = [0.2];
        int p1Counter = 0;
        int p2Counter = 0;
        int expected = 500;
        int tolerance = (int)(expected * 0.1);
        int min = expected - tolerance;
        int max = expected + tolerance;

        //act
        for (int i = 0; i < 1000; i++)
        {
            WeightGenData dataGen = WeightGenData.Create(p1, p2, 0, 0);
            WeightGenerator weightGen = new(dataGen);
            Weight[] resultWeights = weightGen.GenerateWeights();

            Assert.NotNull(resultWeights);
            Assert.Equal(p1.Length, resultWeights.Length);
            Assert.Equal(p2.Length, resultWeights.Length);
            Assert.True(resultWeights[0].Value == p1[0] || resultWeights[0].Value == p2[0]);

            if (resultWeights[0].Value == p1[0])
                p1Counter++;
            else if (resultWeights[0].Value == p2[0])
                p2Counter++;
        }

        //assert
        Assert.InRange(p1Counter, min, max);
        Assert.InRange(p2Counter, min, max);
    }
}
