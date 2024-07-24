using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Create GameConfig", fileName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public List<GameObject> enemySpawnPoints = new ();
        public PlayerConfig playerConfig = new ();
        public EnemiesConfig enemiesConfig = new ();
        public RestartCanvasConfig restartCanvasConfig = new ();
    }

    [Serializable]
    public class PlayerConfig
    {
        public float attackDistance;
        public float attackSpeed;
        public float attackDamage;
        public float bulletSpeed;
        public float playerSpeed;
        public float playerHealth;
    }

    [Serializable]
    public class EnemiesConfig
    {
        public int minEnemyCount, maxEnemyCount;

        public float minSpawnCooldown, maxSpawnCooldown;

        public float minEnemySpeed, maxEnemySpeed;

        public int enemyHealthCount;
    }

    [Serializable]
    public class RestartCanvasConfig
    {
        public string winText, loseText;

        public Sprite winSprite, loseSprite;
    }
}