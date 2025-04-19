using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Skills
{
    public class SkillNun : Skill
    {
        private readonly HashSet<Unit> _units = new HashSet<Unit>();
        private readonly float _detectionRadius = 2f;
        private readonly Collider[] _overlappedColliders = new Collider[10];
        private readonly int _amountRegeneration = 2;

        private HashSet<Unit> _currentDetectedUnits = new HashSet<Unit>();

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
                int count = Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, _overlappedColliders);

                for (int i = 0; i < count; i++)
                {
                    if (_overlappedColliders[i].TryGetComponent(out Unit unit))
                    {
                        _currentDetectedUnits.Add(unit);

                        if (_units.Contains(unit))
                        {
                            unit.TakeHeal(_amountRegeneration);
                        }
                        else
                        {
                            _units.Add(unit);
                        }
                    }
                }

                foreach (var unit in new List<Unit>(_units))
                {
                    if (!_currentDetectedUnits.Contains(unit))
                    {
                        _units.Remove(unit);
                    }
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }
}