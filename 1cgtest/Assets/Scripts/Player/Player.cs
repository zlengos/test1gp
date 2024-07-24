using Configs;
using Enemies;
using Interfaces;
using Tools;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IConfigurable
    {
        #region Fields

        [SerializeField] private GameConfig config;
        [SerializeField] private EnemyFactoryWithPool enemyFactory;
        [SerializeField] private BulletFactoryWithPool bulletFactory;
        [SerializeField] private BoxCollider2D restrictor;

        [SerializeField] private ParticleSystem shootParticles;

        #region Runtime

        private float _attackDistance;
        private float _attackSpeed;
        private float _attackDamage;
        private float _bulletSpeed;
        private float _playerSpeed;
        private float _playerHealth;
        
        private float _currentAttackCooldown;

        private Bounds _restrictorBounds;

        #region API

        public float PlayerHealth => _playerHealth;
        
        #endregion
        
        #endregion

        #endregion

        #region Init

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            CacheSettings();
        }

        public void CacheSettings()
        {
            if (config != null && config.playerConfig != null)
            {
                _attackDistance = config.playerConfig.attackDistance;
                _attackSpeed = config.playerConfig.attackSpeed;
                _attackDamage = config.playerConfig.attackDamage;
                _bulletSpeed = config.playerConfig.bulletSpeed;

                _playerSpeed = config.playerConfig.playerSpeed;
                _playerHealth = config.playerConfig.playerHealth;
            }
            else
            {
                Debug.LogError("Config or playerConfig is not assigned.");
            }

            _restrictorBounds = restrictor.bounds;
        }

        private void OnEnable()
        {
            EventManager.OnPlayerDamaged += OnPlayerDamaged;
        }


        private void OnDisable()
        {
            EventManager.OnPlayerDamaged -= OnPlayerDamaged;
        }

        private void OnPlayerDamaged(int damage)
        {
            _playerHealth -= damage;
            if (_playerHealth <= 0)
            {
                EventManager.OnPlayerDead?.Invoke();
            }
        }   
        
        #endregion
        
        private void Update()
        {
            Shooting();
            Moving();
        }

        private void Shooting()
        {
            _currentAttackCooldown -= Time.deltaTime;
            if (_currentAttackCooldown <= 0)
            {
                _currentAttackCooldown = _attackSpeed;
                Enemy nearestEnemy = FindNearestEnemy();
                if (nearestEnemy != null)
                {
                    Shoot(nearestEnemy);
                }
            }
        }

        private Enemy FindNearestEnemy()
        {
            if (enemyFactory.Enemies == null || enemyFactory.Enemies.Count == 0)
                return null;

            Enemy closestEnemy = null;
            float minDistance = float.MaxValue;

            foreach (var enemy in enemyFactory.Enemies)
            {
                if(!enemy.gameObject.activeSelf) continue;
                
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        private void Shoot(Enemy target)
        {
            bulletFactory.Create(transform.position, _attackDistance, _attackDamage, _bulletSpeed, target);
            shootParticles.Play();
        }
        
        private void Moving()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            float moveSpeed = _playerSpeed * Time.deltaTime;

            Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed;

            Vector3 newPosition = transform.position + move;

            newPosition.x = Mathf.Clamp(newPosition.x, _restrictorBounds.min.x, _restrictorBounds.max.x);
            newPosition.y = Mathf.Clamp(newPosition.y, _restrictorBounds.min.y, _restrictorBounds.max.y);

            transform.position = newPosition;
        }
    }
}
