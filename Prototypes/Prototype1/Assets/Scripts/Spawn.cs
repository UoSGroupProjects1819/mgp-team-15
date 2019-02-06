using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //Speed at which camera tracks player
    public int LerpSpeed = 5;

    //Player prefab to be spawned
    public GameObject Player,startPanel;

    private bool Lerping = false;
    private Transform camTransform, playerTransform;
    public GameObject SpawnedPlayer;

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
    }

    private void Update()
    {
        if (SpawnedPlayer == null)
        {
            camTransform.position = Vector3.Lerp(camTransform.position, new Vector3(0, 0, camTransform.position.z), Time.deltaTime * LerpSpeed);
            return;
        }

        //Move camera between player and back to default view point
        if (Lerping)
        {
            Vector3 adjustedPoisitionForDistance = new Vector3(playerTransform.position.x, playerTransform.position.y, camTransform.position.z);
            camTransform.position = Vector3.Lerp(camTransform.position, adjustedPoisitionForDistance, Time.deltaTime * LerpSpeed);
        }
    }

}
