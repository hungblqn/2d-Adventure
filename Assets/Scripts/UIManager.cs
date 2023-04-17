using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    private Viego playerScript;

    public GameObject gameManager;
    public GameObject TeleportGateUI;

    public GameObject CharacterStatsUI;
    public TextMeshProUGUI CharacterStatsText;
    public bool isCharacterStatsUIActive = false;

    public GameObject InventoryUI;
    public bool isInventoryUIActive = false;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        playerScript = GameObject.Find("Viego").GetComponent<Viego>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectObject();
        OpenCharacterStatsUI();
        OpenInventoryUI();
    }
    void OpenInventoryUI()
    {
        RemoveItemOutOfAmount();
        if (Input.GetKeyDown(KeyCode.T))
        {
            DisplayInventoryUI();
            if (isInventoryUIActive)
            {
                isInventoryUIActive = false;
                InventoryUI.SetActive(false);
            }
            else
            {
                isInventoryUIActive = true;
                InventoryUI.SetActive(true);
            }
        }
    }
    void DisplayInventoryUI()
    {
        for(int i = 0; i < InventoryUI.transform.childCount; i++)
        {
            InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().SetStats();
        }
    }
    void RemoveItemOutOfAmount()
    {
        for(int i = 0; i < InventoryUI.transform.childCount; i++)
        {
            if (InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().amount <= 0)
            {
                InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().item = null;
                InventoryUI.transform.GetChild(i).GetComponent<Image>().sprite = null;
            }
        }
    }
    public void PickUpItem(ItemObject item)
    {
        bool isItemExistInInventory = false;
        for(int i=0; i < InventoryUI.transform.childCount; i++)
        {
            if(InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().item != null 
                && InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().item.itemName == item.item.itemName)
            {
                InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().amount += item.amount;
                isItemExistInInventory = true;
            }
        }
        if (!isItemExistInInventory)
        {
            for(int i = 0; i < InventoryUI.transform.childCount; i++)
            {
                if (InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().item == null)
                {
                    InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().item = item.item;
                    InventoryUI.transform.GetChild(i).GetComponent<InventorySlot>().amount += item.amount;
                }
                break;
            }
        }
        DisplayInventoryUI();
    }
    void OpenCharacterStatsUI()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (isCharacterStatsUIActive)
            {
                isCharacterStatsUIActive = false;
                DisplayCharacterStats();
                CharacterStatsUI.SetActive(false);
            }
            else
            {
                isCharacterStatsUIActive = true;
                DisplayCharacterStats();
                CharacterStatsUI.SetActive(true);
            }
        }
    }
    void DisplayCharacterStats()
    {
        CharacterStatsText.text = playerScript.level + "\n\n" + playerScript.hp + "/" + playerScript.maxHp + "\n\n"
            +playerScript.exp+"/"+playerScript.maxExp+"\n\n"+playerScript.damage;
    }
    void DetectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                if(hit.collider.gameObject.name == "PotionShop")
                {
                    Debug.Log("Display Shop UI ");
                }
                if(hit.collider.gameObject.name == "TeleportGate")
                {
                    Debug.Log("Display Teleport UI");
                    if (!TeleportGateUI.activeInHierarchy)
                    {
                        TeleportGateUI.SetActive(true);
                    }
                    else
                    {
                        TeleportGateUI.SetActive(false);
                    }
                }
                if(hit.collider.gameObject.name == "NPCOldman1")
                {
                    Debug.Log("Display NPC talk");
                }
            }
        }
    }
}
