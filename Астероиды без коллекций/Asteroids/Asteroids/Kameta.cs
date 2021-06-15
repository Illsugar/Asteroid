using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Kameta : BaseObject
    {
        public Kameta(Point pos, Point dir, Size size) : base (pos, dir, size)
        { 
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.kameta, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            //если координата x <0, то есть выходит за границы, то запускать её сначала от координаты 800 по Y
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
