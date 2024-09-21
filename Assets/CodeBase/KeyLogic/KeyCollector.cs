using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [SerializeField] private Exit _exitCollider;
    private int KeyCollected = 0;
    public int KeyNeedToExit = 5;

    public void OnKeyCollected()
    {
        KeyCollected++;
        if (KeyCollected == KeyNeedToExit)
        {
            _exitCollider.SetWin();
        }
    }
}
