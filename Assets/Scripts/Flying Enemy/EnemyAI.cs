using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using REWORK;
using UnityEngine;
using Random = UnityEngine.Random;

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
            GetToSweetSpot
        }
        [Header("Behavior")] 
        [SerializeField] private float playerDetectionRadius = 1f;

        [SerializeField] private float followPlayerRadius = 2f;
        [SerializeField] private float maxPlayerAttackRadius = 3f;
        [SerializeField] private float minPlayerAttackRadius = 2.8f;


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

        private Animator _animator;

        private Transform _startPos;

        private Vector3 _endPosition;

        private bool onWayToEndPos;

        [SerializeField] private float patrolDistance;

        private bool InSweetSpot(float distance) =>
            distance <= maxPlayerAttackRadius && distance >= minPlayerAttackRadius;
        
        

        [Header("Projectile")]
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private int numberOfBullets;
        [SerializeField] private float reloadWaitTime;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _startPos = transform.GetChild(0);
        }

        private void Start()
        {
            _startPosition = transform.position;
            _endPosition = _startPosition + Vector3.right * patrolDistance;
            onWayToEndPos = true;
        }

        private void Update()
        {
            switch (_enemyStates)
            {
                case EnemyStates.LookForPlayer:
                    PerformDetection();
                    _animator.SetBool("Idle", true);
                    break;
                case EnemyStates.FollowPlayer:
                    if (Target == null)
                    {
                        _enemyStates = EnemyStates.ReturnToStart;
                        return;
                    }
                    FollowPlayer();
                    _animator.SetBool("Idle", false);
                    break;
                case EnemyStates.ReturnToStart:
                    ReturnToStart();
                    _animator.SetBool("Idle", false);
                    break;
                case EnemyStates.AttackPlayer:
                    if (Target == null)
                    {
                        _enemyStates = EnemyStates.ReturnToStart;
                        return;
                    }
                    Attack();
                    _animator.SetBool("Idle", false);
                    break;
            }
        }

        private void PerformDetection()
        {
            if(onWayToEndPos)
                transform.position = Vector2.MoveTowards(transform.position,_endPosition,speed * Time.deltaTime);
            else
                transform.position = Vector2.MoveTowards(transform.position,_startPosition,speed * Time.deltaTime);

            if (transform.position == _startPosition)
                onWayToEndPos = true;
            else if (transform.position == _endPosition)
                onWayToEndPos = false;

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
            
            if (Vector2.Distance(transform.position, TargetPosition) <= maxPlayerAttackRadius)
            {
                _enemyStates = EnemyStates.AttackPlayer;
            }
            else
                transform.position = Vector2.MoveTowards(transform.position,TargetPosition,speed * Time.deltaTime);
        }

        private void Attack()
        {
            var distance = Vector2.Distance(transform.position, TargetPosition);
            if (distance > maxPlayerAttackRadius)
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
            var distToStart = Vector2.Distance(transform.position, _startPosition);
            var distToEnd = Vector2.Distance(transform.position, _endPosition);

            var target = distToStart < distToEnd ? _startPosition : _endPosition;
            
            transform.position = Vector2.MoveTowards(transform.position,target,speed * Time.deltaTime);
            if (transform.position == _startPosition)
            {
                _enemyStates = EnemyStates.LookForPlayer;
                onWayToEndPos = true;
                
            }
            else if (transform.position == _endPosition)
            {
                _enemyStates = EnemyStates.LookForPlayer;
                onWayToEndPos = false;
            }
                
        }

        private void Fire()
        {
            float angleStep = 360f / numberOfBullets;
            float angle = 0f;
            
            var floatRadius = 10f;
            var startPos = _startPos.position;
            
            for (int i = 0; i < numberOfBullets; i++)
            {
                var vectorDir = Vector3.zero;
                vectorDir.x = startPos.x + Mathf.Sin((angle * Mathf.PI) / 180) * floatRadius;
                vectorDir.y = startPos.y + Mathf.Cos((angle * Mathf.PI) / 180) * floatRadius;
                
                

                
                var projectile = Instantiate(projectilePrefab, startPos, Quaternion.identity);
                projectile.Init((startPos-vectorDir).normalized);

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
            
            Gizmos.DrawWireSphere(_startPosition, 0.5f);
            Gizmos.DrawWireSphere(_endPosition, 0.5f);

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
                    Gizmos.DrawWireSphere(position,maxPlayerAttackRadius);
                    break;
            }

            Gizmos.color = Color.red;
            foreach (var player in _playersInDetectionZone)
            {
                if(player == null) continue;
                Gizmos.DrawLine(position, player.transform.position);
            }
            
            
            Gizmos.color = Color.blue;
            
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