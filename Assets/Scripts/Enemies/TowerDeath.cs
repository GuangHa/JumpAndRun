using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeath : MonoBehaviour
{
    private GameObject soundManagerObject;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManagerObject = GameObject.FindWithTag("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            soundManager.audioSources[7].Play();
            Destroy(this.gameObject);
        }
    }
}
