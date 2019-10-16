using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

public class Story : MonoBehaviour
{
    public static List<string> story = new List<string> {
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc consequat ornare sodales. Fusce vitae eros ultricies, auctor mi quis, aliquet ex. In non libero nibh. Lorem ipsum dolor",
        "bar",
        "Should be stopped here",
    "The quick brown fox jumps over the lazy dog",
    "Who's got the keys to the jeep? Vrooooom!",
    "",
    "This is going on with the scene.",
    "Adding one more dialog"
    };
    public static List<MovementDirections> directions = new List<MovementDirections>
    {
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions>
        {
            Directions.Right
        }, new List<float> {
            10.5f
        }, new List<float> {
            3.0f
        }),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions>
        {
            Directions.Down
        }, new List<float> {
            3.5f
        }, new List<float> {
            2.0f
        }),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD()
    };
}

public class MovementDirections
{
    private List<Directions> directions;
    private List<float> distances;
    private List<float> durations;

    public MovementDirections(List<Directions> directions, List<float> distances, List<float> durations)
    {
        this.directions = directions;
        this.distances = distances;
        this.durations = durations;
    }

    public static MovementDirections GetEmptyMD()
    {
        return new MovementDirections(new List<Directions> { }, new List<float> { }, new List<float> { });
    }

    public bool HasDirections()
    {
        return directions.Count > 0;
    }

    public Directions GetDirection(int index)
    {
        return directions[index];
    }

    public float GetDistance(int index)
    {
        return distances[index];
    }

    public float GetDuration(int index)
    {
        return durations[index];
    }

    public Vector3 GetDestinationTransformPosition(int index, GameObject entity)
    {
        Vector3 destinationPosition = entity.transform.position;
        Directions direction = directions[index];
        float distance = distances[index];
        switch(direction)
        {
            case Directions.Down:
                destinationPosition.y -= distance;
                break;
            case Directions.Right:
                destinationPosition.x += distance;
                break;

        }
        return destinationPosition;
    }

    public Vector2 GetVelocity(int index)
    {
        Directions direction = directions[index];
        float distance = distances[index];
        float duration = durations[index];
        Vector2 velocity = new Vector2();
        switch (direction)
        {
            case Directions.Up:
                break;
            case Directions.Down:
                velocity.y = -distance / duration;
                break;
            case Directions.Left:
                break;
            case Directions.Right:
                velocity.x = distance / duration;
                break;
        }
        return velocity;
    }
}
