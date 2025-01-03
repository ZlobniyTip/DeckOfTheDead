using System.Collections;
using UnityEngine;

public class ClawStrike : Skill
{
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private Bleeding _bleeding;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _radius;

    private Bleeding _currentBleeding;

    private void Start()
    {
        StartCoroutine(AttackWithClaws());
    }

    private IEnumerator AttackWithClaws()
    {
        var delay = new WaitForSeconds(_cooldown);

        while (true)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;
                if (rigidbody)
                {
                    if (rigidbody.gameObject.TryGetComponent(out Unit enemy))
                    {
                        Instantiate(_hitEffect, transform);
                        _currentBleeding = Instantiate(_bleeding, enemy.transform);
                        _currentBleeding.GetLinkUnit(enemy);
                    }
                }
            }

            yield return delay;
        }
    }
}