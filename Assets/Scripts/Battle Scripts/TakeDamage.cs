using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public GameObject activeSelf;
    public GameObject hitSelf;
    public GameObject DamageText;
    public bool isDefenseBot;

    private int damage;
    private Vector2 startPosition;
    private float hitSelfDisplayDuration = 0.15f;
    private float hitSelfDuration;

    public void DisplayDamage(int damage, Vector3 position)
    {
        if(hitSelf != null)
        {
            activeSelf.SetActive(false);
            hitSelf.SetActive(true);
            hitSelfDuration = hitSelfDisplayDuration;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        BattleStats stats = BattleManager.manager.GetStatsForEntity(gameObject);

        if (stats.isProtectedByWall )
        {
            damage = (int)((float)damage * 0.66f);
        }
        
        if (isDefenseBot && damage > 12)
        {
            damage = 12;
        }

        Vector3 rightPosition = new Vector3(position.x, position.y, 0);
        var clone = (GameObject)Instantiate(DamageText, rightPosition, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<DisplayDamage>().damageNumber = damage;

        if (stats.GasEmitter != null)
        {
            Debug.Log("Should display gas damage");
            PositionAndDamage pad = stats.GasEmitter.GetComponent<RunGasAttack>().ExplodeOnce();
            int explosionDamage = pad.damage;
            var explosionClone = (GameObject)Instantiate(DamageText, pad.position, Quaternion.Euler(Vector3.zero));
            explosionClone.GetComponent<DisplayDamage>().damageNumber = explosionDamage;
            damage += explosionDamage;
        }

        BattleManager.manager.ApplyDamage(gameObject, damage);
    }

    public void TakeStaminaDamage(int damage)
    {
        BattleManager.manager.ApplyStaminaDamage(gameObject, damage);
    }

    public void TakeMPDamage(int damage)
    {
        BattleManager.manager.ApplyMPDamage(gameObject, damage);
    }

    private void Update()
    {
        if(hitSelfDuration > 0)
        {
            hitSelfDuration -= Time.deltaTime;
            if(hitSelfDuration <= 0)
            {
                hitSelf.SetActive(false);
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
