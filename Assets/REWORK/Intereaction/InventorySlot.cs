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

        private float animDuratio = 1f;

        private Vector3 animPos;

        private void Awake()
        {
            _frameHolder = transform.GetChild(0).gameObject;
            
            _itemImage = _frameHolder.transform.GetChild(0).GetComponent<Image>();
            
            _frameHolder.SetActive(false);
            _itemImage.sprite = null;

            animPos = transform.position + Vector3.up * 1f;
        }

        private void Start()
        {
            if(!animate) return;
            
            transform.DOMove(animPos, animDuratio, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
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