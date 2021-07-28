using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] Character[] characters;

    private void Awake()
    {
        App.characterManager = this;
    }

    public void ResetCharacters()
    {
        foreach (Character character in characters)
        {
            character.SetOccupation(false);
        }
    }

    public int AssignCharacter()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!characters[i].IsOccupied())
            {
                characters[i].SetOccupation(true);
                return i;
            }
        }

        return -1;
    }

    public void AssignSpecificCharacter(int index)
    {
        characters[index].SetOccupation(true);
    }

    public void FreeCharacter(int index)
    {
        characters[index].SetOccupation(false);
    }
}