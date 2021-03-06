using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //объявляем форму и её свойства
            var form = new Form()
            {
                MinimumSize = new System.Drawing.Size(1800, 600),
                MaximumSize = new System.Drawing.Size(800, 600),
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Asteroids"
            };

            Game.Init(form);
            form.Show(); //транслируем формы
            Game.Draw(); //отрисовка формы
            Application.Run(form); //приложение работает, пока форма открыта
        }
    }
}
