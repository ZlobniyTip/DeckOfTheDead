using System.Collections;
using Enemy;
using UnityEngine;

namespace Character
{
    public class CharacterScaning : MonoBehaviour
    {
        private readonly Collider[] _overlappedColliders = new Collider[10];

        [SerializeField] private CharacterShooting _characterShooting;

        private Zombie _currentEnemy;

        private void Start()
        {
            StartCoroutine(SearchEnemy());
        }

        public void ActivSearch()
        {
            _characterShooting.StopShooting();
            _currentEnemy = null;

            StartCoroutine(SearchEnemy());
        }

        public void StopSearch()
        {
            StopCoroutine(SearchEnemy());
        }

        private IEnumerator SearchEnemy()
        {
            yield return new WaitForSeconds(0.1f);

            while (_currentEnemy == null)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, 
                    _characterShooting.CurrentWeapon.AttackRange, _overlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (!_overlappedColliders[i].TryGetComponent(out Rigidbody rigidbody) || rigidbody == null)
                        continue;

                    if (!rigidbody.gameObject.TryGetComponent(out Zombie enemy) || enemy.IsDiying)
                        continue;

                    if (enemy.IsIgnored)
                        enemy.SetIgnoredStatus(false);

                    _currentEnemy = enemy;
                    _characterShooting.ActivShooting(_currentEnemy);
                    yield break;
                }

                yield return null;
            }
        }
    }
}