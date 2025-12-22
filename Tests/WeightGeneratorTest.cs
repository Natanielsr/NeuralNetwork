using Application.Assembly.Weight;
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
    public void MutateWeightTest()
    {
        //arrange
        var p1 = new double[] { 0.0 };
        var p2 = new double[] { 0.0 };
        var weight = 0;
        var mutationRate = 1;
        var mutationStrength = 1;

        var data = WeightGenData.Create(p1, p2, mutationRate, mutationStrength);
        var weightGenerator = new WeightGenerator(data);

        var aux = 0.0;
        //act
        for (int i = 0; i < 100; i++)
        {
            var result = weightGenerator.MutateWeight(weight);
            Assert.True(result >= -1.0 || result <= 1.0);
            Assert.NotEqual(aux, result);
            aux = result;
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
        var result = weightGen.GenerateWeights();

        //assert
        Assert.NotNull(result);
        Assert.Equal(p1.Length, result.Length);
        Assert.Equal(p2.Length, result.Length);
        for (int i = 0; i < result.Length; i++)
        {
            Assert.True(result[i] == p1[i] || result[i] == p2[i]);
        }
    }

    [Fact]
    public void GenerateWeightTest2()
    {
        //arrange
        var p1 = new double[] { 0.1, 0.2 };
        var p2 = new double[] { 0.3, 0.4 };

        var data = WeightGenData.Create(p1, p2, 0, 0);
        var weightGen = new WeightGenerator(data);
        //act
        var result = weightGen.GenerateWeights();

        //assert
        Assert.NotNull(result);
        Assert.Equal(p1.Length, result.Length);
        Assert.Equal(p2.Length, result.Length);


        if (result[0] == p1[0])
        {
            Assert.Equal(result[1], p2[1]);
        }
        else if (result[0] == p2[0])
        {
            Assert.Equal(result[1], p1[1]);
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
        var p1 = new double[] { 0.1 };
        var p2 = new double[] { 0.2 };
        var p1Choose = 0;
        var p2Choose = 0;
        double expected = 500;
        double tolerance = expected * 0.1;
        var min = expected - tolerance;
        var max = expected + tolerance;

        double[] result = null!;
        //act
        for (int i = 0; i < 1000; i++)
        {
            var dataGen = new WeightGenData(p1, p2, 0, 0);
            var weightGen = new WeightGenerator(dataGen);
            result = weightGen.GenerateWeights();
            Assert.NotNull(result);
            Assert.Equal(p1.Length, result.Length);
            Assert.Equal(p2.Length, result.Length);
            Assert.True(result[0] == p1[0] || result[0] == p2[0]);

            if (result[0] == p1[0])
                p1Choose++;
            else if (result[0] == p2[0])
                p2Choose++;
        }

        //assert
        Assert.InRange(p1Choose, min, max);
        Assert.InRange(p2Choose, min, max);
    }
}
