using Card;
using Deck;
using Other;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public class DragAndDropCardUnit : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private GameObject _cardObject;
        [SerializeField] private ParticleSystem _prefabSpawnPlaceEffect;
        [SerializeField] private GameObject attackRadiusVisual;

        [SerializeField] private AudioSource _soundCard;

        private GameObject _currentAttackRadiusVisual;
        private RectTransform _rectTransform;
        private Vector3 _originalPosition;
        private ParticleSystem _spawnPlaceEffect;
        private bool _isSpawnPossible;

        private CardView _cardView;
        private UnitSpawner _unitSpawner;
        private PlayerDeck _deck;

        private void Awake()
        {
            _unitSpawner = GetComponentInParent<UnitSpawner>();
            _deck = GetComponentInParent<PlayerDeck>();
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

            _unitSpawner.Spawn(spawnPosition, (_cardView.Card as CardDataUnit).PrefabUnit, _cardView);
            _deck.RemoveCard(_cardView);
            _deck.TakeAwayPlayerEnergy(_cardView.Card.Energy);

            Destroy(gameObject);
        }

        private void ResetPosition() => transform.position = _originalPosition;

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
            if (_currentAttackRadiusVisual == null) 
                return;

            float attackDistance = (_cardView.Card as CardDataUnit).UnitConfig.Weapon.AttackRange;
            Vector3 newScale = new Vector3(attackDistance * 2, _currentAttackRadiusVisual.transform.localScale.y, attackDistance * 2);
            _currentAttackRadiusVisual.transform.localScale = newScale;
        }

        private bool FindSpawnLocation(out Vector3 spawnPosition)
        {
            if (IsPointerOverUI())
            {
                spawnPosition = _originalPosition;
                return false;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
            {
                if (hit.collider.GetComponent<Arm>() != null)
                {
                    spawnPosition = _originalPosition;
                    return false;
                }
            }

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

        private bool IsPointerOverUI()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                if (result.gameObject.GetComponent<Arm>() != null)
                    return true;
            }

            return false;
        }
    }
}