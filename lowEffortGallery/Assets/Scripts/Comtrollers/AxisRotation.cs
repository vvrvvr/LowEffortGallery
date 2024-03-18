using UnityEngine;

public class AxisRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public string inputHorizontalAxis = "Horizontal";
    public string inputVerticalAxis = "Vertical";
    public bool rotateAroundX = true;
    public bool rotateAroundY = false;
    public bool rotateAroundZ = false;
    public bool isLimiter = false;
    private FootstepManager footstepManager;

    void Start()
    {
        footstepManager = GetComponent<FootstepManager>();
        if (footstepManager == null)
        {
            Debug.LogError("FootstepManager component is missing!");
        }
    }
    private void Update()
    {
        if((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !isLimiter)
        {
            Debug.Log("step");
            footstepManager.PlayFootstep();
        }
    }
    void FixedUpdate()
    {
        float rotateInputHorizontal = Input.GetAxis(inputHorizontalAxis);
        float rotateInputVertical = Input.GetAxis(inputVerticalAxis);
        
        if (isLimiter)
        {
            rotateInputHorizontal = Mathf.Clamp(rotateInputHorizontal, 0, 1);
            rotateInputVertical = Mathf.Clamp(rotateInputVertical, 0, 1);
        }

        Vector3 newRotation = GetNewRotation(rotateInputHorizontal, rotateInputVertical);
        SetNewRotation(newRotation);
    }

    Vector3 GetNewRotation(float inputHorizontal, float inputVertical)
    {
        Vector3 rotation = Vector3.zero;

        if (rotateAroundX)
            rotation.x = inputVertical * rotationSpeed * Time.fixedDeltaTime + inputHorizontal * rotationSpeed * Time.fixedDeltaTime;
        
        if (rotateAroundY)
            rotation.y = inputVertical * rotationSpeed * Time.fixedDeltaTime + inputHorizontal * rotationSpeed * Time.fixedDeltaTime;

        if (rotateAroundZ)
            rotation.z = inputVertical * rotationSpeed * Time.fixedDeltaTime + inputHorizontal * rotationSpeed * Time.fixedDeltaTime;

        return rotation;
    }

    void SetNewRotation(Vector3 newRotation)
    {
        transform.Rotate(newRotation);
    }
}