using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        // Xoay CameraPivot quanh tr?c Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
