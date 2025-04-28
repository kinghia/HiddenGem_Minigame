using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public List<Cell> occupiedCells = new List<Cell>();

    private int revealedCells = 0;
    private bool collected = false;

    public void AssignCell(Cell cell)
    {
        occupiedCells.Add(cell);
        cell.SetGemPart(this);
    }

    public void RevealCell(Cell cell)
    {
        if (!occupiedCells.Contains(cell))
            return;

        cell.Reveal();
        revealedCells++;
    }

    public bool IsFullyRevealed()
    {
        return revealedCells >= occupiedCells.Count;
    }

    public void Collect()
    {
        if (collected) return;

        collected = true;

        Debug.Log($"Gem {this.name} đã được collect!");

        // TODO: Add animation collect bay về slot missing hoặc chest
        // Sau đó có thể Destroy(gameObject) nếu cần
        Destroy(this.gameObject);
    }
}
