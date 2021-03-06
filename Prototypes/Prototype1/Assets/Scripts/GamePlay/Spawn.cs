﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //Speed at which camera tracks player
    public int LerpSpeed = 5;

    //Player prefab to be spawned, UI panel for startinh player
    public GameObject Player,startPanel;

    //Controls camera following player or not
    private bool Lerping = false;

    //Positions of camera and player
    private Transform camTransform, playerTransform;

    //Current player in scene, outofbounds object in scene, end point in scene, 
    public GameObject SpawnedPlayer,EndPoint;

    //Time from sim start
    public float Timer = 0.00f;
    public static float orthosize;

    private void Start()
    {
        camTransform = Camera.main.transform;
    }

    public void SpawnPlayer()
    {
        SpawnedPlayer = Instantiate(Player, transform.position, transform.rotation);

        playerTransform = SpawnedPlayer.transform;

        startPanel.SetActive(false);
        Lerping = true;

        EndPoint.SetActive(true);
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if (!Lerping)
        {
            if (Camera.main.orthographicSize < orthosize)
            {
                Camera.main.orthographicSize += 0.1f;
            }
        }

        if (SpawnedPlayer == null)
        {
            camTransform.position = Vector3.Lerp(camTransform.position, new Vector3(0, 0, camTransform.position.z), Time.deltaTime * LerpSpeed);
            if(Vector3.Distance(camTransform.position, new Vector3(0, 0, camTransform.position.z)) < 1)
            {
                Lerping = false;
            }
            return;
        }

        //Move camera between player and back to default view point
        if (Lerping)
        {
            Vector3 adjustedPoisitionForDistance = new Vector3(playerTransform.position.x, Mathf.Clamp(playerTransform.position.y,-8,16), camTransform.position.z);
            camTransform.position = Vector3.Lerp(camTransform.position, adjustedPoisitionForDistance, Time.deltaTime * LerpSpeed);

            if (Camera.main.orthographicSize > 8)
            {
                Camera.main.orthographicSize -= 0.1f;
            }

        }
    }

}
