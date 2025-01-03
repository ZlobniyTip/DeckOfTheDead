using System.Collections;
using UnityEngine;

public class CharacterScaning : MonoBehaviour 
{
    [SerializeField] private CharacterShooting _characterShooting;

    private Zombie _currentEnemy;

    public Zombie Target => _currentEnemy;

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
                    if (rigidbody.gameObject.TryGetComponent(out Zombie enemy))
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
}