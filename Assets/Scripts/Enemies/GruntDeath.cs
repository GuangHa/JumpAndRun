using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodSplash;

    private GameObject soundManagerObject;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManagerObject = GameObject.FindWithTag("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    // Destroy this GameObject OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            soundManager.audioSources[6].Play();
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
