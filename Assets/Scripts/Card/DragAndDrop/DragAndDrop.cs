using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _cardObject;
    [SerializeField] private ParticleSystem _prefabSpawnPlaceEffect;
    [SerializeField] private ParticleSystem _prefabSpawnEffect;

    private RectTransform _rectTransform;
    private Vector3 _originalPosition;

    private Unit _unit;
    private ParticleSystem _spawnPlaceEffect;
    private float _distanceFromRoad = 0.5f;
    private CardView _cardView;
    private Deck _deck;

    private void Awake()
    {
        _deck = GetComponentInParent<Deck>();
        _rectTransform = GetComponent<RectTransform>();
        _cardView = GetComponent<CardView>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPosition = transform.position;

        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            _cardObject.gameObject.SetActive(false);
            _spawnPlaceEffect = Instantiate(_prefabSpawnPlaceEffect, spawnPosition, Quaternion.identity);
        }
        else
        {
            _unit = null;
            _spawnPlaceEffect = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;

        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            if (_spawnPlaceEffect != null)
            {
                _spawnPlaceEffect.transform.position = spawnPosition;
            }
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
            _cardObject.gameObject.SetActive(true);

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
        _unit = Instantiate(_cardView.Card.PrefabUnit, spawnPosition, Quaternion.identity);
        _unit.SetCharacter(_deck.Character);

        Destroy(gameObject);
    }
}