using Domain.Entities;

namespace Tests;

public class NeuronTest
{
    [Fact]
    public void TestHashCode()
    {
        var n1 = new Neuron(0.5, [0.1]);
        var n2 = new Neuron(0.5, [0.1]);

        var hash1 = n1.GetHashCode();
        var hash2 = n2.GetHashCode();

        Assert.True(hash1.Equals(hash2));
    }

    [Fact]
    public void Equals_IsSymmetric()
    {
        var n1 = new Neuron(0.5, [0.1]);
        var n2 = new Neuron(0.5000000000001, [0.1000000000001]);

        Assert.True(n1.Equals(n2));
        Assert.True(n2.Equals(n1));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        var n = new Neuron(0.5, [0.1]);

        Assert.False(n.Equals("not a neuron"));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        var n = new Neuron(0.5, [0.1]);

        Assert.False(n.Equals(null));
    }

    [Fact]
    public void Equals_DifferentWeightsLength_ReturnsFalse()
    {
        var n1 = new Neuron(0.5, [0.1, 0.2]);
        var n2 = new Neuron(0.5, [0.1]);

        Assert.False(n1.Equals(n2));
    }

    [Fact]
    public void Equals_WeightOutsideEpsilon_ReturnsFalse()
    {
        var n1 = new Neuron(0.5, [0.1, 0.2]);
        var n2 = new Neuron(0.5, [0.1, 0.25]);

        Assert.False(n1.Equals(n2));
    }

    [Fact]
    public void Equals_BiasOutsideEpsilon_ReturnsFalse()
    {
        var n1 = new Neuron(0.5, [0.1, 0.2]);
        var n2 = new Neuron(0.51, [0.1, 0.2]);

        Assert.False(n1.Equals(n2));
    }

    [Fact]
    public void Equals_ValuesWithinEpsilon_ReturnsTrue()
    {
        var n1 = new Neuron(0.5, [0.1, 0.2]);
        var n2 = new Neuron(
            0.5000000000001,
            new[] { 0.1000000000001, 0.2000000000001 }
        );

        Assert.True(n1.Equals(n2));
    }

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        var n1 = new Neuron(0.5, [0.1, 0.2, 0.3]);
        var n2 = new Neuron(0.5, [0.1, 0.2, 0.3]);

        Assert.True(n1.Equals(n2));
    }

    [Fact]
    public void GetWeightsTest()
    {
        //arrange
        var neuron = new Neuron(0, [-1, 0, 1]);

        //act
        var weights = neuron.GetWeights();

        //assert
        Assert.Equal(3, weights.Length);
        Assert.Equal(-1, weights[0]);
        Assert.Equal(0, weights[1]);
        Assert.Equal(1, weights[2]);

    }


}
