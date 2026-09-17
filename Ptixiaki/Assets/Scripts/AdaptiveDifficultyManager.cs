using UnityEngine;

public class AdaptiveDifficultyManager : MonoBehaviour
{
    public static AdaptiveDifficultyManager Instance;

    public bool adaptiveDifficultyEnabled = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            adaptiveDifficultyEnabled = true;

        if (Input.GetKeyDown(KeyCode.F2))
            adaptiveDifficultyEnabled = false;
    }
}
