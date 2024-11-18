using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _dragObject;
    [SerializeField] private GameObject _cardView;
    [SerializeField] private ParticleSystem _prefabSpawnPlaceEffect;
    [SerializeField] private ParticleSystem _prefabSpawnEffect;

    private RectTransform _rectTransform;
    private Vector3 _originalPosition;

    private GameObject _gameObject;
    private ParticleSystem _spawnPlaceEffect;

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
        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            ParticleSystem spawnEffect = Instantiate(_prefabSpawnEffect, new Vector3(spawnPosition.x, spawnPosition.y + 0.5f, spawnPosition.z), Quaternion.identity);
            StartCoroutine(SetDelaySpawning(spawnPosition));

            _spawnPlaceEffect.gameObject.SetActive(false);
        }
        else
        {
            if (_gameObject != null)
            {
                Destroy(_gameObject);
            }

            transform.position = _originalPosition;
            _cardView.gameObject.SetActive(true);
        }
    }

    private bool FindSpawnLocation(out Vector3 spawnPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            spawnPosition = hit.point;
            return true;
        }

        spawnPosition = _originalPosition;
        return false;
    }

    private IEnumerator SetDelaySpawning(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(1f);
        _gameObject = Instantiate(_dragObject, spawnPosition, Quaternion.identity);
    }
}