using UnityEngine;

public class HelpPanelController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel; // Panel h??ng d?n
    [SerializeField] private GameObject[] pages;  // Danh sách các trang
    [SerializeField] private GameObject closeButton; // Nút t?t panel
    private int currentPage = 0; // Trang hi?n t?i

    void Start()
    {
        UpdatePage(); // Hi?n th? trang ??u tiên
    }

    public void OnLeftButtonClick()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    public void OnRightButtonClick()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    public void OnCloseButtonClick()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(false); // ?n panel h??ng d?n
        }
    }

    private void UpdatePage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPage); // Ch? hi?n th? trang hi?n t?i
        }
    }

    public void OnHelpButtonClick()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(true); // Hi?n th? panel h??ng d?n
            currentPage = 0; // Reset v? trang ??u
            UpdatePage();
        }
    }
}
