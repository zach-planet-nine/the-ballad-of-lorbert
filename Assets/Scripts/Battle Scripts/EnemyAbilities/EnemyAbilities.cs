using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilities : MonoBehaviour
{
    public GameObject Ability1;
    public GameObject Ability2;
    public GameObject Ability3;
    public GameObject Ability4;

    private float projectileDuration = 0.5f;
    private float sludgeDuration = 1.0f;
    private float sludgeTimer;
    private float projectileTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private GameObject GetGameObjectForAbilityNamed(string name)
    {
        if(Ability1 != null && name == Ability1.name)
        {
            return Ability1;
        } else if(Ability2 != null && name == Ability2.name)
        {
            return Ability2;
        } else if(Ability3 != null && name == Ability3.name)
        {
            return Ability3;
        } else if(Ability4 != null && name == Ability4.name) 
        {
            return Ability4;
        }
        return Ability1;
    }

    public void SludgeEntityWithCallback(GameObject target, Vector3 destination, int staminaDamage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("SludgeBarrel");
        var clone = (GameObject)Instantiate(ability, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunSludgeAttack>().SetTargetWithCallback(target, destination, staminaDamage, callback);
    }

    public void StealMPWithCallback(GameObject target, Vector3 destination, int mpDamage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("MPEmitter");
        var clone = (GameObject)Instantiate(ability, destination, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunStealMP>().SetTargetWithCallback(target, gameObject.transform.position, mpDamage, callback);
    }

    public void DischargeMPWithCallback(GameObject target, Vector3 destination, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("DischargeMP");
        var clone = (GameObject)Instantiate(ability, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunDischargeMP>().SetTargetWithCallback(target, gameObject.transform.position, destination, damage, callback);
    }

    public void AttackWithBit(GameObject target, GameObject attacker, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("Bit");
        Vector3 position = new Vector3(-0.39f + Randomness.GetValueBetween(-0.3f, 0.3f), 3.14f + Randomness.GetValueBetween(-0.3f, 0.3f), 0);
        var clone = (GameObject)Instantiate(ability, position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunBit>().SetTargetWithCallback(target, attacker, callback);
    }

    public void AttackWithMachineGun(List<GameObject> characters, GameObject attacker, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("MachineGun");
        var clone = (GameObject)Instantiate(ability, attacker.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunMachineGun>().SetTargetWithCallback(characters, attacker, callback);
    }

    public void AttackWithScythe(GameObject target, GameObject attacker, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("Scythe");
        var clone = (GameObject)Instantiate(ability, attacker.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunScythe>().SetTargetWithCallback(target, callback);
    }

    public void HealTarget(GameObject target, GameObject healer, int healingAmount, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("HealingParticles");
        var clone = (GameObject)Instantiate(ability, target.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunHealing>().SetTargetWithCallback(target, healingAmount, callback);
    }

    public void UseHourglass(GameObject target, float duration, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("Hourglass");
        var clone = (GameObject)Instantiate(ability, target.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunHourglass>().SetTargetWithCallback(target, duration, callback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
