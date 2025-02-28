using UnityEngine;

namespace UI.Bar
{
    public class TurnSlider : MonoBehaviour
    {
        private readonly float Degree = 180;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.LookAt(new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
            transform.Rotate(0, Degree, 0);
        }
    }
}