using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyActions
{
    None,
    Projectile,
    Sludge,
    StealMP,
    DischargeStoredEnergy,
    Bit,
    BigBullet,
    MachineGun,
    Scythe,
    Healing,
    Hourglass,
    Pollen,
    Fire,
    Ice,
    Bolt,
    Bash,
    Branch,
    MPSlow,
    Tangle,
    Needle,
    CarnifloraRevive,
    SmallBomb,
    Laser
}

public class EnemyAI : MonoBehaviour
{
    public BattleAI ai;
    public GameObject activeSelf;

    public void Start()
    {
        /*int zeros = 0;
        int ones = 0;
        int twos = 0;
        int threes = 0;
        int fours = 0;
        for(int i = 0; i < 1000; i++)
        {
            switch(Randomness.GetIntBetween(0, 5))
            {
                case 0: zeros++;
                    break;
                case 1: ones++;
                    break;
                case 2: twos++;
                    break;
                case 3: threes++;
                    break;
                case 4: fours++;
                    break;
            }
        }
        Debug.Log("zeros: " + zeros);
        Debug.Log("ones: " + ones);
        Debug.Log("twos: " + twos);
        Debug.Log("threes: " + threes);
        Debug.Log("fours: " + fours);*/
        //I feel that there's a better way to do this, but I can't figure out what it is.
        if (gameObject.name.Contains("SludgeMonster"))
        {
            ai = new SludgeMonsterAI();
        }
        else if (gameObject.name.Contains("MPStealer"))
        {
            ai = new MPStealerAI();
        } else if(gameObject.name.Contains("AttackBot"))
        {
            ai = new AttackBotAI();
        } else if(gameObject.name.Contains("Tank"))
        {
            ai = new TankAI();
        } else if(gameObject.name.Contains("Reaper"))
        {
            ai = new ReaperAI();
        } else if(gameObject.name.Contains("MadRobot"))
        {
            ai = new MadRobotAI();
        } else if(gameObject.name.Contains("MadScientist"))
        {
            ai = new MadScientistAI();
        } else if(gameObject.name.Contains("SlowPlant"))
        {
            ai = new SlowPlantAI();
        } else if(gameObject.name.Contains("Seedling"))
        {
            ai = new SeedlingAI();
        } else if(gameObject.name.Contains("Oakenemy"))
        {
            ai = new OakenemyAI();
        } else if(gameObject.name.Contains("WizenTree"))
        {
            ai = new WizenTreeAI();
        } else if(gameObject.name.Contains("TangleWeed"))
        {
            ai = new TangleWeedAI();
        } else if(gameObject.name.Contains("Fireling"))
        {
            ai = new FirelingAI();
        } else if(gameObject.name.Contains("Carniflora"))
        {
            ai = new CarnifloraAI();
        } else if(gameObject.name.Contains("SmallBomber"))
        {
            ai = new SmallBomberAI();
        } else if(gameObject.name.Contains("Magimech"))
        {
            ai = new MagimechAI();
        } else if(gameObject.name.Contains("SpiderBot"))
        {
            ai = new SpiderBotAI();
        }
        else
        {
            ai = new SludgeMonsterAI();
        }
        
    }

    public void Attacked(GameObject attacker)
    {
        ai.Attacked(attacker);
    }

