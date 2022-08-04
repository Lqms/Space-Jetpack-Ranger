using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Randomizer
{
    public static float RandomPositionY()
    {
        float randomY = Random.Range(0, Screen.height);
        Vector2 screenPosition = new Vector2(0, randomY);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        return worldPosition.y;
    }
}
