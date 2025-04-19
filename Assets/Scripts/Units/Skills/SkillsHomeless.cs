using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Units.Skills
{
    public class SkillsHomeless : Skill
    {
        private readonly HashSet<Zombie> _slowedZombies = new HashSet<Zombie>();
        private readonly float _detectionRadius = 2f;
        private readonly float _decelerationFactor = 2f;
        private readonly Collider[] _overlappedColliders = new Collider[10];

        private HashSet<Zombie> _currentDetectedEnemies = new HashSet<Zombie>();

        private void Start()
        {
            StartCoroutine(SearchTarget());
        }

        private void OnDisable()
        {
            foreach (var enemy in _slowedZombies)
            {
                RestoreCharacteristics(enemy);
            }
        }

        private IEnumerator SearchTarget()
        {
            var delaySearch = new WaitForSeconds(0.1f);

            while (enabled)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, _overlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (_overlappedColliders[i].TryGetComponent(out Zombie enemy))
                    {
                        _currentDetectedEnemies.Add(enemy);

                        if (_slowedZombies.Contains(enemy) == false)
                        {
                            if (enemy.IsUnderCamp == false)
                            {
                                enemy.EnterCamp();
                                enemy.ZombieAttack.SlowingDownAttack(_decelerationFactor);
                                enemy.ZombieView.ChangeSpeed(_decelerationFactor);
                                _slowedZombies.Add(enemy);
                            }
                        }
                    }
                }

                foreach (var enemy in new List<Zombie>(_slowedZombies))
                {
                    if (_currentDetectedEnemies.Contains(enemy) == false)
                    {
                        RestoreCharacteristics(enemy);
                        _slowedZombies.Remove(enemy);
                    }
                }

                yield return delaySearch;
            }
        }

        private void RestoreCharacteristics(Zombie enemy)
        {
            if (enemy != null)
            {
                enemy.ExitCamp();
                enemy.ZombieAttack.RestoreAttackSpeed();
                enemy.ZombieView.RestoreAnimationSpeed();
            }
        }
    }
}