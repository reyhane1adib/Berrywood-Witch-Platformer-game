using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpdateMode
{
    Update,
    FixedUpdate,
    LateUpdate
}

public class Parallax : MonoBehaviour
{
    [Header("Needed Objects")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _subject;

    [Header("Config")]
    [SerializeField] private UpdateMode _updateMode = UpdateMode.LateUpdate;
    [SerializeField] private bool lockY = true;
    [SerializeField] private bool lockX = false;

    // This is the one you will see in the Inspector
    [SerializeField] [Range(0f, 1.5f)] private float _parallaxFactor = 0.8f;

    // You can leave smoothing at 1 for now
    [SerializeField] [Range(0.1f, 2f)] private float _smoothingFactor = 1f;

    private Vector3 _startingPos;

    private float TravelX => _camera.transform.position.x - _startingPos.x;
    private float TravelY => _camera.transform.position.y - _startingPos.y;

    // No more distance/clip plane nonsense – just use the slider:
    private float ParallaxFactor => _parallaxFactor;

    private float NewX => lockX ? _startingPos.x : _startingPos.x + (TravelX * ParallaxFactor * _smoothingFactor);
    private float NewY => lockY ? _startingPos.y : _startingPos.y + (TravelY * ParallaxFactor * _smoothingFactor);

    private void Start()
    {
        _startingPos = transform.position;
    }

    private void Update()
    {
        if (_updateMode != UpdateMode.Update) return;
        transform.position = new Vector3(NewX, NewY, _startingPos.z);
    }

    private void FixedUpdate()
    {
        if (_updateMode != UpdateMode.FixedUpdate) return;
        transform.position = new Vector3(NewX, NewY, _startingPos.z);
    }

    private void LateUpdate()
    {
        if (_updateMode != UpdateMode.LateUpdate) return;
        transform.position = new Vector3(NewX, NewY, _startingPos.z);
    }
}
