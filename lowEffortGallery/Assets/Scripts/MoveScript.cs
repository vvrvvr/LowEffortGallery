using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float xMove = 0f;
    public float yMove = 0f;
    public float zMove = 0f;
    public bool isMoving = false;
    public float speed = 5f;

    void Update()
    {
        if (isMoving)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        float moveX = xMove * speed * Time.deltaTime;
        float moveY = yMove * speed * Time.deltaTime;
        float moveZ = zMove * speed * Time.deltaTime;

        transform.Translate(new Vector3(moveX, moveY, moveZ));
    }
}