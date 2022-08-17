using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace REWORK.Prefabs
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class KillTrigger : MonoBehaviour
    {

        private BoxCollider2D _boxCollider2D;
        private Vector2 colliderSize;
        private void OnValidate()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            colliderSize = _boxCollider2D.size;
        }

        [SerializeField] private LayerMask whatToKill;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!whatToKill.Contains(col.gameObject.layer)) return;
            if(!col.TryGetComponent(out IKillable killable)) return;
            
            killable.Kill();
            
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}