using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Asteroid : BaseObject
    {
       
       //конструктор для астероида
        public Asteroid(Point pos, Point dir, Size size) : base (pos, dir, size)
        {
        }

        //отрисовка астероида
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.meteorBrown_big4, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        //обновление астероида в пространстве (делаем его virtual для того, 
        //чтобы в наследниках его можно было переопределить)
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
