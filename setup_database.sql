-- =============================================================
--  Vivero Shalom – Script de base de datos (MariaDB / MySQL)
--  El programa lo ejecuta automáticamente al iniciar,
--  pero puedes correrlo manualmente si lo prefieres.
-- =============================================================

CREATE DATABASE IF NOT EXISTS vivero_shalom
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE vivero_shalom;

-- ── Usuarios ────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS usuarios (
    id            INT AUTO_INCREMENT PRIMARY KEY,
    username      VARCHAR(60)  NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    nombre        VARCHAR(100),
    rol           ENUM('admin','operario') DEFAULT 'operario',
    activo        TINYINT(1) DEFAULT 1,
    creado_en     DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Usuario por defecto:  admin / admin123
INSERT IGNORE INTO usuarios (username, password_hash, nombre, rol)
VALUES ('admin', SHA2('admin123', 256), 'Administrador', 'admin');

-- ── Plantas ─────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS plantas (
    id           INT AUTO_INCREMENT PRIMARY KEY,
    nombre       VARCHAR(100) NOT NULL,
    especie      VARCHAR(100),
    ubicacion    VARCHAR(150),
    descripcion  TEXT,
    activa       TINYINT(1) DEFAULT 1,
    registrada_en DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Datos de ejemplo
INSERT IGNORE INTO plantas (id, nombre, especie, ubicacion) VALUES
(1, 'Azalea',   'Rhododendron',  'Maceta 12 – Sector 2'),
(2, 'Tomate',   'Solanum lycopersicum', 'Semillero 3 – Sector 1'),
(3, 'Fresa',    'Fragaria × ananassa',  'Semillero 3 – Sector 1');

-- ── Tareas ──────────────────────────────────────────────────
CREATE TABLE IF NOT EXISTS tareas (
    id            INT AUTO_INCREMENT PRIMARY KEY,
    planta_id     INT,
    nombre_planta VARCHAR(100) NOT NULL,
    especie_planta VARCHAR(100),
    tipo_accion   VARCHAR(80)  NOT NULL,
    icono         VARCHAR(10)  DEFAULT '🌿',
    prioridad     ENUM('Alta','Media','Baja') DEFAULT 'Media',
    ubicacion     VARCHAR(150),
    fecha_inicio  DATE NOT NULL,
    fecha_fin     DATE,
    repeticion    VARCHAR(50),
    hora_recuerdo TIME,
    descripcion   TEXT,
    fotos_json    TEXT,
    estado        ENUM('Pendiente','Completada','Cancelada') DEFAULT 'Pendiente',
    creada_en     DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (planta_id) REFERENCES plantas(id) ON DELETE SET NULL
);

-- Datos de ejemplo
INSERT IGNORE INTO tareas (id, planta_id, nombre_planta, tipo_accion, icono, prioridad, ubicacion, fecha_inicio, fecha_fin, repeticion, hora_recuerdo, descripcion) VALUES
(1, 1, 'Azalea',  'Reigo de Azaleas',       '💧', 'Alta', 'Maceta 12 Sector 2',     '2026-06-23', '2026-07-30', 'Cada 3 días', '07:00:00', 'Revisar humedad antes de regar'),
(2, 2, 'Tomate',  'Abonar semillas de tomate','🌿','Baja', 'Semillero 3 Sector 1',   '2026-06-23', '2026-07-12', 'Cada 8 días', '08:00:00', 'Tener cuidado al abonar'),
(3, 3, 'Fresa',   'Poda de Fresas',          '✂️','Media','Semillero 3 – Sector 1', '2026-07-01', NULL,          NULL,          NULL,        'Podar brotes laterales');
