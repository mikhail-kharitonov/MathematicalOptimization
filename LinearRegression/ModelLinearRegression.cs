using mathematicalOptimization.Solver;

namespace mathematicalOptimization.LinearRegression;

public class ModelLinearRegression
{
    private readonly Point[] _trainingSet;
    
    public ModelLinearRegression(Point[] trainingSet)
    {
        _trainingSet = trainingSet;
    }
    

    public double CalcLossFunction(double[] w)
    {
        int amount = _trainingSet.Length;
        double l = 0;
        foreach (var point in _trainingSet)
        {
            l = l + (w[1] * point.X + w[0] - point.Y) * (w[1] * point.X + w[0] - point.Y);
        }

        return l / amount;
    }

    public double[] GetOptParams()
    {
        double[] xStart = new[] { 1.0, 1.0 };
        var x = GradientDescent.FindLocalMin(CalcLossFunction, xStart).XOpt;
        return x;
    }
}