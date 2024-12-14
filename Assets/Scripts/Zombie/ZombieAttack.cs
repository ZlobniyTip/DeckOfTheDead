using System.Collections;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private ZombieSearchTarget _zombieSearchTarget;
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBetweenAttack;
    [SerializeField] private float _attackDistance;

    private bool _isAttacking = false; 
    private Coroutine _attackCoroutine;

    public float AttackDistance => _attackDistance;
    public bool IsAttacking => _isAttacking;
    public float DelayBetweenAttack => _delayBetweenAttack; 

    public void ActivateAttack()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        if (_attackCoroutine != null) StopCoroutine(_attackCoroutine); 
        _attackCoroutine = StartCoroutine(Attacking());
    }

    public void SlowingDownAttack(float speed)
    {
        _delayBetweenAttack = speed;
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_delayBetweenAttack);

        while (_zombieSearchTarget.Target != null)
        {
            TurnToTarget();
            var distance = Vector3.Distance(transform.position, _zombieSearchTarget.Target.transform.position);

            if (distance <= _attackDistance)
                _zombieSearchTarget.Target.TakeDamage(_damage);
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

    private void Update()
    {
        Debug.Log(_delayBetweenAttack);
    }
}
