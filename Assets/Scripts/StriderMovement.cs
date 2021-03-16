using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StriderMovement : MonoBehaviour
{
	[Header("Stats")]
	[SerializeField]
	private Bike m_BikeStats;

	private float m_Speed { get => m_BikeStats.Speed; }
	private float m_TurnSpeed { get => m_BikeStats.TurnSpeed; }
	[Header("Visuals")]
	[SerializeField]
	private Transform m_Collisions;
	[SerializeField]
	private Transform m_Model;

	private float m_ModelMove { get => m_BikeStats.ModelMove; }
	private float m_DriftBoost { get => m_BikeStats.DriftBoost; }
	private float m_AutoDrift { get => m_BikeStats.AutoDrift; }

	private float m_NormDrag { get => m_BikeStats.NormDrag; }
	private float m_BreakDrag { get => m_BikeStats.BreakDrag; }

	[Header("Particles")]
	[SerializeField]
	private ParticleSystem m_Drift;
	[SerializeField]
	private ParticleSystem m_Turbo;

	private Rigidbody m_Rigidbody;
	private float m_TurboForce;
	private DriftDir m_DriftDir;

	[Header("Map")]
	[SerializeField]
	private uint m_LastCheckpoint;
	private uint m_Checkpoint;
	[SerializeField]
	private TextMeshProUGUI m_LapCount;
	[SerializeField]
	private uint m_NumLaps;
	private int m_Lap;

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

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Checkpoint"))
		{
			var checkpoint = other.GetComponent<Checkpoint>();
			if (m_Checkpoint == checkpoint.Id)
			{
				m_Checkpoint = (checkpoint.Id + 1) % (m_LastCheckpoint + 1);
				if (checkpoint.Id == 0)
					m_LapCount.text = $"Lap {Mathi.Min(++m_Lap, (int)m_NumLaps)}/{m_NumLaps}";
			}
			else if (m_Checkpoint - 1 > checkpoint.Id || checkpoint.Id == m_LastCheckpoint)
			{
				if (m_Checkpoint == 1 && checkpoint.Id == m_LastCheckpoint)
					m_LapCount.text = $"Lap {Mathi.Min(--m_Lap, (int)m_NumLaps)}/{m_NumLaps}";
				m_Checkpoint = checkpoint.Id;
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