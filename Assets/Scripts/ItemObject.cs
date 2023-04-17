using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    public int amount;
    public LayerMask playerLayerMask;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && playerIsNear())
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().PickUpItem(this);
            Destroy(gameObject);
        }
    }
    bool playerIsNear()
    {
        return Physics2D.OverlapCircle(transform.position, 10, playerLayerMask);
    }
}
