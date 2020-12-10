using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System;
using System.Linq;

public class PlayerAgent : Agent
{
    public GameObject spawnPoint;
    public GameController arena;
    //public Tilemap tileMap;
    public BombController bomb;

    public int points = 0;

    [HideInInspector]
    public List<BombController> bombs;

    const int k_NoAction = 0;  // do nothing!
    const int k_Up = 1;
    const int k_Down = 2;
    const int k_Left = 3;
    const int k_Right = 4;
    const int k_Bomb = 5;

    private void Awake()
    {

    }

    void Start()
    {
        bombs = new List<BombController>();
    }

    private void FixedUpdate()
    {
        if (arena.GetComponentsInChildren<SoftBlock>().Length <= 0)
        {
            //AddReward(10);
            Debug.Log("cleared!");
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position = arena.tileMap.LocalToCell(spawnPoint.transform.position) + new Vector3(0.5f, 0.5f, 0);
        points = 0;
        bombs.Clear();
        arena.ArenaReset();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);

        List<Transform> softBlockTransforms = arena.GetComponentsInChildren<SoftBlock>().ToList().Select(x => x.transform).ToList();
        sensor.AddObservation(ClosestTargetDir(softBlockTransforms));

        List<Transform> bombGameTransforms = arena.GetComponentsInChildren<BombController>().ToList().Select(x => x.transform).ToList();
        sensor.AddObservation(ClosestTargetDir(bombGameTransforms));

        List<Transform> explosionTransforms = arena.GetComponentsInChildren<ExplosionController>().ToList().Select(x => x.transform).ToList();
        sensor.AddObservation(ClosestTargetDir(explosionTransforms));

    }

    public Vector3 ClosestTargetDir(List<Transform> targets)
    {
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        Vector3 closetTargetDir = new Vector3(0f,0f,0f);
        foreach (Transform potentialTarget in targets)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closetTargetDir = directionToTarget;
            }
        }
        return closetTargetDir;
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var action = actionBuffers.DiscreteActions[0];

        switch (action)
        {
            case k_NoAction:
                // do nothing
                break;
            case k_Right:
                Move(new Vector3(1, 0, 0));
                Tick();
                break;
            case k_Left:
                Move(new Vector3(-1, 0, 0));
                Tick();
                break;
            case k_Up:
                Move(new Vector3(0, 1, 0));
                Tick();
                break;
            case k_Down:
                Move(new Vector3(0, -1, 0));
                Tick();
                break;
            case k_Bomb:
                PlaceBomb();
                Tick();
                break;
            default:
                throw new ArgumentException("Invalid action value");
        }

        AddReward(-1f / MaxStep);
    }

    private void Tick()
    {
        foreach (var b in arena.GetComponentsInChildren<BombController>())
        {
            b.Tick();
        }

        foreach (var b in arena.GetComponentsInChildren<ExplosionController>())
        {
            b.Tick();
        }
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = k_NoAction;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            discreteActionsOut[0] = k_Left;
            
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            discreteActionsOut[0] = k_Right;            
        }


        if (Keyboard.current.upArrowKey.isPressed)
        {
            discreteActionsOut[0] = k_Up;
            
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            discreteActionsOut[0] = k_Down;
        }

        // place a bomb
        if (Keyboard.current.bKey.isPressed)
        {
            discreteActionsOut[0] = k_Bomb;
        }
    }

    void Move(Vector3 dir)
    {
        Vector3 gridPos = arena.tileMap.LocalToCell(transform.position + dir);
        Block[] blocks = arena.GetComponentsInChildren<Block>();

        foreach(var b in blocks)
        {
            if (b.GetComponent<BoxCollider2D>().bounds.Contains(gridPos + new Vector3(0.5f, 0.5f))){
                return;
            }
        }        

        transform.position = gridPos + new Vector3(0.5f, 0.5f, 0);
    }

    void PlaceBomb()
    {
        if (bombs.Count < 1)
        {
            BombController newBomb = Instantiate(bomb, transform.position, Quaternion.identity);
            newBomb.owner = this;
            newBomb.transform.parent = arena.transform.Find("Grid").transform;
            bombs.Add(newBomb);
        }
    }
}
