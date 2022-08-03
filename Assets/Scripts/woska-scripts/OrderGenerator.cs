using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace woska_scripts
{
    [RequireComponent(typeof(Highlight), typeof(BoxCollider2D))]
    public class OrderGenerator : MonoBehaviour, IInteractable
    {
        public static Action<Order> onOrderAccepted;


        [SerializeField] private List<Order> orderPool = new List<Order>();
        public bool Interact(PlayerInteract playerInteract)
        {
            var randomOrder = orderPool[Random.Range(0, orderPool.Count)];
            
            onOrderAccepted?.Invoke(randomOrder);

            return true;
        }
    }
}