using UnityEngine;
using TMPro;

public class Pricetag : MonoBehaviour
{
    [SerializeField] private GameObject priceTag;
    [SerializeField] private GameObject priceTagBought;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Awake()
    {
        priceTag.SetActive(false);
        priceTagBought.SetActive(false);
    }

    public void ActivatePriceTag(int price)
    {
        priceTag.SetActive(true);
        if (price == 1)
        {
            priceText.text = price+" coin";
        }
        else
        {
            priceText.text = price+" coins";
        }
    }

    public void ActivatepriceTagBought()
    {
        priceTag.SetActive(false);
        priceTagBought.SetActive(true);
    }
}
