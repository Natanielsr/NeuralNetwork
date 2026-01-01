using Application.Assembly.NeuronFactory;
using Domain.Entities;
using Domain.Exceptions;

namespace Tests;

public class NeuronAssemblyTest
{

    [Fact]
    public void GenerateNeuronChild2()
    {
        Neuron father = new Neuron(0.1, [0.1, 0.2]);
        Neuron mother = new Neuron(0.2, [0.3, 0.4]);

        NeuronGenData genDatadata = NeuronGenData.Create(father, mother, 0, 0);

        Neuron child = new NeuronAssembly(genDatadata).GenerateNeuronChild();
        Assert.NotNull(child);

        if (child.GetBias().NotEqual(father.GetBias()))
            Assert.Equal(child.GetBias(), mother.GetBias());
        else if (child.GetBias().NotEqual(mother.GetBias()))
            Assert.Equal(child.GetBias(), father.GetBias());
        else
            throw new Exception("the child bias cant be different of parents bias");

        Assert.Equal(2, child.GetWeights().Length);
        for (int i = 0; i < child.GetWeights().Length; i++)
        {
            Weight fatherWeight = father.GetWeight(i);
            Weight motherWeight = mother.GetWeight(i);
            Weight childWeight = child.GetWeight(i);

            if (childWeight.NotEqual(fatherWeight))
                Assert.Equal(childWeight, motherWeight);
            else if (childWeight.NotEqual(motherWeight))
                Assert.Equal(childWeight, fatherWeight);
            else
                throw new Exception("the child weight cant be different of parents weights");

        }
    }

    [Fact]
    public void GenerateNeuronChild()
    {
        Neuron father = new Neuron(1, [1]);
        Neuron mother = new Neuron(1, [1]);

        var data = NeuronGenData.Create(father, mother, 0, 0);

        var child = new NeuronAssembly(data).GenerateNeuronChild();
        Assert.Equal(1, child.GetBiasValue());
        Assert.Equal(1, child.GetWeightValue(0));
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
            Assert.True(neuron.GetBiasValue() > -1.0 && neuron.GetBiasValue() < 1.0);

            foreach (var weight in neuron.GetWeights())
            {
                Assert.True(weight.Value > -1.0 && weight.Value < 1.0);
            }
        }
    }


    [Fact]
    public void NeuronSimulationAssemblyTest()
    {
        //arrange
        var neuron = new Neuron(0.1, [0.5, -1.0, 0.25]);

        //assert
        Assert.Equal(0.1, neuron.GetBiasValue(), 3);
        Assert.Contains(neuron.GetWeights(), w => w.Value == 0.5);
        Assert.Contains(neuron.GetWeights(), w => w.Value == -1.0);
        Assert.Contains(neuron.GetWeights(), w => w.Value == 0.25);

    }

    [Fact]
    public void NeuronBasicAssemblyTest()
    {
        Neuron neuron = new Neuron(0.0, [0, 0, 0, 0]);

        Assert.Equal(0.0, neuron.GetBiasValue());
        Assert.Equal(4, neuron.GetWeights().Length);

        foreach (var weight in neuron.GetWeights())
        {
            Assert.Equal(0.0, weight.Value);
        }
    }

}
