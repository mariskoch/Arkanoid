using UnityEngine;

public class StayAlive : MonoBehaviour
{
    private void Awake()
    {
        // this will make the object stay alive between scene switches
        DontDestroyOnLoad(this.gameObject);
    }
}
