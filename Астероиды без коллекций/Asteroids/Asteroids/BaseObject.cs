using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    abstract class BaseObject : ICollision //базовый класс, от которого наследуется все остальное
    {
        protected Point Pos; //начальная координа
        protected Point Dir; //смещение при движении
        protected Size Size; //размер

        //возвращает текущую позицию и размер объекта в пространстве
        public Rectangle Rect => new Rectangle(Pos, Size);

        //конструктор для астероида
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public bool Collision(ICollision obj)
        {
            //сравниваем позицию и размер объекта с позицией и размером текущего и возвращаем true или false
            return obj.Rect.IntersectsWith(Rect);
        }

        //абстрактный класс (не содержит реализации) отрисовки астероида
        public abstract void Draw();

        //обновление астероида в пространстве (делаем его virtual для того, 
        //чтобы в наследниках его можно было переопределить)
        public abstract void Update();
    }
}
