using UnityEngine;

namespace J_Func.Math
{
    /// <summary>
    /// Custom functions for mathematics.
    /// Includes Rounding
    /// </summary>
    public static class J_Mathf
    {
        #region Rounding
        public static float Round(float input, int rountTo = 1)
        {
            if (rountTo == 0) return 0;
            return Mathf.Round(input / rountTo) * rountTo;
        }
        public static int RoundToInt(float input, int rountTo = 1)
        {
            if (rountTo == 0) return 0;
            return Mathf.RoundToInt(input / rountTo) * rountTo;
        }

        // Vector 3
        public static Vector2 Round(Vector2 input, int rountTo = 1)
            => new Vector2(
                Round(input.x, rountTo),
                Round(input.y, rountTo)
                );
        public static Vector2Int RoundToInt(Vector2 input, int rountTo = 1)
            => new Vector2Int(
                RoundToInt(input.x, rountTo),
                RoundToInt(input.y, rountTo)
                );

        // Vector 3
        public static Vector3 Round(Vector3 input, int rountTo = 1)
            => new Vector3(
                Round(input.x, rountTo),
                Round(input.y, rountTo),
                Round(input.z, rountTo)
                );
        public static Vector3Int RoundToInt(Vector3 input, int rountTo = 1)
            => new Vector3Int(
                RoundToInt(input.x, rountTo),
                RoundToInt(input.y, rountTo),
                RoundToInt(input.z, rountTo)
                );
        #endregion
    }
}