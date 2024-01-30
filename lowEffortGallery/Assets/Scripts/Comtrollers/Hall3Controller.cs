using UnityEngine;

public class Hall3Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    void FixedUpdate()
    {
        float moveInputVertical = Input.GetAxis("Vertical");
        float moveInputHorizontal = Input.GetAxis("Horizontal");

        MovePlayer(moveInputVertical, moveInputHorizontal);
        RotatePlayer(moveInputHorizontal);
    }

    void MovePlayer(float inputVertical, float inputHorizontal)
    {
        Vector3 moveDirection = new Vector3(0f, 0f, inputVertical);
        Vector3 moveAmount = moveDirection * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(moveAmount, Space.Self);
    }

    void RotatePlayer(float inputHorizontal)
    {
        float rotationAmount = inputHorizontal * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up * rotationAmount, Space.Self);
    }
}