using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StriderMovement : MonoBehaviour
{
	public Transform m_FrontWheel, m_BackWheel;
	public float m_CheckDist = 1f, m_CheckMul = 10;

	private Vector3 m_FrontWheelBottom, m_BackWheelBottom;

	private void FixedUpdate()
	{
		m_FrontWheelBottom = m_FrontWheel.position;
		m_BackWheelBottom = m_BackWheel.position;

		if (Physics.Raycast(m_FrontWheelBottom, -transform.up, out RaycastHit frontHit, m_CheckDist) && Physics.Raycast(m_BackWheelBottom, -transform.up, out RaycastHit backHit, m_CheckDist))
		{
			transform.localRotation = Quaternion.Euler((frontHit.distance - backHit.distance) * m_CheckMul * Vector3.right + transform.localRotation.eulerAngles);
		}
	}

	private void OnDrawGizmos()
	{
		
	}
}
