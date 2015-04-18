using UnityEngine;
using System.Collections;
using CardboardControlDelegates;

/**
* Creating a vision raycast and handling the data from it
* Relies on Google Cardboard SDK API's
*/
public class CardboardControlBox : MonoBehaviour {
  public bool vibrateOnOrientationTilt = true;
  public KeyCode tiltKey = KeyCode.Tab;
  
  private const DeviceOrientation tiltedOrientation = DeviceOrientation.Portrait;
  private bool tiltReported = false; // triggered at the start
  
  public CardboardControlDelegate OnTilt = delegate {};

  public void Start() {
  }
  
  public void Update() {
    CheckOrientation();
  }

  private bool KeyFor(string direction) {
    return Input.GetKeyDown(tiltKey);
  }

  private void CheckOrientation() {
    if (IsTilted() || KeyFor("tilt")) {
      if (!tiltReported) ReportTilt();
      tiltReported = true;
    } else {
      tiltReported = false;
    }
  }

  private void ReportTilt() {
    OnTilt(this, new CardboardControlEvent());
    //if (debugNotificationsEnabled) Debug.Log(" *** Orientation Tilt *** ");
    if (vibrateOnOrientationTilt) Handheld.Vibrate();
  }
  
  public bool IsTilted() {
    return Input.deviceOrientation == tiltedOrientation;
  }
}