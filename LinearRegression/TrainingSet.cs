namespace mathematicalOptimization.LinearRegression;

public class TrainingSet
{
    private readonly double _k;//угловой коэффициент
    private readonly double _b;//свободный член
    private readonly int _amountPoints; // количество точек
    private readonly Random _rand;
    
    public TrainingSet(double w0, double w1, int amountPoints)
    {
        _k = w1;
        _b = w0;
        _amountPoints = amountPoints;
        _rand = new Random();
    }
    
    /// <summary>
    /// Создание набора данных с небольшим шумом около прямой y = _k * x + _b
    /// </summary>
    /// <returns></returns>
    public Point[] CreateTestData()
    {
        Point[] points = new Point[_amountPoints];
        for (int i = 0; i < _amountPoints; i++)
        {
            double x = i;
            double y = _k * x + _b + Math.Pow(-1, i) *_rand.NextDouble();
            points[i] = new Point(x, y);
        }

        return points;
    }
    
    
}