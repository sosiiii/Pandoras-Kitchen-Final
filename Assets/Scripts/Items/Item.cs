using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Crafting/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;

    public Sprite itemSprite;
}
