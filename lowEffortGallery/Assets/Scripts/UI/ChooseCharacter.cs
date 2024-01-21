using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    public bool isRotate = true;
    public float rotationSpeed = 1f;
    public bool isRotateClockwise = true;
    public Transform CharacterWheel;
    public Button prevButton;
    public Button nextButton;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRotate)
        {
            RotateObject();
        }
    }

    public void NextCharacter()
    {
        isRotateClockwise = true;
        isRotate = true;
        nextButton.interactable = false;
        prevButton.interactable = false;
    }

    public void PrevCharacter()
    {
        isRotateClockwise = false;
        isRotate = true;
        nextButton.interactable = false;
        prevButton.interactable = false;
    }


    void RotateObject()
    {
        float rotationDirection = isRotateClockwise ? 1.0f : -1.0f;

        CharacterWheel.Rotate(Vector3.up * rotationDirection * rotationSpeed * Time.deltaTime);
    }

    public void StopRotation(int characterId)
    {
        isRotate = false;
        nextButton.interactable = true;
        prevButton.interactable = true;
    }
}