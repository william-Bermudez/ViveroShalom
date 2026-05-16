using System;
using System.Drawing;
using System.Windows.Forms;

namespace ViveroShalom.Forms
{
    /// <summary>Panel de navegación lateral reutilizable.</summary>
    internal static class SharedNav
    {
        public static Panel Build(Action onInicio, Action onGestionar, Action onRegistrar, string activeBtn)
        {
            var panel = new Panel
            {
                Location  = new Point(0, 55),
                Size      = new Size(160, 555),
                BackColor = Color.FromArgb(30, 30, 30),
            };

            var btnI = MkBtn("🏠\nInicio",             20);
            var btnG = MkBtn("📅\nGestionar\nTareas", 130);
            var btnR = MkBtn("📋\nRegistrar\nTareas", 250);

            if (activeBtn == "gestionar") btnG.BackColor = Color.FromArgb(70, 130, 180);
            else if (activeBtn == "registrar") btnR.BackColor = Color.FromArgb(70, 130, 180);
            else btnI.BackColor = Color.FromArgb(70, 130, 180);

            btnI.Click += (s, e) => onInicio();
            btnG.Click += (s, e) => onGestionar();
            btnR.Click += (s, e) => onRegistrar();

            var btnCerrar = new Button
            {
                Text = "Cerrar Sesión", Location = new Point(10, 420), Size = new Size(140, 36),
                BackColor = Color.FromArgb(27,94,32), ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold), FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand,
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, e) =>
            {
                var loginF = new LoginForm();
                loginF.Show();
                Application.OpenForms[0]?.Close();
            };

            panel.Controls.AddRange(new Control[] { btnI, btnG, btnR, btnCerrar });
            return panel;
        }

        private static Button MkBtn(string text, int top) => new Button
        {
            Text=text, Location=new Point(10,top), Size=new Size(140,90),
            BackColor=Color.FromArgb(30,30,30), ForeColor=Color.White,
            Font=new Font("Segoe UI",10f), FlatStyle=FlatStyle.Flat, Cursor=Cursors.Hand,
            TextAlign=ContentAlignment.MiddleCenter,
        };
    }
}
