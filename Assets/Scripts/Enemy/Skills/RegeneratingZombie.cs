using System.Collections;
using UnityEngine;

namespace Enemy.Skills
{
    [RequireComponent(typeof(Zombie))]
    public class RegeneratingZombie : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _healingEffect;
        [SerializeField] private float _cooldown;
        [SerializeField] private int _healValue;

        private Zombie _zombie;

        private void Start()
        {
            _zombie = GetComponent<Zombie>();
            Instantiate(_healingEffect, transform);

            StartCoroutine(Regeneration());
        }

        private IEnumerator Regeneration()
        {
            var delay = new WaitForSeconds(_cooldown);

            while (enabled)
            {
                _zombie.TakeHeal(_healValue);

                yield return delay;
            }
        }
    }
}