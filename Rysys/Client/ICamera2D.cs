using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rysys.Client
{
    public interface ICamera2D : IComponent
    {
        Matrix TransformationMatrix { get; }
        Matrix InverseMatrix { get; }

        Vector2 Position { get; set; }
        float Rotation { get; set; }
        Vector2 Zoom { get; set; }
        Vector2 Origin { get; set; }
        float X { get; set; }
        float Y { get; set; }

        void UpdateMatrices();
        Vector2 ScreenToWorld(Vector2 position);
        Vector2 WorldToScreen(Vector2 position);
    }
    public class Camera2D : Component, ICamera2D
    {
        private Matrix _transformationMatrix = Matrix.Identity;
        private Matrix _inverseMatrix = Matrix.Identity;
        private Vector2 _position = Vector2.Zero;
        private float _rotation = 0;
        private Vector2 _zoom = Vector2.One;
        private Vector2 _origin = Vector2.Zero;
        private bool _hasChanged;

        public Matrix TransformationMatrix
        {
            get
            {
                if (_hasChanged) UpdateMatrices();
                return _transformationMatrix;
            }
        }
        public Matrix InverseMatrix
        {
            get
            {
                if (_hasChanged) UpdateMatrices();
                return _inverseMatrix;
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (_position == value) { return; }
                _position = value;
                _hasChanged = true;
            }
        }
        public float Rotation
        {
            get { return _rotation; }
            set
            {
                if (_rotation == value) { return; }
                _rotation = value;
                _hasChanged = true;
            }
        }
        public Vector2 Zoom
        {
            get { return _zoom; }
            set
            {
                if (_zoom == value) { return; }
                _zoom = value;
                _hasChanged = true;
            }
        }
        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                if (_origin == value) { return; }
                _origin = value;
                _hasChanged = true;
            }
        }
        public float X
        {
            get { return _position.X; }
            set
            {
                if (_position.X == value) { return; }
                _position.X = value;
                _hasChanged = true;
            }
        }
        public float Y
        {
            get { return _position.Y; }
            set
            {
                if (_position.Y == value) { return; }
                _position.Y = value;
                _hasChanged = true;
            }
        }

        public Camera2D() { }

        public void UpdateMatrices()
        {
            var size = new Vector2(Settings.Viewport.Width, Settings.Viewport.Height);
            Position = Vector2.Clamp
            (
                Position,
                size / 2,
                Settings.WorldSize - size / 2
            );

            var positionTranslationMatrix = Matrix.CreateTranslation(new Vector3()
            {
                X = -(int)Math.Floor(_position.X),
                Y = -(int)Math.Floor(_position.Y),
                Z = 0
            });
            var rotationMatrix = Matrix.CreateRotationZ(_rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3()
            {
                X = _zoom.X,
                Y = _zoom.Y,
                Z = 1
            });
            var originTranslationMatrix = Matrix.CreateTranslation(new Vector3()
            {
                X = (int)Math.Floor(_origin.X),
                Y = (int)Math.Floor(_origin.Y),
                Z = 0
            });

            _transformationMatrix = positionTranslationMatrix * rotationMatrix * scaleMatrix * originTranslationMatrix;
            _inverseMatrix = Matrix.Invert(_transformationMatrix);

            _hasChanged = false;
        }

        public Vector2 ScreenToWorld(Vector2 position) => Vector2.Transform(position, InverseMatrix);
        public Vector2 WorldToScreen(Vector2 position) => Vector2.Transform(position, TransformationMatrix);
    }
}
