using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CollisionTesting
{
    internal class CollisionCell
    {

        public Color DrawColor { get; set; }

        private float Top => Position.Y;

        private float Left => Position.X;

        public List<BoundingBox> Items { get; } = new List<BoundingBox>();

        public Vector2 Position { get; }

        public CollisionCell(Vector2 pos)
        {
            Position = pos;
        }

        /**
         * Return the bounds of the cell.
         */
        public Rectangle Bounds => new Rectangle((int)Left, (int)Top, 80, 80);

        /**
         * Add an object to the cell.
         */
        public void AddToCell(BoundingBox b)
        {
            Items.Add(b);
            b.AddToCell(this);
        }

        /**
         * Remove an object from the cell
         */
        public void RemoveFromCell(BoundingBox b) => Items.Remove(b);
    }
}
