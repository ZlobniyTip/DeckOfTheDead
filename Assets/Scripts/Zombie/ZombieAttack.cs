using System.Collections;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private ZombieSearchTarget _zombieSearchTarget;
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBetweenAttack;
    [SerializeField] private float _attackDistance;

    public bool IsAttacking { get; private set; } = false;
    public float AttackDistance => _attackDistance;

    public void ActivateAttack()
    {
        if (IsAttacking)
            return;

        IsAttacking = true;

        StopCoroutine(_zombieSearchTarget.SearchTarget());
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        var delay = new WaitForSeconds(_delayBetweenAttack);

        while (_zombieSearchTarget.Target != null)
        {
            transform.LookAt(_zombieSearchTarget.Target.transform);
            var distance = Vector3.Distance(transform.position, _zombieSearchTarget.Target.transform.position);

            if (distance <= _attackDistance)
            {
                yield return delay;
                _zombieSearchTarget.Target.TakeDamage(_damage);
            }
            else
            {
                IsAttacking = false;
            }

            yield return delay;
        }

        IsAttacking = false;
        StartCoroutine(_zombieSearchTarget.SearchTarget());
    }
}