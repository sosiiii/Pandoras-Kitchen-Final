using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using REWORK;
using UnityEngine;

namespace Flying_Enemy
{
    public class EnemyAI : MonoBehaviour, IKillable, IDamagable, IOnDeath
    {
        public enum EnemyStates
        {
            LookForPlayer,
            FollowPlayer,
            ReturnToStart,
            AttackPlayer,
            Reload,
        }
        [Header("Behavior")] 
        [SerializeField] private float playerDetectionRadius = 1f;

        [SerializeField] private float followPlayerRadius = 2f;
        [SerializeField] private float attackPlayerRadius = 3f;


        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask obstaclesLayer;

        [SerializeField] private int maxHealth = 3;
        [SerializeField] private float speed = 2f;
        
        [Header("Items")]
        [SerializeField] private Item deadEnemyItem;
        [SerializeField] private ItemObject itemObjectPrefab;
        


        private Collider2D[] _playersInDetectionZone = new Collider2D[2];

        private EnemyStates _enemyStates = EnemyStates.LookForPlayer;

        private Vector3 _startPosition;

        private Collider2D Target => _playersInDetectionZone.FirstOrDefault(item => item != null);
        private Vector3 TargetPosition => Target.transform.position;

        private SpriteRenderer _spriteRenderer;
        
        

        [Header("Projectile")]
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float startAngle;
        [SerializeField] private float endAngle;
        [SerializeField] private int numberOfBullets;
        [SerializeField] private float reloadWaitTime;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            switch (_enemyStates)
            {
                case EnemyStates.LookForPlayer:
                    PerformDetection();
                    break;
                case EnemyStates.FollowPlayer:
                    FollowPlayer();
                    break;
                case EnemyStates.ReturnToStart:
                    ReturnToStart();
                    break;
                case EnemyStates.AttackPlayer:
                    Attack();
                    break;
                        
            }
        }

        private void PerformDetection()
        {
            _playersInDetectionZone = new Collider2D[2];
            
            var playerDetected = Physics2D.OverlapCircleNonAlloc(transform.position, playerDetectionRadius, _playersInDetectionZone, playerLayer);
            if(playerDetected == 0) return;
            
            RemovePlayerNotInLineOfSight();
            if(Target == null)
                return;
            
            _enemyStates = EnemyStates.FollowPlayer;

        }

        private void FollowPlayer()
        {
            _playersInDetectionZone = new Collider2D[2];
            
            var playerDetected = Physics2D.OverlapCircleNonAlloc(transform.position, followPlayerRadius, _playersInDetectionZone, playerLayer);
            
            if (playerDetected == 0)
            {
                _enemyStates = EnemyStates.ReturnToStart;
                return;
            }
            RemovePlayerNotInLineOfSight();
            if (Target == null)
            {
                _enemyStates = EnemyStates.ReturnToStart;
                return;
            }
            
            if (Vector2.Distance(transform.position, TargetPosition) <= attackPlayerRadius)
            {
                _enemyStates = EnemyStates.AttackPlayer;
            }
            else
                transform.position = Vector2.MoveTowards(transform.position,TargetPosition,speed * Time.deltaTime);
        }

        private void Attack()
        {
            if (Vector2.Distance(transform.position, TargetPosition) > attackPlayerRadius)
            {
                _enemyStates = EnemyStates.FollowPlayer;
                return;
            }
            Fire();
            StartCoroutine(Reload());
            _enemyStates = EnemyStates.Reload;
        }

        private void ReturnToStart()
        {
            transform.position = Vector2.MoveTowards(transform.position,_startPosition,speed * Time.deltaTime);
            if (transform.position == _startPosition)
                _enemyStates = EnemyStates.LookForPlayer;
        }

        private void Fire()
        {
            var angleStep = 360 / numberOfBullets;
            var angle = 0;
            

            for (int i = 0; i < numberOfBullets; i++)
            {
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.Init(MathHelpers.DegreeToVector2(angle));
                angle += angleStep;
            }
            
            
        }

        private void RemovePlayerNotInLineOfSight()
        {
            for (int i = 0; i < _playersInDetectionZone.Length; i++)
            {
                var player = _playersInDetectionZone[i];
                
                if(player == null) continue;
                var dirToTarget = (player.transform.position - transform.position);
                var distanceToTarget = Vector2.Distance(transform.position, player.transform.position);
                
                var hit = Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget,obstaclesLayer);
                if (hit.collider != null)
                {
                    _playersInDetectionZone[i] = null;
                }
            }
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(reloadWaitTime);
            _enemyStates = EnemyStates.AttackPlayer;
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;

            switch (_enemyStates)
            {
                case EnemyStates.LookForPlayer:
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(position, playerDetectionRadius);
                    break;

                case EnemyStates.FollowPlayer:
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(position, followPlayerRadius);
                    break;
                
                case EnemyStates.AttackPlayer:
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(position,attackPlayerRadius);
                    break;
            }

            Gizmos.color = Color.red;
            foreach (var player in _playersInDetectionZone)
            {
                if(player == null) continue;
                Gizmos.DrawLine(position, player.transform.position);
            }
            
            
            Gizmos.color = Color.blue;
            
            
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + MathHelpers.DegreeToVector2(startAngle)*3f);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + MathHelpers.DegreeToVector2(endAngle)*3f);

        }

        public void Kill()
        {
            Death();
        }

        private void Death()
        {
            var itemObject = Instantiate(itemObjectPrefab, transform.position, Quaternion.identity);
            
            DeathAction?.Invoke();
            itemObject.Init(deadEnemyItem);
            Destroy(gameObject);
        }

        IEnumerator ColorHit()
        {
            var blinkWaitTime = new WaitForSeconds(0.1f);

            _spriteRenderer.color = Color.red;
            yield return blinkWaitTime;
            _spriteRenderer.color = Color.white;

        }

        public void Damage(float attackDemage, Vector3 knockbackDir)
        {
            maxHealth--;
            StartCoroutine(ColorHit());

            if (maxHealth <= 0)
            {
                Death();
                return;
            }
            
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Action DeathAction { get; set; }
    }
    
}