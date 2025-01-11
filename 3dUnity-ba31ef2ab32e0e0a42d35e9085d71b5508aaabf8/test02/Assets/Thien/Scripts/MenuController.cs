using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button[] menuButtons; // Danh sách các nút trong menu
    public Image gunPointer;     // Hình cây súng
    public Color normalColor = Color.white;  // Màu m?c ??nh c?a nút
    public Color selectedColor = Color.green; // Màu khi nút ???c ch?n
    public AudioClip selectSound; // Âm thanh khi ch? vào nút m?i
    public AudioClip activateSound; // Âm thanh khi nút ???c kích ho?t

    private AudioSource audioSource; // Ngu?n phát âm thanh
    private int currentIndex = 0; // V? trí hi?n t?i trong danh sách nút

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // T?o AudioSource
        UpdateMenu();
    }

    void Update()
    {
        // Di chuy?n lên
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentIndex = (currentIndex - 1 + menuButtons.Length) % menuButtons.Length;
            PlaySelectSound();
            UpdateMenu();
        }

        // Di chuy?n xu?ng
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentIndex = (currentIndex + 1) % menuButtons.Length;
            PlaySelectSound();
            UpdateMenu();
        }

        // Kích ho?t nút khi nh?n Enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ActivateButton();
        }
    }

    void UpdateMenu()
    {
        // C?p nh?t màu và v? trí c?a cây súng
        for (int i = 0; i < menuButtons.Length; i++)
        {
            // ??i màu nút
            ColorBlock colors = menuButtons[i].colors;
            colors.normalColor = (i == currentIndex) ? selectedColor : normalColor;
            menuButtons[i].colors = colors;

            // Di chuy?n cây súng
            if (i == currentIndex)
            {
                gunPointer.transform.position = menuButtons[i].transform.position;
            }
        }
    }

    void PlaySelectSound()
    {
        if (selectSound != null)
        {
            audioSource.PlayOneShot(selectSound);
        }
    }

    void ActivateButton()
    {
        if (activateSound != null)
        {
            audioSource.PlayOneShot(activateSound);
        }

        // G?i hành ??ng c?a nút ???c ch?n
        menuButtons[currentIndex].onClick.Invoke();
    }
}
