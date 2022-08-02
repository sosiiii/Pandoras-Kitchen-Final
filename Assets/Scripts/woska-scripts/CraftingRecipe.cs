using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using woska_scripts;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting/Recipe", order = 1)]
public class CraftingRecipe : ScriptableObject
{
    public List<Item> materials;
    public Item result;

    private void OnValidate()
    {
        materials.Sort();
    }

    public bool CanCraft(IItemContainer[] itemContainer)
    {

        return true;
    }

    public void Craft(IItemContainer[] itemContainer)
    {
    }
}
