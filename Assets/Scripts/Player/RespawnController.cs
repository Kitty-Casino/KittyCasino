using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RespawnController : MonoBehaviour
{
    public float spawnPointX;
    public float spawnPointY;
    public float spawnPointZ;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (PlayerPrefs.GetFloat("SpawnPointX") != 0f)
        {
            Vector3 spawnPosition = new Vector3(PlayerPrefs.GetFloat("SpawnPointX"), PlayerPrefs.GetFloat("SpawnPointY"), PlayerPrefs.GetFloat("SpawnPointZ"));
            Debug.Log("spawn point: " + spawnPosition);
            //transform.position = spawnPosition;
            if (agent.isOnNavMesh)
                agent.Warp(spawnPosition);
        }
    }
}
