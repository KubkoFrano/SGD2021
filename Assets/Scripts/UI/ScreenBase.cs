using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenBase : MonoBehaviour
{
    [SerializeField] GameObject firstButton;

    public virtual void Show()
    {
        gameObject.SetActive(true);
        SetSelectedButton();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void SetSelectedButton()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (firstButton)
            EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
