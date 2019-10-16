using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject WaterObject;
    public GameObject HealingObject;
    public GameObject AttackObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        WaterObject.SetActive(true);
    }

    private void OnDisable()
    {
        WaterObject.SetActive(false);
    }

    public void HealEntity(GameObject entity, Vector3 destination, int healing)
    {
        var clone = (GameObject)Instantiate(HealingObject, new Vector3(destination.x, destination.y + 0.7f, destination.z), Quaternion.Euler(Vector3.zero));
        clone.GetComponent<HealingWater>().SetTarget(entity, destination, healing);
    }

    public void AttackEntity(GameObject entity, Vector3 destination, int damage)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
