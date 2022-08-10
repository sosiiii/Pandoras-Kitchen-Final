using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/UI/Promt", fileName = "New Zeus Promt")]
public class Promt : ScriptableObject
{
    [field: SerializeField] public PromptContext Context { get; private set; }

    [field: SerializeField] private List<string> AllPrompts;

    private Stack<string> PromptPool;

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
    RandomSmallTalk,
    PlayerDeath
}
