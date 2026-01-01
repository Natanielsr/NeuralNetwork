using Application.Assembly.NeuronFactory;
using Domain.Entities;
using Domain.Exceptions;

namespace Tests;

public class NeuronAssemblyTest
{

    [Theory]
    [InlineData(0.1, new double[2] { 0.1, 0.4 }, false, false)]
    [InlineData(0.2, new double[2] { 0.3, 0.2 }, false, false)]
    [InlineData(0.1, new double[2] { 0.1, 0.5 }, false, true)]
    [InlineData(0.3, new double[2] { 0.1, 0.4 }, true, false)]
    public void VerifyMutationTest(double childBias, double[] childWeights, bool mutationBias, bool mutationWeights)
    {
        Neuron father = new Neuron(0.1, [0.1, 0.2]);
        Neuron mother = new Neuron(0.2, [0.3, 0.4]);

        NeuronGenData data = NeuronGenData.Create(father, mother, 0, 0);

        (bool biasMutation, bool weightMutation) = new NeuronAssembly(data).verifyMutation(childBias, childWeights);

        Assert.True(biasMutation == mutationBias);
        Assert.True(weightMutation == mutationWeights);
    }

    [Fact]
    public void GenerateNeuronChild2()
    {
        Neuron father = new Neuron(0.1, [0.1, 0.2]);
        Neuron mother = new Neuron(0.2, [0.3, 0.4]);

        var data = NeuronGenData.Create(father, mother, 0, 0);

        Neuron child = new NeuronAssembly(data).GenerateNeuronChild();
        Assert.NotNull(child);

        if (child.GetBias() != father.GetBias())
            Assert.True(child.GetBias() == mother.GetBias());
        else if (child.GetBias() != mother.GetBias())
            Assert.True(child.GetBias() == father.GetBias());
        else
            throw new Exception("the child bias cant be different of parents bias");

        Assert.Equal(2, child.GetWeights().Length);
        for (int i = 0; i < child.GetWeights().Length; i++)
        {
            var fatherWeight = father.GetWeights()[i];
            var motherWeight = mother.GetWeights()[i];
            var childWeight = child.GetWeights()[i];

            if (childWeight != fatherWeight)
                Assert.True(childWeight == motherWeight);
            else if (childWeight != motherWeight)
                Assert.True(childWeight == fatherWeight);
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
