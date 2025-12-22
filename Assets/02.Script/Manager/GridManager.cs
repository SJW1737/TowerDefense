using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    private int width;
    private int height;
    private int minPathLength;

    public Node[,] grid;

    public Node startNode;
    public Node endNode;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Init()
    {
        width = 12;
        height = 13;
        minPathLength = 100;

        InitGrid();
    }

    private void InitGrid()
    {
        grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Node(x, y);
            }
        }
        RandomPath();
    }

    public Node GetNode(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return null;
        }

        return grid[x, y];
    }

    public List<Node> RandomPath()
    {
        List<Node> path = new List<Node>();

        int startX = Random.Range(0, width);

        startNode = grid[startX, height - 1];

        int x = startNode.x;
        int y = height - 1;

        path.Add(startNode);
        grid[x, y].isBlocked = false;

        while (path.Count < minPathLength && y > 1)
        {
            int down = -1;
            for (int i = 0; i <= 1; i++)
            {
                if (y + down >= 0)
                {
                    path.Add(grid[x, y + down]);
                    grid[x, y + down].isBlocked = false;
                    y += down;
                }
            }

            if (y == 0)
            {
                break;
            }

            int side;
            int randomSide = Random.Range(8, 11);

            int randomDir = Random.Range(0, 2);
            if (randomDir == 0)
            {
                side = -1;
            }
            else
            {
                side = 1;
            }

            if (x <= 0)
            {
                side = 1;
            }
            else if (x >= width - 1)
            {
                side = -1;
            }

            for (int i = 1; i <= randomSide; i++)
            {
                if (0 <= x + side && x + side <= width - 1)
                {
                    path.Add(grid[x + side, y]);
                    grid[x + side, y].isBlocked = false;
                    x += side;
                }
            }
        }

        endNode = path[path.Count - 1];
        return path;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // 세로선
        for (int x = 0; x <= width; x++)
        {
            Gizmos.DrawLine(
                new Vector3(x, 0, 0),
                new Vector3(x, height, 0)
            );
        }

        // 가로선
        for (int y = 0; y <= height; y++)
        {
            Gizmos.DrawLine(
                new Vector3(0, y, 0),
                new Vector3(width, y, 0)
            );
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = grid[x, y];

                // 기본 색
                if (node.isBlocked)
                    Gizmos.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
                else
                    Gizmos.color = new Color(0f, 0.5f, 1f, 0.5f);

                if (node == startNode)
                    Gizmos.color = Color.green;
                else if (node == endNode)
                    Gizmos.color = Color.red;

                Gizmos.DrawCube(
                    new Vector3(x + 0.5f, y + 0.5f, 0),
                    Vector3.one
                );
            }
        }
    }
}
