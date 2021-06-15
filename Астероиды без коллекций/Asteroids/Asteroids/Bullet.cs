using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.Bullet, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            //если координата x > 0, то есть выходит за границы, то запускать её сначала от координаты 0 по Y
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > 800) Pos.X = 0 + Size.Width;
        }
    }
}
