using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3,
    Position = 4
}

public class Story : MonoBehaviour
{
    public static List<string> story = new List<string> {
        "Oh no! It appears that the station is under attack. Quick Lorbert and team, come over here.",
        "The station is under attack from the Seventians. They're throwing everything they have at us!",
        "They're sending landers and breaching our hull all over! We need to act quickly if we're not going to get overrun.",
    "Do you remember your training? You lot are the only combat trained ones on board. We're counting on you to take them down.",
    "Do you remember or do you need a refresher?",
    "",
    "Alright good. Now I know you're ready for battle. We've gotten reports that the Seventians have invaded the science lab, the hangar, the observation room, the greenhouse, the recycling center, and the engine room.",
    "We need you to head on in there and clear out all the bad guys. Shouldn't be too hard for you if you keep your wits about you.",
    "Follow me. This portal over here will take you to any of the six locations. You can go in any order, just make sure you do it quickly.",
    "Oh I almost forgot. The station's been picking up some Nineum during its researching here. I'll let you borrow this Nineum. It'll help you take down those bad guys.",
    "", // 10
    "Outside of battle you can adjust your Nineum whenever you want, just press the inventory button on your HUD.",
    "Here's the hydroponics room",
    "",
    "Here's the Hangar",
    "",
    "Here's the Engine Room",
    "",
    "Here's the Observation Room",
    "",
    "Here's the Recycling Center", // 20
    "",
    "Here's the Science Lab",
    "",
    "Now that you've checked out all of the rooms. It's time to get this party started. Go ahead and tap once more to get the selection screen.",
    "SelectionScreen",
    "",
    "",
    "",
    "",
    "Head Scientist: Oh Lorbert you're here!", // 30 Science Lab
    "Head Scientist: Things have been quite crazy here. We're under attack from the Seventians.",
    "Head Scientist: Lookout! There's some enemies now!",
    "ScienceLabScene1",
    "Head Scientist: Oh you did it! Those Seventians are tough aren't they?",
    "ScienceLabScene2",
    "Head Scientist: Hoo boy! They just keep coming don't they? Are you sure you've got the right Nineum equipped?",
    "",
    "",
    "",
    "Pilot: Well shoot me sideways! Some help.", // 40 Hangar
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "Young Scientist: It's beautiful isn't it.", // 50 Observation Room
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "Gardener: My plants! What'll happen to my plants?!", // 60 Hydroponics
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "RecycleBOT: Beep boop recycleBOT online!", // 70 Recycling Center
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "Scottie: I can't Lorbert. There's not enough Power!", // 80 Engine Room
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "", // 90
    ""
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
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            17.17f, 0.56f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(), 
        MovementDirections.GetEmptyMD(), // 10
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            7.17f, -34.56f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -91.3f, 36.3f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -97.5f, -1.2f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -89.4f, -42.9f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(), // 20
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -142.7f, -84.6f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -4.2f, -83.0f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -4.2f, -83.0f
            },
            new List<float>
            {
                -1.0f
            }),  // 30 Science Lab
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -4.2f, -83.0f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -4.2f, -83.0f
            },
            new List<float>
            {
                -1.0f
            }),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -91.3f, 36.3f
            },
            new List<float>
            {
                -1.0f
            }), // 40 Hangar
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -89.4f, -42.9f
            },
            new List<float>
            {
                -1.0f
            }), // 50 Observation Room
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            7.17f, -34.56f
            },
            new List<float>
            {
                -1.0f
            }),  // 60 Hydroponics
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -142.7f, -84.6f
            },
            new List<float>
            {
                -1.0f
            }), // 70 Recycling Center
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        new MovementDirections(new List<Directions> {
            Directions.Position
        }, new List<float>
            {
            -97.5f, -1.2f
            },
            new List<float>
            {
                -1.0f
            }), // 80 Engine Room
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(), // 90
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
