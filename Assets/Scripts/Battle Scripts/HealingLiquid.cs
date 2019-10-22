using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingLiquid : MonoBehaviour
{
    GameObject HealedCharacter;
    CharacterDeath HealedCharacterDeath;
    Vector3 destination;
    int healingAmount;
    public float duration = 6.0f;
    float durationThreshold;
    int healingTimes;
    float healingTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject entity, Vector3 destination, int healing)
    {
        this.destination = destination;
        HealedCharacter = entity;
        HealedCharacterDeath = entity.GetComponent<CharacterDeath>();
        healingAmount = healing / 3;
        durationThreshold = duration / 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.battleIsOver && gameObject != null)
        {
            Destroy(gameObject);
            return;
        }
        if(HealedCharacterDeath.isDead)
        {
            Destroy(gameObject);
            return;
        }
        healingTimer += Time.deltaTime;
        if(healingTimer >= durationThreshold)
        {
            healingTimer = 0;
            healingTimes += 1;
            HealedCharacter.GetComponent<TakeHealing>().DisplayHealing(healingAmount, destination);
        }
        if(healingTimes >= 3)
        {
            Destroy(gameObject);
        }
    }
}
