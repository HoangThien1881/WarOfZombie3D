using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayButtonHandler : MonoBehaviour
{
    // Gán TextMeshProUGUI c?a nút
    [SerializeField] private TMP_Text playButtonText;

    // Tên scene c?n chuy?n
    [SerializeField] private string sceneName = "Cutscene";

    public void OnPlayButtonClick()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Tên scene ch?a ???c ??t!");
        }
    }
}

