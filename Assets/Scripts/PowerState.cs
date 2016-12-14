using UnityEngine;
using UnityEngine.UI;

/*

PowerState is the abstract class extended by the PowerState inner classes in BlobPlayer.

Written by: Patrick Lathan
(C) 2016

*/

public abstract class PowerState {

	// A reference to the containing class.
	protected BlobPlayer player;

	public PowerState(BlobPlayer playerObj) {
		player = playerObj;
	}

    // Default Awake() method wipes changes from other states
    public virtual void Awake() {
		//this little bit by Elena :) 
        // Display current power onscreen 
        Text lowText = GameObject.Find("LowCanvas").GetComponent<Text>();
        lowText.text = "Current Power: " + player.currentState.ToString();

        // Disable water particles and sound
        player.waterParticleSystem.Stop();
        player.source.Stop();
        // Disable vapor "floatiness"
        player.gravity = BlobPlayer.BASEGRAVITY;
        // Ice blocks are allowed to remain when state is switched
    }

	public abstract void Update();

    public override abstract string ToString();

}