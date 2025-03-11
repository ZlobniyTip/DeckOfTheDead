using System.Collections.Generic;
using Card;
using Deck;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class DragAndDropCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private readonly RaycastHit[] Hits = new RaycastHit[10];

        [SerializeField] private GameObject _cardObject;
        [SerializeField] private AudioSource _soundCard;
        [SerializeField] private AudioSource _spawnSound;
        [SerializeField] private Image _weaponIndicator;
        [SerializeField] private ParticleSystem _prefabSpawnPlaceEffect;
        [SerializeField] private GameObject _attackRadiusVisual;

        private GameObject _currentAttackRadiusVisual;
        private ParticleSystem _spawnPlaceEffect;
        private RectTransform _rectTransform;
        private Vector3 _originalPosition;
        private bool _isSpawnPossible;

        private CardView _cardView;
        private PlayerDeck _deck;
        private UnitSpawner _unitSpawner;

        private void Awake()
        {
            _deck = GetComponentInParent<PlayerDeck>();
            _unitSpawner = GetComponentInParent<UnitSpawner>();
            _rectTransform = GetComponent<RectTransform>();
            _cardView = GetComponent<CardView>();

            if (_weaponIndicator != null)
                _weaponIndicator.gameObject.SetActive(false);
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
                    if (_cardView.Card is CardDataUnit)
                    {
                        CreateUnitSpawnVisuals(spawnPosition);
                        _cardObject.SetActive(false);
                    }
                    else if (_cardView.Card is CardDataWeapon)
                    {
                        if (_weaponIndicator != null)
                            _weaponIndicator.gameObject.SetActive(true);
                    }
                    _isSpawnPossible = true;
                }
                else
                {
                    if (_spawnPlaceEffect != null) _spawnPlaceEffect.transform.position = spawnPosition;
                    if (_currentAttackRadiusVisual != null) _currentAttackRadiusVisual.transform.position = spawnPosition;
                }
            }
            else if (_isSpawnPossible)
            {
                CleanupVisuals();
                if (_cardView.Card is CardDataUnit)
                    _cardObject.SetActive(true);

                _isSpawnPossible = false;
            }
        }

        private void PerformSpawn()
        {
            if (_cardView.Card is CardDataUnit unitCard)
            {
                Vector3 spawnPosition = _spawnPlaceEffect.transform.position;
                _unitSpawner.Spawn(spawnPosition, unitCard.PrefabUnit, _cardView);
            }
            else if (_cardView.Card is CardDataWeapon weaponCard)
            {
                _deck.Character.CharacterShooting.UseTemporaryWeapons(weaponCard.PrefabWeapon, _cardView);
                _deck.Character.CharacterShooting.StartWeaponTimer(weaponCard.TimeAction);
                _deck.Character.CharacterShooting.PlayWeaponSpawnEffect();
                _deck.Character.CharacterShooting.PlaySoundEffect();
                _spawnSound.Play();
            }

            _deck.RemoveCard(_cardView);
            _deck.TakeAwayPlayerEnergy(_cardView.Card.Energy);

            Destroy(gameObject);
        }

        private void ResetPosition()
        {
            transform.position = _originalPosition;
            if (_weaponIndicator != null)
                _weaponIndicator.gameObject.SetActive(false);
        }

        private void CreateUnitSpawnVisuals(Vector3 position)
        {
            _spawnPlaceEffect = Instantiate(_prefabSpawnPlaceEffect, position, Quaternion.identity);
            _currentAttackRadiusVisual = Instantiate(_attackRadiusVisual, position, Quaternion.identity);
            UpdateAttackRadiusVisual();
        }

        private void UpdateAttackRadiusVisual()
        {
            if (_currentAttackRadiusVisual == null) return;

            float attackDistance = (_cardView.Card as CardDataUnit).UnitConfig.Weapon.AttackRange;
            _currentAttackRadiusVisual.transform.localScale = new Vector3(attackDistance * 2, _currentAttackRadiusVisual.transform.localScale.y, attackDistance * 2);
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

            if (_weaponIndicator != null)
                _weaponIndicator.gameObject.SetActive(false);
        }

        private bool FindSpawnLocation(out Vector3 spawnPosition)
        {
            if (IsPointerOverUI())
            {
                spawnPosition = _originalPosition;
                return false;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int hitCount = Physics.RaycastNonAlloc(ray, Hits);

            for (int i = 0; i < hitCount; i++)
            {
                if (Hits[i].collider.GetComponent<Arm>() != null)
                {
                    spawnPosition = _originalPosition;
                    return false;
                }
            }

            for (int i = 0; i < hitCount; i++)
            {
                if (Hits[i].collider.GetComponent<Road>() != null)
                {
                    spawnPosition = Hits[i].point;
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
                position = Input.mousePosition,
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