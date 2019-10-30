using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterShipAI : BattleAI
{

    private GameObject ChooseTargetForProjectile(List<GameObject> characters)
    {
        return characters[Randomness.GetIntBetween(0, characters.Count)];
    }

    private ActionAndTarget ChooseBoltOrBit(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.Bit;
            actionAndTarget.target = GetEntityWithMostStamina(characters);
        } else
        {
            actionAndTarget.action = EnemyActions.Bolt;
            actionAndTarget.target = characters[0];
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseBoltBitOrMPSlow(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        GameObject characterWithMPBelowThreshold = GetEntityWithMPBelowThreshold(characters, 0.5f);
        if(characterWithMPBelowThreshold != null)
        {
            if(!BattleManager.manager.GetStatsForEntity(characterWithMPBelowThreshold).isSlowedMP)
            {
                actionAndTarget.action = EnemyActions.MPSlow;
                actionAndTarget.target = characterWithMPBelowThreshold;
            } else
            {
                actionAndTarget = ChooseBoltOrBit(characters);
            }
        } else
        {
            actionAndTarget = ChooseBoltOrBit(characters);
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.CoinToss())
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = ChooseTargetForProjectile(characters);
        } else
        {
            actionAndTarget = ChooseBoltBitOrMPSlow(characters);
        }

        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        actionAndTarget = ChooseProjectileOrSkill(characters);

        return actionAndTarget;
    }

}
