using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Machine", menuName = "Crafting/Machine", order = 1)]
public class Machine : ScriptableObject
{
    private void OnValidate()
    {
        Name = name;
    }

    [field: SerializeField] public float CraftingTime { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }
    
    [field: SerializeField] public int MaxRecipeLength { get; private set; }

    [field: SerializeField] public List<CraftingRecipe> CraftingRecipes { get; private set; }
    [field: SerializeField] public Item Trash { get; private set; }
    
}
