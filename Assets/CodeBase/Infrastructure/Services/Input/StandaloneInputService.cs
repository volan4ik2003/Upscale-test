using UnityEngine;

    public class StandaloneInputService : InputService
{
    public override Vector2 Axis => UnityInputAxis();

    public override Vector2 MouseAxis => MouseInputAxis();
}


