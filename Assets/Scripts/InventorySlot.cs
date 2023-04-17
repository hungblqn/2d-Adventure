using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public int amount;

    Image icon;
    TextMeshProUGUI textAmount;
    public void SetStats()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        icon = GetComponent<Image>();
        textAmount = GetComponentInChildren<TextMeshProUGUI>();

        if(item != null)
        {
            icon.sprite = item.itemIcon;
        }
        textAmount.text = amount.ToString();
    }
}
