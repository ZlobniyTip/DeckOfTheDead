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
        if (_unit.Target != null)
            transform.LookAt(_unit.Target.transform);
    }
}
