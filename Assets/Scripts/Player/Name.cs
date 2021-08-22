using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Name : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] Sprite[] sprites;

    public void SetName(int index)
    {
        image.sprite = sprites[index];
        image.enabled = true;
    }

    public void DeleteName()
    {
        image.enabled = false;
    }
}