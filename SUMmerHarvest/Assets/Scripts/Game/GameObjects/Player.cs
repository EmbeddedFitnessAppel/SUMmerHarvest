﻿using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour {

    public enum Direction { None, Left, Right, Up, Down };
    /// <summary>
    ///     If true, local input (actions received from <see cref="Input" /> api) is ignored.
    /// </summary>
    public bool DisableLocalInput;
    private Rigidbody body;
    public string Name { get; private set; }
    public float Speed = 12.0f;
    public int PlayerNumber;

    protected Team _team = null;
    public virtual Team Team {
        get {
            if (_team == null) {
                Debug.LogWarning("Requested Team from Player when it was not set yet (null).");
            }
            return _team;
        }
        set {
            if (_team != null) {
                throw new System.InvalidOperationException("Team was already set for Player.");
            }
            this._team = value;
        }
    }

    public Rigidbody Body {
        get { return body ?? (body = GetComponent<Rigidbody>()); }
    }

}
