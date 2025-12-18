using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    private int width;
    private int height;
    private int minPathLength;

    public Node[,] grid;

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

    public Node GetNode(int x, int y) => grid[x, y];

    public List<Node> RandomPath()
    {
        List<Node> path = new List<Node>();

        int startX = Random.Range(0, width);

        Node start = grid[startX, height - 1];

        int x = start.x;
        int y = height - 1;

        path.Add(start);
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
                return path;
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

        if (grid != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Node node = grid[x, y];

                    // isBlocked에 따라 색 다르게
                    if (node.isBlocked)
                        Gizmos.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);   // 회색 (타워 설치 가능)
                    else
                        Gizmos.color = new Color(0f, 0.5f, 1f, 0.5f);         // 파란색 (몬스터 길)

                    // 각 노드의 중앙 위치에 정사각형 그리기
                    Gizmos.DrawCube(new Vector3(x + 0.5f, y + 0.5f, 0), new Vector3(1, 1, 0));
                }
            }
        }
    }
}
