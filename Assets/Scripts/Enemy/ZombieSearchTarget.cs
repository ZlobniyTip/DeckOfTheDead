using System.Collections;
using Character;
using Other;
using Units;
using UnityEngine;

namespace Enemy
{
    public class ZombieSearchTarget : MonoBehaviour
    {
        private readonly Collider[] _overlappedColliders = new Collider[10];

        [SerializeField] private ZombieAttack _zombieAttack;
        [SerializeField] private float _radius;

        private Health _target;
        private Health _startTarget;

        public Health Target => _target;
 
        public bool SearchingTarget { get; private set; } = true;

        private void Start()
        {
            StartCoroutine(SearchTarget());
        }

        public IEnumerator SearchTarget()
        {
            SearchingTarget = true;

            while (SearchingTarget)
            {
               int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlappedColliders);

                Rigidbody rigidbody;
                Health unitTarget = null;
                Health characterTarget = null;

                foreach (var collider in _overlappedColliders)
                {
                    rigidbody = collider.attachedRigidbody;

                    if (rigidbody && rigidbody.gameObject.TryGetComponent(out Health health))
                    {
                        if (health.IsDiying == false)
                        {
                            if (health is Unit)
                            {
                                unitTarget = health;
                                break;
                            }
                            else if (health is Player)
                            {
                                characterTarget = health;
                            }
                        }
                    }
                }

                if (unitTarget != null)
                {
                    InitializeTarget(unitTarget);
                    _zombieAttack.ActivateAttack();
                }
                else if (characterTarget != null)
                {
                    InitializeTarget(characterTarget);
                    _zombieAttack.ActivateAttack();
                }
                else if (Target == null)
                {
                    SetStartTarget();
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        public void InitializeStartTarget(Health target)
        {
            _target = target;
            _startTarget = target;
        }  

        public void SetStartTarget()
        {
            _target = _startTarget;
        }

        private void OnClearTarget()
        {
            _target.Died -= OnClearTarget;
            SetStartTarget();
        }

        private void InitializeTarget(Health target)
        {
            if (_target != null)
                _target.Died -= OnClearTarget;

            if (target != null && !target.IsDiying)
            {
                _target = target;
                _target.Died += OnClearTarget;
            }
        }

        private void OnDisable()
        {
            SearchingTarget = false;
        }
    }
}