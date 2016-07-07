
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]

public class WandController : MonoBehaviour {
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private GameObject pickup;

    public Rigidbody attachPoint;

    FixedJoint joint;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        var go = GameObject.Find("club");
        go.transform.position = attachPoint.transform.position;

        joint = go.AddComponent<FixedJoint>();
        joint.connectedBody = attachPoint;
    }
	
	// Update is called once per frame
	void Update () {
	    if (controller == null) {
            Debug.Log("Controller not initialized");
            return;
        }

        if (controller.GetPressDown(gripButton) && pickup != null) {
            pickup.transform.parent = this.transform;
            pickup.GetComponent<Rigidbody>().useGravity = false;
        }
        if (controller.GetPressUp(gripButton) && pickup != null) {
            pickup.transform.parent = null;
            pickup.GetComponent<Rigidbody>().useGravity = true;
        }
	}

    private void OnTriggerEnter(Collider collider) {
        pickup = collider.gameObject;
    }

    private void OnTriggerExit(Collider collider) {
        pickup = null;
    }
}