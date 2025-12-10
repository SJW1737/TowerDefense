using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    public int width;
    public int height;

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
    }

    public Node GetNode(int x, int y) => grid[x, y];

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
    }
}
