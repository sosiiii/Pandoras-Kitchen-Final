using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Intereaction
{
    public class InventorySlot : MonoBehaviour
    {
        public Item ItemData { get; private set; }
        public bool IsFree => ItemData == null;

        private GameObject _frameHolder;
        
        private Image _itemImage;

        public Vector3 Position => transform.position;

        [SerializeField] private bool animate = false;

        private float animDuration;

        private void Awake()
        {
            _frameHolder = transform.GetChild(0).gameObject;
            
            _itemImage = _frameHolder.transform.GetChild(0).GetComponent<Image>();
            
            _frameHolder.SetActive(false);
            _itemImage.sprite = null;
        }

        private void Update()
        {
            if(!animate) return;
            transform.DOMove(Vector3.one, animDuration, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            //Twee
        }

        public void AddItem(Item itemObject)
        {
            ItemData = itemObject;
            
            _frameHolder.SetActive(true);
            _itemImage.sprite = ItemData.Sprite;

        }

        public Item RemoveItem()
        {
            var tmp = ItemData;
            ItemData = null;
            
            _frameHolder.SetActive(false);
            _itemImage.sprite = null;
            
            return tmp;
        }
    }
}