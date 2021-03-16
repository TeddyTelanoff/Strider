using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Bike : ScriptableObject
{
	public float Speed { get => m_Speed; }
	public float TurnSpeed { get => m_TurnSpeed; }
	public float ModelMove { get => m_ModelMove; }
	public float MaxTurbo { get => m_MaxTurbo; }
	public float TurboBoost { get => m_TurboBoost; }
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
	private float m_MaxTurbo;
	[SerializeField]
	private float m_TurboBoost;
	[SerializeField]
	private float m_AutoDrift;

	[SerializeField]
	private float m_NormDrag, m_BreakDrag;

	[Header("Visuals")]
	[SerializeField]
	private float m_ModelMove;
}
