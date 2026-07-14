using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlotHandler : MonoBehaviour
{
    public static PlotHandler Instance;

    [SerializeField] private Transform plotParent;
    private List<Plot> plots;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        plots = plotParent.GetComponentsInChildren<Plot>().ToList();
    }

    /// <summary>
    /// Recieve all plots within range
    /// </summary>
    internal List<Plot> GetNeighbours(Plot plot, int range)
    {
        List<Plot> neighbours = new();

        foreach (Plot other in plots)
        {
            if (other == plot)
                continue;

            if (Vector3.Distance(plot.saveData.position, other.saveData.position) <= range)
                neighbours.Add(other);
        }

        return neighbours;
    }
}
