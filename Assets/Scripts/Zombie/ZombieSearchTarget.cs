using System.Collections;
using UnityEngine;

public class ZombieSearchTarget : MonoBehaviour
{
    [SerializeField] private ZombieAttack _zombieAttack;
    [SerializeField] private float _radius;

    private Enemy _enemy;
    private Health _target;
    private Health _startTarget;
    private bool _onlySearchEnemies = false;

    public Health Target => _target;
    public bool OnlySearchEnemies => _onlySearchEnemies;
    public bool SearchingTarget { get; private set; } = true;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        StartCoroutine(SearchTarget());
    }

    public IEnumerator SearchTarget()
    {
        SearchingTarget = true;

        while (SearchingTarget)
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;
            Health target = null;

            foreach (var collider in overlappedColliders)
            {
                rigidbody = collider.attachedRigidbody;

                if (rigidbody)
                {
                    if (_onlySearchEnemies && rigidbody.gameObject.TryGetComponent(out Enemy enemy))
                    {
                        if (enemy != _enemy)
                        {
                            target = enemy;
                            break;
                        }
                    }
                    else if (!_onlySearchEnemies && rigidbody.gameObject.TryGetComponent(out Health health))
                    {
                        if (health is Unit || health is Character)
                        {
                            target = health;
                            break;
                        }
                    }
                }
            }

            if (target != null)
            {
                InitializeTarget(target);
                _zombieAttack.ActivateAttack();
            }
            else if (Target == null)
                SetStartTarget();

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void InitializeStartTarget(Health target)
    {
        _target = target;
        _startTarget = target;
    }

    public void InitializeTarget(Health target) => _target = target;

    public void LookingZombies() => _onlySearchEnemies = true;

    public void DonLookZombies() => _onlySearchEnemies = false;

    public void SetStartTarget() => _target = _startTarget;

    private void OnDisable() => SearchingTarget = false;
}