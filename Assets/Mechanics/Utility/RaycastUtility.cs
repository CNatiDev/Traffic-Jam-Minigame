using UnityEngine;

/// <summary>
/// Utility class for common raycasting operations.
/// </summary>
public static class RaycastUtility
{
    /// <summary>
    /// Get the raycast hit point from the position of the cursor on the screen.
    /// </summary>
    /// <returns>The world space position where the ray hit.</returns>
    public static Vector3 GetMouseRaycastPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;

        }
        return Vector3.zero;
    }

}
