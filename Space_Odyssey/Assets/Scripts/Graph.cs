using UnityEngine;
using System.Collections.Generic;

public class Graph
{
    private Dictionary<Transform, List<Transform>> adjacencyList;

    public Graph(List<Transform> waypoints)
    {
        adjacencyList = new Dictionary<Transform, List<Transform>>();

        foreach (Transform waypoint in waypoints)
        {
            adjacencyList.Add(waypoint, new List<Transform>());
        }

        foreach (Transform waypoint in waypoints)
        {
            foreach (Transform neighbor in waypoints)
            {
                if (waypoint != neighbor)
                {
                    adjacencyList[waypoint].Add(neighbor);
                }
            }
        }
    }

    public Transform GetFarthestNode(Transform playerTransform)
    {
        Transform farthestNode = null;
        float maxXDistance = 0f;
        float maxYDistance = 0f;

        foreach (Transform waypoint in adjacencyList.Keys)
        {
            float xDistance = Mathf.Abs(waypoint.position.x - playerTransform.position.x);
            float yDistance = Mathf.Abs(waypoint.position.y - playerTransform.position.y);

            if (xDistance > maxXDistance || (xDistance == maxXDistance && yDistance > maxYDistance))
            {
                maxXDistance = xDistance;
                maxYDistance = yDistance;
                farthestNode = waypoint;
            }
        }

        return farthestNode;
    }

    public void RemoveNode(Transform node)
    {
        adjacencyList.Remove(node);

        foreach (List<Transform> neighbors in adjacencyList.Values)
        {
            neighbors.Remove(node);
        }
    }

    public int GetNodeCount()
    {
        return adjacencyList.Count;
    }
}
