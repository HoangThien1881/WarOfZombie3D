using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light spotLight;            // ?èn c?n ch?p
    public float flickerSpeed = 0.1f;  // T?c ?? ch?p (??n v?: giây)

    private bool isFlickering = false;

    void Start()
    {
        if (spotLight == null)
        {
            spotLight = GetComponent<Light>();
        }

        // B?t ??u hi?u ?ng ch?p
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            spotLight.enabled = !spotLight.enabled;  // B?t ho?c t?t ?èn
            yield return new WaitForSeconds(flickerSpeed);  // T?m d?ng tr??c khi thay ??i tr?ng thái
        }
    }
}
