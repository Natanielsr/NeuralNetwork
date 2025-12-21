using Domain.Entities;

namespace Tests;

public class NeuronTest
{
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
