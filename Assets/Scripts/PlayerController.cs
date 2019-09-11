using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public float moveSpeed;
    public Animator playerAnimator;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    /** Creating an instance so there's only one player active in the game space.
     *  Also any other component that needs to read a value/state from the player
     *  will read said values from the only existing player in the game space.
     */ 
    public static PlayerController instance;

    public string areaTransitionName;

    private Vector2 velocity;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        this.moveSpeed = 10f; //This sets the move speed of the player character.
        if(instance == null) // If there's no player (like when a new game is started or when a game is loaded)
        {
            instance = this; // The instance is the first player
        }
        else // When the instance is not null (like when a scene is switched)
        {
            if(instance != this)
            {
                Destroy(gameObject); // Destroy the second player that is created
            }
            
        }

        DontDestroyOnLoad(gameObject); // gameObject is by default the object this script is tied to, i.e. the player.
                                       // We don't want to destroy our player instance when we load into another scene.
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();

        // keeping the player inside the map
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                        Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                         transform.position.z);
    }


    /**
     * Manages the animations based on velocity direction.
     *            (+Y)
     *              |
     *     (-X)-----0-----(+X)
     *              |
     *            (-Y)
     */
    private void playerMovement()
    {
        /**
         * When a player enters a dialogue (or for any other purpose) it won't be allowed to move.
         */
        if (canMove) { 
            this.velocity = playerVelocity();
        } else
        {
            playerRB.velocity = Vector2.zero;
        }
        playerAnimator.SetFloat("moveX", velocity.x);
        playerAnimator.SetFloat("moveY", velocity.y);


        if (Input.GetAxisRaw("Horizontal") == 1 || 
            Input.GetAxisRaw("Horizontal") == -1 || 
            Input.GetAxisRaw("Vertical") == 1 || 
            Input.GetAxisRaw("Vertical") == -1)
        {
            /** 
             * Also the moving animations will trigger only when the canMove variable is set to true
             *  so that the player isn't playing moving animations in place.
             */
            if (canMove)
            {
                playerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                playerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        
    }

    /**
     * Returns the player's velocity as a Vector2 object.
     * The player's velocity is composed out of 'x' and 'y' axis values.
     * These values are asked of the player through input.
     * Finally we multiply the velocity by a given float because the
     * default is too low.
     */
    private Vector2 playerVelocity() {
        return playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),
                                               Input.GetAxisRaw("Vertical"))
                                               * moveSpeed;
    }


    /**
     * Sets the threshhold the player will be able to move to on the map.
     * This is necessary because even though the player is bound to the map and cannot visually exit it,
     *  the boundaries make the player sprite clip outside the camera by a small amount of pixels.
     *  i.e. when travelling to the top(north) of the map, when the player reaches the boundary
     *       half of the sprite head will clip outside the camera view.
     */
    public void SetBounds(Vector3 bottomLeft, Vector3 topRight) {
        bottomLeftLimit = bottomLeft + new Vector3(0.5f, 0.5f, 0f);
        topRightLimit = topRight + new Vector3(-0.5f, -0.5f, 0f);
    }
}
