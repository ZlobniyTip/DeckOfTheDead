using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaimMenuImage : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _image;

    private void Start()
    {
        GetMainMenuSprite();
    }

    private void GetMainMenuSprite()
    {
        int random = Random.RandomRange(0 , _sprites.Count + 1);

        _image.sprite = _sprites[random];
    }
}