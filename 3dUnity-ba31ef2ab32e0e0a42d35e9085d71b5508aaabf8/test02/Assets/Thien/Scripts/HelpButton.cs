using UnityEngine;

public class HelpButtonHandler : MonoBehaviour
{
    // Panel h??ng d?n (g?n trong Inspector)
    [SerializeField] private GameObject helpPanel;

    // Hàm x? lý khi b?m nút Help
    public void OnHelpButtonClick()
    {
        if (helpPanel != null)
        {
            // Hi?n th? ho?c ?n panel h??ng d?n
            bool isActive = helpPanel.activeSelf;
            helpPanel.SetActive(!isActive);
        }
        else
        {
            Debug.LogError("Help Panel ch?a ???c gán trong Inspector!");
        }
    }
}
