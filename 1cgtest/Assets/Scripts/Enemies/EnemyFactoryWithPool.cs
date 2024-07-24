using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyFactoryWithPool : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private Enemy prefab;
        
        [SerializeField] private Transform enemyContainer;

        #region API

        public List<Enemy> Enemies => enemies;

        #endregion
        
        #endregion

        public Enemy Create(Vector3 position, float currentEnemySpeed, float initialHealth)
        {
            Enemy newEnemy = enemies.Find(e => !e.gameObject.activeSelf);

            if (!newEnemy)
            {
                newEnemy = Instantiate(prefab, position, Quaternion.identity, enemyContainer);
                enemies.Add(newEnemy);
            }
            else
            {
                newEnemy.gameObject.transform.position = position;
                newEnemy.gameObject.SetActive(true);
            }

            newEnemy.Initialize(currentEnemySpeed, initialHealth);
            newEnemy.StartMoving();

            return newEnemy;
        }


        public void ClearAll()
        {
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            ClearAll();
        }
    }
}