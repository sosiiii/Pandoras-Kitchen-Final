using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item", order = 1)]
public class SOItem : ScriptableObject
{
    public string itemName;

    public Sprite itemSprite;
}
