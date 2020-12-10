using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private int tickCount = 0;
    private int maxTicks = 2;
    public PlayerAgent owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Soft Block"))
        {
            owner.AddReward(1);
            owner.points++;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject == owner.gameObject)
        {
            owner.AddReward(-1);
            owner.EndEpisode();
        }
    }


    public void Tick()
    {
        tickCount ++;
        if (tickCount >= maxTicks)
        {
            Destroy(this.gameObject);
        }
    }

}
