using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField]
    private float bounceAmount = 35;

    private GameObject player;
    private Rigidbody rbPlayer;
    private bool bounce = false;
    private GameObject soundManagerObject;
    private SoundManager soundManager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManagerObject = GameObject.FindWithTag("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bounce)
        {
            soundManager.audioSources[10].Play();
            rbPlayer.AddForce(0, bounceAmount, 0, ForceMode.Impulse);
            bounce = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.root.gameObject == player)
        {
            bounce = true;
        }
    }
}
