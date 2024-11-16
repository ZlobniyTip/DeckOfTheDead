using System.Collections;
using UnityEngine;

public class CharacterScaning : MonoBehaviour
{
    [SerializeField] private CharacterShooting _characterShooting;

    private Enemy _currentEnemy;

    private void Start()
    {
        StartCoroutine(SearchEnemy());
    }

    public IEnumerator SearchEnemy()
    {
        while (_currentEnemy == null)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _characterShooting.CurrentWeapon.AttackDistance);
            Rigidbody rigidbody;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;

                if (rigidbody)
                {
                    if (rigidbody.gameObject.TryGetComponent(out Enemy enemy))
                    {
                        _currentEnemy = enemy;
                        _characterShooting.ActivShooting(_currentEnemy);
                        yield break;
                    }
                }
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _characterShooting.CurrentWeapon.AttackDistance);
    }
}