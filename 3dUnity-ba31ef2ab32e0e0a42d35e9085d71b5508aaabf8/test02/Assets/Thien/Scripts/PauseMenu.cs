using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Giao di?n PauseMenu
    private bool isPaused = false; // Tr?ng thái t?m d?ng
    public PlayerInput playerInput;
    void Update()
    {
        if (isPaused) return; // Không x? lý n?u game ?ang t?m d?ng
                              // Code ?i?u khi?n nhân v?t
    }


    public void Pause()
    {
        Debug.Log("Pause method called"); // Dòng ki?m tra
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu is not assigned!");
            return;
        }

        pauseMenu.SetActive(true); // Hi?n th? giao di?n PauseMenu
        Time.timeScale = 0f;       // D?ng th?i gian trong game
        isPaused = true;           // C?p nh?t tr?ng thái t?m d?ng
    }

    public void Resume()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1f;
        isPaused = false;

        // ??m b?o Action Map c?a nhân v?t ???c kích ho?t l?i
        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void Home()
    {
        SceneManager.LoadScene(1); // Quay v? màn hình chính
        Time.timeScale = 1f;       // Khôi ph?c th?i gian
    }
}
