using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class ZombieAttack : MonoBehaviour
    {
        [SerializeField] private ZombieSearchTarget _zombieSearchTarget;
        [SerializeField] private int _damage;
        [SerializeField] private float _delayBetweenAttack;
        [SerializeField] private float _attackDistance;

        private bool _isAttacking = false;
        private Coroutine _attackCoroutine;
        private float _startDelayBetweenAttack;
        private Zombie _enemy;

        public float AttackDistance => _attackDistance;
        public bool IsAttacking => _isAttacking;
        public float DelayBetweenAttack => _delayBetweenAttack;


        private void Awake()
        {
            _startDelayBetweenAttack = _delayBetweenAttack;
            _enemy = GetComponent<Zombie>();
        }

        public void ActivateAttack()
        {
            if (_isAttacking) return;

            _isAttacking = true;
            if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
            _attackCoroutine = StartCoroutine(Attacking());
        }

        public void SlowingDownAttack(float speed) => _delayBetweenAttack *= speed;
        public void RestoreAttackSpeed() => _delayBetweenAttack = _startDelayBetweenAttack;

        public void BuffAttack(int multiplyAttackSpeed, int multiplyDamage)
        {
            _delayBetweenAttack *= multiplyAttackSpeed;
            _damage *= multiplyDamage;
        }

        private IEnumerator Attacking()
        {
            var delay = new WaitForSeconds(_delayBetweenAttack);

            while (_zombieSearchTarget.Target != null && _zombieSearchTarget.Target.IsDiying == false)
            {
                TurnToTarget();
                var distance = Vector3.Distance(transform.position, _zombieSearchTarget.Target.transform.position);

                if (distance <= _attackDistance)
                {
                    _zombieSearchTarget.Target.TakeDamageFromEnemy(_damage, _enemy);
                }
                else
                    break;

                yield return delay;
            }

            _isAttacking = false;
            _attackCoroutine = null;

            if (!_zombieSearchTarget.SearchingTarget)
            {
                StartCoroutine(_zombieSearchTarget.SearchTarget());
            }
        }

        private void TurnToTarget()
        {
            if (_zombieSearchTarget.Target != null)
            {
                Vector3 direction = _zombieSearchTarget.Target.transform.position - transform.position;
                direction.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
            }
        }
    }
}