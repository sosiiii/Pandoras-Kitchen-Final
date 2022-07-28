using UnityEngine;


    public interface IPickable
    {
        public GameObject PickUp();
        public void Drop();
        public void Throw();
    }