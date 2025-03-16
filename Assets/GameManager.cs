using Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentLegIndex;
    public int currentArmIndex;
    public int unequippedComponents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveBodyManagerData(BodyManager bodyManager)
    {
        currentLegIndex = bodyManager.currentLegIndex;
        currentArmIndex = bodyManager.currentArmIndex;
        unequippedComponents = bodyManager.unequippedComponents;
    }

    public void LoadBodyManagerData(BodyManager bodyManager)
    {
        bodyManager.currentLegIndex = currentLegIndex;
        bodyManager.currentArmIndex = currentArmIndex;
        bodyManager.unequippedComponents = unequippedComponents;
    }
}
