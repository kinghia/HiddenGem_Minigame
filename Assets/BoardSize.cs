using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardSize : MonoBehaviour
{
    [SerializeField] GameObject Main;
    [SerializeField] GameObject Stage1;
    [SerializeField] GameObject Stage2;
    [SerializeField] GameObject Stage3;
    [SerializeField] GameObject Stage4;
    [SerializeField] GameObject Stage5;

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
        Debug.Log($"BoardSize da lua chon: {width} x {height}");
        // Bạn có thể thêm hành động khác tại đây, như chuyển scene, tạo board mới, v.v.
        if (width == 4 && height == 4)
        {
            Instantiate(Stage1, transform.position, Quaternion.identity);  
        }
    }

    private void UpdateUI()
    {
        widthText.text = width.ToString();
        heightText.text = height.ToString();
    }
}
