using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private int tickCount = 0;
    private int maxTicks = 5;
    public PlayerAgent owner;

    public ExplosionController explosion;

    public void Tick()
    {
        tickCount++;
        if (tickCount >= maxTicks)
        {
            ExplosionController explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
            explosionInstance.owner = owner;
            explosionInstance.transform.parent = owner.arena.transform.Find("Grid").transform;
            owner.bombs.Remove(this);
            Destroy(this.gameObject);
        }

    }
}
