using UnityEngine;
using System.Collections;

public abstract class PowerState {

	// A reference to the containing class.
	protected BlobPlayer player;

	public PowerState(BlobPlayer playerObj) {
		player = playerObj;
	}

	public abstract void Awake ();

	public abstract void Update();

}