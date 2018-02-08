using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct CameraLocation
{
    public Vector3 position_;
    public Vector3 rotation_;
    public Vector3 moveDirection_;
    public Vector3 rotationDirection_;
    public float rotationSpeed_;

    public CameraLocation(Vector3 position, Vector3 rotation, Vector3 moveDirection, Vector3 rotationDirection, float rotationSpeed)
    {
        position_ = position;
        rotation_ = rotation;
        moveDirection_ = moveDirection;
        rotationDirection_ = rotationDirection;
        rotationSpeed_ = rotationSpeed;
    }
}
