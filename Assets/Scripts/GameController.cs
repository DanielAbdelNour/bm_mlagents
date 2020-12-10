using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{

    public Grid tileMap;

    public BoxCollider2D blockBounds;

    public SoftBlock softBlock;

    public GameObject softBlockParent;


    //public SoftBlock[] currentSoftBlocks;
    //public Block[] currentBlocks;
    //public BombController[] currentBombs;
    //public ExplosionController[] currentExplosions;

    private void Start()
    {
        //currentBlocks = GetComponentsInChildren<Block>();
        //currentSoftBlocks = GetComponentsInChildren<SoftBlock>();
        //currentBombs = GetComponentsInChildren<BombController>();
        //currentExplosions = GetComponentsInChildren<ExplosionController>();
        //Time.timeScale = 0.1f;
    }

    private void FixedUpdate()
    {
        //Debug.Log(Academy.Instance.TotalStepCount);
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void ArenaReset()
    {
        foreach (var b in GetComponentsInChildren<ExplosionController>())
        {
            Destroy(b.gameObject);
        }
        foreach (var b in GetComponentsInChildren<SoftBlock>())
        {
            Destroy(b.gameObject);
        }
        foreach (var b in GetComponentsInChildren<BombController>())
        {
            Destroy(b.gameObject);
        }
        

        int nBlocks = Random.Range(5, 10);

        for(int i = 0; i < nBlocks; i++)
        {
            Vector3 randPoint = RandomPointInBounds(blockBounds.bounds);
            Vector3 gridPos = tileMap.LocalToCell(randPoint) + new Vector3(0.5f, 0.5f, 0);
            var newBlock = Instantiate(softBlock, gridPos, Quaternion.identity);
            newBlock.transform.parent = softBlockParent.transform;
        }

    }

}
