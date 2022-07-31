using UnityEngine;


    public interface IPickable
    {
        public GameObject GetOwner();
        public GameObject PickUp();
        public void Drop();
        public void Throw();
        public void ChangeParent(Transform parent);
    }