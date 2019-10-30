using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBotAI : BattleAI
{
    private int autoTurretCounter;
    private int autoTurretThreshold = 6;
    private bool lastRoundHadAutoTurret;

    private bool IsThereAnAutoTurret(List<GameObject> enemies)
    {
        bool autoTurret = false;
        enemies.ForEach(enemy =>
        {
            if (enemy.name.Contains("AutoTurret"))
            {
                autoTurret = true;
            }
        });
        return autoTurret;
    }

    private ActionAndTarget ChooseProjectileOrSmallBomb(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.SmallBomb;
            if(Randomness.CoinToss())
            {
                actionAndTarget.target = GetEntityWithLeastHP(characters);
            } else
            {
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        }

        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        autoTurretCounter += 1;
        bool hasAutoTurret = IsThereAnAutoTurret(enemies);

        if(enemies.Count == 3)
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget = ChooseProjectileOrSmallBomb(characters);
            }
        }
        else if(enemies.Count == 2)
        {
            if(autoTurretCounter >= autoTurretThreshold && !hasAutoTurret && !lastRoundHadAutoTurret)
            {
                actionAndTarget.action = EnemyActions.SummonAutoTurret;
                actionAndTarget.target = enemies[0];
            } else if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrSmallBomb(characters);
            }
        } else
        {
            if(autoTurretCounter >= autoTurretThreshold && !lastRoundHadAutoTurret)
            {
                actionAndTarget.action = EnemyActions.SummonAutoTurret;
                actionAndTarget.target = enemies[0];
            } else if(Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileOrSmallBomb(characters);
            }
        }

        if(lastRoundHadAutoTurret && !hasAutoTurret)
        {
            autoTurretCounter = 0;
        }
        lastRoundHadAutoTurret = hasAutoTurret;

        return actionAndTarget;
    }

}
