using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea(3, 3)] public string itemDescription;
    public enum Type
    {
        potion
    }
    public enum Rarity
    {
        common,
        uncommon,
        rare,
        epic,
        unique,
        legendary
    }
    public GameObject prefab;
    public Sprite itemIcon;
    public Type type;
    public Rarity rarity;
    public int maxStack;
}
