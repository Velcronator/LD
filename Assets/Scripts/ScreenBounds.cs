using UnityEngine;

public static class ScreenBounds
{
    public static float MinX { get; private set; }
    public static float MaxX { get; private set; }
    public static float MinY { get; private set; }
    public static float MaxY { get; private set; }

    static ScreenBounds()
    {
        UpdateBounds();
    }

    public static void UpdateBounds()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
            Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

            MinX = screenBottomLeft.x;
            MaxX = screenTopRight.x;
            MinY = screenBottomLeft.y;
            MaxY = screenTopRight.y;
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }
    }
}
