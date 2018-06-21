using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour {

    public LayerMask collisionMask;

    public const float skinWidth = .015f;
    const float dstBetweenRays = 0.25f;
	
    [HideInInspector]
    public int horizontalRayCount;
    [HideInInspector]
    public int verticalRayCount;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D boxCollider;
    public BoundVertices boundVertices;

    public virtual void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public virtual void Start () {
        CalculateRaySpacing();
    }
	
    public void UpdateRayOrigins(){
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth*-2);

        boundVertices.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        boundVertices.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        boundVertices.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        boundVertices.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing(){
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth*-2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight/dstBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth/dstBetweenRays);

        horizontalRaySpacing = boundsHeight / (horizontalRayCount-1);
        verticalRaySpacing = boundsWidth / (verticalRayCount-1);
    }

    public struct BoundVertices{
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }
}
