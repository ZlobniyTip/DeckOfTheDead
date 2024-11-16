using UnityEngine;

public class TurnSlider : MonoBehaviour
{
    private Camera _camera;
    private float _degree = 180;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, _degree, 0);
    }
}