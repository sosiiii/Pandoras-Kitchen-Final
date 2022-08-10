using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/UI/Promt", fileName = "New Zeus Promt")]
public class Promt : MonoBehaviour
{
    [field: SerializeField] public PromptContext Sprite { get; private set; }

    [field: SerializeField] private List<string> AllPrompts;

    public Stack<string> PromptPool;


    public string GetRandomPrompt()
    {
        if(PromptPool == null || PromptPool.Count == 0) 
            PromptPool = new Stack<string>(AllPrompts.OrderBy(i => Random.value));
        return PromptPool.Pop();
    }








}

public enum PromptContext
{
    CompletedOrder,
    NotCompletedOrder,
    WrongOrderTurnIn,
    NewOrderGenerated,
    RandomSmallTalk
}
