using UnityEngine;

namespace Weapons
{
    public class WeaponRotation : MonoBehaviour
    {
        private IAim _aim;

        private void Awake()
        {
            _aim = GetComponentInParent<IAim>();
        }

        private void Update()
        {
            if (_aim.Target != null)
                transform.LookAt(_aim.Target.transform);
        }
    }
}