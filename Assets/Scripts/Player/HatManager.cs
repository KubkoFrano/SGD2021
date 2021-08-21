using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    [SerializeField] GameObject[] hats;

    public void EnableHat(int index)
    {
        hats[index].SetActive(true);
    }
}