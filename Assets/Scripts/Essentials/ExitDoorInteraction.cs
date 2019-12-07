using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorInteraction : MonoBehaviour
{
    private GameObject player;
    private GameObject soundManagerObject;
    private GameObject exitInteractHUDObject;
    private SoundManager soundManager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        exitInteractHUDObject = GameObject.FindGameObjectWithTag("ExitInteraction");
    }

    private void Start()
    {
        soundManagerObject = GameObject.FindWithTag("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
        exitInteractHUDObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player)
        {
            exitInteractHUDObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player && Input.GetKeyDown(KeyCode.E))
        {
            // play sound fbi_open_up
            soundManager.audioSources[9].Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        exitInteractHUDObject.SetActive(false);
    }
}
