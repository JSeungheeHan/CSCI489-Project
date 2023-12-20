using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezzierCurve
{
    
    private static Vector2 Bezier(Vector2 a, Vector2 b, float t) {
    return Vector2.Lerp(a, b, t);
    }

    public static Vector2 Bezier(Vector2 a, Vector2 b, Vector2 c, float t) {
        return Vector2.Lerp(Bezier(a, b, t), Bezier(b, c, t), t);
    }

    public static Vector2 Bezier(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t) {
        return Vector2.Lerp(Bezier(a, b, c, t), Bezier(b, c, d, t), t);
    }
}
