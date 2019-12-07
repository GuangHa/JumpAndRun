using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorInteraction : MonoBehaviour
{
    private GameObject player;
    private GameObject soundManagerObject;
    private SoundManager soundManager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        soundManagerObject = GameObject.FindWithTag("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player && Input.GetKeyDown(KeyCode.E))
        {
            // play sound fbi_open_up
            soundManager.audioSources[9].Play();
        }
    }
}
