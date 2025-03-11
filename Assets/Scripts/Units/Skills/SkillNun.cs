using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Skills
{
    public class SkillNun : Skill
    {
        private readonly HashSet<Unit> Units = new HashSet<Unit>();
        private readonly float DetectionRadius = 2f;
        private readonly Collider[] OverlappedColliders = new Collider[10];
        private readonly int AmountRegeneration = 2;

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
            while (enabled)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, DetectionRadius, OverlappedColliders);
                HashSet<Unit> currentDetectedUnits = new HashSet<Unit>();

                for (int i = 0; i < count; i++)
                {
                    if (OverlappedColliders[i].TryGetComponent(out Unit unit))
                    {
                        currentDetectedUnits.Add(unit);

                        if (Units.Contains(unit))
                        {
                            unit.TakeHeal(AmountRegeneration);
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