﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event System.Action OnCubeSpawned = delegate { };

    private CubeSpawner[] spawners;
    private CubeSpawner currentSpawner;
    private int spawnerIndex;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube();
            OnCubeSpawned();
        }
    }
}