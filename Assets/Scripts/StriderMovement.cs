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
	[SerializeField]
	private float m_DriftBoost;
	[SerializeField]
	private float m_AutoDrift;

	[SerializeField]
	private float m_NormDrag, m_BreakDrag;

	[SerializeField]
	private ParticleSystem m_Drift, m_Turbo;

	private Rigidbody m_Rigidbody;
	private float m_TurboForce;
	private DriftDir m_DriftDir;

	private void Awake() =>
		m_Rigidbody = GetComponent<Rigidbody>();

	private void FixedUpdate()
	{
		float accel = Input.GetAxis("Vertical");
		m_Rigidbody.AddForce(transform.forward * accel * m_Speed * Time.deltaTime, ForceMode.Acceleration);

		float turn = Input.GetAxis("Horizontal");
		if (m_DriftDir != DriftDir.None)
			turn = Mathf.Clamp(turn, Mathf.Min((int)m_DriftDir, m_AutoDrift * (float)m_DriftDir), Mathf.Max((int)m_DriftDir, m_AutoDrift * (float)m_DriftDir));
        m_Rigidbody.AddTorque(m_Collisions.up * turn * m_TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
		//m_Rigidbody.MoveRotation(Quaternion.Euler(m_Rigidbody.rotation.eulerAngles + transform.up * turn * m_TurnSpeed * Time.deltaTime));

		m_Model.localRotation = Quaternion.Euler(new Vector3(m_Collisions.localRotation.eulerAngles.x + m_ModelMove * -turn, m_Collisions.localRotation.eulerAngles.y + m_ModelMove * turn, m_Collisions.localRotation.eulerAngles.z));


		bool breaking = Input.GetButton("Break");
		m_Rigidbody.drag = breaking ? m_BreakDrag : m_NormDrag;

		if (breaking)
		{
			if (m_DriftDir != DriftDir.None)
				m_TurboForce += m_DriftBoost * Time.deltaTime * turn;
			else if (turn != 0)
			{
				m_Drift.Play();
				m_DriftDir = turn > 0 ? DriftDir.Right : DriftDir.Left;
			}
		}
		else
		{
			m_Rigidbody.AddForce(transform.forward * m_TurboForce, ForceMode.Impulse);
			m_TurboForce = 0;
			if (m_DriftDir != DriftDir.None)
			{
				m_Drift.Stop();
				m_Turbo.Play();
				m_DriftDir = DriftDir.None;
			}
		}
	}
}

public enum DriftDir
{
	None,

	Left = -1,
	Right = 1,
}