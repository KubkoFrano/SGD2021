using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomProps : MonoBehaviour
{
    public Transform[] RandomDecorationPos = new Transform[6];       //position array
    public GameObject[] RandomDecorations = new GameObject[1];       //array of prop GameObjects

    void Start()
    {
        int ammountOfProps = Random.Range(0, RandomDecorationPos.Length + 1);
        int []assignedPositions = new int[ammountOfProps];
        int[] usedIndexes = new int[6];

        for (int i = 0; i < ammountOfProps; i++) 
        {
            int TransformIndex = 0;
            bool repeat = true;

            while (repeat == true)
            {
                TransformIndex = Random.Range(0, RandomDecorationPos.Length);           //choose random index for position array

                foreach (int p in usedIndexes)                              //check if that transform is already taken or not . . . if it is -> repeat
                {
                    if (assignedPositions[p] == TransformIndex)
                    {
                        repeat = true;
                    }
                    else repeat = false;
                }
            }

            GameObject prop = Instantiate(RandomDecorations[Random.Range(0, RandomDecorations.Length)]);

            
            prop.transform.Rotate(0, 0, Random.value * 360);
            prop.transform.localScale += new Vector3((Random.value - .5f) * 8, (Random.value - .5f) * 8, (Random.value - .5f) * 8);

            prop.transform.position = RandomDecorationPos[TransformIndex].position;

            prop.transform.SetParent(RandomDecorationPos[TransformIndex]);
            
        }
    }

    
}
