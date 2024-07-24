using Configs;
using UnityEngine;
using DG.Tweening;
using Enemies;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameConfig config;
        [SerializeField] private HitFactoryWithPool factory;
        
        private float _distance;
        private float _damage;
        private float _speed;
        private Enemy _target;
        private Tween _movementTween;

        #endregion

        public void Initialize(float distance, float damage, float speed, Enemy target, HitFactoryWithPool hitFactory)
        {
            _distance = distance;
            _damage = damage;
            _speed = speed;
            _target = target;

            StartMovement();

            factory = hitFactory;
        }

        private void StartMovement()
        {
            float distance = Vector3.Distance(transform.position, _target.gameObject.transform.position);
    
            float duration = distance / _speed / 2;

            _movementTween = transform.DOMove(_target.gameObject.transform.position, duration)
                .SetEase(Ease.Linear)
                .OnKill(() => gameObject.SetActive(false));

            Vector3 targetDirection = _target.gameObject.transform.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

            transform.DORotate(new Vector3(0, 0, angle), duration)
                .SetEase(Ease.Linear);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.GetDamage(_damage);

                _movementTween.Kill();
                gameObject.SetActive(false);
                factory.Create(transform.position);
            }
        }
    }
}