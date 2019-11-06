using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleControls : MonoBehaviour
{
    public GameObject LorbertRest;
    public GameObject ArtroRest;
    public GameObject IORest;
    public GameObject LorbertActive;
    public GameObject ArtroActive;
    public GameObject IOActive;
    public GameObject Enemy1Rest;
    public GameObject Enemy1Active;
    public GameObject Enemy2Rest;
    public GameObject Enemy2Active;
    public GameObject Enemy3Rest;
    public GameObject Enemy3Active;
    public GameObject Enemy4Rest;
    public GameObject Enemy4Active;

    public GameObject battleManager;

    private GameObject AlienWithPriority;
    private GameObject SpellBeingDragged;
    private bool isDraggingSpell;
    private Vector3 spellStartingPosition;
    private Vector2 lastPosition;

    private float enemy1Timer = 1.8f;
    private float enemy2Timer = 0.66f;
    private float enemy3Timer;
    private float enemy4Timer = -1.5f;
    private float enemyThreshold = 2.0f;

    private float battleIsOverTimer;
    private float battleIsOverDuration = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        IORest.SetActive(false);
        IORest.SetActive(true);
    }

    private void HandleTap(RaycastHit2D hitInfo)
    {
        string hitName = hitInfo.transform.gameObject.name;
        Debug.Log("Hit name is: " + hitName);
        if (hitName == "LorbertRest" && !BattleManager.manager.CheckIfEntityIsStoppedOrDead(LorbertRest))
        {
            LorbertRest.SetActive(false);
            LorbertActive.SetActive(true);
            ArtroRest.SetActive(true);
            ArtroActive.SetActive(false);
            IORest.SetActive(true);
            IOActive.SetActive(false);
            AlienWithPriority = LorbertActive;
        }
        else if (hitName == "ArtroRest" && !BattleManager.manager.CheckIfEntityIsStoppedOrDead(ArtroRest))
        {
            LorbertRest.SetActive(true);
            LorbertActive.SetActive(false);
            ArtroRest.SetActive(false);
            ArtroActive.SetActive(true);
            IORest.SetActive(true);
            IOActive.SetActive(false);
            AlienWithPriority = ArtroActive;
        }
        else if (hitName == "IORest" && !BattleManager.manager.CheckIfEntityIsStoppedOrDead(IORest))
        {
            LorbertRest.SetActive(true);
            LorbertActive.SetActive(false);
            ArtroRest.SetActive(true);
            ArtroActive.SetActive(false);
            IORest.SetActive(false);
            IOActive.SetActive(true);
            AlienWithPriority = IOActive;
        }
        else if ((hitName == Enemy1Rest.name || hitName == Enemy1Active.name) && !BattleManager.manager.GetStatsForEntity(AlienWithPriority).isStopped)
        {
            int damage = battleManager.GetComponent<BattleManager>().EntityAttacksEntity(AlienWithPriority, Enemy1Rest);
            AlienWithPriority.GetComponent<Attack>().AttackEntity(Enemy1Rest, Camera.main.ScreenToWorldPoint(Input.mousePosition), damage);
            Enemy1Rest.GetComponent<EnemyAI>().Attacked(AlienWithPriority);
        }
        else if (Enemy2Rest != null && (hitName == Enemy2Rest.name || hitName == Enemy2Active.name) && !BattleManager.manager.GetStatsForEntity(AlienWithPriority).isStopped)
        {
            int damage = battleManager.GetComponent<BattleManager>().EntityAttacksEntity(AlienWithPriority, Enemy2Rest);
            AlienWithPriority.GetComponent<Attack>().AttackEntity(Enemy2Rest, Camera.main.ScreenToWorldPoint(Input.mousePosition), damage);
            Enemy2Rest.GetComponent<EnemyAI>().Attacked(AlienWithPriority);
        }
        else if (Enemy3Rest != null && (hitName == Enemy3Rest.name || hitName == Enemy3Active.name) && !BattleManager.manager.GetStatsForEntity(AlienWithPriority).isStopped)
        {
            int damage = battleManager.GetComponent<BattleManager>().EntityAttacksEntity(AlienWithPriority, Enemy3Rest);
            AlienWithPriority.GetComponent<Attack>().AttackEntity(Enemy3Rest, Camera.main.ScreenToWorldPoint(Input.mousePosition), damage);
            Enemy3Rest.GetComponent<EnemyAI>().Attacked(AlienWithPriority);
        }
        else if (Enemy4Rest != null && (hitName == Enemy4Rest.name || hitName == Enemy4Active.name) && !BattleManager.manager.GetStatsForEntity(AlienWithPriority).isStopped)
        {
            int damage = battleManager.GetComponent<BattleManager>().EntityAttacksEntity(AlienWithPriority, Enemy4Rest);
            AlienWithPriority.GetComponent<Attack>().AttackEntity(Enemy4Rest, Camera.main.ScreenToWorldPoint(Input.mousePosition), damage);
            Enemy4Rest.GetComponent<EnemyAI>().Attacked(AlienWithPriority);
        }
    }

    void RelaxEnemy(GameObject enemy)
    {
        if (enemy.name == Enemy1Rest.name || enemy.name == Enemy1Active.name)
        {
            Enemy1Rest.SetActive(true);
            Enemy1Active.SetActive(false);
        }
        else if (Enemy2Rest != null && (enemy.name == Enemy2Rest.name || enemy.name == Enemy2Active.name))
        {
            Enemy2Rest.SetActive(true);
            Enemy2Active.SetActive(false);
        }
        else if (Enemy3Rest != null && (enemy.name == Enemy3Rest.name || enemy.name == Enemy3Active.name))
        {
            Enemy3Rest.SetActive(true);
            Enemy3Active.SetActive(false);
        }
        else if (Enemy4Rest != null && (enemy.name == Enemy4Rest.name || enemy.name == Enemy4Active.name))
        {
            Enemy4Rest.SetActive(true);
            Enemy4Active.SetActive(false);
        }
    }

    void SpellIsDropped()
    {
        isDraggingSpell = false;
        SpellBeingDragged.transform.position = spellStartingPosition;
        CircleCollider2D spellCollider = SpellBeingDragged.GetComponent<CircleCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(spellCollider.bounds.min, spellCollider.bounds.max);
        if (overlap.Length == 2 && !BattleManager.manager.GetStatsForEntity(AlienWithPriority).isStopped)
        {
            Debug.Log(overlap.Length);
            Debug.Log(overlap[0].gameObject == SpellBeingDragged);
            Debug.Log(overlap[1].gameObject == SpellBeingDragged);
            GameObject target = overlap[0].gameObject == SpellBeingDragged ? overlap[1].gameObject : overlap[0].gameObject;
            if (target == Enemy1Rest || target == Enemy1Active ||
                target == Enemy2Rest || target == Enemy2Active ||
                target == Enemy3Rest || target == Enemy3Active ||
                target == Enemy4Rest || target == Enemy4Active)
            {
                switch (SpellBeingDragged.name)
                {
                    case "LorbertLiquid":
                    case "ArtroLiquid":
                    case "IOLiquid":
                        int damage = BattleManager.manager.EntityUsesLiquidToAttackEntity(AlienWithPriority, target);
                        AlienWithPriority.GetComponent<Liquid>().AttackEntity(target, Camera.main.ScreenToWorldPoint(Input.mousePosition), damage);
                        break;
                    case "LorbertSolid":
                    case "ArtroSolid":
                    case "IOSolid":
                        int solidDamage = BattleManager.manager.EntityUsesSolidToAttackEntity(AlienWithPriority, target);
                        AlienWithPriority.GetComponent<Solid>().AttackEntity(target, solidDamage);
                        break;
                    case "LorbertGas":
                    case "ArtroGas":
                    case "IOGas":
                        Debug.Log("Should attack entity with gas");
                        BattleManager.manager.EntityUsesGasOnEntity(AlienWithPriority);
                        AlienWithPriority.GetComponent<Gas>().AttackEntity(target);
                        break;
                    case "LorbertPlasma":
                    case "ArtroPlasma":
                    case "IOPlasma":
                        Debug.Log("Should attack entity with plasma");
                        int plasmaDamage = BattleManager.manager.EntityUsesPlasmaOnEntity(AlienWithPriority, target);
                        AlienWithPriority.GetComponent<Plasma>().AttackEntity(target, plasmaDamage);
                        break;
                }
            }
            else if (target == LorbertRest || target == LorbertActive ||
                target == ArtroRest || target == ArtroActive ||
                target == IORest || target == IOActive)
            {
                if(target == LorbertActive)
                {
                    target = LorbertRest;
                } else if(target == ArtroActive)
                {
                    target = ArtroRest;
                } else if(target == IOActive)
                {
                    target = IORest;
                }
                switch(SpellBeingDragged.name)
                {
                    case "LorbertLiquid":
                    case "ArtroLiquid":
                    case "IOLiquid":
                        if(!BattleManager.manager.CheckIfEntityIsDead(target))
                        {
                            int healing = BattleManager.manager.EntityUsesLiquidToHealEntity(AlienWithPriority, target);
                            AlienWithPriority.GetComponent<Liquid>().HealEntity(target, target.transform.position, healing);
                        }
                        break;
                    case "LorbertSolid":
                    case "ArtroSolid":
                    case "IOSolid":
                        if(!BattleManager.manager.CheckIfEntityIsDead(target))
                        {
                            float duration = BattleManager.manager.EntityUsesSolidForWall(AlienWithPriority);
                            AlienWithPriority.GetComponent<Solid>().UseWall(target, duration);
                        }
                        break;
                    case "LorbertGas":
                    case "ArtroGas":
                    case "IOGas":
                        int gasHealing = BattleManager.manager.EntityUsesGasToHealEntity(AlienWithPriority, target);
                        AlienWithPriority.GetComponent<Gas>().HealEntity(target, gasHealing);
                        break;
                    case "LorbertPlasma":
                    case "ArtroPlasma":
                    case "IOPlasma":
                        Debug.Log("Should do plasma healing");
                        int plasmaHealing = BattleManager.manager.EntityUsesPlasmaToHealEntity(AlienWithPriority, target);
                        AlienWithPriority.GetComponent<Plasma>().HealEntity(target, plasmaHealing);
                        break;
                }
                
            }

        }
    }

    void StartDragging()
    {
        if(AlienWithPriority == null)
        {
            return;
        }
        lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hitInfo && !BattleManager.manager.GetStatsForEntity(AlienWithPriority).isStopped)
        {
            Debug.Log(hitInfo.transform.gameObject.name);
            switch (hitInfo.transform.gameObject.name)
            {
                case "LorbertLiquid":
                case "ArtroLiquid":
                case "IOLiquid":
                case "LorbertSolid":
                case "ArtroSolid":
                case "IOSolid":
                case "LorbertGas":
                case "ArtroGas":
                case "IOGas":
                case "LorbertPlasma":
                case "ArtroPlasma":
                case "IOPlasma":
                    Debug.Log("Should start dragging spell");
                    Debug.Log(hitInfo.transform.gameObject.name);
                    isDraggingSpell = true;
                    SpellBeingDragged = hitInfo.transform.gameObject;
                    spellStartingPosition = SpellBeingDragged.transform.position;
                    break;
            }
        } else
        {
            
            Debug.Log(Input.mousePosition + " No hitInfo");
        }
    }

    void DragSpell()
    {
        if(BattleManager.manager.GetStatsForEntity(AlienWithPriority).isStopped)
        {
            SpellIsDropped();
        }
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = new Vector2(currentPosition.x - lastPosition.x, currentPosition.y - lastPosition.y);
        SpellBeingDragged.transform.position = new Vector3(SpellBeingDragged.transform.position.x + delta.x, SpellBeingDragged.transform.position.y + delta.y, SpellBeingDragged.transform.position.z);
        lastPosition = currentPosition;
    }

    bool CheckIfCharacterIsDead(GameObject character)
    {
        return character.GetComponent<CharacterDeath>().isDead;
    }

    bool CheckIfNullOrDead(GameObject entity)
    {
        if(entity == null)
        {
            return true;
        } else
        {
            return entity.GetComponent<EnemyDeath>().isDead;
        }
    }

    List<GameObject> GetAliveCharacters()
    {
        List<GameObject> potentialTargets = new List<GameObject>();
        if (!CheckIfCharacterIsDead(LorbertRest))
        {
            potentialTargets.Add(LorbertRest);
        }
        if (!CheckIfCharacterIsDead(ArtroRest))
        {
            potentialTargets.Add(ArtroRest);
        }
        if (!CheckIfCharacterIsDead(IORest))
        {
            potentialTargets.Add(IORest);
        }
        return potentialTargets;
    }

    List<GameObject> GetAliveEnemies()
    {
        List<GameObject> potentialTargets = new List<GameObject>();
        if(!CheckIfNullOrDead(Enemy1Rest))
        {
            potentialTargets.Add(Enemy1Rest);
        }
        if (!CheckIfNullOrDead(Enemy2Rest))
        {
            potentialTargets.Add(Enemy2Rest);
        }
        if (!CheckIfNullOrDead(Enemy3Rest))
        {
            potentialTargets.Add(Enemy3Rest);
        }
        if (!CheckIfNullOrDead(Enemy4Rest))
        {
            potentialTargets.Add(Enemy4Rest);
        }
        return potentialTargets;
    }

    bool CheckIfBattleOver()
    {
        return CheckIfNullOrDead(Enemy1Rest) && CheckIfNullOrDead(Enemy2Rest) && CheckIfNullOrDead(Enemy3Rest) && CheckIfNullOrDead(Enemy4Rest);
    }

    // Update is called once per frame
    void Update()
    {
        if(battleManager.GetComponent<BattleManager>().gameOver)
        {
            StoryManager.manager.GameOver();
            SceneManager.LoadScene("GameOverScene");
            return;
        }
        if (CheckIfBattleOver())
        {
            BattleManager.manager.battleIsOver = true;
            battleIsOverTimer += Time.deltaTime;
            if (battleIsOverTimer >= battleIsOverDuration)
            {
                StoryManager.manager.shouldWrite = true;
                StoryManager.manager.isFading = true;
                SceneManager.LoadScene("IntroductionScene");
            }
            return;
        }
        if(AlienWithPriority != null && BattleManager.manager.CheckIfEntityIsDead(AlienWithPriority))
        {
            if(AlienWithPriority == LorbertActive)
            {
                LorbertRest.SetActive(true);
            } else if(AlienWithPriority == ArtroActive)
            {
                ArtroRest.SetActive(true);
            } else
            {
                IORest.SetActive(true);
            }
            AlienWithPriority.SetActive(false);
            AlienWithPriority = null;
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }
        if (isDraggingSpell)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SpellIsDropped();
            }
            else
            {
                DragSpell();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {

            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hitInfo)
            {
                HandleTap(hitInfo);
            } else
            {
                Debug.Log("No hitinfo on mouseup");
            }
        }
        if (!CheckIfBattleOver())
        {
            enemy1Timer += Time.deltaTime;
            enemy2Timer += Time.deltaTime;
            enemy3Timer += Time.deltaTime;
            enemy4Timer += Time.deltaTime;

            if(enemy1Timer > enemyThreshold)
            {
                enemy1Timer = 0;
                if(!CheckIfNullOrDead(Enemy1Rest))
                {
                    List<GameObject> potentialCharacters = GetAliveCharacters();
                    List<GameObject> potentialEnemies = GetAliveEnemies();

                    Enemy1Rest.GetComponent<EnemyAI>().DecideWhatToDo(potentialCharacters, potentialEnemies, a =>
                    {
                        RelaxEnemy(Enemy1Rest);
                    });
                }  
            }
            if(enemy2Timer > enemyThreshold)
            {
                Debug.Log("Met enemy2 threshold");
                enemy2Timer = 0;
                if (!CheckIfNullOrDead(Enemy2Rest))
                {
                    List<GameObject> potentialCharacters = GetAliveCharacters();
                    List<GameObject> potentialEnemies = GetAliveEnemies();

                    Enemy2Rest.GetComponent<EnemyAI>().DecideWhatToDo(potentialCharacters, potentialEnemies, a =>
                    {
                        RelaxEnemy(Enemy2Rest);
                    });
                }
            }
            if (enemy3Timer > enemyThreshold)
            {
                enemy3Timer = 0;
                if (!CheckIfNullOrDead(Enemy3Rest))
                {
                    List<GameObject> potentialCharacters = GetAliveCharacters();
                    List<GameObject> potentialEnemies = GetAliveEnemies();

                    Enemy3Rest.GetComponent<EnemyAI>().DecideWhatToDo(potentialCharacters, potentialEnemies, a =>
                    {
                        RelaxEnemy(Enemy3Rest);
                    });
                }
            }
            if (enemy4Timer > enemyThreshold)
            {
                enemy4Timer = 0;
                if (!CheckIfNullOrDead(Enemy4Rest))
                {
                    List<GameObject> potentialCharacters = GetAliveCharacters();
                    List<GameObject> potentialEnemies = GetAliveEnemies();

                    Enemy4Rest.GetComponent<EnemyAI>().DecideWhatToDo(potentialCharacters, potentialEnemies, a =>
                    {
                        RelaxEnemy(Enemy4Rest);
                    });
                }
            }
        }
    }
}
