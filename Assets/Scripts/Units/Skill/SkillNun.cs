using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNun : Skill
{
    private HashSet<Unit> _units = new HashSet<Unit>();

    private float _detectionRadius = 2f;

    private void Start()
    {
        StartCoroutine(RestoreHealth());
    }

    private void OnDisable()
    {
        StopCoroutine(RestoreHealth());
    }

    public IEnumerator RestoreHealth()
    {
        while (true)
        {
            Collider[] detectedColliders = Physics.OverlapSphere(transform.position, _detectionRadius);
            HashSet<Unit> currentDetectedUnits = new HashSet<Unit>();

            for (int i = 0; i < detectedColliders.Length; i++)
            {
                if (detectedColliders[i].TryGetComponent(out Unit unit))
                {
                    currentDetectedUnits.Add(unit);

                    if (_units.Contains(unit))
                    {
                        unit.TakeHeal(2);
                    }
                    else
                    {
                        //unit.RestoringHealth();
                        _units.Add(unit); 
                    }
                }
            }

            foreach (var unit in new List<Unit>(_units))
            {
                if (!currentDetectedUnits.Contains(unit))
                {
                    _units.Remove(unit);
                    //unit.NotRestoringHealth();
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
