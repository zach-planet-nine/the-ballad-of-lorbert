using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadScientistAI : BattleAI
{
    private bool hasStolenMP;
    private int healingCooldown;

    private ActionAndTarget ChooseStealMPDischargeOrHealing(List<GameObject> characters, List<GameObject> enemies)
    {
        BattleStats stats = BattleManager.manager.GetStatsForEntity(enemies[0]);
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        if (healingCooldown <= 0 && stats.currentHP < (stats.maxHP / 2))
        {
            healingCooldown = 16;
            actionAndTarget.action = EnemyActions.Healing;
            actionAndTarget.target = enemies[0];
        } else if (hasStolenMP)
        {
            actionAndTarget.action = EnemyActions.DischargeStoredEnergy;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        } else { 
            hasStolenMP = true;
            actionAndTarget.action = EnemyActions.StealMP;
            actionAndTarget.target = GetEntityWithMostMP(characters);
        }
        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        int roll = Randomness.GetIntBetween(0, 3);
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        healingCooldown -= 1;
        switch (roll)
        {
            case 0:
                return actionAndTarget;
            case 1:
                actionAndTarget.action = EnemyActions.Projectile;
                actionAndTarget.target = GetEntityWithMostHP(characters);
                break;
            case 2:
                actionAndTarget = ChooseStealMPDischargeOrHealing(characters, enemies);
                break;
        }
        return actionAndTarget;
    }

}
