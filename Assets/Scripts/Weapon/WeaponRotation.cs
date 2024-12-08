using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private Unit _unit;

    private void Awake()
    {
        _unit = GetComponentInParent<Unit>();
    }

    private void Update()
    {
        transform.LookAt(_unit.Target.transform);
    }
}
