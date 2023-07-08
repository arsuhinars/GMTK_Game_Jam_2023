using System.Collections;
using UnityEngine;

namespace GMTK_2023.Utils
{
    public static class RandomUtils
    {
        public static Vector3 GetRandomVectorInRadius(float minRadius, float maxRadius)
        {
            float r = Random.Range(minRadius, maxRadius);
            float a = Random.Range(0f, 2f * Mathf.PI);
            return new Vector3(Mathf.Cos(a), 0f, Mathf.Sin(a)) * r;
        }
    }
}
