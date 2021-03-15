using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StriderMovement : MonoBehaviour
{
	[SerializeField]
	private float m_Speed = 100, m_TurnSpeed = 0.5f;

	[SerializeField]
	private Transform m_FrontWheel, m_BackWheel;
	[SerializeField]
	private float m_CheckDist = 1.5f, m_CheckMul = 10;

	private Vector3 m_FrontWheelBottom, m_BackWheelBottom;

	private Rigidbody m_Rigidbody;

	private void Awake() =>
		m_Rigidbody = GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		m_FrontWheelBottom = m_FrontWheel.position;
		m_BackWheelBottom = m_BackWheel.position;

		if (Physics.Raycast(m_FrontWheelBottom, -transform.up, out RaycastHit frontHit, m_CheckDist) && Physics.Raycast(m_BackWheelBottom, -transform.up, out RaycastHit backHit, m_CheckDist))
		{
			transform.localRotation = Quaternion.Euler((frontHit.distance - backHit.distance) * m_CheckMul * Vector3.right + transform.localRotation.eulerAngles);
		}

		transform.localRotation = Quaternion.Euler(Vector3.up * Input.GetAxis("Horizontal") * m_TurnSpeed + transform.localRotation.eulerAngles);
		m_Rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * m_Speed);
	}

	private void OnDrawGizmos()
	{
		
	}
}
