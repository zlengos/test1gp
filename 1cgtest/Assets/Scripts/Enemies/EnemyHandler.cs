using Configs;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemyHandler : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameConfig config;
        [SerializeField] private EnemyFactoryWithPool factory;

        #region Runtime

        private int _currentEnemyCount;
        private int _enemiesToKill;
        private float _currentEnemySpawnCooldown;
        private float _currentEnemySpeed;

        private float _minSpawnCooldown;
        private float _maxSpawnCooldown;
        private float _minSpeed;
        private float _maxSpeed;
        private float _initialHealth;
        
        #endregion
        
        #endregion

        #region Init
        
        private void Awake()
        {  
            Initialize();
        }

        private void Initialize()
        {
            _currentEnemyCount = Random.Range(
                config.enemiesConfig.minEnemyCount, 
                config.enemiesConfig.maxEnemyCount);

            CacheSettings();

            GetEnemySettings();

            _enemiesToKill = _currentEnemyCount;
        }

        private void CacheSettings()
        {
            _minSpawnCooldown = config.enemiesConfig.minSpawnCooldown;
            _maxSpawnCooldown = config.enemiesConfig.maxSpawnCooldown;
            _minSpeed = config.enemiesConfig.minEnemySpeed;
            _maxSpeed = config.enemiesConfig.maxEnemySpeed;
            _initialHealth = config.enemiesConfig.enemyHealthCount;
        }

        private void OnEnable()
        {
            EventManager.OnEnemyDead += OnEnemyDead;
        }

        private void OnDisable()
        {
            EventManager.OnEnemyDead -= OnEnemyDead;
        }

        #endregion
        
        private void OnEnemyDead()
        {
            _enemiesToKill--;

            if (_enemiesToKill == 0)
            {
                EventManager.OnEnemiesDestroyed?.Invoke();
            }
        }
        
        private void GetEnemySettings()
        {
            _currentEnemySpeed = Random.Range(
                _minSpeed, 
                _maxSpeed);
            
            _currentEnemySpawnCooldown = Random.Range(
                _minSpawnCooldown, 
                _maxSpawnCooldown);
        }
        
        private void SpawnEnemy()
        {
            _currentEnemyCount--;
            
            GetEnemySettings();

            var randomSpawnPointIndex = Random.Range(0, config.enemySpawnPoints.Count);

            var position2D = config.enemySpawnPoints[randomSpawnPointIndex].transform.position;
            position2D.z = 0;
            
            factory.Create(position2D, _currentEnemySpeed, _initialHealth);
        }

        private void Update()
        {
            _currentEnemySpawnCooldown -= Time.deltaTime;
            if (_currentEnemySpawnCooldown <= 0)
            {
                SpawnEnemy();
                _currentEnemySpawnCooldown = Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
            }
        }
    }
}
