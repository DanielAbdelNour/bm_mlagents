using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerAgent2 : MonoBehaviour
{
    //public Tilemap tileMap;
    //public BombController bomb;

    //public int points = 0;

    //[HideInInspector]
    //public List<BombController> bombs;

    //void Start()
    //{
    //    bombs = new List<BombController>();
    //}

    //void Update()
    //{
    //    // move on the grid
    //    if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
    //    {
    //        Move(new Vector3(1, 0, 0));
    //    }

    //    if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
    //    {
    //        Move(new Vector3(-1, 0, 0));
    //    }

    //    if (Keyboard.current.upArrowKey.wasPressedThisFrame)
    //    {
    //        Move(new Vector3(0, 1, 0));
    //    }

    //    if (Keyboard.current.downArrowKey.wasPressedThisFrame)
    //    {
    //        Move(new Vector3(0, -1, 0));
    //    }

    //    // place a bomb
    //    if (Keyboard.current.bKey.wasPressedThisFrame && bombs.Count < 1)
    //    {
    //        BombController newBomb = Instantiate(bomb, transform.position, Quaternion.identity);
    //        //newBomb.owner = this;
    //        bombs.Add(newBomb);
    //        GameController.Instance.Step();
    //    }


    //}

    //void Move(Vector3 dir)
    //{
    //    Vector3 gridPos = tileMap.WorldToCell(transform.position + dir);
    //    SoftBlock[] softBlocks = FindObjectsOfType<SoftBlock>();
    //    foreach (var b in softBlocks)
    //    {
    //        if (b.GetComponent<BoxCollider2D>().bounds.Contains(gridPos + new Vector3(0.5f, 0.5f)))
    //        {
    //            GameController.Instance.Step();
    //            return;
    //        }
    //    }



    //    transform.position = gridPos + new Vector3(0.5f, 0.5f, 0);
    //    GameController.Instance.Step();
    //}



}
