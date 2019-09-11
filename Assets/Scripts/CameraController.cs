using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;
    /**
     * The target variable gets the player's transform variables at startup (X,Y,Z position)
     */
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        // Setting the bounds of the map
        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    /**
     * The camera gets updated every frame with the player's X and Y position but not with the Z position.
     * The Z position remains the camera's default Z position because, in 3D, the camera needs to stay above the game plane(2D)
     * in order for it and any other game object in the game space to be displayed correctly.
     * The camera gets the player's X and Y position every frame so that the camera stays centered(and follows) the player constantly. 
     */ 
    // LateUpdate is called once per frame after Update. This is used to mitigate camera lag/stutterting.
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, 
                                         target.position.y, 
                                         transform.position.z);

        // Keeping the camera inside the bounds of the map
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                        Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                         transform.position.z);
    }
}
