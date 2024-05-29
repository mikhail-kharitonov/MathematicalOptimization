namespace mathematicalOptimization;


public static class VectorTools
{
    
    /// <summary>
    /// Вычисление евклидовой нормы вектора
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double СalculateVectorNorm(double[] x)
    {
        double sum = x.Sum(d => d * d);
        return Math.Sqrt(sum);
    }
    
    /// <summary>
    /// Вычисление суммы векторов одной размерности
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static double[] СalculateVectorSum(double[] x, double[] y)
    {
        int dim = x.Length;
        double[] sum = new double[dim];
        for (int i = 0; i < dim; i++)
        {
            sum[i] = x[i] + y[i];
        }
        return sum;
    }

    /// <summary>
    /// Умножение вектора на число
    /// </summary>
    /// <param name="x"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static double[] MultiplyVectorByNumber(double[] x, double k)
    {
        return x.Select(i => k * i).ToArray();
    }
    
    
}