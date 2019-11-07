using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHomingMissile : MonoBehaviour
{

    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private float forwardAcceleration = 8.0f;
    private Rigidbody2D rbdy;
    private float angleTraversed;
    private float angleThreshold;
    private float angleDiffThreshold = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int missileDamage, Action<bool> callback)
    {
        Target = target;
        damage = missileDamage;
        this.callback = callback;
        rbdy = gameObject.GetComponent<Rigidbody2D>();
        rbdy.velocity = new Vector2(0, 1.5f);
        rbdy.angularVelocity = 180.0f;

        //float angleBetween = Vector2.Angle(gameObject.transform.position, target.transform.position);
        Vector2 dist = new Vector2(target.transform.position.x - gameObject.transform.position.x, target.transform.position.y - gameObject.transform.position.y);
        float angle = Mathf.Atan2(dist.y, dist.x);
        float angleDegrees = RadianToDegree(angle);
        if(angleDegrees > 0)
        {
            angleThreshold = angleDegrees - 70.0f;
        } else
        {
            float diff = 180.0f + angleDegrees;
            angleThreshold = 180.0f + diff - 70.0f;
        }
        
        Debug.Log("I am " + gameObject.name + " and my angle is: " + RadianToDegree(angle));
    }

    private float DegreeToRadian(float angle)
    {
        return Mathf.PI * angle / 180.0f;

    }

    private float RadianToDegree(float angle)
    {
        return angle / Mathf.PI * 180.0f;

    }

    private void UpdateVelocity()
    { 

        angleTraversed += rbdy.angularVelocity * Time.deltaTime;

        //Debug.Log("Angle traversed: " + angleTraversed);

        if (Mathf.Abs(angleTraversed - angleThreshold) < angleDiffThreshold)
        {
            rbdy.angularVelocity = 0;
        }

        if(rbdy.angularVelocity - 0.5f < 0.6f)
        {
            float fx = forwardAcceleration * Mathf.Sin(DegreeToRadian(-angleTraversed));
            float fy = forwardAcceleration * Mathf.Cos(DegreeToRadian(-angleTraversed));

            float vx = rbdy.velocity.x + (fx * Time.deltaTime);
            float vy = rbdy.velocity.y + (fy * Time.deltaTime);
            rbdy.velocity = new Vector2(vx, vy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null && BattleManager.manager.CheckIfEntityIsDead(Target))
        {
            if (callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
        }
        if (gameObject != null && (gameObject.transform.position.x < Target.transform.position.x))
        {
            if(callback != null)
            {
                callback(false);
            }
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, gameObject.transform.position);
            Destroy(gameObject);
        } else
        {
            UpdateVelocity();
        }
    }
}
