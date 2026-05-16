using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ViveroShalom.Data;

namespace ViveroShalom
{
    /// <summary>
    /// Servicio de notificaciones: revisa tareas pendientes para HOY y muestra globos en la bandeja del sistema.
    /// </summary>
    public static class NotificationService
    {
        private static System.Windows.Forms.Timer? _timer;
        private static NotifyIcon? _notifyIcon;

        public static void Start(NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 60_000; // cada minuto
            _timer.Tick += OnTick;
            _timer.Start();

            // Verificación inicial al iniciar
            CheckNow();
        }

        public static void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }

        private static void OnTick(object? sender, EventArgs e) => CheckNow();

        public static void CheckNow()
        {
            try
            {
                var tareas = TareaRepository.GetProximas(0); // solo hoy
                foreach (var t in tareas)
                {
                    if (t.HoraRecuerdo.HasValue)
                    {
                        var ahora = DateTime.Now.TimeOfDay;
                        var diff  = (t.HoraRecuerdo.Value - ahora).TotalMinutes;
                        if (diff >= 0 && diff <= 1)
                            Notify($"⏰ Tarea próxima",
                                   $"{t.Icono} {t.TipoAccion} – {t.NombrePlanta}\n📍 {t.Ubicacion}");
                    }
                }
            }
            catch { /* no bloquear si la BD no está disponible */ }
        }

        public static void Notify(string titulo, string mensaje)
        {
            if (_notifyIcon == null) return;
            _notifyIcon.BalloonTipTitle = titulo;
            _notifyIcon.BalloonTipText  = mensaje;
            _notifyIcon.BalloonTipIcon  = ToolTipIcon.Info;
            _notifyIcon.ShowBalloonTip(5000);
        }
    }
}
