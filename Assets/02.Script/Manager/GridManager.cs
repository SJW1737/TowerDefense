using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    public int width;
    public int height;
    public int minPathLength;

    public Node[,] grid;

    protected override void Awake()
    {
        base.Awake();
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
        int endX = Random.Range(0, width);

        Node start = grid[startX, height - 1];
        Node end = grid[endX, 0];

        int x = start.x;
        int y = height - 1;

        int a = -1;
        int b = 1;

        // i = 0,1,2,3,4,5
        for (int i = 0; i < width / 2; i++) // width에 맞게 할려고 하는거임 ㅋㅋ
        {
            if ((Mathf.Abs(x - (width / 2))) > (width / 2 - i)) // 6, 5, 4, 3,2,1
            {
                if (x - (width / 2) > 0)
                {
                    a -= (width / 2 - i) / 2;
                }
                else
                {
                    b += (width / 2 - i) / 2;
                }
            }
        }
        // 1. 얼마나 떨어져있냐 > 6
        // 2. 얼머나 떨어져있냐 > 5
        

        path.Add(start);
        grid[x, y].isBlocked = false;

        while (path.Count < minPathLength && y > 0)
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

            int side;
            int randomSide = Random.Range(7, 11);

            int randomDir = Random.Range(a, b);
            if (randomDir < 0)
            {
                side = -1;
                b += 1;
            }
            else
            {
                side = 1;
                a -= 1;
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

        path.Add(end);

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
