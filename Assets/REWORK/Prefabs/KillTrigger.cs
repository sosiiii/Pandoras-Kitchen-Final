using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace REWORK.Prefabs
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class KillTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.TryGetComponent(out IKillable killable)) return;
            
            killable.Kill();
            
        }
    }
}