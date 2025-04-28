using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int rows = 4;
    public int columns = 4;
    public float cellSize = 100f;
    public GameObject cellPrefab;
    public Transform gridParent;

    private Cell[,] gridCells;

    void Start()
    {
        GenerateGrid();
        PlaceGems();
    }

    private void GenerateGrid()
    {
        gridCells = new Cell[rows, columns];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject cellObj = Instantiate(cellPrefab, gridParent);
                cellObj.transform.localPosition = new Vector3(x * cellSize, -y * cellSize, 0);
                Cell cell = cellObj.GetComponent<Cell>();
                cell.Init(x, y);
                gridCells[x, y] = cell;
            }
        }
    }

    private void PlaceGems()
    {
        // Ví dụ đặt 1 gem 1x2
        PlaceGem(1, 2);

        // Đặt 1 gem 2x2
        PlaceGem(2, 2);

        // Đặt 1 gem 1x3
        PlaceGem(1, 3);
    }

    private void PlaceGem(int width, int height)
    {
        bool placed = false;
        int attempts = 0;
        while (!placed && attempts < 100)
        {
            int startX = Random.Range(0, columns);
            int startY = Random.Range(0, rows);

            if (CanPlaceGem(startX, startY, width, height))
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        gridCells[startX + x, startY + y].SetGemPart();
                    }
                }
                placed = true;
            }

            attempts++;
        }

        if (!placed)
        {
            Debug.LogWarning($"Không thể đặt Gem {width}x{height} sau {attempts} lần thử.");
        }
    }

    private bool CanPlaceGem(int startX, int startY, int width, int height)
    {
        if (startX + width > columns || startY + height > rows)
            return false;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridCells[startX + x, startY + y].HasGem())
                    return false;
            }
        }

        return true;
    }
}
