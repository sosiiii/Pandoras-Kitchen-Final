using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using woska_scripts;

[RequireComponent(typeof(BoxCollider2D), typeof(Highlight))]
public class Machine : MonoBehaviour, IInteractable
{
    private BoxCollider2D _boxCollider2D;
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.isTrigger = true;
    }
}
