using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public Image backgroundImage;
    public Image iconImage;

    private bool hasGem = false;

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetGemPart()
    {
        hasGem = true;
        // Set icon (tùy bạn), ví dụ bật 1 sprite gem
        if (iconImage != null)
            iconImage.enabled = true;
    }

    public bool HasGem()
    {
        return hasGem;
    }
}
