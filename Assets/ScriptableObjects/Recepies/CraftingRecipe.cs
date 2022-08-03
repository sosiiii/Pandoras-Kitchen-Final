using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using woska_scripts;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting/Recipe", order = 1)]
public class CraftingRecipe : ScriptableObject
{
    [field: SerializeField] public List<Item> ItemsNeeded { get; private set; }
    [field: SerializeField] public Item Result { get; private set; }
    [field: SerializeField] public Machine CanBeCraftedIn { get; private set; }

    public bool CanCraft(List<Item> availableItems, Machine currentMachine)
    {
        return Enumerable.SequenceEqual(ItemsNeeded.OrderBy(e => e), availableItems.OrderBy(e => e));
    }
    
}
