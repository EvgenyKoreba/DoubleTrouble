using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class HangGlider : Modifier
{
    #region Fields
    [Header("Set in Inspector: Parachute")]
    [SerializeField] private GameObject _hangGliderPrefab;
    [SerializeField] private float _fallResistance = 20f;

    [SerializeField] private Vector2 _prefabSpawnLocalPosition = new Vector2(0.5f,1.5f);

    private GameObject _instance;
    private float _defaulDrag = 1;
    #endregion

    private void Awake()
    {
        AnimatorParameter = "isHangGliderUses";
    }

    protected override void Activate()
      {
        base.Activate();
        _defaulDrag = PlayerPhysics.Drag;

        Init();
        PreparePlayer();
    }

    private void Init()
    {
        _instance = Instantiate(_hangGliderPrefab, PlayerPhysics.transform);
        _instance.transform.localPosition = _prefabSpawnLocalPosition;
    }

    private void PreparePlayer()
    {
        PlayerPhysics.NullifyVerticalVelocity();
        PlayerPhysics.Drag = _fallResistance;
        
    }

    public override void Disable()
    {
        PlayerPhysics.Drag = _defaulDrag;
        Destroy(_instance);
        base.Disable();
    }

    public void PlayerTouchedGround() => ResetActivations();
}
