using UnityEngine;

	public interface IShooter
	{
		public int Damage { get; set; }
		public float Range { get; set; }
		public LayerMask WhatToHit { get; set; }
		public QueryTriggerInteraction ShouldHitTriggers { get; set; }
		public GameObject BulletHole { get; set; }
	}