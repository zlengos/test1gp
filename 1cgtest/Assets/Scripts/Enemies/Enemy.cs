using DG.Tweening;
using Tools;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        #region Fields

        private float _moveSpeed;
        private float _health;
        
        private Tween _movementTween;

    
        #endregion

        private void OnEnable()
        {
            StartMoving();
        }
        
        private void Start()
        {
            Debug.Log(gameObject.transform.position.z);
            StartMoving();
            Debug.Log(gameObject.transform.position.z);
        }

        public void Initialize(float moveSpeed, float health)
        {
            _health = health;
            _moveSpeed = moveSpeed;
        }

        public void StartMoving()
        {
            Move(_moveSpeed, 2f);
            Debug.Log(gameObject.transform.position.z);
        }

        private void Move(float distance, float duration)
        {
            float startY = gameObject.transform.position.y;

            var pos2d = gameObject.transform.position;
            pos2d.z = 0;
            gameObject.transform.position = pos2d;

            _movementTween = transform.DOMoveY(startY - distance, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Move(distance, duration);
                });
        }

        public void GetDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                DOTween.Kill(transform);
                EventManager.OnEnemyDead?.Invoke();
                gameObject.SetActive(false);
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Restrictor"))
            {
                EventManager.OnPlayerDamaged?.Invoke(1);
            }
        }
    }
}