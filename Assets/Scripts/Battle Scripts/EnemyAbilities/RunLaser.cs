using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunLaser : MonoBehaviour
{
    public bool shouldFireBeam;

    LineRenderer line;
    GameObject target;
    private int targetDamage;
    private float beamTimer;
    private float beamDuration = 0.5f;
    private Action<bool> callback;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        //line.enabled = true;

    }

    public void SetTargetWithCallback(GameObject target, int damage, Action<bool> callback)
    {
        FireLaserAt(target, damage);
        this.callback = callback;
        beamDuration = 0.3f;
    }

    public void FireLaserAt(GameObject target, int damage)
    {
        shouldFireBeam = true;
        this.target = target;
        targetDamage = damage;
        Debug.Log("Should fire beam at: " + target);
        Debug.Log(transform.position);
        Debug.Log(target.transform.position);
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;
        Debug.Log("Line is now enabled");
        while(shouldFireBeam)
        {
            //Ray ray = new Ray(transform.position, transform.forward);
           
            line.SetPosition(0, transform.position);
            line.SetPosition(1, target.transform.position);
            //line.SetPosition(1, ray.GetPoint(100));
            yield return null;
        }
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && BattleManager.manager.CheckIfEntityIsDead(target) && gameObject != null)
        {
            StopAllCoroutines();
            if(callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
            return;
        }
        if(shouldFireBeam)
        {
            beamTimer += Time.deltaTime;
            StartCoroutine("FireLaser");
            if(beamTimer > beamDuration)
            {
                target.GetComponent<TakeDamage>().DisplayDamage(targetDamage, target.transform.position);
                beamTimer = 0;
                shouldFireBeam = false;
            }
        }
    }


}
