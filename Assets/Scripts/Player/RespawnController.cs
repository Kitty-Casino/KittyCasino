using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public float spawnPointX;
    public float spawnPointY;
    public float spawnPointZ;

    void Start()
    {
        if (PlayerPrefs.GetFloat("SpawnPointX") != 0f)
        {
            Vector3 spawnPosition = new Vector3(PlayerPrefs.GetFloat("SpawnPointX"), PlayerPrefs.GetFloat("SpawnPointY"), PlayerPrefs.GetFloat("SpawnPointZ"));
            Debug.Log("spawn point: " + spawnPosition);
            transform.position = spawnPosition;
        }
    }
}
