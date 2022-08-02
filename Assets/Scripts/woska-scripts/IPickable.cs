using UnityEngine;


    public interface IPickable
    {
        public void Throw();
        Item GetItem();
        void DestroyItem();
    }