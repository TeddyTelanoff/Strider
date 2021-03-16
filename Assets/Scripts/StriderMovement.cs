using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StriderMovement : MonoBehaviour
{
	[SerializeField]
	private float m_Speed;
	[SerializeField]
	private float m_TurnSpeed;
	[SerializeField]
	private Transform m_Collisions;
	[SerializeField]
	private Transform m_Model;
	[SerializeField]
	private float m_ModelMove;

	private Rigidbody m_Rigidbody;

	private void Awake() =>
		m_Rigidbody = GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		float accel = Input.GetAxis("Vertical");
		m_Rigidbody.AddForce(transform.forward * accel * m_Speed * Time.deltaTime, ForceMode.Acceleration);

		float turn = Input.GetAxis("Horizontal");
        m_Rigidbody.AddTorque(transform.up * turn * m_TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
		//m_Rigidbody.MoveRotation(Quaternion.Euler(m_Rigidbody.rotation.eulerAngles + transform.up * turn * m_TurnSpeed * Time.deltaTime));

		m_Model.localRotation = Quaternion.Euler(new Vector3(m_Collisions.localRotation.eulerAngles.x + m_ModelMove * -turn, m_Collisions.localRotation.eulerAngles.y + m_ModelMove * turn, m_Collisions.localRotation.eulerAngles.z));
	}
}
