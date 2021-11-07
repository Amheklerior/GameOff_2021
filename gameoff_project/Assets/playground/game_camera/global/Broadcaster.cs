using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broadcaster : MonoBehaviour {
    
    [SerializeField] private MovementDirection _direction;

    void FixedUpdate() {
        if (GetComponent<Rigidbody2D>().velocity.x > 0) _direction.Value = Direction.RIGHT;
        else if (GetComponent<Rigidbody2D>().velocity.x < 0) _direction.Value = Direction.LEFT;
        else _direction.Value = Direction.NONE;
    }

}
