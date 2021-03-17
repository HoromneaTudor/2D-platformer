using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask spykeLayer;
    public LayerMask waterLayer;
    public LayerMask movingPlatfotmLayer;
    //public LayerMask nextLevelLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;
    public bool onSpykeWall;
    public bool inWater;
    public bool isTouchingLedge;
    public bool canclimbLedge = false;
    public bool ledgeDetected;
    public bool onMovingPlatform;
    //public Vector2 ledgePosBot;
    //public Vector2 ledgePos1;
    //public Vector2 ledgePos2;

    //public float ledgeClimbXOffset1;
    //public float ledgeClimbYOffset1;
    //public float ledgeClimbXOffset2;
    //public float ledgeClimbYOffset2;


    //public bool nextLevel;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset,ledgeRightOffset,ledgeLeftOffset;
    //public float ledgeGrabOffset;
    //public Vector3 colliderOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    //cred ca si ground colision and such as putea sa le pun intr-un FixedUpdate
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onSpykeWall = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, spykeLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, spykeLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, spykeLayer);
        inWater = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, waterLayer);
        //nextLevel = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, nextLevelLayer);
        isTouchingLedge = Physics2D.OverlapCircle((Vector2)transform.position + ledgeLeftOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + ledgeRightOffset, collisionRadius, groundLayer);
        //onMovingPlatform = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, movingPlatfotmLayer)
        //    || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, movingPlatfotmLayer)
        //    || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, movingPlatfotmLayer);
        //if (onMovingPlatform)
        //{
        //    GameMaster.onMovingPlatform = true;
        //}
        //else
        //    GameMaster.onMovingPlatform = false;
        wallSide = onRightWall ? -1 : 1;
        if (onWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            //ledgePosBot = this.transform.position;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset ,ledgeLeftOffset,ledgeRightOffset};

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        //Gizmos.DrawLine(transform.position, transform.position + colliderOffset + Vector3.down * collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + ledgeLeftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + ledgeRightOffset, collisionRadius);
    }
}
