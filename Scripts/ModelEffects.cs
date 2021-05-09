using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelEffects : MonoBehaviour
{
    [SerializeField] ModelInteraction modInteract;
    [SerializeField] bool rendEnabled = true;
    bool firstRendered = false;
    Renderer rend;
    Material m_Material;
    public bool isBlinking = true;
    public bool isAnimating = true;
    public bool isPaused = false;
    [SerializeField] private float blinkSpeed = 5f;
    float colorAlpha;
    float timeSpent = 0;
    public Animator modelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        m_Material = rend.material;
        modInteract.OnInteraction += PauseEffects;
        modInteract.OnEndInteraction += ResumeEffects;
        modInteract.OnPauseToggle += ToggleEffects;
        ToggleRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstRendered)
        {
            CheckFirstRender();
            return;
        }

        if (!isPaused)
        {
            BlinkEffect();
            if (isAnimating)
                modelAnimator.speed = 1;
            else
                modelAnimator.speed = 0;
        }
        else
            modelAnimator.speed = 0;
    }

    private void BlinkEffect()
    {
        timeSpent += Time.deltaTime * blinkSpeed;
        if (isBlinking)
            colorAlpha = (Mathf.Cos(timeSpent) * 3 / 4 + 0.75f);
        else
            colorAlpha = 1f;

        m_Material.color = new Color(m_Material.color.r, m_Material.color.g, m_Material.color.b, colorAlpha);
    }

    //pauses animation and blinking
    public void PauseEffects()
    {
        isBlinking = false;
        isAnimating = false;
    }

    public void ResumeEffects()
    {
        isBlinking = true;
        isAnimating = true;
    }

    public void ToggleEffects()
    {
        isPaused = !isPaused;
    }

    public void ToggleRenderer()
    {
        rendEnabled = !rendEnabled;
        rend.enabled = rendEnabled;
    }

    private void CheckFirstRender()
    {
        if (rend.enabled)
        {
            firstRendered = true;
            ToggleRenderer();
        }
    }
}
