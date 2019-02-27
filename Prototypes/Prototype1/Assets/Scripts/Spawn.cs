using System.Collections;
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
    public static float Timer = 0.00f;

    private void Start()
    {
        camTransform = Camera.main.transform;
    }

    public void SpawnPlayer()
    {
        Timer = 0.00f;
        SpawnedPlayer = Instantiate(Player, transform.position, transform.rotation);

        playerTransform = SpawnedPlayer.transform;

        startPanel.SetActive(false);
        Lerping = true;

        EndPoint.SetActive(true);
    }

    private void Update()
    {
        if (SpawnedPlayer == null)
        {
            camTransform.position = Vector3.Lerp(camTransform.position, new Vector3(0, 0, camTransform.position.z), Time.deltaTime * LerpSpeed);
            return;
        }

        Timer += Time.deltaTime;

        //Move camera between player and back to default view point
        if (Lerping)
        {
            Vector3 adjustedPoisitionForDistance = new Vector3(playerTransform.position.x, playerTransform.position.y, camTransform.position.z);
            camTransform.position = Vector3.Lerp(camTransform.position, adjustedPoisitionForDistance, Time.deltaTime * LerpSpeed);
        }
    }

}
