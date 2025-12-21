using Domain.Assembly;
using Domain.Entities;
using Domain.Exceptions;

namespace Tests;

public class NeuronAssemblyTest
{

    [Fact]
    public void GenerateNeuronChild()
    {
        Neuron father = new Neuron(1, [1]);
        Neuron mother = new Neuron(1, [1]);

        var child = NeuronAssembly.GenerateNeuronChild(father, mother);
        Assert.Equal(1, child.GetBias());
        Assert.Equal(1, child.GetWeights()[0]);
    }

    [Fact]
    public void NodeValueTest()
    {
        Assert.Throws<InvalidValueException>(() =>
        {
            var input = new Input(2);
        });
    }

    [Fact]
    public void AssembleRandomNeuronsTest()
    {
        var inputLegth = 1;

        var neuronsLength = 100;
        var neurons = NeuronAssembly.AssembleRandomNeurons(inputLegth, neuronsLength);

        Assert.Equal(neuronsLength, neurons.Length);

        foreach (var neuron in neurons)
        {
            Assert.Equal(inputLegth, neuron.GetWeights().Length);
            Assert.True(neuron.GetBias() > -1.0 && neuron.GetBias() < 1.0);

            foreach (var weight in neuron.GetWeights())
            {
                Assert.True(weight > -1.0 && weight < 1.0);
            }
        }
    }


    [Fact]
    public void NeuronSimulationAssemblyTest()
    {
        //arrange
        var neuron = new Neuron(0.1, [0.5, -1.0, 0.25]);

        //assert
        Assert.Equal(0.1, neuron.GetBias(), 3);
        Assert.Contains(neuron.GetWeights(), w => w == 0.5);
        Assert.Contains(neuron.GetWeights(), w => w == -1.0);
        Assert.Contains(neuron.GetWeights(), w => w == 0.25);

    }

    [Fact]
    public void NeuronBasicAssemblyTest()
    {
        Neuron neuron = new Neuron(0.0, [0, 0, 0, 0]);

        Assert.Equal(0.0, neuron.GetBias());
        Assert.Equal(4, neuron.GetWeights().Length);

        foreach (var weight in neuron.GetWeights())
        {
            Assert.Equal(0.0, weight);
        }
    }

}
