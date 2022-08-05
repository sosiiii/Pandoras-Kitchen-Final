using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace woska_scripts
{
    [RequireComponent(typeof(Highlight), typeof(BoxCollider2D))]
    public class OrderGenerator : MonoBehaviour, IInteractable
    {
        public static Action<Order> onOrderAccepted;


        [SerializeField] private List<Order> orderPool = new List<Order>();


        private Order currentOrder;

        private ItemSlot _newOrderIcon;

        private void Awake()
        {
            _newOrderIcon = transform.GetChild(0).GetChild(0).GetComponent<ItemSlot>();
        }

        private void Start()
        {
            GenerateRandomOrder();
        }

        public bool Interact(PlayerInteract playerInteract)
        {
            if (currentOrder == null) return false;

            onOrderAccepted?.Invoke(currentOrder);
            
            Invoke(nameof(GenerateRandomOrder), Random.Range(5f, 10f));

            _newOrderIcon.RemoveItem();

            currentOrder = null;

            return true;
        }

        private void GenerateRandomOrder()
        {
            currentOrder = orderPool[Random.Range(0, orderPool.Count)];

            _newOrderIcon.AddItem(currentOrder.WhatWasOrdered.Result);
        }
        
        
    }
}