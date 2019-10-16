using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamage : MonoBehaviour
{

    public int damageNumber;
    public Text damageText;
    public float decayTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 startingVelocity = new Vector2(Randomness.GetPositiveOrNegative() * Randomness.GetValueBetween(3.0f, 7.0f), Randomness.GetValueBetween(7.0f, 10.0f));
        Rigidbody2D rbdy = damageText.GetComponent<Rigidbody2D>();
        rbdy.velocity = startingVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        damageText.text = "" + damageNumber;
        decayTime -= Time.deltaTime;
        if(decayTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
