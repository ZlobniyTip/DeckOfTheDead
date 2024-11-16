using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private float _easing;
    [SerializeField] private Transform _pointOfInterest;

    private Vector3 _destination;
    private float _camCoordinate;

    private void Awake()
    {
        _camCoordinate = transform.position.z;
    }

    private void FixedUpdate()
    {
        if (_pointOfInterest == null)
            return;

        _destination = _pointOfInterest.transform.position + new Vector3(0, 10, 0);
        _destination = Vector3.Lerp(transform.position, _destination, _easing);
        _destination.z = _camCoordinate;
        transform.position = _destination;
    }
}
