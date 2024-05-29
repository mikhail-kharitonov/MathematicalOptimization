namespace mathematicalOptimization.Solver;

public static class GradientDescent
{

    private const int MaxIterations = 100000;
    private const double AbsoluteError = 0.00001;
    private const double IncrementArgument = 0.0001;
    private const double StepLength = 0.001;
    
    private static double[] CalcDelta(int number, double[] x)
    {
        int n = x.Length;
        double[] delta = new double[n];
        for (int i = 0; i < n; i++)
        {
            if (i != number)
            {
                delta[i] = x[i];
            }
            else
            {
                delta[i] = x[i] + IncrementArgument;
            }
        }

        return delta;
    }

    private static double[] CalcGradient(Func<double[], double> function, double[] x)
    {
        int n = x.Length;
        double[] gradient = new double[n];
        for (int i = 0; i < n; i++)
        {
            double[] delta = CalcDelta(i, x);
            gradient[i] = (function(delta) - function(x)) / IncrementArgument;
        }

        return gradient;
    }

    private static bool CheckStopCriterion(Func<double[], double> function, double[] xStart, double[] xNext)
    {
        bool diffFunction = Math.Abs(function(xStart) - function(xNext)) < AbsoluteError;
        double[] negativeVector = VectorTools.MultiplyVectorByNumber(xNext, -1);
        double[] vectorDifference = VectorTools.СalculateVectorSum(xStart, negativeVector);
        bool diffVector = VectorTools.СalculateVectorNorm(vectorDifference) < AbsoluteError;

        return diffFunction && diffVector;
    }


    public static Result FindLocalMin(Func<double[], double> function, double[] xStart)
    {
        double stepLength = StepLength;
        int numberIter = 0;
        while (true)
        {
            double[] gradient = CalcGradient(function, xStart);
            double gradientNorm = VectorTools.СalculateVectorNorm(gradient);
            if (gradientNorm < AbsoluteError)
            {
                return new Result(xStart, function(xStart), true);
            }

            double[] xNext = CalcNewPoint(function, xStart, stepLength, gradient);
            bool stopCriterion = CheckStopCriterion(function, xStart, xNext);
            if (stopCriterion)
            {
                return new Result(xNext, function(xNext), true);
            }

            numberIter++;
            if (numberIter >= MaxIterations)
            {
                return new Result(xNext, function(xNext), false);
            }

            xStart = xNext;
        }
    }

    private static double[] CalcNewPoint(Func<double[], double> function, double[] xStart, double stepLength,
        double[] gradient)
    {
        double[] multiplyGradientByNumber = VectorTools.MultiplyVectorByNumber(gradient, -stepLength);
        double[] xNext = VectorTools.СalculateVectorSum(xStart, multiplyGradientByNumber);

        
        if (!(function(xNext) > function(xStart)))
            return xNext;
        stepLength /= 2;
        xNext = CalcNewPoint(function, xStart, stepLength, gradient);
        

        return xNext;
    }
}