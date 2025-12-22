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
}
