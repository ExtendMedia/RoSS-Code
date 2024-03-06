using UnityEngine;

/// <summary>
/// Blinking spaceship lights
/// </summary>
public class BlinkingLight : MonoBehaviour
{
    Light blinkingLight;
    [SerializeField] float maxIntensity = 50;
    [SerializeField] float blinkingSpeed = 50;

    void Start()
    {
        blinkingLight = GetComponent<Light>();
    }
    void Update()
    {
        blinkingLight.intensity = Mathf.PingPong(blinkingSpeed * Time.time, maxIntensity);
    }
}
