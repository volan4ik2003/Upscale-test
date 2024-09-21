
using UnityEngine;

    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }
        public abstract Vector2 MouseAxis { get; }

        public static Vector2 UnityInputAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        }

        public static Vector2 MouseInputAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
        }
    }



