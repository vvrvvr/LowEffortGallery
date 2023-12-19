using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public bool isRotate = true;
    public bool isRotateClockwise = true;
    public bool isRotateX = true;
    public bool isRotateY = false;
    public bool isRotateZ = false;
    public float rotationSpeed = 5.0f;

    void Update()
    {
        if (isRotate)
        {
            RotateObject();
        }
    }

    void RotateObject()
    {
        float rotationDirection = isRotateClockwise ? 1.0f : -1.0f;

        if (isRotateX)
        {
            transform.Rotate(Vector3.right * rotationDirection * rotationSpeed * Time.deltaTime);
        }

        if (isRotateY)
        {
            transform.Rotate(Vector3.up * rotationDirection * rotationSpeed * Time.deltaTime);
        }

        if (isRotateZ)
        {
            transform.Rotate(Vector3.forward * rotationDirection * rotationSpeed * Time.deltaTime);
        }
    }
}
