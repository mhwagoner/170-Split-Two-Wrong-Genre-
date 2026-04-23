using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private SpriteMask mask;

    public void EnableFlashlight()
    {
        mask.enabled = true;
    }

}
