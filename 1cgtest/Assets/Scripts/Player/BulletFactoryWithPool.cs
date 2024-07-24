using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Player
{
    public class BulletFactoryWithPool : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<Bullet> bullets;
        [SerializeField] private Bullet prefab;

        [SerializeField] private HitFactoryWithPool hitFactory;
        
        [SerializeField] private Transform bulletContainer;

        #region API

        public List<Bullet> Bullets => bullets;

        #endregion
        
        #endregion

        public Bullet Create(Vector3 position, float attackDistance, float attackDamage, float bulletSpeed, Enemy enemy)
        {
            Bullet newBullet = bullets.Find(e => !e.gameObject.activeSelf);

            if (!newBullet)
            {
                newBullet = Instantiate(prefab, position, Quaternion.identity, bulletContainer);
                bullets.Add(newBullet);
            }
            else
            {
                newBullet.gameObject.SetActive(true);
                newBullet.gameObject.transform.position = position;
            }
            
            newBullet.Initialize(attackDistance, attackDamage, bulletSpeed, enemy, hitFactory);

            return newBullet;
        }

        public void ClearAll()
        {
            foreach (var enemy in bullets)
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