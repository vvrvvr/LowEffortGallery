using UnityEngine;

public class AxisRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public string inputAxis = "Vertical";
    public bool rotateAroundX = true;
    public bool rotateAroundY = false;
    public bool rotateAroundZ = false;

    void FixedUpdate()
    {
        float rotateInput = Input.GetAxis(inputAxis);
        Vector3 newRotation = GetNewRotation(rotateInput);
        SetNewRotation(newRotation);
    }

    Vector3 GetNewRotation(float input)
    {
        Vector3 rotation = Vector3.zero;

        if (rotateAroundX)
            rotation.x = input * rotationSpeed * Time.fixedDeltaTime;
        else if (rotateAroundY)
            rotation.y = input * rotationSpeed * Time.fixedDeltaTime;
        else if (rotateAroundZ)
            rotation.z = input * rotationSpeed * Time.fixedDeltaTime;

        return rotation;
    }

    void SetNewRotation(Vector3 newRotation)
    {
        transform.Rotate(newRotation);
    }
}