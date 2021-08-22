using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer mesh;

    [SerializeField] Material[] materials;

    [SerializeField] float[] riseHeights;

    [SerializeField] float riseSpeed;

    HatManager hatManager;

    private void Awake()
    {
        hatManager = GetComponentInChildren<HatManager>();
    }

    public void SetPlayer(int index)
    {
        mesh.material = materials[index];
        hatManager.EnableHat(index);
    }

    public void RiseHex(int height)
    {
        StartCoroutine(ManageRise(height));
    }

    IEnumerator ManageRise(int height)
    {
        while (transform.parent.localPosition.y < riseHeights[height])
        {
            transform.parent.localPosition += Vector3.up * Time.deltaTime * riseSpeed;
            yield return new WaitForEndOfFrame();
        }

        transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, riseHeights[height], transform.parent.localPosition.z);

        if (height == 0)
            App.winScreen.ShowButton();
    }
}