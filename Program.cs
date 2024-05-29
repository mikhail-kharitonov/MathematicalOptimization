using mathematicalOptimization;
using mathematicalOptimization.LinearRegression;
using mathematicalOptimization.Solver;


namespace mathematicalOptimization;

public static class Program
{
    public static void Main()
    {
        //Линейная регрессия
        double w0 = 5;
        double w1 = 7;
        Console.WriteLine($"Начальные параметры: w_0 = {w0}, w_1 = {w1}");
        
        TrainingSet trainingSet = new TrainingSet(w0, w1, 100);
        Point[] data = trainingSet.CreateTestData();

        ModelLinearRegression linearRegression = new ModelLinearRegression(data);
        double[] w = linearRegression.GetOptParams();
        
        Console.WriteLine($"Оптимальные параметры: w_0 = {w[0]}, w_1 = {w[1]}");
        


    }
    
    
}