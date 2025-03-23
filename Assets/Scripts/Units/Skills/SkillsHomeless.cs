using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Units.Skills
{
    public class SkillsHomeless : Skill
    {
        private readonly HashSet<Zombie> SlowedZombies = new HashSet<Zombie>();
        private readonly float DetectionRadius = 2f;
        private readonly float DecelerationFactor = 2f;
        private readonly Collider[] OverlappedColliders = new Collider[10];

        private HashSet<Zombie> _currentDetectedEnemies = new HashSet<Zombie>();

        private void Start()
        {
            StartCoroutine(SearchTarget());
        }

        private void OnDisable()
        {
            foreach (var enemy in SlowedZombies)
            {
                RestoreCharacteristics(enemy);
            }
        }

        private IEnumerator SearchTarget()
        {
            var delaySearch = new WaitForSeconds(0.1f);

            while (enabled)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, DetectionRadius, OverlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (OverlappedColliders[i].TryGetComponent(out Zombie enemy))
                    {
                        _currentDetectedEnemies.Add(enemy);

                        if (SlowedZombies.Contains(enemy) == false)
                        {
                            if (enemy.IsUnderCamp == false)
                            {
                                enemy.EnterCamp();
                                enemy.ZombieAttack.SlowingDownAttack(DecelerationFactor);
                                enemy.ZombieView.ChangeSpeed(DecelerationFactor);
                                SlowedZombies.Add(enemy);
                            }
                        }
                    }
                }

                foreach (var enemy in new List<Zombie>(SlowedZombies))
                {
                    if (_currentDetectedEnemies.Contains(enemy) == false)
                    {
                        RestoreCharacteristics(enemy);
                        SlowedZombies.Remove(enemy);
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