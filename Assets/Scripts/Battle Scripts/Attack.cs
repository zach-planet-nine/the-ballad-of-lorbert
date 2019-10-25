using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject AttackObject;
    public Vector3 startPosition;

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    private float flashTimer;
    private float flashDuration = 0.08f;
    private int flashTimes = 4;
    private bool isWhite;

    public void AttackEntity(GameObject entity, Vector3 destination, int damage)
    {
        if(BattleManager.manager.GetStatsForEntity(gameObject).isBlinded)
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if(coinToss == 0)
            {
                damage = 0;
            }
        }
        var clone = (GameObject)Instantiate(AttackObject, startPosition, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunAttack>().SetTarget(entity, destination, damage);
    }

    public void AttackEntityWithCallback(GameObject entity, Vector3 destination, int damage, Action<bool> callback)
    {
        if (BattleManager.manager.GetStatsForEntity(gameObject).isBlinded)
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if (coinToss == 0)
            {
                damage = 0;
            }
        }
        var clone = (GameObject)Instantiate(AttackObject, startPosition, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunAttack>().SetTargetWithCallback(entity, destination, damage, callback);
    }

    /*private void turnWhite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
        isWhite = true;
    }

    private void turnBack()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
        isWhite = false;
    }

    private void OnEnable()
    {
        Debug.Log("Should set color white");
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
        turnWhite();
        flashTimes = 3;
    }

    private void Update()
    {
        flashTimer += Time.deltaTime;
        if(flashTimes > 0 && flashTimer > flashDuration)
        {
            flashTimes -= 1;
            flashTimer = 0;
            if(isWhite)
            {
                turnBack();
            } else
            {
                turnWhite();
            }
        }
    }*/
}
