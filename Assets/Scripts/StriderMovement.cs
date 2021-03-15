﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StriderMovement : MonoBehaviour
{
	[SerializeField]
	private float m_Speed, m_TurnSpeed;

	private Rigidbody m_Rigidbody;

	private void Awake() =>
		m_Rigidbody = GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		float accel = Input.GetAxis("Vertical");
		m_Rigidbody.AddForce(transform.forward * accel * m_Speed * Time.deltaTime);

		float turn = Input.GetAxis("Horizontal");
		m_Rigidbody.AddTorque(transform.up * turn * m_TurnSpeed * Time.deltaTime);
	}
}
