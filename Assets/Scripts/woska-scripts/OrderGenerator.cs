using System;
using System.Collections;
using System.Collections.Generic;
using Intereaction;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace woska_scripts
{
    public class OrderGenerator : MonoBehaviour
    {

        public bool CanGenerate => _currentNumberOfActiveOrders < _levelSettings.MaxActiveOrders;
        private int _currentNumberOfActiveOrders = 0;
        public static Action<CraftingRecipe> orderGenerated;

        [SerializeField] private LevelSettings _levelSettings;

        private void OnEnable()
        {
            OrderController.orderRemoved += OrderFinished;
            
        }

        private void OnDisable()
        {
            OrderController.orderRemoved -= OrderFinished;
        }

        private void Start()
        {
            StartCoroutine(Generate()); 
        }
        private IEnumerator Generate()
        {
            while (true)
            {
                while (CanGenerate)
                {
                    _currentNumberOfActiveOrders++;
                    GenerateRandomOrder();
                    yield return new WaitForSeconds(3f);
                }

                yield return new WaitUntil(() => CanGenerate);
            }

            //Reached maximum of orders 
            //Wait for number of active orders to decrease
            //Then
            
        }
        private void GenerateRandomOrder()
        {
            var order = _levelSettings.OrderPool[Random.Range(0, _levelSettings.OrderPool.Count)];

            orderGenerated?.Invoke(order);
        }
        
        private void OrderFinished()
        {
            _currentNumberOfActiveOrders--;
        }
        
        
    }
}