using UnityEngine;

public static class J_Mathf
{
    public static Vector3Int RoundToVector3Int(Vector3 input, int rountTo = 1)
    {
        Vector3Int output = Vector3Int.zero;
        output.x = Mathf.RoundToInt(input.x / rountTo) * rountTo;
        output.y = Mathf.RoundToInt(input.y / rountTo) * rountTo;
        output.z = Mathf.RoundToInt(input.z / rountTo) * rountTo;
        return output;
    }
}
