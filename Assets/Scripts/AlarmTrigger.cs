using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    public Light[] sirenLights;
    public AudioSource alarmAudio;
    public float alarmDuration = 180f;
    public bool blink = true;
    public GameObject suit;

    private bool isBlinking = false;
    private bool isAlarmActive = false;

    public float rotationSpeed = 100f; // Dönme hýzý
    private float originalVolume; // Ses seviyesi yedeði

    void Start()
    {
        if (alarmAudio != null)
            originalVolume = alarmAudio.volume;
    }

    void Update()
    {
        if (!isAlarmActive) return;

        foreach (Light light in sirenLights)
        {
            if (light != null)
                light.transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
        }
    }

    public void TriggerAlarm()
    {
        if (isAlarmActive) return;

        isAlarmActive = true;

        foreach (Light light in sirenLights)
        {
            if (light != null)
                light.enabled = true;
        }

        if (blink && !isBlinking)
        {
            isBlinking = true;
            InvokeRepeating(nameof(BlinkLights), 0f, 0.5f);
        }

        if (alarmAudio != null && !alarmAudio.isPlaying)
        {
            alarmAudio.loop = true;
            alarmAudio.volume = originalVolume; // Ses seviyesini sýfýrdan baþlat
            alarmAudio.Play();
            Invoke(nameof(ReduceAlarmVolume), 120f); // 2 dakika sonra ses azalt
        }

        Invoke(nameof(StopAlarm), alarmDuration);
    }

    void ReduceAlarmVolume()
    {
        if (alarmAudio != null)
            alarmAudio.volume = originalVolume * 0.3f; // %30'a düþür
    }

    void StopAlarm()
    {
        if (alarmAudio != null)
            alarmAudio.Stop();

        isAlarmActive = false;
        isBlinking = false;
        CancelInvoke(nameof(BlinkLights));

        foreach (Light light in sirenLights)
        {
            if (light != null)
                light.enabled = false;
        }
    }

    void BlinkLights()
    {
        foreach (Light light in sirenLights)
        {
            if (light != null)
                light.enabled = !light.enabled;
        }
    }

    public void WearSuit()
    {
        if (suit != null)
            suit.SetActive(false);
    }
}
