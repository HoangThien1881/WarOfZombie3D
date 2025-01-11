using UnityEngine;

public class DiscoEffect : MonoBehaviour
{
    public Light discoLight; // Ánh sáng t?o hi?u ?ng
    public AudioSource victoryMusic; // Âm nh?c chi?n th?ng
    public ParticleSystem confettiEffect; // Hi?u ?ng pháo hoa (n?u có)

    public float colorChangeSpeed = 5f; // T?c ?? ??i màu
    private bool isDiscoActive = false; // Tr?ng thái hi?u ?ng disco

    private void Update()
    {
        if (isDiscoActive && discoLight != null)
        {
            // ??i màu ánh sáng liên t?c
            discoLight.color = new Color(
                Mathf.Sin(Time.time * colorChangeSpeed),
                Mathf.Sin(Time.time * colorChangeSpeed + 2f),
                Mathf.Sin(Time.time * colorChangeSpeed + 4f)
            );
        }
    }

    public void StartDisco()
    {
        if (isDiscoActive) return; // N?u ?ang ch?y, không kích ho?t l?i

        isDiscoActive = true;

        // B?t ánh sáng
        if (discoLight != null)
            discoLight.enabled = true;

        // Phát nh?c chi?n th?ng
        if (victoryMusic != null)
            victoryMusic.Play();

        // Kích ho?t hi?u ?ng pháo hoa
        if (confettiEffect != null)
            confettiEffect.Play();

        Debug.Log("Disco Time! Chúc m?ng chi?n th?ng!");
    }

    public void StopDisco()
    {
        isDiscoActive = false;

        // T?t ánh sáng
        if (discoLight != null)
            discoLight.enabled = false;

        // D?ng nh?c
        if (victoryMusic != null)
            victoryMusic.Stop();

        // D?ng hi?u ?ng pháo hoa
        if (confettiEffect != null)
            confettiEffect.Stop();
    }
}
