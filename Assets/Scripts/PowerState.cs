using UnityEngine;
using UnityEngine.UI;

public abstract class PowerState {

	// A reference to the containing class.
	protected BlobPlayer player;

	public PowerState(BlobPlayer playerObj) {
		player = playerObj;
	}

    public void Awake() {
        // Display current power onscreen
        Text lowText = GameObject.Find("LowCanvas").GetComponent<Text>();
        lowText.text = "Left mouse: " + player.currentState.ToString();

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