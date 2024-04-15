using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PianoKey : MonoBehaviour
{
    public Color32 pressColor = new Color32(255, 0, 0, 255);
    public Color32 hoverColor = new Color32(0, 255, 0, 255);
    public AnimationCurve pressCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private Color32 defaultColor = new Color32(255, 255, 255, 255);

    [HideInInspector] public bool isPressed = false;
    private float pressDuration = 1.0f;

    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;
    }

    public void Press()
    {
        if (isPressed)
        {
            return;
        }
        isPressed = true;
        StartCoroutine(ITransitionColor(material, material.color, pressColor, 0.3f));
        StartCoroutine(IPressAnimation(pressCurve, 0.1f, pressDuration));
        StartCoroutine(IUnpressWaiter());
    }

    public void Hover()
    {
        if (isPressed)
        {
            return;
        }
        StartCoroutine(ITransitionColor(material, material.color, hoverColor, 0.1f));
    }

    public void UnHover()
    {
        if (isPressed)
        {
            return;
        }
        StartCoroutine(ITransitionColor(material, material.color, defaultColor, 0.1f));
    }

    private IEnumerator IPressAnimation(AnimationCurve pressCurve, float animationMagnifier, float pressDuration)
    {
        pressDuration -= 0.1f;
        Vector3 originalPosition = transform.localPosition;
        float time = 0f;

        while (time < pressDuration)
        {
            float displacement = (1 - pressCurve.Evaluate(time / pressDuration)) * animationMagnifier;
            transform.localPosition = originalPosition - new Vector3(0f, displacement, 0f);

            time += Time.deltaTime;
            yield return null;
        }

        // Reset the key to its original position after animation
        transform.localPosition = originalPosition;
    }


    private IEnumerator ITransitionColor(Material material, Color32 startColor, Color32 endColor, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            material.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator IUnpressWaiter()
    {
        yield return new WaitForSeconds(pressDuration);
        isPressed = false;
        StartCoroutine(ITransitionColor(material, material.color, defaultColor, 0.3f));
    }
}
