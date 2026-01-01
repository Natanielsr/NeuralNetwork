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

    TrainingField CreateTrainingFieldTest()
    {
        return CreateTrainingMutationFieldTest(0, 0);
    }

    TrainingField CreateTrainingMutationFieldTest(double mutationRate, double mutationStrength)
    {
        Input[] inputs = Input.CreateInputs(1, 1);
        int neuronSize = 10;
        Output expectedOutput = Output.CreateOutput(0);
        TrainingData trainingData = TrainingData.Create(inputs, neuronSize, expectedOutput, mutationRate, mutationStrength);
        return new(trainingData);
    }


    [Fact]
    public void MutationPercentageTest()
    {
        //arrange
        int mutationCounter = 0;
        int expected = 80;
        int tolerance = (int)(expected * 0.1);
        int min = expected - tolerance;
        int max = expected + tolerance;

        TrainingField trainingField = CreateTrainingMutationFieldTest(0.125, 1);

        //act
        for (int i = 0; i < 1000; i++)
        {
            Assert.NotNull(trainingField);
            Neuron[] neurons = trainingField.Train(2);

            Assert.Equal(10, neurons.Length);

            foreach (var neuron in neurons)
            {
                //if (neuron.BiasMutation || neuron.WeightMutation)
                //    mutationCounter++;

            }
        }

        //assert
        //Assert.InRange(mutationCounter, min, max);
    }

    [Fact]
    public void TwoTraningTest()
    {
        //arrange
        TrainingField training = CreateTrainingFieldTest();

        //act
        Neuron[] result = training.Train(2);

        //assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Length);

        for (int i = 0; i < result.Length - 1; i++)
        {
            var current = Math.Abs(result[i].Value);
            var next = Math.Abs(result[i + 1].Value);

            Assert.True(
                current <= next,
                $"valor incorreto na posição {i}: |{current}| > |{next}|"
            );
        }
    }

    [Fact]
    public void FisrtTraningTest()
    {
        //arrange
        var training = CreateTrainingFieldTest();

        //act
        var result = training.Train(1);

        //assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Length);
        Neuron? aux = null;
        foreach (var neuron in result)
        {
            Assert.Equal(2, neuron.GetWeights().Length);
            Assert.NotEqual(neuron, aux);
            aux = neuron;
        }
    }

    [Fact]
    public void TrainFullRandomMutationTest()
    {
        //arrange
        var inputs = Input.CreateInputs(1, 1);
        var neuronSize = 10;
        var expectedOutput = Output.CreateOutput(0);
        var trainingData = TrainingData.Create(inputs, neuronSize, expectedOutput, 1, 1);
        var training = new TrainingField(trainingData);

        //act
        var result = training.Train(1000);

        //assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Length);
        //_output.WriteLine("\n");
        Neuron? aux = null;
        foreach (var neuron in result)
        {
            Assert.Equal(2, neuron.GetWeights().Length);
            Assert.NotEqual(neuron, aux);
            aux = neuron;
            // _output.WriteLine(neuron.ToString() + " O:" + neuron.Activation(inputs).Value);
        }
    }

    [Fact]
    public void TrainTest()
    {
        //arrange
        var training = CreateTrainingFieldTest();
        //act
        var result = training.Train(1000);

        //assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Length);
        foreach (var neuron in result)
        {
            Assert.Equal(2, neuron.GetWeights().Length);
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
