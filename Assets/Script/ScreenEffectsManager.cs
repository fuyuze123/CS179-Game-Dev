using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenEffectsManager : MonoBehaviour
{
    public static ScreenEffectsManager instance;

    [Header("Screen Shake")] //Tune this if screenshake is too much
    public Transform cameraTransform;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    [Header("Red Tint")]
    public Image redOverlay; // Fullscreen UI Image with red color and 0 alpha value at start
    public float flashDuration = 0.3f; //Tune this if screenshake is too much

    private Vector3 originalCamPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            originalCamPos = cameraTransform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerDamageEffects()
    {
        StartCoroutine(Shake());
        StartCoroutine(FlashRed());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            cameraTransform.position = originalCamPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraTransform.position = originalCamPos;
    }

    private IEnumerator FlashRed()
    {
        Color c = redOverlay.color;
        c.a = 0.5f;
        redOverlay.color = c;

        yield return new WaitForSeconds(flashDuration);

        c.a = 0f;
        redOverlay.color = c;
    }
}
