using UnityEngine;

namespace Infrastructure.Utils
{
    public static class BezierCurve
    {
        public static Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float step)
        {
            step = Mathf.Clamp01(step);

            float delta = 1 - step;
            Vector2 firstEquation = delta * delta * delta * p0;
            Vector2 secondEquation = 3 * delta * delta * step * p1;
            Vector2 thirdEquation = 3 * delta * step * step * p2;
            Vector2 fourthEquation = step * step * step * p3;

            return firstEquation + secondEquation + thirdEquation + fourthEquation;
        }
    }
}