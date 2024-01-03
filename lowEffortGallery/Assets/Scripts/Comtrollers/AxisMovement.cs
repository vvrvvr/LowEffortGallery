using UnityEngine;

public class AxisMovement : MonoBehaviour
{
    public float speed = 5f;
    public float minStart = 0f;
    public float maxEnd = 10f;
    public string inputAxis = "Vertical";
    public bool axisX = true;
    public bool axisY = false;
    public bool axisZ = false;

    void Update()
    {
        float moveInput = Input.GetAxis(inputAxis);
        float newPosition = GetNewPosition(moveInput);
        newPosition = ClampPosition(newPosition);
        SetNewPosition(newPosition);
    }

    float GetNewPosition(float input)
    {
        if (axisX)
            return transform.position.x + input * speed * Time.deltaTime;
        else if (axisY)
            return transform.position.y + input * speed * Time.deltaTime;
        else if (axisZ)
            return transform.position.z + input * speed * Time.deltaTime;

        return transform.position.y; // Default to Y-axis if none is selected
    }

    float ClampPosition(float position)
    {
        return Mathf.Clamp(position, minStart, maxEnd);
    }

    void SetNewPosition(float newPosition)
    {
        if (axisX)
            transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
        else if (axisY)
            transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
        else if (axisZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);
    }
}