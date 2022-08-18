using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Crafting/Machine", fileName = "New Machine")]
public class Machine : ScriptableObject
{
    public enum MachineType
    {
        Automatic,
        NeedsInput
    }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    
    [field: SerializeField] public MachineType Type { get; private set; }

    [field: SerializeField] public int CraftingTime { get; private set; }

    [field: SerializeField, Range(1,3)] public int MaxRecipeLength { get; private set; }

    [field: SerializeField] public List<CraftingRecipe> CraftingRecipes { get; private set; }

    [field: SerializeField] public bool CantProccessEnemies { get; private set; }
    
    [field: SerializeField] public Item Trash { get; private set; }


    public Item Craft(List<Item> ingredients)
    {
        foreach (var craftingRecipe in CraftingRecipes)
        {
            if (craftingRecipe.CanCraft(ingredients))
            {
                return craftingRecipe.Result;
                
            }
        }

        return Trash;
    }
    
}
