using Asteroids.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        static BaseObject[] _asteroids; //массив для астероидов
        static BaseObject[] _stars; //массив для звёзд
        static BaseObject[] _kamets; //массив для камет
        static Bullet _bullet; //пуля
        static Ship _ship; //корабль
        static Random random = new Random();
        static Timer timer;
        static int counter = 0; //очётчик очков
        static MedHelp _med; //аптечка

        public static int Width { get; set; } //ширина экранной формы
        public static int Height { get; set; } //высота экранной формы

        public static void Init(Form form)
        {
            //графический конструктор
            Graphics g = form.CreateGraphics();
            _context = BufferedGraphicsManager.Current;
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            //таймер для отрисовки и обновления объектов в пространстве
            timer = new Timer { Interval = 60 };
            timer.Tick += OnTime;
            timer.Start();

            form.KeyDown += Form_KeyDown; ; //подписываемся на событие "нажатие кнопки"
        }

        //событие "нажатие кнопки"
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //если нажат пробел, то создаётся пуля, т.е. производится выстрел
            if(e.KeyCode == Keys.Space)
            {
                //создаём пулю относительно корабля _ship.Rect.X/Y
                _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 20), new Point(10, 0), new Size(65, 20));
            }
            //если нажата кнопка вверх, то вызываем метод Up и движемся вверх
            if (e.KeyCode == Keys.Up)
            {
                _ship.Up();
            }
            //если нажата кнопка вниз, то вызываем метод Down и движемся вниз
            if (e.KeyCode == Keys.Down)
            {
                _ship.Down();
            }
        }

        private static void OnTime(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        //отрисовка сцены с объектами
        public static void Draw()
        {
            Buffer.Graphics.DrawImage(Resources.universe, new Rectangle(0, 0, 800, 600)); //фон чёрный
            Buffer.Graphics.DrawImage(Resources.planet, new Rectangle(100, 100, 200, 200)); //планета

            //отрисовка астероидов
            foreach (BaseObject asteroid in _asteroids)
                if (asteroid != null)
                    asteroid.Draw();

            //отрисовка звёзд
            foreach (BaseObject star in _stars)
                star.Draw();

            foreach (BaseObject kameta in _kamets)
                kameta.Draw();

            //если пуля создана, то отрисовывать, т.е. если произошло событие нажатия на пробел
            if (_bullet != null) 
                _bullet.Draw();

            _ship.Draw();
            Buffer.Graphics.DrawString("Energy: " + _ship.Energy, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), Brushes.Red, 0, 0);
            Buffer.Graphics.DrawString("Score: " + counter, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), Brushes.Red, 0, 20);

            if (_ship.Energy < 50)
                _med.Draw();

            Buffer.Render();
        }

        public static void Load()
        {
            //15 астероидов, размер определяется рандомно
            _asteroids = new BaseObject[2];
            for (int i = 0; i < _asteroids.Length; i++)
            {
                int size = random.Next(5, 30);
                _asteroids[i] = new Asteroid(new Point(600, i * 20 + 10), new Point(-i - 1, -i - 1), new Size(size, size));
            }

            //50 звёзд, размеры вшиты
            _stars = new BaseObject[50];
            for (int i = 0; i < _stars.Length; i++)
            {
                int x = random.Next(10, 750);
                _stars[i] = new Star(new Point(i + 10 + x, i * 40 + 10), new Point(0, i - 1), new Size(10, 10));
            }

            //2 каметы, размер определяется рандомно
            _kamets = new BaseObject[2];
            for (int i = 0; i < _kamets.Length; i++)
            {
                int size = random.Next(30, 100);
                int y = random.Next(10, 550);
                _kamets[i] = new Kameta(new Point(800, y), new Point(i + 5, 0), new Size(size, size));
            }

            //_bullet = new Bullet(new Point(0, 300), new Point(10, 0), new Size(65, 20));
            int med_y = random.Next(10, 550);
            _med = new MedHelp(new Point(800, med_y), new Point(10, 0), new Size(40, 30));

            _ship = new Ship(new Point(0, 270), new Point(5, 5), new Size(60, 60));
            Ship.DieEvent += Ship_DieEvent; //подписка на событие смерти корабля
            Ship.WinEvent += Ship_WinEvent; //подписка на событие победы
        } 

        private static void Ship_WinEvent(object sender, EventArgs e)
        {
            timer.Stop();
            Buffer.Graphics.Clear(Color.Red);
            Buffer.Graphics.DrawString("You WIN!!! Congratulate!!!", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Underline), Brushes.Yellow, 50, 200);
            Buffer.Render();
        }

        //событие смерти корабля
        private static void Ship_DieEvent(object sender, EventArgs e)
        {
            //останавливаем таймер
            timer.Stop();
            Buffer.Graphics.Clear(Color.Black); //очищаем поле и красим фон в чёрный цвет
            //пишем Game Over
            Buffer.Graphics.DrawString("Game Over", new Font(FontFamily.GenericSansSerif, 50, FontStyle.Underline), Brushes.Red, 200, 200);
            Buffer.Render();
        }

        //обновление в пространстве
        public static void Update()
        {
            /*foreach (BaseObject asteroid in _asteroids)
            {
                asteroid.Update();
                if (_bullet != null && asteroid.Collision(_bullet))
                {
                    //добавляем звук
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer("soundasteroid.wav");
                    player.Play();
                    //если пуля сталкивается с астроидом, то её координаты сбрасываются до начальной
                    //_bullet = new Bullet(new Point(0, 300), new Point(10, 0), new Size(65, 20));
                    Debug.WriteLine("Столкновение с астероидом");
                };
            }*/

            for (int i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) //если астероид равер нулю,
                    continue; //то переходим к следующему астероиду
                
                _asteroids[i].Update();

                //если произошло с толкноверие с пулей
                if (_bullet != null && _asteroids[i].Collision(_bullet))
                {
                    //добавляем звук
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer("soundasteroid.wav");
                    player.Play();
                    //если пуля сталкивается с астроидом, то её координаты сбрасываются до начальной
                    //_bullet = new Bullet(new Point(0, 300), new Point(10, 0), new Size(65, 20));
                    Debug.WriteLine("Столкновение с астероидом");
                    _asteroids[i] = null; //обнуляем астероид
                    _bullet = null; //обнуляем пулю
                    counter++; //наращиваем очки за каждый сбитый астероид
                    continue; // переходим к следующему астероиду
                };

                //если корабль НЕ пересёкся с астероидом
                if (!_ship.Collision(_asteroids[i]))
                    continue; //то идём в следующиему
                
                _ship.EnergyLow(_asteroids[i].Rect.X / 5);
                System.Media.SystemSounds.Beep.Play();

                //если хр корабля меньше или равно 0, то корабль умирает(
                if (_ship.Energy <= 0)
                    _ship.Die();
            }

            foreach (BaseObject star in _stars)
            {
                star.Update();
                if (_bullet != null && star.Collision(_bullet))
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer("soundstar.wav");
                    player.Play();
                    Debug.WriteLine("Столкновение со звездой");
                }
            }

            foreach (BaseObject kameta in _kamets)
                kameta.Update();

            //если пуля создана, то обновлять, т.е. если произошло событие нажатия на пробел
            if (_bullet != null)
                _bullet.Update();

            //если хр корабля меньше 50, то отправлять аптечки
            if (_ship.Energy < 50)
                _med.Update();

            //если корабль столкнулся с аптечкой, то добавлять +10 к хр
            if (_ship.Collision(_med))
            { 
                _ship.EnergyHelp(10);
                int med_y = random.Next(10, 550);
                _med = new MedHelp(new Point(800, med_y), new Point(10, 0), new Size(40, 30));
            }

            //если количество убитых кораблей (очков) стало равно длине массива астероидов, то вызвать метод победы
            if (_asteroids.Length == counter)
                _ship.Win();
        }
    }
}
