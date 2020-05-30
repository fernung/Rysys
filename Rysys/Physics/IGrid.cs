using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.ECS;
using Rysys.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rysys.Physics
{
    public interface IGrid : IComponent
    {
        ISpring[] Springs { get; set; }
        IPointMass[,] Points { get; set; }
        Vector2 Size { get; }
        Color Color { get; set; }

        void Draw(SpriteBatch spriteBatch);

        void ApplyDirectedForce(Vector3 force, Vector2 position, float radius);
        void ApplyDirectedForce(Vector3 force, Vector3 position, float radius);
        void ApplyImplosiveForce(float force, Vector2 position, float radius);
        void ApplyImplosiveForce(float force, Vector3 position, float radius);
        void ApplyExplosiveForce(float force, Vector2 position, float radius);
        void ApplyExplosiveForce(float force, Vector3 position, float radius);

        Vector2 ToVector2(Vector3 v);
    }

    public class Grid : Component, IGrid
    {
        private const float DefaultStiffness = 0.28f;
        private const float DefaultDamping = 0.06f;

        public ISpring[] Springs { get; set; }
        public IPointMass[,] Points { get; set; }
        public Vector2 Size { get; protected set; }
        public Color Color { get; set; }

        public Grid(Rectangle size, Vector2 spacing) : this(size, spacing, new Color(Color.Green, 85)) { }
        public Grid(Rectangle size, Vector2 spacing, Color color)
        {
            Size = new Vector2(size.Width, size.Height);
            Color = color;
            var springs = new List<Spring>();
            int cols = (int)(size.Width / spacing.X) + 1;
            int rows = (int)(size.Height / spacing.Y) + 1;
            Points = new PointMass[cols, rows];
            PointMass[,] fixedPoints = new PointMass[cols, rows];

            int col = 0, row = 0;
            for (float y = size.Top; y <= size.Bottom; y += spacing.Y)
            {
                for (float x = size.Left; x <= size.Right; x += spacing.X)
                {
                    Points[col, row] = new PointMass(new Vector3(x, y, 0), 1);
                    fixedPoints[col, row] = new PointMass(new Vector3(x, y, 0), 0);
                    col++;
                }
                row++;
                col = 0;
            }

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1) springs.Add(new Spring(fixedPoints[x, y], Points[x, y], 0.1f, 0.1f));
                    else if (x % 3 == 0 && y % 3 == 0) springs.Add(new Spring(fixedPoints[x, y], Points[x, y], 0.002f, 0.02f));

                    if (x > 0) springs.Add(new Spring(Points[x - 1, y], Points[x, y], DefaultStiffness, DefaultDamping));
                    if (y > 0) springs.Add(new Spring(Points[x, y - 1], Points[x, y], DefaultStiffness, DefaultDamping));
                }
            }
            Springs = springs.ToArray();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var spring in Springs) spring.Update(gameTime);
            foreach (var mass in Points) mass.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Points.GetLength(0);
            int height = Points.GetLength(1);


            for (int y = 1; y < height; y++)
            {
                for (int x = 1; x < width; x++)
                {
                    Vector2 left = new Vector2(), up = new Vector2(), p = ToVector2(Points[x, y].Position);
                    if (x > 1)
                    {
                        left = ToVector2(Points[x - 1, y].Position);
                        float thickness = y % 3 == 1 ? 3.0f : 1.0f;

                        int clampedX = Math.Min(x + 1, width - 1);
                        Vector2 mid = Vector2.CatmullRom(ToVector2(Points[x - 2, y].Position), left, p, ToVector2(Points[clampedX, y].Position), 0.5f);

                        if (Vector2.DistanceSquared(mid, (left + p) / 2) > 1)
                        {
                            spriteBatch.DrawLine(left, mid, Color, thickness);
                            spriteBatch.DrawLine(mid, p, Color, thickness);
                        }
                        else spriteBatch.DrawLine(left, p, Color, thickness);
                    }
                    if (y > 1)
                    {
                        up = ToVector2(Points[x, y - 1].Position);
                        float thickness = x % 3 == 1 ? 3.0f : 1.0f;
                        spriteBatch.DrawLine(up, p, Color, thickness);
                    }
                    if (x > 1 && y > 1)
                    {
                        Vector2 upLeft = ToVector2(Points[x - 1, y - 1].Position);
                        spriteBatch.DrawLine(0.5f * (upLeft + up), 0.5f * (left + p), Color, 1.0f);
                        spriteBatch.DrawLine(0.5f * (upLeft + left), 0.5f * (up + p), Color, 1.0f);
                    }
                }
            }
        }

        public void ApplyDirectedForce(Vector3 force, Vector2 position, float radius) => ApplyDirectedForce(force, new Vector3(position, 0), radius);
        public void ApplyDirectedForce(Vector3 force, Vector3 position, float radius)
        {
            foreach (var mass in Points)
                if (Vector3.DistanceSquared(position, mass.Position) < radius * radius)
                    mass.ApplyForce(10 * force / (10 + Vector3.Distance(position, mass.Position)));
        }
        public void ApplyImplosiveForce(float force, Vector2 position, float radius) => ApplyImplosiveForce(force, new Vector3(position, 0), radius);
        public void ApplyImplosiveForce(float force, Vector3 position, float radius)
        {
            foreach (var mass in Points)
            {
                float dist2 = Vector3.DistanceSquared(position, mass.Position);
                if (dist2 < radius * radius)
                {
                    mass.ApplyForce(10 * force * (position - mass.Position) / (100 + dist2));
                    mass.IncreaseDamping(0.6f);
                }
            }
        }
        public void ApplyExplosiveForce(float force, Vector2 position, float radius) => ApplyExplosiveForce(force, new Vector3(position, 0), radius);
        public void ApplyExplosiveForce(float force, Vector3 position, float radius)
        {
            foreach (var mass in Points)
            {
                float dist2 = Vector3.DistanceSquared(position, mass.Position);
                if (dist2 < radius * radius)
                {
                    mass.ApplyForce(100 * force * (mass.Position - position) / (10000 + dist2));
                    mass.IncreaseDamping(0.6f);
                }
            }
        }

        public Vector2 ToVector2(Vector3 v)
        {
            float factor = (v.Z + 2000) / 2000;
            Vector2 size = Settings.WorldSize;
            return (new Vector2(v.X, v.Y) - size / 2.0f) * factor + size / 2;
        }
    }
}
