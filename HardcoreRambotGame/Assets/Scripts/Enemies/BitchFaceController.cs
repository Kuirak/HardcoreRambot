using UnityEngine;
using System.Collections;

public class BitchFaceController : MonoBehaviour {

    public float maxSpeed = 1;
    public float turnSpeed = 0.1f;
    public float maxMoveAngle = 90;
    public float maxDistanceToPlayer = 1;
    CharacterController controller;
    Player player;

    public bool selfdestruct = true;
    public float selfdestructDamage = 0;

    float jpspeed = 0;
    public float jetpackpower = 100;
    public float gravity = 10;
    public float maxfall = 10;

    public Vector3 jpVec;

	void Start () {
        controller = GetComponent<CharacterController>();

        player = FindObjectOfType(typeof(Player)) as Player;
	}
	
	void Update () 
    {
        if (!player)
            return;
        Vector3 targetPosProj = player.transform.position;
        targetPosProj.y = transform.position.y;
        float distToPlayerProj = (targetPosProj - transform.position).magnitude;
        Quaternion targetRot = Quaternion.LookRotation(targetPosProj - transform.position);
        Quaternion rot = Quaternion.Slerp(transform.rotation, targetRot, Mathf.Clamp(turnSpeed * Time.deltaTime,0,1));
        transform.rotation = rot;
        float angleToTarget = Quaternion.Angle(transform.rotation, targetRot);
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        /*
                // Jetpack
        float playerAbove = player.transform.position.y - transform.position.y;
        if (playerAbove > 4)
        {
            jpspeed += jetpackpower;

        }
        jpspeed -= gravity;
        jpspeed = 0;// Mathf.Clamp(jpspeed, -maxfall, maxfall);*/



        float curSpeed = maxSpeed * Mathf.Clamp((maxMoveAngle - angleToTarget) / maxMoveAngle, 0, 1) /* * Mathf.Clamp((distToPlayerProj-maxDistanceToPlayer) / maxDistanceToPlayer, 0, 1)*/;
        //controller.SimpleMove(forward * curSpeed * Time.deltaTime);

        Vector3 m = Vector3.zero;
        m += forward * curSpeed * Time.deltaTime + jpVec;


        //if (jpVec == Vector3.zero)
        {
            m += Physics.gravity * 1.05f * Time.deltaTime;
        }
        
        controller.Move(m);
        



	}

    void OnTriggerEnter(Collider other)
    //void OnCollisionEnter(Collision collisionInfo)
    {
        //Collider other = collisionInfo.collider;
        Transform parent = other.transform.parent;
        if (!parent)
            return;

        Affiliation player = parent.GetComponent<Affiliation>();
        if (!player)
            return;


        if (player.GetType() == typeof(Player))
        {
            if (selfdestruct)
            {
                Destroyable playerDest = Player.instance.GetComponent<Destroyable>();
                playerDest.receiveDamage(selfdestructDamage);

                Destroyable d = GetComponent<Destroyable>();
                if (d)
                    d.Die();
                else
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
