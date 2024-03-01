using UnityEngine;

public class RunsCounter : MonoBehaviour
{
    void Start()
    {
        GameVariables.instance.RunsCompleted += 1;
    }
}
