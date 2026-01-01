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
            var result = weightGenerator.GenerateWeights();
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);

            Assert.True(result[0] == p1[0] || result[0] == p2[0]);
            Assert.True(result[1] == p1[1] || result[1] == p2[1]);
        }

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
            WeightGenData dataGen = new(p1, p2, 0, 0);
            WeightGenerator weightGen = new(dataGen);
            double[] result = weightGen.GenerateWeights();

            Assert.NotNull(result);
            Assert.Equal(p1.Length, result.Length);
            Assert.Equal(p2.Length, result.Length);
            Assert.True(result[0] == p1[0] || result[0] == p2[0]);

            if (result[0] == p1[0])
                p1Counter++;
            else if (result[0] == p2[0])
                p2Counter++;
        }

        //assert
        Assert.InRange(p1Counter, min, max);
        Assert.InRange(p2Counter, min, max);
    }
}
