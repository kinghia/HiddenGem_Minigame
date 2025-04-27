using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class BoardSize : MonoBehaviour
{
    [SerializeField] GameObject Main;
    [SerializeField] GameObject Stage1;
    [SerializeField] GameObject Stage2;
    [SerializeField] GameObject Stage3;
    [SerializeField] GameObject Stage4;
    [SerializeField] GameObject Stage5;
    private GameObject currentStage = null;

    public TextMeshProUGUI widthText;
    public TextMeshProUGUI heightText;
    public Button widthUpButton;
    public Button widthDownButton;
    public Button heightUpButton;
    public Button heightDownButton;
    public Button okButton;

    private int width = 1;
    private int height = 1;
    private const int minSize = 1;
    private const int maxSize = 8;

    void Start()
    {
        // Gán sự kiện cho các nút
        widthUpButton.onClick.AddListener(IncreaseWidth);
        widthDownButton.onClick.AddListener(DecreaseWidth);
        heightUpButton.onClick.AddListener(IncreaseHeight);
        heightDownButton.onClick.AddListener(DecreaseHeight);
        okButton.onClick.AddListener(ConfirmSize);

        UpdateUI();
    }

    private void IncreaseWidth()
    {
        width++;
        if (width > maxSize)
            width = minSize;

        UpdateUI();
    }

    private void DecreaseWidth()
    {
        width--;
        if (width < minSize)
            width = maxSize;

        UpdateUI();
    }

    private void IncreaseHeight()
    {
        height++;
        if (height > maxSize)
            height = minSize;

        UpdateUI();
    }

    private void DecreaseHeight()
    {
        height--;
        if (height < minSize)
            height = maxSize;

        UpdateUI();
    }


    private void ConfirmSize()
    {
        Debug.Log($"BoardSize đã lựa chọn: {width} x {height}");

        GameObject nextStage = null;

        if (width == 4 && height == 4)
        {
            nextStage = Stage1;
        }
        else if (width == 5 && height == 5)
        {
            nextStage = Stage2;
        }
        else if (width == 6 && height == 6)
        {
            nextStage = Stage3;
        }
        else if (width == 7 && height == 7)
        {
            nextStage = Stage4;
        }
        else if (width == 8 && height == 8)
        {
            nextStage = Stage5;
        }
        else
        {
            Debug.Log("Size không hợp lệ");
            return;
        }

        // Nếu đang có stage cũ bật, tắt nó
        if (currentStage != null)
        {
            currentStage.SetActive(false);
        }

        // Bật stage mới
        nextStage.SetActive(true);

        // Cập nhật stage hiện tại
        currentStage = nextStage;
    }


    private void UpdateUI()
    {
        widthText.text = width.ToString();
        heightText.text = height.ToString();
    }
}
