﻿using System.Collections;
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
    "ScienceLabScene3",
    "Head Scientist: Yeehaw that's how we do it! Keep the hits coming!",
    "ScienceLabScene4",
    "Head Scientist: Looks like you've almost got them all. Keep going Lorbert!", // 40
    "ScienceLabScene5",
    "Head Scientist: Oh no, this doesn't look good. Make sure you've got that Nineum equipped before taking these guys down.",
    "ScienceLabScene6",
    "Head Scientist: Good job clearing out the science lab! Those were some nasty bad guys.",
    "Head Scientist: I've been messing around with a new spell, and I think it's ready to try out. Here you go.",
    "LearnedSpell Solid",
    "Head Scientist: There you go! That should help you clear out the rest of the bad guys around here.",
    "Head Scientist: Now you should head back to the bridge and decide where to go next.",
    "SelectionScreen",
    "Pilot: Well shoot me sideways! Some help.", // 50 Hangar
    "Pilot: We've been battling these Seventians for hours, but we ain't gettin' no where.",
    "Pilot: Lorbert hopefully you'll have better luck than us. Go get 'em!",
    "HangarScene1",
    "Pilot: That's what I'm talkin' about. Be careful these next guys use lots of magic.",
    "HangarScene2",
    "Pilot: Way to teach 'em a lesson. It looks like it's starting to thin out. Keep it up!",
    "HangarScene3",
    "Pilot: There we go. Now comes some of their minions. Be careful with them!",
    "HangarScene4",
    "Pilot: Awwww yeah that's the good stuff there!", // 60
    "HangarScene5",
    "Pilot: Looks like we're starting to neutralize the threat. Oh no that's a big one!",
    "HangarScene6",
    "Pilot: Wow! You did it! You actually did it. Cleared out the hangar. Nice job.",
    "Pilot: We've got a new spell tech here for you if you want it.",
    "LearnedSpell Gas",
    "Pilot: Those Seventians don't stand a chance now!",
    "Pilot: Get back to the bridge and figure out where to clear out next.",
    "SelectionScreen",
    "Young Scientist: It's beautiful isn't it.", // 70 Observation Room
    "Young Scientist: The view from this room. The stars and this sun.",
    "Young Scientist: Too bad these Seventians are ruining it! Go get 'em Lorbert!",
    "ObservationRoomScene1",
    "Young Scientist: They're ruining the ambience. Take 'em out!",
    "ObservationRoomScene2",
    "Young Scientist: Can't we catch a break. There's still more of 'em. Keep it up!",
    "ObservationRoomScene3",
    "Young Scientist: Good work Lorbert it looks like they may be... oh oh no!",
    "ObservationRoomScene4",
    "Young Scientist: *Huff* *puff* We're definitely getting a work out here huh Lorbert?", // 80
    "ObservationRoomScene5",
    "Young Scientist: How about that? It looks like there's just that one more shadowy guy over there.",
    "ObservationRoomScene6",
    "Young Scientist: Alright Lorbert! You cleared out all of the Seventians from here.",
    "Young Scientist: Now I can enjoy the view again. Thank you Lorbert.",
    "Young Scientist: Head on back to the bridge now and see where you need to take care of next.",
    "SelectionScreen",
    "",
    "",
    "Gardener: My plants! What'll happen to my plants?!", // 90 Hydroponics
    "Gardener: The Seventians are sending their own killer plants to try and take them out!",
    "Gardener: I need your help Lorbert. You've got to save my plants!!",
    "HydroponicsScene1",
    "Gardener: That's a good start, but there's still a ton of bad guys by my plants.",
    "HydroponicsScene2",
    "Gardener: Wow you're really taking it to them. Watch out! There's more!",
    "HydroponicsScene3",
    "Gardener: They're coming after my plants because my plants have special powers. Be careful you don't get pollened!",
    "HydroponicsScene4",
    "Gardener: These guys will tangle ya. Be careful and keep your defenses up!", // 100
    "HydroponicsScene5",
    "Gardener: Phew looks like we're almost done here. Just have that one more... oh no! It's growing huge!!",
    "HydroponicsScene6",
    "Gardener: Hoo boy that was quite an ordeal. Hopefully you got some good Nineum out of it.",
    "Gardener: To help you out I'll teach you this spell I've been working on. It harnesses the power of gas.",
    "LearnedSpell Gas",
    "Gardener: I hope this spell helps you to clear out these Seventians from the rest of the ship.",
    "Gardener: Now you should be heading back to find out where to clear out next.",
    "SelectionScreen",
    "",
    "RecycleBOT: Beep boop recycleBOT online!", // 110 Recycling Center
    "RecycleBOT: I detect multiple errors in the Recycling Center. Looks like Seventians.",
    "RecycleBOT: Lorbert your attack power is much greater than mine. Please help clearing them out.",
    "RecyclingCenterScene1",
    "RecycleBOT: Looks like you're taking out the garbage. Watch out!",
    "RecyclingCenterScene2",
    "RecycleBOT: Keep it up! Those Seventians don't stand a chance.",
    "RecyclingCenterScene3",
    "RecycleBOT: They just keep coming! You can do it Lorbert and team!",
    "RecyclingCenterScene4",
    "RecycleBOT: *BEEP* My power core is depleting, but you're doing well. To battle *BOOP*!", // 120
    "RecyclingCenterScene5",
    "RecycleBOT: Looks like there's just one more. It's a big one though! Look out!",
    "RecyclingCenterScene6",
    "RecycleBOT: RecycleBOT is impressed with your skills Lorbert and team.",
    "RecycleBOT: RecycleBOT has a spell for you to use.",
    "LearnedSpell Plasma",
    "RecycleBOT: Now you'll have no trouble with the rest of the Seventians.",
    "RecycleBOT: Head back to the bridge and clear them all out.",
    "SelectionScreen",
    "",
    "Scottie: I can't Lorbert. There's not enough Power!", // 130 Engine Room
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "", // 140
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    ""  // 150
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
        MovementDirections.GetEmptyMD(), // 40
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
            -91.3f, 36.3f
            },
            new List<float>
            {
                -1.0f
            }), // 50 Hangar
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(), // 60
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
            }), // 70 Observation Room
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(), // 80
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
            }),  // 90 Hydroponics
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(), // 100
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
            }), // 110 Recycling Center
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(), // 120
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
            }), // 130 Engine Room
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(), // 140
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD(),
        MovementDirections.GetEmptyMD()   // 150

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
