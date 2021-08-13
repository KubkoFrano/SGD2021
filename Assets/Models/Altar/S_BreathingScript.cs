using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BreathingScript : MonoBehaviour
{
    private Transform thisTransform = null;
    private float counter = 1;

    public float rotationSpeed = 0.025f;

    public float upDownHeight = 2400.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Get transform component
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float RandomX = Random.Range(0.0f, rotationSpeed);
        float RandomY = Random.Range(0.0f, rotationSpeed);
        float RandomZ = Random.Range(0.0f, rotationSpeed);
        thisTransform.Rotate(RandomX, RandomY, RandomZ);

        if (upDownHeight == 0)
        {
            return;
        }
        else
        {
            counter += 1;

            if (counter < (upDownHeight + 1.5f))
            {
                thisTransform.Translate((Vector3.up * Time.deltaTime) / 8, Space.World);

            }
            if (counter > (upDownHeight + 1.5f) && counter < ((upDownHeight * 2) + 1.5f))
            {
                thisTransform.Translate((Vector3.up * -Time.deltaTime) / 8, Space.World);

            }
            if (counter > ((upDownHeight * 2) + 1.5f))
            {
                counter = 0;
            }
        }
    }
}
