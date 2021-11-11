using System;
using UnityEngine;

// TODO: 
// * Handle infiniteScrolling
// * Give a selector for axis management: BOTH, X_ONLY, Y_ONLY
// * Handle X and Y axis separately (with different parallax factors)
// * Remove the cam dependency and hook it to an event? Put a broadcaster to the camera to fire events everytime it moves?

public class ParallaxFX : MonoBehaviour {
    
    [Header("Dependencies:")]
    [SerializeField] private Transform _cam;

    [Header("Settings:")]
    [SerializeField] private ParallaxLayer[] _layers;


    void Awake() => CheckDeps();
    
    void LateUpdate() {
        if (NoLayer || NoCameraMovement) return;
        MoveLayers(CameraMovementDelta);
        StoreLastCamPosition();
    }


    #region Internals

    private Vector3 _previousCamPosition;
    private Vector2 CameraMovementDelta => _cam.position - _previousCamPosition;
    private bool NoCameraMovement => CameraMovementDelta == Vector2.zero;
    private bool NoLayer => _layers.Length == 0;

    private void CheckDeps() {
        if (!_cam) _cam = Camera.main.transform;
        if (!_cam) throw new NullReferenceException("ParallaxFX: No reference to the scene camera has been provided!");
        if (NoLayer) Debug.LogWarning("ParallaxFX: No layers!");
    }

    private void MoveLayers(Vector2 delta) {
        foreach (ParallaxLayer layer in _layers) layer.Move(delta);
    }

    private void StoreLastCamPosition() => _previousCamPosition = _cam.position;

    #endregion

}

[Serializable]
public struct ParallaxLayer {

    public string _id;
    public Transform _layerTransform;
    public float _parallaxFactor;

    public void Move(Vector2 delta) {
        Debug.Log($"Layer {_id}: Move.");
        _layerTransform.Translate(delta * _parallaxFactor);
    }

}
