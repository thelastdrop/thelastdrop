﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    Transform tr;

    public GameObject m_Player_Camera;
    public GameObject m_Particle;

    [Header("Character Movement Stats")]
    [Tooltip("Character increments of speed H axis")]
    public float m_Speed_H;
    [Tooltip("Character increments of speed V axis")]
    public float m_Speed_V;
    [Tooltip("Character max total speed, counting gravity too")]
    public float m_Max_Speed;

    [Space(5), Header("Character Abilities")]
    [Tooltip("Cooldown for ability on Fire1 input")]
    public float m_CD_ability1;
    [Tooltip("Force applied to ability")]
    public float m_ability1_force;
    [Tooltip("Recoil force from ability 1 applied to player(*0-1)"), Range(0f,1f)]
    public float m_ability1_recoil;


    private float m_V_Axis1;
    private float m_H_Axis1;
    private float m_V_Axis2;
    private float m_H_Axis2;
    private Vector3 m_cam_grav_vector;
    private Vector2 m_player_applied_speed;

    private float m_last_time_ability1;

    // Use this for initialization
    void Start ()
    {
        tr = gameObject.GetComponent<Transform>();
        m_cam_grav_vector = new Vector3(Physics2D.gravity.x, Physics2D.gravity.y, 0.0f);
        if (m_Player_Camera == null) m_Player_Camera = GameObject.Find("Player_Cameras");
        m_Player_Camera.transform.rotation.SetLookRotation(m_cam_grav_vector, Vector3.up);
    }

    // Update is called once per frame
    void Update() {
        m_V_Axis1 = Input.GetAxis("Vertical");
        m_H_Axis1 = Input.GetAxis("Horizontal");
        m_V_Axis2 = Input.GetAxis("Vertical2");
        m_H_Axis2 = Input.GetAxis("Horizontal2");

/********************************************/
/*    Ability(jump, shoot, stretch ecc)     */
/********************************************/
/*
        if ( (      Input.GetButton("Fire1")                   ) &&
             (Time.time - m_last_time_ability1) > m_CD_ability1) 
        {
                m_last_time_ability1 = Time.time;
                GameObject particle = ObjectPoolingManager.Instance.GetObject(m_Particle.name);
                Dynam_Particle particleScript = particle.GetComponent<Dynam_Particle>();
                particleScript.SetLifeTime(3);
                particleScript.SetState(Dynam_Particle.STATES.LAVA);
                particleScript.rb.AddForce(new Vector2(m_H_Axis2, m_V_Axis2) * m_ability1_force);
                rb.AddForce((new Vector2(-m_H_Axis2, -m_V_Axis2) - Physics2D.gravity.normalized ).normalized * m_ability1_force * m_ability1_recoil);
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), particle.GetComponent<Collider2D>());
                particle.transform.position = tr.position;
        }

        if (Input.GetButton("Fire2"))
        {
        }
*/
        //  Debug to test input
        //        Debug.Log("H axis1: " + m_H_Axis1.ToString() + "H axis2: " +  m_H_Axis2.ToString() + "V axis1: " + m_V_Axis1.ToString() + "V axis2: " + m_V_Axis2.ToString());
    }

    void FixedUpdate()
    {
        if ( !GameManager.Instance.m_Gravity_Type )
        {   // gravity changed to the next, or previous index based on H Axis 1, check game manager
            if (m_H_Axis1 != 0.0f)
            {
                GameManager.Instance.Gravity_Change( (m_H_Axis1 > 0) ? true : false );
            }
        }
        else   // continous gravity adjustments
        {
 /*           m_player_applied_speed.Set(0.0f, 0.0f);
            if (m_H_Axis1 != 0.0f) m_player_applied_speed += new Vector2(Physics2D.gravity.y, -Physics2D.gravity.x).normalized * m_Speed_H * m_H_Axis1 * Time.fixedDeltaTime * -1;//force applied perpendiculary to gravity
            if (m_V_Axis1 != 0.0f) m_player_applied_speed += new Vector2(-Physics2D.gravity.x, -Physics2D.gravity.y).normalized * m_Speed_V * m_V_Axis1 * Time.fixedDeltaTime;
            m_player_applied_speed += rb.velocity;
            rb.velocity = (rb.velocity.magnitude > m_Max_Speed) ? (m_player_applied_speed.normalized * m_Max_Speed) : m_player_applied_speed;
            if ((m_H_Axis2 != 0.0f) && (!Input.GetButton("Fire1")))
            {
                Physics2D.gravity = Quaternion.Euler(0f, 0f, m_H_Axis2 * Time.fixedDeltaTime * 100.0f) * Physics2D.gravity;
        }
 */       }
    }

    /*  
    void LateUpdate()
    {
        // Camera Logic, changing so that camera.vector.up is opposite to gravity.vector
        if ((Physics2D.gravity.x != m_cam_grav_vector.x) ||
            (Physics2D.gravity.y != m_cam_grav_vector.y))
        {
            m_cam_grav_vector = Physics2D.gravity;
            m_Player_Camera.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), -m_cam_grav_vector);
        }
        if ( (tr.transform.position.x != m_Player_Camera.transform.position.x)  ||
             (tr.transform.position.y != m_Player_Camera.transform.position.y) )
        {
            m_Player_Camera.transform.position = new Vector3(tr.position.x, tr.position.y, m_Player_Camera.transform.position.z);
        }

    }
    */
}