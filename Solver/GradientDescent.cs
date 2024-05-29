namespace mathematicalOptimization.Solver;

public static class GradientDescent
{

    private const int MaxIterations = 100000;
    private const double AbsoluteError = 0.00001;
    private const double IncrementArgument = 0.0001;
    private const double StepLength = 0.01;
    
    /// <summary>
    /// Вычисление приращения функции по одному аргументу
    /// </summary>
    /// <param name="number"></param>
    /// <param name="x"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Вычисление градиента
    /// </summary>
    /// <param name="function"></param>
    /// <param name="x"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Проверка выполнения критериев останова
    /// (функция почти не меняется, решение почти не меняется)
    /// </summary>
    /// <param name="function"></param>
    /// <param name="xStart"></param>
    /// <param name="xNext"></param>
    /// <returns></returns>
    private static bool CheckStopCriterion(Func<double[], double> function, double[] xStart, double[] xNext)
    {
        bool isFunctionNotChange = Math.Abs(function(xStart) - function(xNext)) < AbsoluteError;
        double[] negativeVector = VectorTools.MultiplyVectorByNumber(xNext, -1);
        double[] vectorDifference = VectorTools.СalculateVectorSum(xStart, negativeVector);
        bool isPointNotChange = VectorTools.СalculateVectorNorm(vectorDifference) < AbsoluteError;

        return isFunctionNotChange && isPointNotChange;
    }


    /// <summary>
    /// Вычисление локального минимума целевой функции
    /// </summary>
    /// <param name="function"></param>
    /// <param name="xStart"></param>
    /// <returns></returns>
    public static Result FindLocalMin(Func<double[], double> function, double[] xStart)
    {
        double stepLength = StepLength;
        for(int iter = 0; iter < MaxIterations; iter++)
        {
            double[] gradient = CalcGradient(function, xStart);
            double gradientNorm = VectorTools.СalculateVectorNorm(gradient);
            if (gradientNorm < AbsoluteError)
            {
                return new Result(xStart, function(xStart), true);
            }

            double[] xNext = CalcNewPoint(function, xStart, stepLength, gradient);
            bool isStopCriterion = CheckStopCriterion(function, xStart, xNext);
            if (isStopCriterion)
            {
                return new Result(xNext, function(xNext), true);
            }
            xStart = xNext;
        }
        return new Result(xStart, function(xStart), false);
        
    }

    /// <summary>
    /// Вычисление нового решения, т.е. новой точки x
    /// </summary>
    /// <param name="function"></param>
    /// <param name="xStart"></param>
    /// <param name="stepLength"></param>
    /// <param name="gradient"></param>
    /// <returns></returns>
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