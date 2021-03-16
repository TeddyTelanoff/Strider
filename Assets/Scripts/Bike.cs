using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Bike : ScriptableObject
{
	public float Speed { get => m_Speed; }
	public float TurnSpeed { get => m_TurnSpeed; }
	public float ModelMove { get => m_ModelMove; }
	public float DriftBoost { get => m_DriftBoost; }
	public float AutoDrift { get => m_AutoDrift; }

	public float NormDrag { get => m_NormDrag; }
	public float BreakDrag { get => m_BreakDrag; }


	[Header("Stats")]
	[SerializeField]
	private float m_Speed;
	[SerializeField]
	private float m_TurnSpeed;

	[Header("Drifting")]
	[SerializeField]
	private float m_DriftBoost;
	[SerializeField]
	private float m_AutoDrift;

	[SerializeField]
	private float m_NormDrag, m_BreakDrag;

	[Header("Visuals")]
	[SerializeField]
	private float m_ModelMove;
}
