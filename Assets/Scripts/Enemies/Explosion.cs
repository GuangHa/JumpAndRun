using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BombFollow))]
[RequireComponent(typeof(BombStraight))]
public class Explosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explotion;
    [SerializeField]
    private GameObject schockWave;
    [SerializeField]
    private AudioClip sound;

    private BombFollow bf;
    private BombStraight bs;

    // Start is called before the first frame update
    void Start()
    {
        bf = GetComponent<BombFollow>();
        if (bf != null)
        {
            bf.BombExp += Explode;
        }

        bs = GetComponent<BombStraight>();
        if(bs != null)
        {
            bs.BombExp += Explode;
        }        
    }

    private void Explode()
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);
        explotion = Instantiate(explotion, transform.position, transform.rotation) as GameObject;
        schockWave = Instantiate(schockWave, transform.position, transform.rotation)as GameObject;
        Destroy(gameObject);
        Destroy(explotion, 1f);
        Destroy(schockWave, 1f);
    }

}
