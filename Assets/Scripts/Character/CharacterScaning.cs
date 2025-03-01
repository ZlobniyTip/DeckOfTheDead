using System.Collections;
using Enemy;
using UnityEngine;

namespace Character
{
    public class CharacterScaning : MonoBehaviour
    {
        private readonly Collider[] OverlappedColliders = new Collider[10];

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
                int count = Physics.OverlapSphereNonAlloc(transform.position, _characterShooting.CurrentWeapon.AttackRange, OverlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (!OverlappedColliders[i].TryGetComponent(out Rigidbody rigidbody) || rigidbody == null)
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