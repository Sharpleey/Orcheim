using System.Diagnostics;

public static class MyStopwatch
{
    private static Stopwatch Stopwatch = new Stopwatch();

    public static void Start()
    {
        Stopwatch.Reset();
        Stopwatch.Start();
    }

    public static void Stop()
    {
        Stopwatch.Stop();
        UnityEngine.Debug.Log("ms: " + Stopwatch.ElapsedMilliseconds + " Ticks: " + Stopwatch.ElapsedTicks);
    }
}
