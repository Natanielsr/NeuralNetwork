using Domain.Assembly;
using Domain.Entities;

namespace Tests;

public class ActivationTest
{

    [Fact]
    public void RandomNeuronActivationTest()
    {

        var inputs = Input.CreateInputs(1, 1, 1);

        var neurons = NeuronAssembly.AssembleRandomNeurons(inputs.Length, 10);

        foreach (var neuron in neurons)
        {
            var output = neuron.Activation(inputs);
            Assert.True(output.Value >= -1.0 && output.Value <= 1.0, $"Output value out of range -1.0 ... 1.0");
        }
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, 0.7615941559)]
    [InlineData(1, 0.9640275801)]
    public void NeuronActivationBiasTest(double bias, double result)
    {
        //arrange
        Neuron neuron = new Neuron(bias, [1]);
        var inputs = Input.CreateInputs(1);

        //act
        var output = neuron.Activation(inputs);

        //assert
        Assert.Equal(result, output.Value, 9);
    }


    [Theory]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public void NeuronActivationTest(double inputValue, double weight)
    {
        //arrange
        Neuron neuron = new Neuron(0, [weight]);
        var inputs = Input.CreateInputs(inputValue);

        //act
        var output = neuron.Activation(inputs);

        //assert
        Assert.True(output.Value != 0, "inactive neuron");
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, -1)]
    [InlineData(1, 0)]
    [InlineData(-1, 0)]
    public void NeuronNoActivationTest(double inputValue, double weight)
    {
        //arrange
        Neuron neuron = new Neuron(0, [weight]);
        var inputs = Input.CreateInputs(inputValue);

        //act
        var output = neuron.Activation(inputs);

        //assert
        Assert.True(output.Value == 0, "active neuron");
    }

    [Fact]
    public void NeuronSimulationActivationTest()
    {
        //arrange
        var neuron = new Neuron(0.1, [0.5, -1.0, 0.25]);
        var inputs = Input.CreateInputs(1.0, -0.2, 1.0);

        //act
        Output output = neuron.Activation(inputs);

        //assert
        Assert.Equal(0.782, output.Value, 3);
    }
}
