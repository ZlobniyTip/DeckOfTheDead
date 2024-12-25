using System.Collections;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    [SerializeField] private ParticleSystem _burningEffect;
    [SerializeField] private ParticleSystem _radiusEffect;
    [SerializeField] private float _delayBetweenDamage;
    [SerializeField] private float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private float _durationBurning;

    private float _timer = 0;

    private void OnDestroy()
    {
        StopCoroutine(Flame());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            StartCoroutine(Flame());
            Instantiate(_burningEffect, transform);
            Instantiate(_radiusEffect, transform.localPosition,Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private IEnumerator Flame()
    {
        while (_timer < _durationBurning)
        {
            var delay = new WaitForSeconds(_delayBetweenDamage);

            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            Rigidbody rigidbody;

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                rigidbody = overlappedColliders[i].attachedRigidbody;

                if (rigidbody)
                {
                    if (rigidbody.gameObject.TryGetComponent(out Zombie enemy))
                    {
                        enemy.TakeDamage(_damage);

                        yield return delay;
                    }
                }
            }

            yield return null;
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
