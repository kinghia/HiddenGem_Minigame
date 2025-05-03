using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int rows = 4;
    public int columns = 4;
    public float cellSize = 1f;
    public GameObject cellPrefab;
    public Transform gridParent;

    public GameObject gem1x2Prefab;
    public GameObject gem1x2Prefabs;
    public GameObject gem1x3Prefab;
    public GameObject gem2x2Prefab;

    private Cell[,] gridCells;

    void Start()
    {
        GenerateGrid();
        PlaceGem(1, 2, gem1x2Prefab);
        PlaceGem(1, 2, gem1x2Prefabs);
        PlaceGem(2, 2, gem2x2Prefab);
        PlaceGem(1, 3, gem1x3Prefab);
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
        
    }

    private void PlaceGem(int width, int height, GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab bị null khi đặt gem " + width + "x" + height);
            return;
        }

        bool placed = false;
        int attempts = 0;

        while (!placed && attempts < 100)
        {
            int startX = Random.Range(0, columns - width + 1);
            int startY = Random.Range(0, rows - height + 1);

            if (CanPlaceGem(startX, startY, width, height))
            {
                // Tính vị trí trung tâm của các Cell được gem chiếm
                Vector3 gemPosition = Vector3.zero;
                List<Cell> gemCells = new List<Cell>();

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Cell cell = gridCells[startX + x, startY + y];
                        gemCells.Add(cell);
                        gemPosition += cell.transform.position;
                    }
                }

                gemPosition /= gemCells.Count; // Trung bình vị trí

                // Instantiate Gem ở vị trí đó
                GameObject gemObj = Instantiate(prefab, gemPosition, Quaternion.identity);
                Gem gem = gemObj.GetComponent<Gem>();

                foreach (var cell in gemCells)
                {
                    gem.AssignCell(cell);
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
