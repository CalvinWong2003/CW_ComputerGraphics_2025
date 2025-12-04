using Unity.VisualScripting;
using UnityEngine;

public class RangeX
{
    internal static float start = -1;
    internal static float end = -1;
    public static void AddPoint(float x)
    {
        if (start == -1)
        {
            start = x;
            return;
        }
        if (end == -1)
        {
            if (start <= x)
            {
                end = x;
            }
            else
            {
                end = start;
                start = x;
            }
            return;
        }

        if (x < start)
        {
            start = x;
        }
        if (x > end)
        {
            end = x;
        }
    }
}
