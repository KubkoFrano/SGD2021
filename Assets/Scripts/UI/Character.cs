using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] Color32 baseColor;
    [SerializeField] Color32 highlightColor;

    bool isOccupied = false;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = baseColor;
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
            image.color = highlightColor;
        }
        else
        {
            isOccupied = false;
            image.color = baseColor;
        }
    }
}