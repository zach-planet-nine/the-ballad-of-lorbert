using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialControls : MonoBehaviour
{

    public GameObject LorbertRest;
    public GameObject ArtroRest;
    public GameObject IORest;
    public GameObject LorbertActive;
    public GameObject ArtroActive;
    public GameObject IOActive;
    public GameObject Enemy;
    public GameObject EnemyActive;

    public GameObject battleManager;

    public TextAsset boxAsset;

    private GameObject AlienWithPriority;
    private GameObject SpellBeingDragged;
    private int tutorialIndex;
    private int bossHitCount;
    private int bossHitMax = 3;
    private bool bossHasAttacked;
    private bool isDraggingSpell;
    private Vector3 spellStartingPosition;
    private Vector2 lastPosition;

    private float bossTimer;
    private float bossTimerThreshold = 2.0f;

    private float battleIsOverTimer;
    private float battleIsOverDuration = 0.5f;

    private List<string> tutorial = new List<string>
    {
        "Welcome to battle in The Ballad of Lorbert!",
        "To start go ahead and tap on Lorbert.",
        "When you tap on a character they will become the active character.",
        "Once a character has become active you can tap on an enemy to attack it. Go ahead and try that now.",
        "Good! When you attack, the attack will use some of your character's stamina. The lower the stamina, the weaker the attack.",
        "Stamina and mp both recharge over time.",
        "Go ahead and try attacking with the other characters",
        "Make sure to watch your stamina as you attack. Switch characters often to keep that stamina up.",
        "Now your enemy is going to attack. As the enemy attacks your characters' hp will go down.",
        "That's where spells come in handy! Your first spell is Liquid. To use it just drag it onto a character. Go ahead and try that now.",
        "Good. Water will heal your characters over time. You can also attack with water by dragging it to an enemy. Give it a try.",
        "There you go! Spells will use mp, which also recharges over time although slower than stamina.",
        "That's it for this tutorial. You have everything you need to take out these baddies. Tap once more to go all out on this monster."
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void RenderDialog(string dialog)
    {
        GUIStyle dialogStyle = new GUIStyle();
        dialogStyle.font = (Font)Resources.Load("Orbitron-Bold");
        dialogStyle.fontSize = 32;
        Texture2D blueTexture = new Texture2D(1, 1);
        blueTexture.SetPixel(0, 0, Color.blue);
        blueTexture.Apply();
        dialogStyle.normal.background = blueTexture;
        dialogStyle.normal.textColor = Color.white;
        dialogStyle.wordWrap = true;

        GUI.Box(new Rect(150, 10, Screen.width - 300, 150), GUIContent.none, dialogStyle);
        GUI.Label(new Rect(160, 20, Screen.width - 320, 130), dialog, dialogStyle);
    }

    private void OnGUI()
    {
        if(Enemy.GetComponent<EnemyDeath>().isDead)
        {
            RenderDialog("Congratulations you won this battle!");
        }
        if(tutorialIndex >= tutorial.Count)
        {
            return;
        }
        string dialog = tutorial[tutorialIndex];
        if(dialog.Length > 0)
        {
            RenderDialog(dialog);
        }
        if(tutorialIndex == 1)
        {
            GUIStyle boxStyle = new GUIStyle();
            Texture2D boxTexture = new Texture2D(100, 200);
            boxTexture.LoadImage(boxAsset.bytes);
            boxStyle.normal.background = boxTexture;
            GUI.Box(new Rect(50, 40, 180, 300), "Here is a box", boxStyle);
        }
        
    }

    private void HandleTap(RaycastHit2D hitInfo)
    {
        switch (hitInfo.transform.gameObject.name)
        {
            case "LorbertRest":
                LorbertRest.SetActive(false);
                LorbertActive.SetActive(true);
                ArtroRest.SetActive(true);
                ArtroActive.SetActive(false);
                IORest.SetActive(true);
                IOActive.SetActive(false);
                AlienWithPriority = LorbertActive;
                break;
            case "ArtroRest":
                LorbertRest.SetActive(true);
                LorbertActive.SetActive(false);
                ArtroRest.SetActive(false);
                ArtroActive.SetActive(true);
                IORest.SetActive(true);
                IOActive.SetActive(false);
                AlienWithPriority = ArtroActive;
                break;
            case "IORest":
                LorbertRest.SetActive(true);
                LorbertActive.SetActive(false);
                ArtroRest.SetActive(true);
                ArtroActive.SetActive(false);
                IORest.SetActive(false);
                IOActive.SetActive(true);
                AlienWithPriority = IOActive;
                break;
            case "SludgeMonster":
                if (AlienWithPriority != null)
                {
                    int damage = battleManager.GetComponent<BattleManager>().EntityAttacksEntity(AlienWithPriority, Enemy);
                    Debug.Log("Deal " + damage + " damage.");
                    AlienWithPriority.GetComponent<Attack>().AttackEntity(Enemy, Camera.main.ScreenToWorldPoint(Input.mousePosition), damage);
                }
                break;
        }
    }

    void RelaxEnemy(bool finished)
    {
        Enemy.SetActive(true);
        EnemyActive.SetActive(false);
    }

    void SpellIsDropped()
    {
        isDraggingSpell = false;
        SpellBeingDragged.transform.position = spellStartingPosition;
        CircleCollider2D spellCollider = SpellBeingDragged.GetComponent<CircleCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(spellCollider.bounds.min, spellCollider.bounds.max);
        if (overlap.Length == 2)
        {
            Debug.Log(overlap.Length);
            Debug.Log(overlap[0].gameObject == SpellBeingDragged);
            Debug.Log(overlap[1].gameObject == SpellBeingDragged);
            GameObject target = overlap[0].gameObject == SpellBeingDragged ? overlap[1].gameObject : overlap[0].gameObject;
            if(target == Enemy || target == EnemyActive)
            {
                int damage = BattleManager.manager.EntityUsesLiquidToAttackEntity(AlienWithPriority, target);
                AlienWithPriority.GetComponent<Liquid>().AttackEntity(target, Camera.main.ScreenToWorldPoint(Input.mousePosition), damage);
                Debug.Log("Gonna put the cloud at");
                Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            } else
            {
                int healing = BattleManager.manager.EntityUsesLiquidToHealEntity(AlienWithPriority, target);
                AlienWithPriority.GetComponent<Liquid>().HealEntity(target, target.transform.position, healing);
                tutorialIndex += 1;
            }
            
        }
    }

    void StartDragging()
    {
        lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hitInfo)
        {
            switch (hitInfo.transform.gameObject.name)
            {
                case "LorbertLiquid":
                    isDraggingSpell = true;
                    SpellBeingDragged = hitInfo.transform.gameObject;
                    spellStartingPosition = SpellBeingDragged.transform.position;
                    break;
                case "ArtroLiquid":
                    isDraggingSpell = true;
                    SpellBeingDragged = hitInfo.transform.gameObject;
                    spellStartingPosition = SpellBeingDragged.transform.position;
                    break;
                case "IOLiquid":
                    isDraggingSpell = true;
                    SpellBeingDragged = hitInfo.transform.gameObject;
                    spellStartingPosition = SpellBeingDragged.transform.position;
                    break;
            }
        }
    }

    void DragSpell()
    {
        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = new Vector2(currentPosition.x - lastPosition.x, currentPosition.y - lastPosition.y);
        SpellBeingDragged.transform.position = new Vector3(SpellBeingDragged.transform.position.x + delta.x, SpellBeingDragged.transform.position.y + delta.y, SpellBeingDragged.transform.position.z);
        lastPosition = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemy.GetComponent<EnemyDeath>().isDead)
        {
            BattleManager.manager.battleIsOver = true;
            battleIsOverTimer += Time.deltaTime;
            if(battleIsOverTimer >= battleIsOverDuration && Input.GetMouseButtonUp(0))
            {
                StoryManager.manager.shouldWrite = true;
                StoryManager.manager.isFading = true;
                SceneManager.LoadScene("IntroductionScene");
            }
            return;
        }
        if(tutorialIndex == 9 && Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }
        if(tutorialIndex == 9 && isDraggingSpell)
        {
            if(Input.GetMouseButtonUp(0))
            {
                SpellIsDropped();
            } else
            {
                DragSpell();
            }
        }
        if(tutorialIndex >= tutorial.Count && Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }
        if(tutorialIndex >= tutorial.Count && isDraggingSpell)
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
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (tutorialIndex == 1)
            {
                if(hitInfo && hitInfo.transform.gameObject.name == "LorbertRest")
                {
                    LorbertRest.SetActive(false);
                    LorbertActive.SetActive(true);
                    ArtroRest.SetActive(true);
                    ArtroActive.SetActive(false);
                    IORest.SetActive(true);
                    IOActive.SetActive(false);
                    AlienWithPriority = LorbertActive;
                    tutorialIndex += 1;
                }
            }
            else if (tutorialIndex == 3)
            {
                if (AlienWithPriority != null && hitInfo && hitInfo.transform.gameObject.name == "SludgeMonster")
                {
                    int damage = battleManager.GetComponent<BattleManager>().EntityAttacksEntity(AlienWithPriority, Enemy);
                    Debug.Log("Deal " + damage + " damage.");
                    tutorialIndex += 1;
                    AlienWithPriority.GetComponent<Attack>().AttackEntity(Enemy, Camera.main.ScreenToWorldPoint(Input.mousePosition), damage);
                }
            } else if (tutorialIndex == 6)
            {
                if(hitInfo)
                {
                    if(hitInfo.transform.gameObject.name == "SludgeMonster")
                    {
                        bossHitCount += 1;
                    }
                    HandleTap(hitInfo);
                    if(bossHitCount >= bossHitMax)
                    {
                        tutorialIndex += 1;
                    }
                }
            } else if(tutorialIndex == 8)
            {
                if(bossHasAttacked)
                {
                    tutorialIndex += 1;
                } else
                {
                    //Enemy.SetActive(false);
                    EnemyActive.SetActive(true);
                    int damage = BattleManager.manager.EntityAttacksEntity(Enemy, AlienWithPriority);
                    EnemyActive.GetComponent<Attack>().AttackEntityWithCallback(AlienWithPriority, AlienWithPriority.transform.position, damage, RelaxEnemy);
                    bossHasAttacked = true;
                }
            } else if(tutorialIndex == 9)
            {

            }
            else if (tutorialIndex < tutorial.Count)
            {
                tutorialIndex += 1;
            }
            else if (hitInfo)
            {
                HandleTap(hitInfo);
            }
        }

        if(tutorialIndex >= tutorial.Count)
        {
            bossTimer += Time.deltaTime;
            if(bossTimer > bossTimerThreshold)
            {
                bossTimer = 0;
                EnemyActive.SetActive(true);
                int val = Randomness.GetIntBetween(0, 3);
                if(val == 3)
                {
                    Debug.Log("val is 3");
                    val = 2;
                }
                GameObject Target = LorbertRest;
                switch(val)
                {
                    case 0: Target = LorbertRest;
                        break;
                    case 1: Target = ArtroRest;
                        break;
                    case 2: Target = IORest;
                        break;
                }
                int damage = BattleManager.manager.EntityAttacksEntity(Enemy, Target);
                EnemyActive.GetComponent<Attack>().AttackEntityWithCallback(Target, Target.transform.position, damage, RelaxEnemy);
            }
        }
    }
}
