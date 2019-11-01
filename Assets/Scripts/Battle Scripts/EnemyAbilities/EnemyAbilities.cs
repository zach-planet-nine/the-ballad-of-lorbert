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
        Vector3 position = new Vector3(-0.39f + Randomness.GetValueBetween(-0.6f, 0.6f), 3.14f + Randomness.GetValueBetween(-0.6f, 0.6f), 0);
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

    public void UsePollen(GameObject target, float duration, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("PollenEmitter");
        var clone = (GameObject)Instantiate(ability, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunPollen>().SetTargetWithCallback(target, duration, callback);
    }

    public void UseFire(List<GameObject> characters, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("FireEmitterObject");
        Debug.Log("Characters count: " + characters.Count);
        characters.ForEach(character =>
        {
            int damage = BattleManager.manager.EntityUsesFireOnEntity(gameObject, character);
            var clone = (GameObject)Instantiate(ability, new Vector3(character.transform.position.x, character.transform.position.y - 0.6f, 0), Quaternion.identity);
            clone.GetComponent<RunFire>().SetTargetWithCallback(character, damage, callback);
        });
    }

    public void UseIce(List<GameObject> characters, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("IceObject");
        characters.ForEach(character =>
        {
            int damage = BattleManager.manager.EntityUsesIceOnEntity(gameObject, character);
            var clone = (GameObject)Instantiate(ability, new Vector3(character.transform.position.x, character.transform.position.y - 0.6f, 0), Quaternion.identity);
            clone.GetComponent<RunIce>().SetTargetWithCallback(character, damage, callback);
        });
    }

    public void UseBolt(List<GameObject> characters, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("BoltObject");
        characters.ForEach(character =>
        {
            int damage = BattleManager.manager.EntityUsesBoltOnEntity(gameObject, character);
            var clone = (GameObject)Instantiate(ability, new Vector3(character.transform.position.x, character.transform.position.y - 0.6f, 0), Quaternion.identity);
            clone.GetComponent<RunBolt>().SetTargetWithCallback(character, damage, callback);
        });
    }

    public void BashEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("Bash");
        var clone = (GameObject)Instantiate(ability, target.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunBash>().SetTargetWithCallback(target, damage, callback);
    }

    public void BranchEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("TreeBranch");
        //var clone = (GameObject)Instantiate(ability, target.transform.position, Quaternion.Euler(Vector3.zero));
        var clone = (GameObject)Instantiate(ability, target.transform.position, Quaternion.Euler(new Vector3(0, 0, -45)));
        clone.GetComponent<RunBash>().SetTargetWithCallback(target, damage, callback);
    }

    public void MPSlowEntity(GameObject target, float duration, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("MPSlow");
        var clone = (GameObject)Instantiate(ability, target.transform.position, Quaternion.Euler(new Vector3(0, 0, -45)));
        clone.GetComponent<RunMPSlow>().SetTargetWithCallback(target, duration, callback);
    }

    public void TangleEntity(GameObject target, float duration, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("TangleEmitter");
        var clone = (GameObject)Instantiate(ability, target.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunTangle>().SetTargetWithCallback(target, duration, callback);
    }

    public void NeedleEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("NeedleEmitter");
        var clone = (GameObject)Instantiate(ability, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunNeedles>().SetTargetWithCallback(target, damage, callback);
    }

    public void ReviveAllies(Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("HealingParticles");
        List<GameObject> deadEnemies = BattleManager.manager.GetDeadEnemies();
        deadEnemies.ForEach(enemy =>
        {
            enemy.GetComponent<EnemyDeath>().Revive();
            Debug.Log("Reviving: " + enemy);
            var clone = (GameObject)Instantiate(ability, enemy.transform.position, Quaternion.Euler(Vector3.zero));
            int healAmount = BattleManager.manager.GetStatsForEntity(enemy).maxHP;
            clone.GetComponent<RunHealing>().SetTargetWithCallback(enemy, healAmount, callback);
        });
    }

    public void SmallBombEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("SmallBomb");
        Vector3 startPosition = new Vector3(target.transform.position.x + Randomness.GetValueBetween(-1.8f, -0.8f), target.transform.position.y + 1.5f, 0);
        var clone = (GameObject)Instantiate(ability, startPosition, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunSmallBomb>().SetTargetWithCallback(target, damage, callback);
    }

    public void LaserEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("Laser");
        var clone = (GameObject)Instantiate(ability, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunLaser>().SetTargetWithCallback(target, damage, callback);
    }

    public void GrenadeEntities(List<GameObject> characters, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("Grenade");
        var clone = (GameObject)Instantiate(ability, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunGrenade>().SetTargetsWithCallback(characters, callback);
    }

    public void SummonAutoTurret(List<GameObject> deadEnemies, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("HealingParticles");
        deadEnemies.ForEach(enemy =>
        {
            if (enemy.name.Contains("AutoTurret"))
            {
                enemy.GetComponent<EnemyDeath>().Revive();
                var clone = (GameObject)Instantiate(ability, enemy.transform.position, Quaternion.Euler(Vector3.zero));
                int healAmount = BattleManager.manager.GetStatsForEntity(enemy).maxHP;
                clone.GetComponent<RunHealing>().SetTargetWithCallback(enemy, healAmount, callback);
            }
        });
    }

    public void TrashEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("TrashObject");
        var clone = (GameObject)Instantiate(ability, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunTrash>().SetTargetWithCallback(target, damage, callback);
    }

    public void LiquidAttackEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("StormCloud");
        Vector3 position = new Vector3(target.transform.position.x - 0.5f, target.transform.position.y + 1.2f, 0);
        var clone = (GameObject)Instantiate(ability, position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<Storm>().SetTargetWithCallback(target, position, damage, callback);
    }

    public void WaveEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("Wave");
        Vector3 position = new Vector3(gameObject.transform.position.x, target.transform.position.y, 0);
        var clone = (GameObject)Instantiate(ability, position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunWave>().SetTargetWithCallback(target, damage, callback);
    }

    public void OrkastBallEntity(GameObject target, int damage, Action<bool> callback)
    {
        GameObject ability = GetGameObjectForAbilityNamed("Wave");
        Vector3 position = new Vector3(gameObject.transform.position.x + 1.5f, target.transform.position.y + 2.0f, 0);
        var clone = (GameObject)Instantiate(ability, position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunOrkastBall>().SetTargetWithCallback(target, damage, callback);
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.CheckIfEntityIsDead(gameObject))
        {
            gameObject.SetActive(false);
        }
    }
}
