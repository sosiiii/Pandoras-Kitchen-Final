using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using woska_scripts;

[CreateAssetMenu(fileName = "New Order", menuName = "Crafting/Order", order = 1)]
public class Order : ScriptableObject
{
    
    [field: SerializeField] public CraftingRecipe WhatWasOrdered { get; private set; }
    
    [field: SerializeField] public int TimeToAcceptOrder { get; private set; }
    [field: SerializeField] public int TimeToFinishOrder { get; private set; }
    
    
}
