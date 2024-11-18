using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _prefabGameObject;
    [SerializeField] private GameObject _cardView;
    [SerializeField] private ParticleSystem _prefabSpawnPlaceEffect;
    [SerializeField] private ParticleSystem _prefabSpawnEffect;

    private RectTransform _rectTransform;
    private Vector3 _originalPosition;

    private GameObject _gameObject;
    private ParticleSystem _spawnPlaceEffect;
    private float _distanceFromRoad = 0.5f;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPosition = transform.position;

        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            _cardView.gameObject.SetActive(false);
            _spawnPlaceEffect = Instantiate(_prefabSpawnPlaceEffect, spawnPosition, Quaternion.identity);
        }
        else
        {
            _gameObject = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;

        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            _spawnPlaceEffect.transform.position = spawnPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_spawnPlaceEffect != null && _spawnPlaceEffect.gameObject.activeSelf)
        {
            Vector3 spawnPosition = _spawnPlaceEffect.transform.position;

            ParticleSystem spawnEffect = Instantiate(_prefabSpawnEffect,
                new Vector3(spawnPosition.x, spawnPosition.y + _distanceFromRoad, spawnPosition.z),
                Quaternion.identity);

            StartCoroutine(SetDelaySpawning(spawnPosition));
            _spawnPlaceEffect.gameObject.SetActive(false);
        }
        else
        {
            transform.position = _originalPosition;
            _cardView.gameObject.SetActive(true);

            _spawnPlaceEffect.gameObject.SetActive(false);
        }
    }

    private bool FindSpawnLocation(out Vector3 spawnPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<Road>() != null)
            {
                spawnPosition = hit.point;
                return true;
            }
        }

        spawnPosition = _originalPosition;
        return false;
    }

    private IEnumerator SetDelaySpawning(Vector3 spawnPosition)
    {
        float amountDelayBeforeSpawning = 1f;

        yield return new WaitForSeconds(amountDelayBeforeSpawning);
        _gameObject = Instantiate(_prefabGameObject, spawnPosition, Quaternion.identity);
    }
}