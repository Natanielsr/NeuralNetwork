using Application.Assembly.NeuronFactory;
using Application.Training;
using Domain.Entities;
using Xunit.Abstractions;
namespace Tests;

public class TraningTest
{
    private readonly ITestOutputHelper _output;

    public TraningTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TrainMutationTest()
    {
        //arrange
        var inputs = Input.CreateInputs(1, 1);
        var neuronSize = 10;
        var expectedOutput = Output.CreateOutput(0);
        var trainingData = TrainingData.Create(inputs, neuronSize, expectedOutput, 0.125, 0.01);
        var training = new TrainingField(trainingData);

        //act
        var result = training.Train(1000);

        //assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Length);
        _output.WriteLine("\n");
        foreach (var neuron in result)
        {
            Assert.Equal(2, neuron.GetWeights().Length);
            _output.WriteLine(neuron.ToString() + " O:" + neuron.Activation(inputs).Value);
        }
    }

    [Fact]
    public void TrainTest()
    {
        //arrange
        var inputs = Input.CreateInputs(1, 1, 1);
        var neuronSize = 10;
        var expectedOutput = Output.CreateOutput(0);
        var trainingData = TrainingData.Create(inputs, neuronSize, expectedOutput, 0, 0);
        var training = new TrainingField(trainingData);

        //act
        var result = training.Train(1000);

        //assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Length);
        foreach (var neuron in result)
        {
            Assert.Equal(3, neuron.GetWeights().Length);
        }
    }

    [Fact]
    public void CreateRankTest()
    {
        //arrange
        var neuron = new Neuron(0, []);
        var pontuation = 0;

        //act
        var rank = new Rank(neuron, pontuation);

        //assert
        Assert.NotNull(rank);
        Assert.Equal(neuron, rank.Neuron);
        Assert.Equal(pontuation, rank.Pontuation);

    }

    [Fact]
    public void RankingTest()
    {
        var inputs = Input.CreateInputs(1.0);

        var expectedOutput = new Output(1.0);

        var neurons = NeuronAssembly.AssembleRandomNeurons(inputs.Length, 10);

        var ranking = Ranking.RankTheBest(inputs, neurons, expectedOutput);

        Assert.NotNull(ranking);
        Assert.True(ranking.Count > 1);

        for (int i = 0; i < ranking.Count - 1; i++)
        {
            var current = ranking[i].Pontuation;
            var next = ranking[i + 1].Pontuation;

            Assert.True(
                current <= next,
                $"Ranking incorreto na posição {i}: |{ranking[i].Pontuation}| > |{ranking[i + 1].Pontuation}|"
            );
        }
    }
}
