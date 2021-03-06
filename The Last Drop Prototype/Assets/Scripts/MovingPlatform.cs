﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	[Header("Movement speed"), Range(0f, 2f)]
    public float m_speed = 0.75f;
    [Header("Movement stops at extremes for this time:")]
    public float m_pause = 1;
	public int m_mov_verse = 1;
	private float tspeed;

	private Transform tr;
    private Transform starttr;
    private Transform endtr;

	void Start () {
		tr = GetComponent<Transform>().GetChild(2);
		starttr = tr.parent.GetChild(0);
        starttr.position = tr.position;
		endtr = tr.parent.GetChild(1);
		tspeed = m_speed;

	}
	
	void FixedUpdate () {
		float step = m_speed * Time.deltaTime;
		if(m_mov_verse == 1)
		{
		  	tr.position = Vector2.MoveTowards(tr.position, endtr.position, step);
		}
		else
		{
			tr.position = Vector2.MoveTowards(tr.position, starttr.position, step);
		}

		if(tr.position == starttr.position || tr.position == endtr.position)
		{
			m_mov_verse *= -1;
			m_speed = 0;
			StartCoroutine(Wait());
		}
	}

	IEnumerator Wait()
    {
       	yield return new WaitForSeconds(m_pause);
		m_speed = tspeed;
    }
}
