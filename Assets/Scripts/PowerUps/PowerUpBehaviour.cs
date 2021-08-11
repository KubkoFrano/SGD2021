using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{

    public float lifeTime = 7;

    BehaviourHexagon[] behaviour;
    GameObject topParent;

    void Start()
    {
        topParent = transform.root.gameObject;
        behaviour = topParent.GetComponentsInChildren<BehaviourHexagon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(BehaviourHexagon i in behaviour) i.MoveStateChange();
        
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            if (!Physics.CheckSphere(this.transform.position, 12, LayerMask.GetMask("Player")))
            {
                GetComponentInParent<PowerUpSpawner>().RemovePowerUp(this.gameObject);
                Destroy(this.gameObject);
            }
            
        }
    }

}
