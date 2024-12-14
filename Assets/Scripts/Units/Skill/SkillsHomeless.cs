using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsHomeless : MonoBehaviour
{
    private HashSet<Enemy> _slowedZombies = new HashSet<Enemy>();

    private float _detectionRadius = 2f;
    private float _decelerationFactor = 2f;

    private float _currentAttackSpeed;
    private float _currentAnivftionSpeed;


    private void Start()
    {
        StartCoroutine(SearchTarget());
    }

    public IEnumerator SearchTarget()
    {
        while (true)
        {
            Collider[] enemyes = Physics.OverlapSphere(transform.position, _detectionRadius);

            for (int i = 0; i < enemyes.Length; i++)
            {
                if (enemyes[i].TryGetComponent(out Enemy enemy))
                {
                    if (!_slowedZombies.Contains(enemy))
                    {
                        if(enemy.IsUnderCamp == false)
                        {
                            enemy.EnterCamp();
                            enemy.ZombieAttack.SlowingDownAttack(2);
                            enemy.ZombieView.ChangeSpeed(0.5f);
                            _slowedZombies.Add(enemy);
                        }
                        else
                        {
                            if (Vector3.Distance(transform.position, enemy.transform.position) > _detectionRadius)
                            {
                                enemy.ExitCamp();
                                _slowedZombies.Remove(enemy);
                                enemy.ZombieAttack.SlowingDownAttack(1);
                                enemy.ZombieView.ChangeSpeed(1);
                            }
                        }
                    }
                }
            }

            yield return null;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _slowedZombies)
        {
            enemy.ExitCamp();
            _slowedZombies.Remove(enemy);
            enemy.ZombieAttack.SlowingDownAttack(1);
            enemy.ZombieView.ChangeSpeed(1);
        }
    }
}
