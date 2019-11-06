using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    public GameObject PlasmaObject;
    public GameObject CharacterObject;
    public GameObject AttackObject;

    private void OnEnable()
    {
        if(BattleManager.manager.plasmaLearned)
        {
            PlasmaObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        PlasmaObject.SetActive(false);
    }

    public void HealEntity(GameObject entity, int healing)
    {
        Debug.Log("Should heal entity");
        var clone = (GameObject)Instantiate(CharacterObject, entity.transform.position, Quaternion.Euler(Vector3.zero));
        entity.GetComponent<TakeHealing>().DisplayHealing(healing, entity.transform.position);
    }

    public void AttackEntity(GameObject entity, int damage)
    {
        Vector3 position = new Vector3(Randomness.GetValueBetween(-3.0f, -1.0f), Randomness.GetValueBetween(3.5f, 4.5f), 0);
        var clone = (GameObject)Instantiate(AttackObject, position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunPlasma>().SetTarget(entity, damage);
    }

    private void Update()
    {
        int currentMP = BattleManager.manager.GetStatsForEntity(gameObject).currentMP;
        if (currentMP < BattleManager.manager.plasmaCost)
        {
            float mpPercent = (float)currentMP / (float)BattleManager.manager.plasmaCost;
            var col = PlasmaObject.GetComponent<SpriteRenderer>().material.color;
            col.a = mpPercent - 0.25f;
            float scalePercent = 3.03f * mpPercent;
            PlasmaObject.GetComponent<SpriteRenderer>().material.color = col;
            PlasmaObject.GetComponent<Transform>().localScale = new Vector3(scalePercent, scalePercent, 1.0f);
            PlasmaObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            var col = PlasmaObject.GetComponent<SpriteRenderer>().material.color;
            col.a = 1.0f;
            PlasmaObject.GetComponent<SpriteRenderer>().material.color = col;
            PlasmaObject.GetComponent<Transform>().localScale = new Vector3(3.03f, 3.03f, 3.03f);
            PlasmaObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
