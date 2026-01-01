using Domain.Exceptions;
using Domain.Utils;

namespace Domain.Entities;

public class Neuron : Node
{
    private double _bias;
    private double[] _weights;

    public Neuron(double bias, double[] weights)
    {
        ValidatorDomain.ValidateValue(bias);
        ValidatorDomain.ValidateValues(weights);
        _bias = bias;
        _weights = weights;
    }

    public override string ToString()
    {
        return FormatNeuron.ToString(this);
    }

    public double[] GetWeights()
    {
        return _weights;
    }

    public double GetBias()
    {
        return _bias;
    }

    public Output Activation(Input[] inputs)
    {
        double sum = _bias;

        for (int i = 0; i < inputs.Length; i++)
        {
            double value = inputs[i].Value;
            double weight = _weights[i];
            sum += value * weight;
        }

        this.SetValue(Math.Tanh(sum));

        return new Output(this.Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Neuron otherNeuron)
            return false;

        if (_weights.Length != otherNeuron._weights.Length)
            return false;

        const double epsilon = 1e-9;

        if (Math.Abs(_bias - otherNeuron._bias) > epsilon)
            return false;

        return _weights
        .Zip(otherNeuron._weights, (a, b) => Math.Abs(a - b) <= epsilon)
        .All(equal => equal);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(_bias);

        foreach (var w in _weights)
            hash.Add(w);

        return hash.ToHashCode();
    }

    public Neuron Clone()
    {
        return new Neuron(_bias, _weights!, BiasMutation, WeightMutation);
    }

}
