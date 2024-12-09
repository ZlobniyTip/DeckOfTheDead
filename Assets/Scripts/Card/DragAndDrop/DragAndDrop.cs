using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _cardObject;
    [SerializeField] private ParticleSystem _prefabSpawnPlaceEffect;
    [SerializeField] private ParticleSystem _prefabSpawnEffect;
    [SerializeField] private GameObject attackRadiusVisual;

    private GameObject _currentAttackRadiusVisual;
    private RectTransform _rectTransform;
    private Vector3 _originalPosition;
    private ParticleSystem _spawnPlaceEffect;
    private float _distanceFromRoad = 0.5f;
    private CardView _cardView;
    private UnitSpawner _unitSpawner;
    private Deck _deck;

    private bool _isSpawnPossible;

    private void Awake()
    {
        _unitSpawner = GetComponentInParent<UnitSpawner>();
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

            ShowAttackRadius(spawnPosition);
            _isSpawnPossible = true;
        }
        else
        {
            _spawnPlaceEffect = null;
            _currentAttackRadiusVisual = null;
            _isSpawnPossible = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;

        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            if (!_isSpawnPossible)
            {
                if (_spawnPlaceEffect == null)
                {
                    _spawnPlaceEffect = Instantiate(_prefabSpawnPlaceEffect, spawnPosition, Quaternion.identity);
                }

                if (_currentAttackRadiusVisual == null)
                {
                    ShowAttackRadius(spawnPosition);
                }

                _isSpawnPossible = true;
                _cardObject.gameObject.SetActive(false);
            }
            else
            {
                if (_spawnPlaceEffect != null)
                    _spawnPlaceEffect.transform.position = spawnPosition;

                if (_currentAttackRadiusVisual != null)
                    _currentAttackRadiusVisual.transform.position = spawnPosition;
            }
        }
        else
        {
            if (_isSpawnPossible)
            {
                if (_spawnPlaceEffect != null)
                {
                    Destroy(_spawnPlaceEffect.gameObject);
                    _spawnPlaceEffect = null;
                }

                if (_currentAttackRadiusVisual != null)
                {
                    Destroy(_currentAttackRadiusVisual.gameObject);
                    _currentAttackRadiusVisual = null;
                }

                _cardObject.gameObject.SetActive(true);
                _isSpawnPossible = false;
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


            _unitSpawner.Spawn(spawnPosition, _cardView.Card.PrefabUnit);
            _spawnPlaceEffect.gameObject.SetActive(false);
            _currentAttackRadiusVisual.gameObject.SetActive(false);

            _deck.RemoveCard(_cardView);
            _deck.TakeAwayPlayerEnergy(_cardView.Card.Energy);
            Destroy(gameObject);
        }
        else
        {
            transform.position = _originalPosition;
        }
    }

    private bool FindSpawnLocation(out Vector3 spawnPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (var hit in hits)
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

    private void ShowAttackRadius(Vector3 position)
    {
        _currentAttackRadiusVisual = Instantiate(attackRadiusVisual, position, Quaternion.identity);
        Vector3 newScale = new Vector3(
         _cardView.Card.UnitConfig.Weapon.AttackDistance * 2,
         _currentAttackRadiusVisual.transform.localScale.y,
         _cardView.Card.UnitConfig.Weapon.AttackDistance * 2);

        _currentAttackRadiusVisual.transform.localScale = newScale;
    }
}