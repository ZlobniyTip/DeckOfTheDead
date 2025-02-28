using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Skills
{
    public class SkillsHomeless : Skill
    {
        private readonly HashSet<Zombie> SlowedZombies = new HashSet<Zombie>();
        private readonly float DetectionRadius = 2f;
        private readonly float DecelerationFactor = 2f;

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
            while (true)
            {
                Collider[] enemyes = Physics.OverlapSphere(transform.position, DetectionRadius);
                HashSet<Zombie> currentDetectedEnemies = new HashSet<Zombie>();

                for (int i = 0; i < enemyes.Length; i++)
                {
                    if (enemyes[i].TryGetComponent(out Zombie enemy))
                    {
                        currentDetectedEnemies.Add(enemy);

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
                    if (currentDetectedEnemies.Contains(enemy) == false)
                    {
                        RestoreCharacteristics(enemy);
                        SlowedZombies.Remove(enemy);
                    }
                }

                yield return new WaitForSeconds(0.1f);
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