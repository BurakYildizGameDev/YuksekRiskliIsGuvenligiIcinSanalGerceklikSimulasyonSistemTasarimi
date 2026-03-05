using UnityEngine;

public class Doorhinge : MonoBehaviour
{
    public HingeJoint hinge;
    JointMotor motor;
    public float angle;
    public float stopThreshold = 1f; // Durma toleransý (derece)
    private float motorForce = 75f;  // EKLENDÝ: Kapýya uygulanan maksimum kuvvet

    void Start()
    {
        motor = hinge.motor;
        motor.force = motorForce;      // Motor gücünü ayarla
        hinge.motor = motor;           // Hinge'e uygula
    }

    void Update()
    {
        angle = hinge.angle;

        if (Mathf.Abs(angle) <= stopThreshold)
        {
            motor.targetVelocity = 0f;
            hinge.useMotor = false;
        }
        else
        {
            motor.targetVelocity = -angle;
            motor.force = motorForce;      // Her frame'de güncellemek daha güvenli olabilir
            hinge.motor = motor;
            hinge.useMotor = true;
        }
    }
}