    public void DecideWhatToDo(List<GameObject> characters, List<GameObject> enemies, Action<bool> callback)
    {
        ActionAndTarget actionAndTarget = ai.ChooseActionAndTarget(characters, enemies);
        switch(actionAndTarget.action)
        {
            case EnemyActions.Projectile:
                activeSelf.SetActive(true);
                int damage = BattleManager.manager.EntityAttacksEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<Attack>().AttackEntityWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, damage, callback);
                break;
            case EnemyActions.Sludge:
                activeSelf.SetActive(true);
                int staminaDamage = BattleManager.manager.EntityStaminaAttacksEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().SludgeEntityWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, staminaDamage, callback);
                break;
            case EnemyActions.StealMP:
                Debug.Log("Steal MP here");
                activeSelf.SetActive(true);
                int mpDamage = BattleManager.manager.EntityMPAttacksEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().StealMPWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, mpDamage, callback);
                break;
            case EnemyActions.DischargeStoredEnergy:
                Debug.Log("Discharge energy here");
                activeSelf.SetActive(true);
                int dischargeDamage = BattleManager.manager.EntityDischargesMPAtEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().DischargeMPWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, dischargeDamage, callback);
                break;
            case EnemyActions.Bit:
                Debug.Log("Should deploy the bit here");
                activeSelf.SetActive(true);
                activeSelf.GetComponent<EnemyAbilities>().AttackWithBit(actionAndTarget.target, activeSelf, callback);
                break;
            case EnemyActions.BigBullet:
                Debug.Log("Should shoot big bullet here now");
                activeSelf.SetActive(true);
                int bigBulletDamage = BattleManager.manager.EntityShootsBigBulletAtEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<Attack>().AttackEntityWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, bigBulletDamage, callback);
                break;
            case EnemyActions.MachineGun:
                Debug.Log("Should shoot machine gun here now");
                activeSelf.SetActive(true);
                activeSelf.GetComponent<EnemyAbilities>().AttackWithMachineGun(characters, activeSelf, callback);
                break;
            case EnemyActions.Scythe:
                Debug.Log("Should Scythe here");
                activeSelf.SetActive(true);
                activeSelf.GetComponent<EnemyAbilities>().AttackWithScythe(actionAndTarget.target, activeSelf, callback);
                break;
            case EnemyActions.Healing:
                Debug.Log("Should do healing here");
                activeSelf.SetActive(true);
                int healing = BattleManager.manager.EntityUsesCureToHealEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().HealTarget(actionAndTarget.target, activeSelf, healing, callback);
                break;
            case EnemyActions.Hourglass:
                Debug.Log("Should do hourglass here");
                activeSelf.SetActive(true);
                float duration = BattleManager.manager.EntityUsesHourglassOnEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().UseHourglass(actionAndTarget.target, duration, callback);
                break;
            case EnemyActions.Pollen:
                Debug.Log("Should do pollen here");
                activeSelf.SetActive(true);
                float pollenDuration = BattleManager.manager.EntityUsesPollenOnEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().UsePollen(actionAndTarget.target, pollenDuration, callback);
                break;
            case EnemyActions.Fire:
                Debug.Log("Should do fire here");
                activeSelf.SetActive(true);
                activeSelf.GetComponent<EnemyAbilities>().UseFire(characters, callback);
                break;
            case EnemyActions.Ice:
                Debug.Log("Should do ice here");
                activeSelf.SetActive(true);
                activeSelf.GetComponent<EnemyAbilities>().UseIce(characters, callback);
                break;
            case EnemyActions.Bolt:
                Debug.Log("Should do bolt here");
                activeSelf.GetComponent<EnemyAbilities>().UseBolt(characters, callback);
                break;
            case EnemyActions.Bash:
                Debug.Log("Should bash here");
                activeSelf.SetActive(true);
                int bashDamage = BattleManager.manager.EntityBashesEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().BashEntity(actionAndTarget.target, bashDamage, callback);
                break;
            case EnemyActions.Branch:
                Debug.Log("Should bash here");
                activeSelf.SetActive(true);
                int branchDamage = BattleManager.manager.EntityBashesEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().BranchEntity(actionAndTarget.target, branchDamage, callback);
                break;
            case EnemyActions.MPSlow:
                Debug.Log("Should MP Slow here");
                activeSelf.SetActive(true);
                float slowDuration = BattleManager.manager.EntityMPSlowsEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().MPSlowEntity(actionAndTarget.target, slowDuration, callback);
                break;
            case EnemyActions.Tangle:
                Debug.Log("Should Tangle here");
                activeSelf.SetActive(true);
                float tangleDuration = BattleManager.manager.EntityUsesStopOnEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().TangleEntity(actionAndTarget.target, tangleDuration, callback);
                break;
            case EnemyActions.Needle:
                Debug.Log("Should Needle here");
                activeSelf.SetActive(true);
                int needleDamage = BattleManager.manager.EntityIgnoreDefenseWithMagicAttack(activeSelf);
                activeSelf.GetComponent<EnemyAbilities>().NeedleEntity(actionAndTarget.target, needleDamage, callback);
                break;
            case EnemyActions.CarnifloraRevive:
                Debug.Log("Should revive here");
                activeSelf.SetActive(true);
                activeSelf.GetComponent<EnemyAbilities>().ReviveAllies(callback);
                break;
            case EnemyActions.SmallBomb:
                Debug.Log("Should bomb here");
                activeSelf.SetActive(true);
                int smallBombDamage = BattleManager.manager.EntitySmallBombsEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().SmallBombEntity(actionAndTarget.target, smallBombDamage, callback);
                break;
            case EnemyActions.Laser:
                Debug.Log("Should laser here");
                activeSelf.SetActive(true);
                int laserDamage = BattleManager.manager.EntityLasersEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().LaserEntity(actionAndTarget.target, laserDamage, callback);
                break;
        }
    }
}
