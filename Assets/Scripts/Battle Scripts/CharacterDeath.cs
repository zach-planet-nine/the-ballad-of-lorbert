using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        isDead = true;
        gameObject.transform.RotateAround(gameObject.transform.position, new Vector3(0, 0, 1), 90.0f);
    }

    public void Revive()
    {
        isDead = false;
        gameObject.transform.RotateAround(gameObject.transform.position, new Vector3(0, 0, 1), -90.0f);
    }
}
