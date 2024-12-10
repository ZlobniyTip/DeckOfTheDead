using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _cardObject;
    [SerializeField] private ParticleSystem _prefabSpawnPlaceEffect;
    [SerializeField] private ParticleSystem _prefabSpawnEffect;
    [SerializeField] private GameObject attackRadiusVisual;

    [SerializeField] private AudioSource _soundCard;

    private GameObject _currentAttackRadiusVisual;
    private RectTransform _rectTransform;
    private Vector3 _originalPosition;
    private ParticleSystem _spawnPlaceEffect;
    private bool _isSpawnPossible;

    private CardView _cardView;
    private UnitSpawner _unitSpawner;
    private Deck _deck;

    private void Awake()
    {
        _unitSpawner = GetComponentInParent<UnitSpawner>();
        _deck = GetComponentInParent<Deck>();
        _rectTransform = GetComponent<RectTransform>();
        _cardView = GetComponent<CardView>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _soundCard.Play();

        _originalPosition = transform.position;
        TryUpdateSpawnVisuals();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
        TryUpdateSpawnVisuals();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isSpawnPossible)
            PerformSpawn();
        else
            ResetPosition();

        CleanupVisuals();
    }

    private void TryUpdateSpawnVisuals()
    {
        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            if (!_isSpawnPossible)
            {
                CreateSpawnVisuals(spawnPosition);
                _cardObject.SetActive(false);
                _isSpawnPossible = true;
            }
            else
            {
                UpdateSpawnVisuals(spawnPosition);
            }
        }
        else
        {
            if (_isSpawnPossible)
            {
                CleanupVisuals();
                _cardObject.SetActive(true);
                _isSpawnPossible = false;
            }
        }
    }

    private void PerformSpawn()
    {
        Vector3 spawnPosition = _spawnPlaceEffect.transform.position;
        Instantiate(_prefabSpawnEffect, spawnPosition + Vector3.up * 0.5f, Quaternion.identity);

        _unitSpawner.Spawn(spawnPosition, _cardView.Card.PrefabUnit);
        _deck.RemoveCard(_cardView);
        _deck.TakeAwayPlayerEnergy(_cardView.Card.Energy);

        Destroy(gameObject);
    }

    private void ResetPosition()
    {
        transform.position = _originalPosition;
    }

    private void CreateSpawnVisuals(Vector3 position)
    {
        _spawnPlaceEffect = Instantiate(_prefabSpawnPlaceEffect, position, Quaternion.identity);
        _currentAttackRadiusVisual = Instantiate(attackRadiusVisual, position, Quaternion.identity);
        UpdateAttackRadiusVisual();
    }

    private void UpdateSpawnVisuals(Vector3 position)
    {
        if (_spawnPlaceEffect != null) 
            _spawnPlaceEffect.transform.position = position;
        if (_currentAttackRadiusVisual != null) 
            _currentAttackRadiusVisual.transform.position = position;
    }

    private void CleanupVisuals()
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
    }

    private void UpdateAttackRadiusVisual()
    {
        if (_currentAttackRadiusVisual == null) return;

        float attackDistance = _cardView.Card.UnitConfig.Weapon.AttackDistance;
        Vector3 newScale = new Vector3(attackDistance * 2, _currentAttackRadiusVisual.transform.localScale.y, attackDistance * 2);
        _currentAttackRadiusVisual.transform.localScale = newScale;
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
}
