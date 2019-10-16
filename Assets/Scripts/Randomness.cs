using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomness : MonoBehaviour
{

    public static float GetPositiveOrNegative()
    {
        float posOrNeg = Random.value;
        return posOrNeg < 0.5 ? -1 : 1;
    }

    public static float GetValueBetween(float startValue, float endValue)
    {
        float max = endValue - startValue;
        float randValue = Random.value * max;
        return startValue + randValue;
    }

    public static int GetIntBetween(int startValue, int endValue)
    {
        int max = endValue - startValue;
        float randValue = Random.value * (float)max;
        int randValueInt = (int) Mathf.Floor(randValue);
        return startValue + randValueInt;
    }
}
