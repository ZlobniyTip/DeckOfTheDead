using UnityEngine;

public class PressIncreaseEnergy : Skill
{
    [SerializeField] private Unit _unit;

    private void Start()
    {
        _unit.Died += UseSkill;
    }

    private void OnDestroy()
    {
        _unit.Died -= UseSkill;
    }

    public override void UseSkill()
    {
        StartCoroutine(_unit.Character.Energy.IncreaseEnergyJournalist());
    }
}