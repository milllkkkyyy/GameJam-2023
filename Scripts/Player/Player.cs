using GameJame.System;
using Godot;

namespace GameJame.Player
{
    public class Player : KinematicBody
    {
        [Export]
        private readonly int _maxSpeed = 20;

        [Export]
        private readonly int _fallAcceleration = 24;

        [Export]
        public readonly int _jumpImpluse = 18;

        [Export]
        private readonly float _acceleration = 4.5f;

        [Export]
        private readonly float _friction = 16f;

        [Export]
        private readonly float _mouseSensitivityY = 0.05f;

        private Vector3 _targetVelocity = Vector3.Zero;

        private Vector3 _direction = Vector3.Zero;

        private Camera _camera;

        public override void _Ready()
        {
            _camera = GetNode<Camera>("Pivot/Camera");
        }

        public override void _Process(float delta)
        {
            _direction = Vector3.Zero;
            var camXform = _camera.GlobalTransform;
            var inputMovementVector = new Vector2();

            if (Input.IsActionPressed("move_right"))
            {
                inputMovementVector.x += 1.0f;
            }
            if (Input.IsActionPressed("move_left"))
            {
                inputMovementVector.x -= 1.0f;
            }
            if (Input.IsActionPressed("move_back"))
            {
                inputMovementVector.y -= 1.0f;
            }
            if (Input.IsActionPressed("move_forward"))
            {
                inputMovementVector.y += 1.0f;
            }
            
            inputMovementVector = inputMovementVector.Normalized();

            // translate input vector to a forward facing vector
            _direction += -camXform.basis.z * inputMovementVector.y;
            _direction += camXform.basis.x * inputMovementVector.x;

            if (IsOnFloor() && Input.IsActionJustPressed("jump"))
            {
                _targetVelocity.y = _jumpImpluse;
            }
        }

        public override void _PhysicsProcess(float delta)
        {
            _direction.y = 0;
            _direction = _direction.Normalized();

            // calculate the horizontal velocity
            var hvel = _targetVelocity;
            hvel.y = 0;
            var target = _direction * _maxSpeed;
            var weight = _direction.Dot(hvel) > 0 ? _acceleration : _friction;
            hvel = hvel.LinearInterpolate(target, weight * delta);

            // set the velocity
            _targetVelocity.x = hvel.x;
            _targetVelocity.z = hvel.z;
            _targetVelocity.y -= _fallAcceleration * (float)delta;

            _targetVelocity = MoveAndSlide( _targetVelocity, Vector3.Up );
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
            {
                RotateY(Mathf.Deg2Rad(-mouseMotion.Relative.x * _mouseSensitivityY));
            }
        }
    }
}
