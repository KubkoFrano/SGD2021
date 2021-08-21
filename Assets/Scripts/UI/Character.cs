using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] Sprite baseSprite;
    [SerializeField] Sprite highlightedSprite;

    bool isOccupied = false;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = baseSprite;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void SetOccupation(bool value)
    {
        if (value)
        {
            isOccupied = true;
            image.sprite = highlightedSprite;
        }
        else
        {
            isOccupied = false;
            image.sprite = baseSprite;
        }
    }
}