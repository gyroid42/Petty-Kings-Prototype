using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CameraLocation
{
    public Vector3 position_;
    public Vector3 rotation_;
    public float moveSpeed_;
    public Vector3 moveDirection_;
    public Vector3 rotationDirection_;
    public float rotationSpeed_;

    public CameraLocation(Vector3 position, Vector3 rotation, float moveSpeed, Vector3 moveDirection, Vector3 rotationDirection, float rotationSpeed)
    {
        position_ = position;
        rotation_ = rotation;
        moveSpeed_ = moveSpeed;
        moveDirection_ = moveDirection;
        rotationDirection_ = rotationDirection;
        rotationSpeed_ = rotationSpeed;
    }
}

[System.Serializable]
public struct CameraGoto
{
    public Vector3 position_;
    public Vector3 rotation_;
    public float speed_;
    public bool startMoving_;

    public CameraGoto(Vector3 position, Vector3 rotation, float speed = 0.0f, bool startMoving = true)
    {
        position_ = position;
        rotation_ = rotation;
        speed_ = speed;
        startMoving_ = startMoving;
    }

    public CameraGoto(Vector3 position, Vector3 rotation, bool startMoving = true)
    {
        position_ = position;
        rotation_ = rotation;
        speed_ = 0.0f;
        startMoving_ = startMoving;
    }
}
