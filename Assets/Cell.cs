using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public GameObject backgroundObject;
    public GameObject iconObject;

    private bool hasGem = false;
    private Gem linkedGem = null; // Gem mà cell này thuộc về

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
        if (iconObject != null)
            iconObject.SetActive(false);
    }

    public void SetGemPart(Gem gem)
    {
        hasGem = true;
        linkedGem = gem;
    }

    public bool HasGem()
    {
        return hasGem;
    }

    public void OnMouseDown()
    {
        Dig();
    }

    private void Dig()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        Debug.Log($"Đào cell ({x},{y})");

        if (linkedGem != null)
        {
            linkedGem.RevealCell(this);

            if (linkedGem.IsFullyRevealed())
            {
                linkedGem.Collect();
            }
        }
        else
        {
            // Không có gem, chỉ đào đất
            if (backgroundObject != null)
                backgroundObject.SetActive(true);
        }
    }

    public void Reveal()
    {
        if (iconObject != null)
            iconObject.SetActive(true);

        if (backgroundObject != null)
            backgroundObject.SetActive(false);
    }
}
