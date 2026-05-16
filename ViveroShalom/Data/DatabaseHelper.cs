using System;
using MySql.Data.MySqlClient;

namespace ViveroShalom.Data
{
    public static class DatabaseHelper
    {
        // =====================================================================
        // CONFIGURACIÓN DE CONEXIÓN – ajusta Host, Port, Database, User, Password
        // =====================================================================
        private const string Host     = "localhost";
        private const int    Port     = 3306;
        private const string Database = "vivero_shalom";
        private const string User     = "root";
        private const string Password = "1234";          // ← cambia tu contraseña

        private static string ConnectionString =>
            $"Server={Host};Port={Port};Database={Database};Uid={User};Pwd={Password};CharSet=utf8mb4;";

        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        // Crea la base de datos y tablas si no existen
        public static void Initialize()
        {
            try
            {
                // Primero conectar sin base de datos para crearla
                string initCS = $"Server={Host};Port={Port};Uid={User};Pwd={Password};CharSet=utf8mb4;";
                using var conn = new MySqlConnection(initCS);
                conn.Open();

                string createDb = $"CREATE DATABASE IF NOT EXISTS `{Database}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";
                new MySqlCommand(createDb, conn).ExecuteNonQuery();
                conn.ChangeDatabase(Database);

                string createUsuarios = @"
                    CREATE TABLE IF NOT EXISTS usuarios (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        username VARCHAR(60) NOT NULL UNIQUE,
                        password_hash VARCHAR(255) NOT NULL,
                        nombre VARCHAR(100),
                        rol ENUM('admin','operario') DEFAULT 'operario',
                        activo TINYINT(1) DEFAULT 1,
                        creado_en DATETIME DEFAULT CURRENT_TIMESTAMP
                    );";

                string createPlantas = @"
                    CREATE TABLE IF NOT EXISTS plantas (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        nombre VARCHAR(100) NOT NULL,
                        especie VARCHAR(100),
                        ubicacion VARCHAR(150),
                        descripcion TEXT,
                        activa TINYINT(1) DEFAULT 1,
                        registrada_en DATETIME DEFAULT CURRENT_TIMESTAMP
                    );";

                string createTareas = @"
                    CREATE TABLE IF NOT EXISTS tareas (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        planta_id INT,
                        nombre_planta VARCHAR(100),
                        especie_planta VARCHAR(100),
                        tipo_accion VARCHAR(80) NOT NULL,
                        icono VARCHAR(10) DEFAULT '🌿',
                        prioridad ENUM('Alta','Media','Baja') DEFAULT 'Media',
                        ubicacion VARCHAR(150),
                        fecha_inicio DATE NOT NULL,
                        fecha_fin DATE,
                        repeticion VARCHAR(50),
                        hora_recuerdo TIME,
                        descripcion TEXT,
                        fotos_json TEXT,
                        estado ENUM('Pendiente','Completada','Cancelada') DEFAULT 'Pendiente',
                        creada_en DATETIME DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (planta_id) REFERENCES plantas(id) ON DELETE SET NULL
                    );";

                new MySqlCommand(createUsuarios, conn).ExecuteNonQuery();
                new MySqlCommand(createPlantas,  conn).ExecuteNonQuery();
                new MySqlCommand(createTareas,   conn).ExecuteNonQuery();

                // Usuario admin por defecto si no existe
                string insertAdmin = @"
                    INSERT IGNORE INTO usuarios (username, password_hash, nombre, rol)
                    VALUES ('admin', SHA2('admin123', 256), 'Administrador', 'admin');";
                new MySqlCommand(insertAdmin, conn).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar la base de datos: " + ex.Message, ex);
            }
        }
    }
}
