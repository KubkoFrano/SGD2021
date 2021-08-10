using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{

    public float lifeTime = 7;

    BehaviourHexagon behaviour;

    void Start()
    {
        behaviour = GetComponentInParent<BehaviourHexagon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        behaviour.MoveStateChange();
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            GetComponentInParent<PowerUpSpawner>().RemovePowerUp(this.gameObject);
            Destroy(this.gameObject);
        }
    }

}
