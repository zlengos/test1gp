using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HitFactoryWithPool : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<BulletHit> hits;
        [SerializeField] private BulletHit prefab;
        
        [SerializeField] private Transform bulletContainer;

        #region API

        public List<BulletHit> Hits => hits;

        #endregion
        
        #endregion

        public BulletHit Create(Vector3 position)
        {
            BulletHit newBullet = hits.Find(e => !e.gameObject.activeSelf);

            if (!newBullet)
            {
                newBullet = Instantiate(prefab, position, Quaternion.identity, bulletContainer);
                hits.Add(newBullet);
            }
            else
            {
                newBullet.gameObject.SetActive(true);
                newBullet.gameObject.transform.position = position;
            }
            
            return newBullet;
        }

        public void ClearAll()
        {
            foreach (var enemy in hits)
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