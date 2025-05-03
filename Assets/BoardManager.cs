using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public int gridWidth = 4;
    public int gridHeight = 4;
    public float cellSize = 1f;
    public Transform gridParent;
    public GameObject cellPrefab;
    public GameObject gemPrefab;

    private Cell[,] gridCells;

    void Start()
    {
        GenerateGrid();
        PlaceGem(1, 2); // Gem 1x2
        PlaceGem(2, 2); // Gem 2x2
        PlaceGem(1, 3); // Gem 1x3
    }

    void GenerateGrid()
    {
        gridCells = new Cell[gridWidth, gridHeight];

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject cellObj = Instantiate(cellPrefab, gridParent);
                cellObj.transform.localPosition = new Vector3(x * cellSize, -y * cellSize, 0);
                Cell cell = cellObj.GetComponent<Cell>();
                cell.Init(x, y);
                gridCells[x, y] = cell;
            }
        }
    }

    void PlaceGem(int width, int height)
    {
        bool placed = false;
        int attempt = 0;

        while (!placed && attempt < 100)
        {
            int startX = Random.Range(0, gridWidth - width + 1);
            int startY = Random.Range(0, gridHeight - height + 1);

            if (CanPlace(startX, startY, width, height))
            {
                GameObject gemObj = Instantiate(gemPrefab);
                Gem gem = gemObj.GetComponent<Gem>();

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Cell cell = gridCells[startX + x, startY + y];
                        gem.AssignCell(cell);
                    }
                }

                // Set vị trí Gem trung tâm dựa trên các cell nó chiếm
                float worldX = (startX + width / 2f - 0.5f) * cellSize;
                float worldY = -(startY + height / 2f - 0.5f) * cellSize;
                gem.transform.position = gridParent.position + new Vector3(worldX, worldY, 0);

                placed = true;
            }

            attempt++;
        }

        if (!placed)
        {
            Debug.LogWarning($"Không thể đặt Gem {width}x{height}");
        }
    }

    bool CanPlace(int startX, int startY, int width, int height)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (gridCells[startX + x, startY + y].HasGem())
                    return false;
            }
        }

        return true;
    }
}
