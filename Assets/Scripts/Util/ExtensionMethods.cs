using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods {
    private static System.Random random = new System.Random();

    public static void Shuffle(this IList list) {
        int n = list.Count;

        while (n > 1) {
            n--;

            int k = random.Next(n + 1);
            System.Object value = list[k];

            list[k] = list[n];
            list[n] = value;
        }
    }

    public static bool EqualsIgnoreCase(this string text, string compareTo) {
        return text.Equals(compareTo, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Make transform look at an Vector2 point in world space
    /// Defaults to up position
    /// </summary>
    /// <param name="target"></param>
    public static void LookAt2D(this Transform transform, Vector2 target) {
        Vector3 dir = ((Vector3) target) - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90F;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// Make transform look at an Vector2 point in world space
    /// </summary>
    /// <param name="target"></param>
    /// <param name="up"></param>
    public static void LookAt2D(this Transform transform, Vector2 target, Direction direction) {
        Vector3 dir = ((Vector3)target) - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + ((int) direction);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
