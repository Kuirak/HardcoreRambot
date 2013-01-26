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

    Player player;

    private CharacterController _controller;
    private ParticleSystem[] _jetpackstreams;
	// Use this for initialization
	void Start ()
	{
	    _controller = GetComponent<CharacterController>();
	    var jetpack = transform.Find("Body").FindChild("Jetpack");
	    _jetpackstreams = jetpack.GetComponentsInChildren<ParticleSystem>();

        player = FindObjectOfType(typeof(Player)) as Player;
	}
	
	// Update is called once per frame
	void Update ()
	{
        float playerAbove = player.transform.position.y - transform.position.y;
        bool jump = (playerAbove > 4);
       
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
	        var moveDir = Vector3.up*JetpackStrength;
            moveDir += _controller.velocity.normalized;
	        moveDir *= Time.deltaTime;
	        _controller.Move(moveDir);
	        foreach (var jetpackstream in _jetpackstreams)
	        {
	            jetpackstream.emissionRate = MaxEmissionRate;
	            jetpackstream.startSpeed = Fuel /ParticleStartSpeedDivider;
	        }
	    }
        IsFlying = false;
	}
}
