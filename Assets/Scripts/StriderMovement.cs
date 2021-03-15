using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StriderMovement : MonoBehaviour
{
	private const float c_HitDist = 1;

	[SerializeField]
	private float m_Speed, m_TurnSpeed;

	private Rigidbody m_Rigidbody;

	[SerializeField]
	private Transform m_Front, m_Back;

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		RaycastHit hitFront, hitBack;
		bool frontHit = Physics.Raycast(m_Front.position, -m_Front.up, out hitFront, c_HitDist);
		bool backHit = Physics.Raycast(m_Back.position, -m_Front.up, out hitBack, c_HitDist);
		if (frontHit || backHit)
		{
			float diff = hitFront.distance - hitBack.distance;
            //m_Rigidbody.AddTorque(transform.right * diff * m_TurnSpeed * Time.deltaTime, ForceMode.Impulse);
            //m_Rigidbody.MoveRotation(Quaternion.Euler(m_Rigidbody.rotation.eulerAngles + transform.right * diff * Time.deltaTime));
        }

		float accel = Input.GetAxisRaw("Vertical");
		m_Rigidbody.AddForce(transform.forward * accel * m_Speed * Time.deltaTime, ForceMode.Acceleration);

		float turn = Input.GetAxisRaw("Horizontal");
        //m_Rigidbody.AddTorque(transform.up * turn * m_TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
        m_Rigidbody.MoveRotation(Quaternion.Euler(m_Rigidbody.rotation.eulerAngles + transform.up * turn * m_TurnSpeed * Time.deltaTime));
    }
}
