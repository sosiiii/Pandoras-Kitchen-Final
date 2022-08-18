using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable] 
[CreateAssetMenu(menuName = "Scriptable Objects/Crafting/Level Settings", fileName = "New Level Settings")]
public class LevelSettings : ScriptableObject
{
    [field: SerializeField] public List<CraftingRecipe> OrderPool { get; private set; }
    [field: SerializeField] public int MaxActiveOrders { get; private set; }
}
