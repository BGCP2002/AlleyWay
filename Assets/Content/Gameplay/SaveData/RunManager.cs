using UnityEngine;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public static StructureDataModule StructureData = new();
}
