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
        private List<CraftingRecipe> loteria;
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
        void ResetLottery()
        {
            loteria = new List<CraftingRecipe>();
            foreach (CraftingRecipe craftingRecipe in _levelSettings.OrderPool)
            {
                loteria.Add(craftingRecipe);
            }
            foreach (CraftingRecipe craftingRecipe in _levelSettings.OrderPool)
            {
                loteria.Add(craftingRecipe);
            }
        }
        private void Start()
        {
            ResetLottery();
            StartCoroutine(Generate()); 

        }
        private IEnumerator Generate()
        {
            while (true)
            {
                while (CanGenerate)
                {
                    _currentNumberOfActiveOrders++;
                    GetOrderFromOrderFromLottery();
                    yield return new WaitForSeconds(Random.Range(_levelSettings.MinGenerationTime, _levelSettings.MaxGenerationTime));
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
            Debug.Log("Current number of orders: " + _currentNumberOfActiveOrders);
        }
        
        private void GetOrderFromOrderFromLottery()
        {
            if (loteria.Count <= 0)
            {
                ResetLottery();
            }
            var order = loteria[Random.Range(0, loteria.Count)];

            orderGenerated?.Invoke(order);
            loteria.Remove(order);
        }
        
    }
}