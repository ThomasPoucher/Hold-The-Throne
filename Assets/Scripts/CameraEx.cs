using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraEx
{
    public static bool IsObjectVisible(this UnityEngine.Camera @this, Renderer renderer, Vector3? offset = null)
    {
        if(offset == null)
        {
            offset = Vector3.zero;
        }
        var testBounds = renderer.bounds;
      //  testBounds.min = testBounds.min + offset.Value;
      //  testBounds.max = testBounds.min + offset.Value;
        testBounds.center = testBounds.center + offset.Value;
       // Debug.Log(testBounds);
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(@this), testBounds);
    }
}
