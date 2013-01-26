using UnityEngine;
using System.Collections;

public class BitchFaceController : MonoBehaviour {

    public float maxSpeed = 1;
    public float turnSpeed = 0.1f;
    public float maxMoveAngle = 90;
    public float maxDistanceToPlayer = 1;
    CharacterController controller;
    Player player;

	void Start () {
        controller = GetComponent<CharacterController>();

        player = FindObjectOfType(typeof(Player)) as Player;
	}
	
	void Update () 
    {
        Vector3 targetPosProj = player.transform.position;
        targetPosProj.y = transform.position.y;
        float distToPlayerProj = (targetPosProj - transform.position).magnitude;
        Quaternion targetRot = Quaternion.LookRotation(targetPosProj - transform.position);
        Quaternion rot = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        transform.rotation = rot;
        float angleToTarget = Quaternion.Angle(transform.rotation, targetRot);
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        float curSpeed = maxSpeed * Mathf.Clamp((90 - angleToTarget) / 90, 0, 1) * Mathf.Clamp((distToPlayerProj-maxDistanceToPlayer) / maxDistanceToPlayer, 0, 1);
        controller.SimpleMove(forward * curSpeed * Time.deltaTime);

	}
}
