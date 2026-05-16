using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ViveroShalom.Data;
using ViveroShalom.Models;

namespace ViveroShalom.Data
{
    public static class TareaRepository
    {
        public static List<Tarea> GetAll(string filtroEstado = "")
        {
            var list = new List<Tarea>();
            using var conn = DatabaseHelper.GetConnection();
            string sql = "SELECT * FROM tareas";
            if (!string.IsNullOrEmpty(filtroEstado))
                sql += " WHERE estado = @estado";
            sql += " ORDER BY fecha_inicio ASC";
            using var cmd = new MySqlCommand(sql, conn);
            if (!string.IsNullOrEmpty(filtroEstado))
                cmd.Parameters.AddWithValue("@estado", filtroEstado);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(Map(r));
            return list;
        }

        public static List<Tarea> GetProximas(int dias = 7)
        {
            var list = new List<Tarea>();
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"SELECT * FROM tareas
                           WHERE estado='Pendiente' AND fecha_inicio BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL @dias DAY)
                           ORDER BY fecha_inicio ASC LIMIT 10";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@dias", dias);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(Map(r));
            return list;
        }

        public static Tarea? GetById(int id)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd  = new MySqlCommand("SELECT * FROM tareas WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var r = cmd.ExecuteReader();
            return r.Read() ? Map(r) : null;
        }

        public static int Insert(Tarea t)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"INSERT INTO tareas
                (planta_id,nombre_planta,especie_planta,tipo_accion,icono,prioridad,
                 ubicacion,fecha_inicio,fecha_fin,repeticion,hora_recuerdo,descripcion,fotos_json,estado)
                VALUES(@pid,@np,@ep,@ta,@ic,@pr,@ub,@fi,@ff,@re,@hr,@de,@fj,@es);
                SELECT LAST_INSERT_ID();";
            using var cmd = new MySqlCommand(sql, conn);
            AddParams(cmd, t);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static void Update(Tarea t)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"UPDATE tareas SET
                planta_id=@pid,nombre_planta=@np,especie_planta=@ep,tipo_accion=@ta,icono=@ic,
                prioridad=@pr,ubicacion=@ub,fecha_inicio=@fi,fecha_fin=@ff,repeticion=@re,
                hora_recuerdo=@hr,descripcion=@de,fotos_json=@fj,estado=@es
                WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            AddParams(cmd, t);
            cmd.Parameters.AddWithValue("@id", t.Id);
            cmd.ExecuteNonQuery();
        }

        public static void Delete(int id)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd  = new MySqlCommand("DELETE FROM tareas WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public static void MarcarCompletada(int id)
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd  = new MySqlCommand("UPDATE tareas SET estado='Completada' WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public static int CountPendientes()
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd  = new MySqlCommand("SELECT COUNT(*) FROM tareas WHERE estado='Pendiente'", conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        // ---- helpers ----
        private static void AddParams(MySqlCommand cmd, Tarea t)
        {
            cmd.Parameters.AddWithValue("@pid", (object?)t.PlantaId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@np",  t.NombrePlanta);
            cmd.Parameters.AddWithValue("@ep",  t.EspeciePlanta);
            cmd.Parameters.AddWithValue("@ta",  t.TipoAccion);
            cmd.Parameters.AddWithValue("@ic",  t.Icono);
            cmd.Parameters.AddWithValue("@pr",  t.Prioridad);
            cmd.Parameters.AddWithValue("@ub",  t.Ubicacion);
            cmd.Parameters.AddWithValue("@fi",  t.FechaInicio.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@ff",  t.FechaFin.HasValue ? (object)t.FechaFin.Value.ToString("yyyy-MM-dd") : DBNull.Value);
            cmd.Parameters.AddWithValue("@re",  t.Repeticion);
            cmd.Parameters.AddWithValue("@hr",  t.HoraRecuerdo.HasValue ? (object)t.HoraRecuerdo.Value.ToString(@"hh\:mm\:ss") : DBNull.Value);
            cmd.Parameters.AddWithValue("@de",  t.Descripcion);
            cmd.Parameters.AddWithValue("@fj",  t.FotosJson);
            cmd.Parameters.AddWithValue("@es",  t.Estado);
        }

        private static Tarea Map(MySqlDataReader r) => new Tarea
        {
            Id           = r.GetInt32("id"),
            PlantaId     = r.IsDBNull(r.GetOrdinal("planta_id")) ? null : r.GetInt32("planta_id"),
            NombrePlanta = r.GetString("nombre_planta"),
            EspeciePlanta= r.IsDBNull(r.GetOrdinal("especie_planta")) ? "" : r.GetString("especie_planta"),
            TipoAccion   = r.GetString("tipo_accion"),
            Icono        = r.GetString("icono"),
            Prioridad    = r.GetString("prioridad"),
            Ubicacion    = r.IsDBNull(r.GetOrdinal("ubicacion")) ? "" : r.GetString("ubicacion"),
            FechaInicio  = r.GetDateTime("fecha_inicio"),
            FechaFin     = r.IsDBNull(r.GetOrdinal("fecha_fin")) ? null : r.GetDateTime("fecha_fin"),
            Repeticion   = r.IsDBNull(r.GetOrdinal("repeticion")) ? "" : r.GetString("repeticion"),
            HoraRecuerdo = r.IsDBNull(r.GetOrdinal("hora_recuerdo")) ? null : r.GetTimeSpan("hora_recuerdo"),
            Descripcion  = r.IsDBNull(r.GetOrdinal("descripcion")) ? "" : r.GetString("descripcion"),
            FotosJson    = r.IsDBNull(r.GetOrdinal("fotos_json")) ? "" : r.GetString("fotos_json"),
            Estado       = r.GetString("estado"),
            CreadaEn     = r.GetDateTime("creada_en"),
        };
    }

    public static class PlantaRepository
    {
        public static List<Planta> GetAll()
        {
            var list = new List<Planta>();
            using var conn = DatabaseHelper.GetConnection();
            using var cmd  = new MySqlCommand("SELECT * FROM plantas WHERE activa=1 ORDER BY nombre", conn);
            using var r    = cmd.ExecuteReader();
            while (r.Read())
                list.Add(new Planta
                {
                    Id        = r.GetInt32("id"),
                    Nombre    = r.GetString("nombre"),
                    Especie   = r.IsDBNull(r.GetOrdinal("especie")) ? "" : r.GetString("especie"),
                    Ubicacion = r.IsDBNull(r.GetOrdinal("ubicacion")) ? "" : r.GetString("ubicacion"),
                    Descripcion=r.IsDBNull(r.GetOrdinal("descripcion")) ? "" : r.GetString("descripcion"),
                    Activa    = r.GetBoolean("activa"),
                });
            return list;
        }

        public static int CountTotal()
        {
            using var conn = DatabaseHelper.GetConnection();
            using var cmd  = new MySqlCommand("SELECT COUNT(*) FROM plantas WHERE activa=1", conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    public static class UsuarioRepository
    {
        public static Usuario? Login(string username, string password)
        {
            using var conn = DatabaseHelper.GetConnection();
            string sql = @"SELECT id,username,nombre,rol FROM usuarios
                           WHERE username=@u AND password_hash=SHA2(@p,256) AND activo=1";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;
            return new Usuario
            {
                Id       = r.GetInt32("id"),
                Username = r.GetString("username"),
                Nombre   = r.GetString("nombre"),
                Rol      = r.GetString("rol"),
            };
        }
    }
}
