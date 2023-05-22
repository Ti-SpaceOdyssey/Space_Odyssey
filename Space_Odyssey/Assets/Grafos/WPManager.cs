using UnityEngine;
using System.Collections.Generic;

public class WPManager : MonoBehaviour
{
    private Graph graph; // Reference to the graph
    public List<Transform> waypoints; // List to store your waypoints

    private void Awake()
    {
        // Populate the waypoints list with all child transforms
        waypoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            waypoints.Add(child);
        }
    }

    // You can add more methods or functionality to your WPManager class as needed

    // Example method to get a random waypoint from the list
    public Transform GetRandomWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogWarning("No waypoints found!");
            return null;
        }

        int randomIndex = Random.Range(0, waypoints.Count);
        return waypoints[randomIndex];
    }
}
