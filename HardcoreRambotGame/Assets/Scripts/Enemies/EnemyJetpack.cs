using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class EnemyJetpack : MonoBehaviour
{
    public float Fuel = 10;
    public float MaxFuel = 10;
    public float BurnRate = 10;
    public float RefillRate = 8;
    public float Offset = 10;
    public float JetpackStrength = 40;
    public bool IsFlying;

    public float ParticleStartSpeedDivider =10;
    public float MaxEmissionRate = 150;
    public float EmissionAfterBurn = 150;

    public float activationDist = 4;
    public float randomActivationDist = 20;

    public float forwardBoost = 10;

    float activationDistAdd = 0;
    float jppow = 1;

    Player player;

    bool jump = false;
    BitchFaceController bc;

    private CharacterController _controller;
    private ParticleSystem[] _jetpackstreams;
	// Use this for initialization
	void Start ()
	{
        bc = GetComponent<BitchFaceController>();
	    _controller = GetComponent<CharacterController>();
	    var jetpack = transform.Find("Body").FindChild("Jetpack");
	    _jetpackstreams = jetpack.GetComponentsInChildren<ParticleSystem>();

        player = FindObjectOfType(typeof(Player)) as Player;
        activationDistAdd = Random.value * randomActivationDist;
        jppow = Random.value * 0.2f + 0.9f;
	}
	
	// Update is called once per frame
	void Update ()
	{
        bc.jpVec = Vector3.zero;

        float playerAbove = player.transform.position.y - transform.position.y;
        
        if (!jump)
            jump = (playerAbove > activationDist + activationDistAdd);
        else
            jump = (playerAbove > -activationDist);
       
        if (Fuel + RefillRate * Time.deltaTime < MaxFuel && !jump)
        {
           
            Fuel += RefillRate * Time.deltaTime;

        }
	
	    if (!jump)
	    {
            foreach (var jetpackstream in _jetpackstreams)
            {
                if (jetpackstream.emissionRate > 0)
                {
                    jetpackstream.emissionRate -= EmissionAfterBurn * Time.deltaTime;
                }
            }
            IsFlying = false;
            return;
	    }
        
        
	    if(Fuel +0.5f > BurnRate*Time.deltaTime && Fuel>0)
	    {
	        IsFlying = true;
	        Fuel -= BurnRate*Time.deltaTime;
            var moveDir = Vector3.up * JetpackStrength * jppow;
            moveDir += _controller.velocity.normalized;

            //moveDir += transform.forward * forwardBoost;

	        moveDir *= Time.deltaTime;
            bc.jpVec = moveDir;
	        //_controller.Move(moveDir);
	        foreach (var jetpackstream in _jetpackstreams)
	        {
	            jetpackstream.emissionRate = MaxEmissionRate;
	            jetpackstream.startSpeed = Fuel /ParticleStartSpeedDivider;
	        }
	    }
        IsFlying = false;
	}
}
