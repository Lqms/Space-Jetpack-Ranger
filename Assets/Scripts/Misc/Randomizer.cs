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

    public static float RandomScreenPosition(this Camera camera)
    {
        float randomY = Random.Range(0, Screen.height);
        Vector2 screenPosition = new Vector2(0, randomY);
        Vector2 worldPosition = camera.ScreenToWorldPoint(screenPosition);
        return worldPosition.y;
    }
    /*
    public static Vector3 RandomChangePosition(Vector3 position, float maxOffsetX, float maxOffsetY)
    {
        float randomX = position.x + Random.Range(-maxOffsetX, maxOffsetX);
        float randomY = position.y + Random.Range(-maxOffsetY, maxOffsetY);

        return new Vector3(randomX, randomY);
    }
    */

    public static Vector3 RandomChange(this Vector3 position, float maxOffsetX, float maxOffsetY)
    {
        float randomX = position.x + Random.Range(-maxOffsetX, maxOffsetX);
        float randomY = position.y + Random.Range(-maxOffsetY, maxOffsetY);

        return new Vector3(randomX, randomY);
    }
}
