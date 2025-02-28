using Enemy;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class CharacterScaning : MonoBehaviour
    {
        [SerializeField] private CharacterShooting _characterShooting;

        private Zombie _currentEnemy;

        public Zombie CurrentEnemy => _currentEnemy;

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
                Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _characterShooting.CurrentWeapon.AttackRange);
                Rigidbody rigidbody;

                for (int i = 0; i < overlappedColliders.Length; i++)
                {
                    rigidbody = overlappedColliders[i].attachedRigidbody;

                    if (rigidbody)
                    {
                        if (rigidbody.gameObject.TryGetComponent(out Zombie enemy))
                        {
                            if (enemy.IsDiying == false)
                            {
                                if (enemy.IsIgnored == true)
                                    enemy.SetIgnoredStatus(false);

                                _currentEnemy = enemy;
                                _characterShooting.ActivShooting(_currentEnemy);
                                yield break;
                            }
                        }
                    }
                }

                yield return null;
            }
        }
    }
}