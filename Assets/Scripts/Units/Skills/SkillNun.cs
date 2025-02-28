using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Skills
{
    public class SkillNun : Skill
    {
        private readonly HashSet<Unit> Units = new HashSet<Unit>();
        private readonly float DetectionRadius = 2f;

        private void Start()
        {
            StartCoroutine(RestoreHealth());
        }

        private void OnDestroy()
        {
            StopCoroutine(RestoreHealth());
        }

        public IEnumerator RestoreHealth()
        {
            while (true)
            {
                Collider[] detectedColliders = Physics.OverlapSphere(transform.position, DetectionRadius);
                HashSet<Unit> currentDetectedUnits = new HashSet<Unit>();

                for (int i = 0; i < detectedColliders.Length; i++)
                {
                    if (detectedColliders[i].TryGetComponent(out Unit unit))
                    {
                        currentDetectedUnits.Add(unit);

                        if (Units.Contains(unit))
                        {
                            unit.TakeHeal(2);
                        }
                        else
                        {
                            Units.Add(unit);
                        }
                    }
                }

                foreach (var unit in new List<Unit>(Units))
                {
                    if (!currentDetectedUnits.Contains(unit))
                    {
                        Units.Remove(unit);
                    }
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }
}