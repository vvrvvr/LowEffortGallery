using UnityEngine;
using UnityEngine.UI;

public class QRCodeHandler : MonoBehaviour
{
    public Sprite[] codes = new Sprite[9];
    public Image codeImage;
    void Start()
    {
        if (!GameVariables.instance.isExposition)
        {
            gameObject.SetActive(false);
        }
    }

    public void ApplyCode(int codeIndex)
    {
        codeImage.sprite = codes[codeIndex];
    }
}
