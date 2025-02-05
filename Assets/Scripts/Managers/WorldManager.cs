﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject Lorbert;
    public GameObject Artro;
    public GameObject IO;
    public static WorldManager manager;

    private bool isPerforming = false;
    private float directionDuration;
    private MovementDirections currentDirections;
    private Vector3 lorbertDestination;
    private Vector3 artroDestination;
    private Vector3 ioDestination;

    private int index;
    public int storyIndex
      
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
            StopMovingAndMoveToDestination();
            CheckForDirections(value);
            CharacterStats.characterStats.partyData.storyIndex = value;
            CharacterStats.characterStats.Save(CharacterStats.characterStats.continueFile);
            Debug.Log("Set storyIndex to " + value);
        }
    }

    private void Awake()
    {
        manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPerforming)
        {
            directionDuration -= Time.deltaTime;
            if (directionDuration < 0)
            {

                StopMovingAndMoveToDestination();
            }
        }
        
    }

    void StopMovingAndMoveToDestination()
    {
        if(isPerforming)
        {
            Debug.Log("Should stop moving");
            Lorbert.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Artro.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            IO.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            Lorbert.transform.position = lorbertDestination;
            Artro.transform.position = artroDestination;
            IO.transform.position = ioDestination;

            Camera.main.transform.position = new Vector3(Lorbert.transform.position.x, Lorbert.transform.position.y, Camera.main.transform.position.z);

            isPerforming = false;
        }
        
    }

    void CheckForDirections(int newIndex)
    {
        MovementDirections directions = Story.directions[newIndex];
        if(directions.HasDirections())
        {
            Debug.Log("Perform directions here");
            currentDirections = directions;
            PerformDirections(directions);
        } else
        {
            Debug.Log("No directions for " + newIndex);
        }
    }

    void PerformDirections(MovementDirections directions)
    {
        isPerforming = true;

        if(Directions.Down.Equals(Directions.Position))
        {
            Debug.Log("what the fuck");
        } else
        {
            Debug.Log("This makes sense");
        }
        if(Directions.Right.Equals(Directions.Position))
        {
            Debug.Log("Double fuck");
        } else
        {
            Debug.Log("This makes sense too.");
        }

        if(directions.GetDirection(0).Equals(Directions.Position))
        {
            Debug.Log("Getting into position");
            Debug.Log(directions.GetDirection(0));
            Vector3 artroOffset = Artro.transform.position - Lorbert.transform.position;
            Vector3 ioOffset = IO.transform.position - Lorbert.transform.position;
            Lorbert.transform.position = new Vector3(directions.GetDistance(0), directions.GetDistance(1), Lorbert.transform.position.z);
            Artro.transform.position = Lorbert.transform.position + artroOffset;
            IO.transform.position = Lorbert.transform.position + ioOffset;
            Camera.main.transform.position = new Vector3(Lorbert.transform.position.x, Lorbert.transform.position.y, Camera.main.transform.position.z);
            lorbertDestination = Lorbert.transform.position;
            artroDestination = Artro.transform.position;
            ioDestination = IO.transform.position;
        }
        else {
            Vector2 velocity = directions.GetVelocity(0);
            directionDuration = directions.GetDuration(0);
            Debug.Log(directionDuration);
            Lorbert.GetComponent<Rigidbody2D>().velocity = velocity;
            Artro.GetComponent<Rigidbody2D>().velocity = velocity;
            IO.GetComponent<Rigidbody2D>().velocity = velocity;
            lorbertDestination = currentDirections.GetDestinationTransformPosition(0, Lorbert);
            artroDestination = currentDirections.GetDestinationTransformPosition(0, Artro);
            ioDestination = currentDirections.GetDestinationTransformPosition(0, IO);
        }
    }
}
