using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Crafting/Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }

    private string _name => name;
    
    [field: SerializeField] public bool IsEnemy { get; private set; }





}
