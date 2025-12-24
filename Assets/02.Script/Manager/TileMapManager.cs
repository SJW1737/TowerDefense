using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] private GameObject towerTilePrefab;
    [SerializeField] private GameObject pathTilePrefab;

    private GridManager gridManager;

    private void Start()
    {
        gridManager = GridManager.Instance;
        CreateTiles();
    }

    private void CreateTiles()
    {
        Node[,] grid = gridManager.grid;

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = grid[x, y];

                GameObject prefab = node.isBlocked ? towerTilePrefab : pathTilePrefab;

                Vector3 pos = new Vector3(x + 0.5f, y + 0.5f, 0f);

                Instantiate(prefab, pos, Quaternion.identity, transform);
            }
        }
    }
}