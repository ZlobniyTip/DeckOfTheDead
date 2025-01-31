using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CutScenes : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _camers; 
    [SerializeField] private TMP_Text[] _texts; 
    [SerializeField] private Image _blackout; 
    [SerializeField] private float _fadeDuration = 1.5f; 

    public event Action EndCutScene;

    private void Awake()
    {
        for (int i = 0; i < _camers.Length; i++)
            _camers[i].gameObject.SetActive(false);

        for (int i = 0; i < _texts.Length; i++)
            _texts[i].gameObject.SetActive(false);

        _blackout.color = new Color(0, 0, 0, 1);
    }

    private void Start()
    {
        StartCoroutine(TurnOnCameras());
    }

    private IEnumerator TurnOnCameras()
    {
        yield return StartCoroutine(FadeOutBlackout());

        for (int i = 0; i < _camers.Length; i++)
        {
            _camers[i].gameObject.SetActive(true);

            if (i < _texts.Length)
                _texts[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(7f);

            _camers[i].gameObject.SetActive(false);

            if (i < _texts.Length)
                _texts[i].gameObject.SetActive(false);
        }

        //EndCutScene?.Invoke();

        yield return StartCoroutine(FadeInBlackout());
    }

    private IEnumerator FadeOutBlackout()
    {
        float elapsedTime = 0f;
        Color startColor = _blackout.color;
        Color endColor = new Color(0, 0, 0, 0); 

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _blackout.color = Color.Lerp(startColor, endColor, elapsedTime / _fadeDuration);
            yield return null; 
        }

        _blackout.color = endColor;
    }

    private IEnumerator FadeInBlackout()
    {
        float elapsedTime = 0f;
        Color startColor = _blackout.color;
        Color endColor = new Color(0, 0, 0, 1); 

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            _blackout.color = Color.Lerp(startColor, endColor, elapsedTime / _fadeDuration);
            yield return null; 
        }

        _blackout.color = endColor;
        EndCutScene?.Invoke();
    }
}