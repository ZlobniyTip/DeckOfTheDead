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

    public void ActivateAttack()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        if (_attackCoroutine != null) StopCoroutine(_attackCoroutine); 
        _attackCoroutine = StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_delayBetweenAttack);

        while (_zombieSearchTarget.Target != null)
        {
            transform.LookAt(_zombieSearchTarget.Target.transform);
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
}
