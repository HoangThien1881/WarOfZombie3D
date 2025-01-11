using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NPC : MonoBehaviour
{
    public GameObject NPCPanel; // B?ng tho?i c?a NPC
    public TextMeshProUGUI NPCTextContext; // Text hi?n th? n?i dung tho?i
    public string[] content; // N?i dung các dòng tho?i
    Coroutine coroutine; // Bi?n dùng ?? qu?n lý coroutine hi?n th? n?i dung
    public string nextSceneName; // Tên c?a scene b?n mu?n chuy?n ??n

    private void Start()
    {
        nextSceneName = "game3d"; // Gán tr?c ti?p tên scene trong script
        NPCPanel.SetActive(true);
        NPCTextContext.text = "";
        coroutine = StartCoroutine(ReadContext());
        // Ki?m tra n?u các tham chi?u ?ã ???c gán
        if (NPCPanel == null || NPCTextContext == null)
        {
            Debug.LogError("Vui lòng gán NPCPanel và NPCTextContext trong Inspector!");
            return;
        }

        // Hi?n th? b?ng tho?i ngay t? ??u
        NPCPanel.SetActive(true);
        NPCTextContext.text = ""; // ??t text ban ??u là r?ng
        coroutine = StartCoroutine(ReadContext()); // B?t ??u ??c tho?i
    }

    IEnumerator ReadContext()
    {
        foreach (var line in content)
        {
            NPCTextContext.text = ""; // Xóa text c? tr??c khi hi?n th? dòng m?i
            for (int i = 0; i < line.Length; i++)
            {
                NPCTextContext.text += line[i]; // Gõ t?ng ký t?
                yield return new WaitForSeconds(0.1f); // ?i?u ch?nh t?c ?? gõ ch?
            }
            yield return new WaitForSeconds(2); // Th?i gian ch? gi?a các dòng tho?i
        }

        // ?n b?ng tho?i khi hoàn thành
        yield return new WaitForSeconds(5); // ??i 3 giây tr??c khi t?t b?ng tho?i
        ChangeScene();
    }
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName)) // Ki?m tra n?u tên scene không r?ng
        {
            SceneManager.LoadScene("game3d"); // T?i scene m?i
        }
        else
        {
            Debug.LogError("Tên scene ch?a ???c gán!");
        }
    }
}
