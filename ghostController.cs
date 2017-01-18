using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ghostController : MonoBehaviour {
    //packmanController p;
    // Use this for initialization
    private GameObject packMan;
    public AudioClip death;
    AudioSource audio;
    NavMeshAgent Ghost;
    Animator anim;
    Vector3 escape;
    void Start () {
        Ghost = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        Ghost.speed = 30;

        if (packMan == null)
        {
            packMan = GameObject.FindGameObjectWithTag("Player");
        }
        
        
    }
	
	// Update is called once per frame
	void Update () {
                Ghost.destination = packMan.transform.position;
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                audio.PlayOneShot(death);
        }
        if (other.gameObject.CompareTag("ghostBusters"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("ghostBuster"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("timeBonus"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("lifeBonus"))
        {
            other.gameObject.SetActive(false);
        }
    }

   
}
