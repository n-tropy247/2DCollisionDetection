using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CollisionTesting
{
    internal class BoundingBox
    {
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public float Top
        {
            get => _position.Y;
            set => _position.Y = value;
        }
        public float Bottom
        {
            get => _position.Y + Height;
            set => _position.Y = value - Height;
        }

        public float Left
        {
            get => _position.X;
            set => _position.X = value;
        }

        public float Right
        {
            get => _position.X + Width;
            set => _position.X = value - Width;
        }

        private readonly List<CollisionCell> _cells = new List<CollisionCell>();

        public Color DrawColor { get; set; }

        private Vector2 _position;

        public float Width { get; }

        public float Height { get; }

        public BoundingBox(Vector2 position, float width, float height)
        {
            _position = position;
            Width = width;
            Height = height;
        }
        
        /**
         * Return bounds of AABB.
         */
        public Rectangle Bounds => new Rectangle((int)Left, (int)Top, (int)Width, (int)Height);

        /**
         * Add cell to internal belonging tracker.
         */
        public void AddToCell(CollisionCell c)
        {
            _cells.Add(c);
        }

        /**
         * Remove all cells from internal belonging tracker and notify those cells.
         */
        public void RemoveAllCells()
        {
            foreach (var c in _cells)
            {
                c.RemoveFromCell(this);
            }

            _cells.Clear();
        }
    }
}
