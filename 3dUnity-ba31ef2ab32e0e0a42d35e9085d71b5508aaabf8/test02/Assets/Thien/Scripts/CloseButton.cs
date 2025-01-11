using UnityEngine;

public class CloseButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject panelToClose; // Panel c?n t?t

    public void OnCloseButtonClick()
    {
        if (panelToClose != null)
        {
            panelToClose.SetActive(false); // T?t panel
        }
    }
}
