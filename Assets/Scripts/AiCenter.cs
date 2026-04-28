using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AiCenter : MonoBehaviour
{
 
    [SerializeField] private AudioClip positiveValueSound;
    [SerializeField] private AudioClip negativeValueSound;
    [SerializeField] private ScoreUI scoreUI;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        int childCount = other.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = other.transform.GetChild(i);

            if (child.TryGetComponent(out Prompt prompt))
            {
                int severity = prompt.GetSeverity();

                float severityScore = CalculateSeverityScore(severity);

                Debug.Log("Severity: " + severity + ", Score: " + severityScore.ToString("F2"));

                ScoreUI.Instance.UpdateScore(severityScore);
            }
        }

        Destroy(other.gameObject);
    }
    private float CalculateSeverityScore(int severity)
    {
        severity = Math.Clamp(severity, 0, 3);

        return (-12.5f * severity) + 7.5f;
    }
}

