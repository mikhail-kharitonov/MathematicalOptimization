using mathematicalOptimization.Solver;

namespace mathematicalOptimization.LinearRegression;

public class ModelLinearRegression
{
    private readonly Point[] _trainingSet;
    
    public ModelLinearRegression(Point[] trainingSet)
    {
        _trainingSet = trainingSet;
    }


    private double CalcLossFunction(double[] w)
    {
        int amount = _trainingSet.Length;
        double lossFunction = 0;
        foreach (Point point in _trainingSet)
        {
            lossFunction = lossFunction + (w[1] * point.X + w[0] - point.Y) * (w[1] * point.X + w[0] - point.Y);
        }

        return lossFunction / amount;
    }

    public double[] GetOptParams()
    {
        double[] xStart = new[] { 1.0, 1.0 };
        return GradientDescent.FindLocalMin(CalcLossFunction, xStart).XOpt;
    }
}