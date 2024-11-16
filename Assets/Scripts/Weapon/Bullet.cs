using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _force = 10000;
    private float _timer = 2;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        _rigidbody.AddForce(transform.forward * _force * Time.deltaTime);

        if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}