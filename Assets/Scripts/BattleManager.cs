using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject Lorbert;
    public GameObject Artro;
    public GameObject IO;

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;

    private BattleStats LorbertStats;
    private BattleStats ArtroStats;
    private BattleStats IOStats;
    private BattleStats Enemy1Stats;
    private BattleStats Enemy2Stats;
    private BattleStats Enemy3Stats;
    private BattleStats Enemy4Stats;
    // Start is called before the first frame update
    void Start()
    {
        LorbertStats = CharacterStats.characterStats.GetLorbertBattleStats();
        ArtroStats = CharacterStats.characterStats.GetArtroBattleStats();
        IOStats = CharacterStats.characterStats.GetIOBattleStats();

        Enemy1Stats = Enemy1.GetComponent<EnemyStats>().GetBattleStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int EntityAttacksEntity(GameObject attacker, GameObject defender)
    {
        return 38;
    }
}

public class BattleStats
{
    public int currentHP;
    public int maxHP;
    public int currentStamina;
    public int maxStamina;
    public int currentMP;
    public int maxMP;
    public int strength;
    public int vitality;
    public int agility;
    public int dexterity;
    public int wisdom;
    public int aura;
    public int perception;
    public int luck;
}
