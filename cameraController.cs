using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cameraController : MonoBehaviour {

    public GameObject packman;
    
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        offset = transform.position - packman.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = packman.transform.position + offset;
    }
    

}

    
