using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViegoItemUsing : MonoBehaviour
{
    UIManager UIManager;
    private void Start()
    {
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    // Update is called once per frame
    void Update()
    {
        UseItem();    
    }
    void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            for(int i=0;i< UIManager.InventoryUI.transform.childCount; i++)
            {
                if(UIManager.InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().item.itemName == "Common Health Potion")
                {
                    GetComponentInParent<Viego>().hp += 50;
                    UIManager.InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().amount -= 1;
                    UIManager.InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().SetStats();
                    break;
                }
            }
        }
    }
}
