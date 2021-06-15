using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class MedHelp : BaseObject
    {
        public MedHelp (Point pos, Point dir, Size size) : base (pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(new Bitmap(Resources.m_med, new Size(Size.Width, Size.Height)), Pos.X, Pos.Y);
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
