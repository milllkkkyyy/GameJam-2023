using Godot;
using System;


namespace GameJame.System 
{
    public class PlayerCamera : Spatial
    {
        [Export]
        private readonly float _mouseSensitivityX;

        public override void _Ready()
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
            {
                RotateX(Mathf.Deg2Rad(mouseMotion.Relative.y * _mouseSensitivityX * -1));

                var cameraRotation = RotationDegrees;
                cameraRotation.x = Mathf.Clamp(cameraRotation.x, -70, 70);
                RotationDegrees = cameraRotation;
            }
        }
    }
}