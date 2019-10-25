using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if(isDead)
        {
            gameObject.SetActive(false);
        }
    }

    public void Die()
    {
        Debug.Log("Should die");
        isDead = true;
        gameObject.SetActive(false);
    }
}
