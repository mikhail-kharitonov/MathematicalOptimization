using mathematicalOptimization;
using mathematicalOptimization.LinearRegression;
using mathematicalOptimization.Solver;


namespace mathematicalOptimization;

public static class Program
{
    public static void Main()
    {
        
        //GradientDescent gradientDescent = new GradientDescent();
        double[] xStart = new[] { 1.0, 1.0 };
        var res = GradientDescent.FindLocalMin(GoalFunc, xStart);
        
        foreach (double d in res.XOpt)
        {
            Console.WriteLine($"{d}");
        }
        
        //Линейная регрессия
        TrainingSet trainingSet = new TrainingSet(5, 7, 175);
        var trDta = trainingSet.CreateTestData();

        ModelLinearRegression linearRegression = new ModelLinearRegression(trDta);
        var w = linearRegression.GetOptParams();
        
        foreach (double d in w)
        {
            Console.WriteLine($"{d}");
        }


    }

    public static double GoalFunc(double[] x)
    {
        return x[0] * x[0] + x[1] * x[1];
    }
    
}