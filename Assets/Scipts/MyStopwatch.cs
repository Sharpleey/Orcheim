using System.Diagnostics;

public static class MyStopwatch
{
    private static Stopwatch Stopwatch = new Stopwatch();

    public static void StartStopwatch()
    {
        Stopwatch.Reset();
        Stopwatch.Start();
    }

    public static void StopStopwatch()
    {
        Stopwatch.Stop();
        UnityEngine.Debug.Log("ms: " + Stopwatch.ElapsedMilliseconds + " Ticks: " + Stopwatch.ElapsedTicks);
    }
}
