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
    MachineGunSingle,
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
    Laser,
    Grenade,
    SummonAutoTurret,
    Trash,
    LiquidAttack,
    Wave,
    OrkastBall,
    Baton,
    Stalactite,
    SolidAttack,
    AgileStar,
    SwordSlash,
    StaminaSlowAttack
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

        string gameObjectName = gameObject.name;

        if (gameObjectName.Contains("SludgeMonster"))
        {
            ai = new SludgeMonsterAI();
        }
        else if (gameObjectName.Contains("MPStealer"))
        {
            ai = new MPStealerAI();
        } else if(gameObjectName.Contains("AttackBot"))
        {
            ai = new AttackBotAI();
        } else if(gameObjectName.Contains("Tank"))
        {
            ai = new TankAI();
        } else if(gameObjectName.Contains("Reaper"))
        {
            ai = new ReaperAI();
        } else if(gameObjectName.Contains("MadRobot"))
        {
            ai = new MadRobotAI();
        } else if(gameObjectName.Contains("MadScientist"))
        {
            ai = new MadScientistAI();
        } else if(gameObjectName.Contains("SlowPlant"))
        {
            ai = new SlowPlantAI();
        } else if(gameObjectName.Contains("Seedling"))
        {
            ai = new SeedlingAI();
        } else if(gameObjectName.Contains("Oakenemy"))
        {
            ai = new OakenemyAI();
        } else if(gameObjectName.Contains("WizenTree"))
        {
            ai = new WizenTreeAI();
        } else if(gameObjectName.Contains("TangleWeed"))
        {
            ai = new TangleWeedAI();
        } else if(gameObjectName.Contains("Fireling"))
        {
            ai = new FirelingAI();
        } else if(gameObjectName.Contains("Carniflora"))
        {
            ai = new CarnifloraAI();
        } else if(gameObjectName.Contains("SmallBomber"))
        {
            ai = new SmallBomberAI();
        } else if(gameObjectName.Contains("Magimech"))
        {
            ai = new MagimechAI();
        } else if(gameObjectName.Contains("SpiderBot"))
        {
            ai = new SpiderBotAI();
        } else if(gameObjectName.Contains("Infantry"))
        {
            ai = new InfantryAI();
        } else if(gameObjectName.Contains("DefenseBot"))
        {
            ai = new DefenseBotAI();
        } else if(gameObjectName.Contains("AutoTurret"))
        {
            ai = new AutoTurretAI();
        } else if(gameObjectName.Contains("FighterShip"))
        {
            ai = new FighterShipAI();
        } else if(gameObjectName.Contains("Clunker"))
        {
            ai = new ClunkerAI();
        } else if(gameObjectName.Contains("LiquidFlan"))
        {
            ai = new LiquidFlanAI();
        } else if(gameObjectName.Contains("LiquidElemental"))
        {
            ai = new LiquidElementalAI();
        } else if(gameObjectName.Contains("Commando"))
        {
            ai = new CommandoAI();
        } else if(gameObjectName.Contains("OrkastBallPlayer"))
        {
            ai = new OrkastBallPlayerAI();
        } else if(gameObjectName.Contains("RecycleMonster"))
        {
            ai = new RecycleMonsterAI();
        } else if(gameObjectName.Contains("Marine"))
        {
            ai = new MarineAI();
        } else if(gameObjectName.Contains("SolidElemental"))
        {
            ai = new SolidElementalAI();
        } else if(gameObjectName.Contains("SolidFlan"))
        {
            ai = new SolidFlanAI();
        } else if(gameObjectName.Contains("Chimera"))
        {
            ai = new ChimeraAI();
        } else if(gameObjectName.Contains("AgileBot"))
        {
            ai = new AgileBotAI();
        } else if(gameObjectName.Contains("ShadowyFigure"))
        {
            ai = new ShadowyFigureAI();
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
        Debug.Log(enemies.Count);
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
            case EnemyActions.MachineGunSingle:
                Debug.Log("Should shoot machine gun here now");
                activeSelf.SetActive(true);
                List<GameObject> mgCharacters = new List<GameObject>();
                mgCharacters.Add(actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().AttackWithMachineGun(mgCharacters, activeSelf, callback);
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
            case EnemyActions.Grenade:
                Debug.Log("Should grenade here");
                activeSelf.SetActive(true);
                activeSelf.GetComponent<EnemyAbilities>().GrenadeEntities(characters, callback);
                break;
            case EnemyActions.SummonAutoTurret:
                Debug.Log("Should summon auto turret here");
                activeSelf.SetActive(true);
                List<GameObject> deadEnemies = BattleManager.manager.GetDeadEnemies();
                Debug.Log(deadEnemies.Count);
                activeSelf.GetComponent<EnemyAbilities>().SummonAutoTurret(deadEnemies, callback);
                break;
            case EnemyActions.Trash:
                Debug.Log("Should trash here");
                activeSelf.SetActive(true);
                int trashDamage = BattleManager.manager.EntityTrashesEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().TrashEntity(actionAndTarget.target, trashDamage, callback);
                break;
            case EnemyActions.LiquidAttack:
                Debug.Log("Should liquid attack here");
                activeSelf.SetActive(true);
                int liquidDamage = BattleManager.manager.EntityUsesLiquidToAttackEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().LiquidAttackEntity(actionAndTarget.target, liquidDamage, callback);
                break;
            case EnemyActions.Wave:
                Debug.Log("Should wave here");
                activeSelf.SetActive(true);
                int waveDamage = BattleManager.manager.EntityWavesEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().WaveEntity(actionAndTarget.target, waveDamage, callback);
                break;
            case EnemyActions.OrkastBall:
                Debug.Log("Should OrkastBall here");
                activeSelf.SetActive(true);
                int orkastballDamage = BattleManager.manager.EntityOrkastBallsEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().OrkastBallEntity(actionAndTarget.target, orkastballDamage, callback);
                break;
            case EnemyActions.Baton:
                Debug.Log("Should bash here");
                activeSelf.SetActive(true);
                int batonDamage = BattleManager.manager.EntityBashesEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().BatonEntity(actionAndTarget.target, batonDamage, callback);
                break;
            case EnemyActions.Stalactite:
                Debug.Log("Should bomb here");
                activeSelf.SetActive(true);
                int stalactiteDamage = BattleManager.manager.EntityStalactitesEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().StalactiteEntity(actionAndTarget.target, stalactiteDamage, callback);
                break;
            case EnemyActions.SolidAttack:
                Debug.Log("Should Solid Attack here");
                activeSelf.SetActive(true);
                int solidDamage = BattleManager.manager.EntityUsesSolidToAttackEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().SolidAttackEntity(actionAndTarget.target, solidDamage, callback);
                break;
            case EnemyActions.AgileStar:
                Debug.Log("Should agile star here");
                activeSelf.SetActive(true);
                int agileDamage = BattleManager.manager.EntityUsesAgileStarsToAttackEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().AgileStarEntity(actionAndTarget.target, agileDamage, callback);
                break;
            case EnemyActions.SwordSlash:
                Debug.Log("Should wave here");
                activeSelf.SetActive(true);
                int slashDamage = BattleManager.manager.EntitySwordSlashesEntity(activeSelf, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().SwordSlashEntity(actionAndTarget.target, slashDamage, callback);
                break;
            case EnemyActions.StaminaSlowAttack:
                activeSelf.SetActive(true);
                int attackDamage = BattleManager.manager.EntityAttacksEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<Attack>().AttackEntityWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, attackDamage, a =>
                {
                    Debug.Log("Should do hourglass here");
                    activeSelf.SetActive(true);
                    float staminaDuration = BattleManager.manager.EntityUsesHourglassOnEntity(activeSelf, actionAndTarget.target);
                    activeSelf.GetComponent<EnemyAbilities>().UseHourglass(actionAndTarget.target, staminaDuration, callback);
                });
                break;
        }
    }
}
