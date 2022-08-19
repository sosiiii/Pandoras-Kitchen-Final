using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Crafting/Recipe", fileName = "New Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [field: SerializeField] public List<Item> ItemsNeeded { get; private set; }
    [field: SerializeField] public Item Result { get; private set; }
    
    [field: SerializeField] public int CraftingTime { get; private set; }
    
    [field: SerializeField] public int ScoreForOrder { get; private set; }

    
    
    
    

    public bool CanCraft(List<Item> availableItems)
    {
        var first = availableItems.OrderBy(e => e.name);
        var second = ItemsNeeded.OrderBy(e => e.name);

        return first.SequenceEqual(second);
    }
    
}
