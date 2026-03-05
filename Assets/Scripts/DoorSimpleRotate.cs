using UnityEngine;

public class DoorSimpleRotate : MonoBehaviour
{
    [Header("Açýlacak Eksen")]
    public Vector3 rotationAxis = Vector3.up;

    [Header("Açýlma Ayarlarý")]
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool autoClose = false;
    public float closeDelay = 3f;

    private bool isOpening = false;
    private bool isOpen = false;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float timer;

    void Start()
    {
        startRotation = transform.localRotation;
        endRotation = Quaternion.AngleAxis(openAngle, rotationAxis) * startRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isOpen)
            {
                isOpening = true;
                isOpen = true;
                timer = 0f;
            }
        }
    }

    void Update()
    {
        if (isOpening)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, endRotation, Time.deltaTime * openSpeed);
        }

        if (autoClose && isOpen)
        {
            timer += Time.deltaTime;
            if (timer >= closeDelay)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotation, Time.deltaTime * openSpeed);
                if (Quaternion.Angle(transform.localRotation, startRotation) < 1f)
                {
                    isOpen = false;
                    isOpening = false;
                }
            }
        }
    }
}
