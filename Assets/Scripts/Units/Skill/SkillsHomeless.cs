using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsHomeless : Skill
{
    private HashSet<Zombie> _slowedZombies = new HashSet<Zombie>();

    private float _detectionRadius = 2f;
    private float _decelerationFactor = 2f;

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
        while (true)
        {
            Collider[] enemyes = Physics.OverlapSphere(transform.position, _detectionRadius);
            HashSet<Zombie> currentDetectedEnemies = new HashSet<Zombie>();

            for (int i = 0; i < enemyes.Length; i++)
            {
                if (enemyes[i].TryGetComponent(out Zombie enemy))
                {
                    currentDetectedEnemies.Add(enemy);

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
                if (currentDetectedEnemies.Contains(enemy) == false)
                {
                    RestoreCharacteristics(enemy);
                    _slowedZombies.Remove(enemy);
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