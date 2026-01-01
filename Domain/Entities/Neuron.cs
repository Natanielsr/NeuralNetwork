using Domain.Exceptions;
using Domain.Utils;

namespace Domain.Entities;

public class Neuron : Node
{
    private Bias bias;
    private Weight[] weights;

    public Neuron(double bias, double[] weights)
    {
        this.bias = Bias.Create(bias);
        this.weights = Weight.Create(weights);
    }

    public Neuron(Bias bias, Weight[] weights)
    {
        this.bias = bias;
        this.weights = weights;
    }


    public override string ToString()
    {
        return FormatNeuron.ToString(this);
    }

    public Weight GetWeight(int pos)
    {
        return weights[pos];
    }

    public Weight[] GetWeights()
    {
        return weights;
    }

    public double[] GetWeightValues()
    {
        double[] weightValues = new double[weights.Length];
        for (int i = 0; i < weightValues.Length; i++)
        {
            double weightValue = weights[i].Value;
            weightValues[i] = weightValue;
        }
        return weightValues;
    }

    public double GetWeightValue(int pos)
    {
        return weights[pos].Value;
    }

    public Bias GetBias()
    {
        return bias;
    }

    public double GetBiasValue()
    {
        return bias.Value;
    }

    public Output Activation(Input[] inputs)
    {
        double sum = bias.Value;

        for (int i = 0; i < inputs.Length; i++)
        {
            double value = inputs[i].Value;
            double weight = weights[i].Value;
            sum += value * weight;
        }

        this.SetValue(Math.Tanh(sum));

        return new Output(this.Value);
    }

    public bool NotEqual(Neuron other)
    {
        return !Equals(other);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Neuron otherNeuron)
            return false;

        if (weights.Length != otherNeuron.weights.Length)
            return false;

        const double epsilon = 1e-9;

        if (Math.Abs(bias.Value - otherNeuron.bias.Value) > epsilon)
            return false;

        return weights
        .Zip(otherNeuron.weights, (a, b) => Math.Abs(a.Value - b.Value) <= epsilon)
        .All(equal => equal);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(bias.Value);

        foreach (var w in weights)
            hash.Add(w.Value);

        return hash.ToHashCode();
    }

    public Neuron Clone()
    {
        return new Neuron(bias, weights);
    }

}
