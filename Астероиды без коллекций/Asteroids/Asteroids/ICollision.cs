using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    interface ICollision
    {
        bool Collision(ICollision obj);

        //возвращает текущую позицию и размер объекта в пространстве
        Rectangle Rect { get; }
    }
}
