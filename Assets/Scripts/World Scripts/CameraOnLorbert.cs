using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnLorbert : MonoBehaviour
{
    public GameObject Lorbert;
    public static bool shouldFollowLorbert = true;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(Lorbert.transform.position.x, Lorbert.transform.position.y, Camera.main.transform.position.z);
    }

    private void FixedUpdate()
    {
        if(shouldFollowLorbert)
        {
            Camera.main.transform.position = new Vector3(Lorbert.transform.position.x, Lorbert.transform.position.y, Camera.main.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
