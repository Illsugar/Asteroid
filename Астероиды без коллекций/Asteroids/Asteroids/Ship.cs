using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ship : BaseObject
    {
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        private int energy = 100; // хр корабля
        public static event EventHandler DieEvent; //собитие смерти корабля
        public static event EventHandler WinEvent; //событие победы корабля

        //метод для подсчёта энергии
        public void EnergyLow(int damage)
        {
            energy -= damage; // energy = energy - damage
        }

        //метод для пересчёта энергии при лечении
        public void EnergyHelp(int help)
        {
            energy += help;
        }

        //свойдство для получения текущего количества хр
        public int Energy
        {
            get { return energy; }
        }
        
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(new Bitmap(Resources.ship, new Size(Size.Width, Size.Height)), Pos.X, Pos.Y);
        }

        public override void Update()
        {
        }

        //метод для движения вверх
        public void Up()
        {
            if (Pos.Y > 0) //если мы движемся вверх, т.е. в координаты (0,0), то из начальной координаты вычитаем координату перемещение
                Pos.Y = Pos.Y - Dir.Y; //вычитаем, потому что координата (0,0) находится вверху

        }

        //метод для движения вниз
        public void Down()
        {
            if (Pos.Y < Game.Height) //если мы движемся вниз, т.е. в координаты (0,600), то к начальной координате прибавляем координату перемещение
                Pos.Y = Pos.Y + Dir.Y;
        }

        //метод окончания игры = смерти корабля
        public void Die()
        {
            if (DieEvent != null) //если на событие смерти корабля кто-то подписан
            {
                DieEvent.Invoke(this, new EventArgs()); //this - указатель на данный(этот) кораблять
            }
        }

        //метод победы корабля
        public void Win()
        {
            if (WinEvent != null)
            {
                WinEvent.Invoke(this, new EventArgs()); 
            }
        }
    }
}
