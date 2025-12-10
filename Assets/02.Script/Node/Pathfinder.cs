using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Pathfinder : Singleton<Pathfinder>
{
    public GridManager grid;

    private readonly int[,] directions =
    {
        { 1, 0 },
        { -1, 0 },
        { 0, 1 },
        { 0, -1 }
    };

    public List<Node> FindPath(Node start, Node goal)
    {
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(start);

        start.gCost = 0;
        start.hCost = GetHeuristic(start, goal);
        start.parent = null;

        while (openList.Count > 0)
        {
            Node current = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < current.fCost || (openList[i].fCost == current.fCost && openList[i].hCost < current.hCost))
                {
                    current = openList[i];
                }
            }

            openList.Remove(current);
            closedList.Add(current);

            if (current == goal)
                return BuildPath(goal);

            for (int i = 0; i < 4; i++)
            {
                Node neighbor = grid.GetNode(current.x + directions[i, 0], current.y + directions[i, 1]);

                if (neighbor == null || neighbor.isBlocked || closedList.Contains(neighbor))
                    continue;

                int newGCost = current.gCost + 1;

                if (newGCost < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newGCost;
                    neighbor.hCost = GetHeuristic(neighbor, goal);
                    neighbor.parent = current;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        return null;
    }

    private int GetHeuristic(Node a, Node b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private List<Node> BuildPath(Node goal)
    {
        List<Node> path = new List<Node>();
        Node current = goal;

        while (current != null)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }

}
