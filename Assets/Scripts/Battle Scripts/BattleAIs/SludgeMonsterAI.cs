using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeMonsterAI : BattleAI
{

    private bool haveSludgedRecently;

    private EnemyActions ChooseProjectileOrSludge(List<GameObject> characters)
    {
        if(haveSludgedRecently)
        {
            haveSludgedRecently = false;
            return EnemyActions.Projectile;
        }
        GameObject characterWithLowStamina = null;
        characters.ForEach(character =>
        {
            int characterStamina = BattleManager.manager.GetStatsForEntity(character).currentStamina;
            if (characterStamina < 100)
            {
                characterWithLowStamina = character;
            }
        });
        if(characterWithLowStamina != null)
        {
            haveSludgedRecently = true;
            return EnemyActions.Sludge;
        } else
        {
            return EnemyActions.Projectile;
        }
    }

    public override EnemyActions ChooseAction(List<GameObject> characters, List<GameObject> enemies)
    {
        if(enemies.Count == 3)
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if (coinToss == 1)
            {
                return EnemyActions.None;
            }
            return ChooseProjectileOrSludge(characters);
        } else if(enemies.Count == 2)
        {
            int twoOfThree = Randomness.GetIntBetween(0, 3);
            Debug.Log("Got this in twoOfThree: " + twoOfThree);
            if(twoOfThree == 0)
            {
                return EnemyActions.None;
            } else
            {
                return ChooseProjectileOrSludge(characters);
            }
        } else
        {
            return ChooseProjectileOrSludge(characters);
        }
        
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        EnemyActions action = ChooseAction(characters, enemies);
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        actionAndTarget.action = action;
        if(action == EnemyActions.Sludge)
        {
            GameObject characterWithLowStamina = characters[0];
            characters.ForEach(character =>
            {
                if (BattleManager.manager.GetStatsForEntity(character).currentStamina < 50)
                {
                    characterWithLowStamina = character;
                }
            });
            actionAndTarget.target = characterWithLowStamina;
        } else if(action == EnemyActions.Projectile)
        {
            int roll = Randomness.GetIntBetween(0, characters.Count);
            actionAndTarget.target = characters[roll];
        }
        return actionAndTarget;
    }

    public override GameObject ChooseTarget(List<GameObject> potentialTargets)
    {
        return potentialTargets[0];
    }
}
