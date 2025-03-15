using UnityEngine;

namespace Units.Skills
{
    public class PressIncreaseEnergy : Skill
    {
        [SerializeField] private Unit _unit;

        private void Start()
        {
            _unit.Died -= OnUseSkill;
            _unit.Died += OnUseSkill;
        }

        private void OnDestroy()
        {
            _unit.Died -= OnUseSkill;
        }

        public override void OnUseSkill()
        {
            if (!_unit.IsDiying)
            {
                _unit.StartCoroutine(_unit.Character.Energy.IncreaseEnergyJournalist());
            }
        }
    }
}