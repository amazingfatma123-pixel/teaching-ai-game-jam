using TMPro;
using UnityEngine;
using System;

public class AiCenter : MonoBehaviour
{



    public float companyValue = 0;
    public TextMeshProUGUI valueText;

    [SerializeField] private AudioClip positiveValueSound;
    [SerializeField] private AudioClip negativeValueSound;


    void Start()
    {
        UpdateUI();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.TryGetComponent(out Prompt prompt))
        {
            int severity = prompt.GetSeverity();

            float severityScore = CalculateSeverityScore(severity);

            //Instead of this, create an UpdateScore(float change) method in this script and call that from the AI-center script, passing in the score change.
            companyValue += severityScore;
            UpdateUI();

            Debug.Log("Severity: " + severity + ", Score: " + severityScore + ", Company Value: " + companyValue.ToString("F2"));
        }


    }



    private float CalculateSeverityScore(int severity)
    {
        severity = Math.Clamp(severity, 0, 3);

        return (-12.5f * severity) + 7.5f;
    }



    void UpdateUI()
    {
        if (valueText != null)
        {
            valueText.text = "Company Value = " + companyValue.ToString("F2");

        }


    }
}

