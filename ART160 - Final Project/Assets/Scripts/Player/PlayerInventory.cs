using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<Item, int> items = new Dictionary<Item, int>();
    public int maxInventorySize = 10;
    public int currentSize = 0; 

    public void AddItem(Item item)
    {
        if (items.ContainsKey(item))
        {
            // If we already have a slot in inventory with this item,
            // then we simply add one to our total. 
            items[item] += 1; 
        }

        else
        {
            // If our inventory isn't full 
            if (currentSize != maxInventorySize)
            {
                // Then we now have one item! 
                items[item] = 1;
                currentSize += 1; 
            }
        }
    }

    public void RemoveItem(Item item)
    {
        // If we have the item 
        if (items.ContainsKey(item))
        {
            items[item] -= 1;

            if (items[item] == 0)
            {
                items.Remove(item);
                currentSize -= 1; 
            }
        }
    }
}
