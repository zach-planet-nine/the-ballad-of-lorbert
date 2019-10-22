using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPStealerAI : BattleAI
{

    private bool hasStoredMP;
    private int storedRounds = 0;

    private GameObject GetTargetForMPSteal(List<GameObject> characters)
    {
        GameObject target = characters[0];
        int mpLeader = 0;
        characters.ForEach(character =>
        {
            BattleStats stats = BattleManager.manager.GetStatsForEntity(character);
            if (stats.currentMP > mpLeader && stats.currentMP > 0)
            {
                target = character;
                mpLeader = stats.currentMP;
            }
        });

        return target;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        storedRounds += 1;
        ActionAndTarget response = new ActionAndTarget();
        if(hasStoredMP && storedRounds >= 3)
        {
            hasStoredMP = false;
            response.action = EnemyActions.DischargeStoredEnergy;
            response.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            return response;
        }
        if(enemies.Count == 2)
        {
            int oneOfThree = Randomness.GetIntBetween(0, 3);
            int stealerRoll = Randomness.GetIntBetween(0, 5);
            if (stealerRoll > 0 && oneOfThree == 0)
            {
                return response;
            }
            if(!hasStoredMP && stealerRoll == 0)
            {
                storedRounds = 0;
                hasStoredMP = true;
                response.action = EnemyActions.StealMP;
                response.target = GetTargetForMPSteal(characters);
                return response;
            } else
            {
                response.action = EnemyActions.Projectile;
                response.target = characters[Randomness.GetIntBetween(0, characters.Count)];
                return response;
            }
        } else
        {
            int stealerRoll = Randomness.GetIntBetween(0, 5);
            if (!hasStoredMP && stealerRoll == 0)
            {
                storedRounds = 0;
                hasStoredMP = true;
                response.action = EnemyActions.StealMP;
                response.target = GetTargetForMPSteal(characters);
                return response;
            }
            else
            {
                response.action = EnemyActions.Projectile;
                response.target = characters[Randomness.GetIntBetween(0, characters.Count)];
                return response;
            }
        }
    }

   
}
