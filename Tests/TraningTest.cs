using Application.Training;
using Domain.Assembly;
using Domain.Entities;
namespace Tests;

public class TraningTest
{


    [Fact]
    public void TrainTest()
    {
        //arrange
        var inputs = Input.CreateInputs(1, 1, 1);
        var training = new TrainingField(inputs, 10, Output.CreateOutput(0));

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
