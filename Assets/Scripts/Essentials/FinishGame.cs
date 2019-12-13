using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FinishGame : MonoBehaviour
{
    [SerializeField]
    private GameObject videoPlayerObject;
    [SerializeField]
    private GameObject hudObject;

    private GameObject player;
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += LoadScene;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && videoPlayer.isPlaying)
        {
            LoadScene(videoPlayer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.root.gameObject == player)
        {
            hudObject.SetActive(false);
            videoPlayer.Play();
        }
    }

    private void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("GameEnd");
    }
}
