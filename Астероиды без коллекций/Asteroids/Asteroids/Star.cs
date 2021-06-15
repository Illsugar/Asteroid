using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Star : BaseObject //наследуем звезду от астероида
    {
        //конструктр от базового класса астероид
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        //переобределяем метод отрисовки звезды
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.star3, Pos.X, Pos.Y, Size.Width, Size.Height);
            //Game.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            //Game.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        //переобределяем метод обновления звезды в пространстве
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;

            //если координата x <0 / >ширины, то есть выходит за границы, то менять знак у смещения
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;

            //если координата x <0 / >высоты, то есть выходит за границы, то менять знак у смещения
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;

        }
    }
}
