# 🌿 Vivero Shalom — Sistema de Gestión

Sistema de escritorio en **C# .NET 6 / Windows Forms** para gestionar plantas y tareas de un vivero,
con persistencia en **MariaDB/MySQL** y notificaciones de escritorio.

---

## Requisitos

| Componente | Versión mínima |
|---|---|
| .NET SDK | 6.0 o superior |
| Visual Studio | 2022 (Community gratuito) |
| MariaDB / MySQL | 10.x / 8.x |

---

## Primeros pasos

### 1 – Clonar / descomprimir
Coloca la carpeta `ViveroShalom` donde quieras.

### 2 – Configurar la conexión
Abre **`ViveroShalom/Data/DatabaseHelper.cs`** y cambia:

```csharp
private const string Host     = "localhost";
private const int    Port     = 3306;
private const string Database = "vivero_shalom";
private const string User     = "root";
private const string Password = "";   // ← tu contraseña de MariaDB
```

### 3 – Base de datos (automático)
Al lanzar la app por primera vez crea la base de datos y las tablas
automáticamente. Si prefieres hacerlo a mano ejecuta:

```sql
-- En tu cliente MariaDB / MySQL:
SOURCE setup_database.sql;
```

### 4 – Compilar y ejecutar
Abre `ViveroShalom.sln` en **Visual Studio 2022** → **F5**.

---

## Credenciales por defecto

| Campo | Valor |
|---|---|
| Usuario | `admin` |
| Contraseña | `admin123` |

---

## Estructura del proyecto

```
ViveroShalom/
├─ Data/
│   ├─ DatabaseHelper.cs   ← cadena de conexión + inicialización
│   └─ Repositories.cs     ← CRUD (Tareas, Plantas, Usuarios)
├─ Forms/
│   ├─ LoginForm            ← pantalla de inicio de sesión
│   ├─ MainForm             ← dashboard (inicio)
│   ├─ GestorTareasForm     ← listar, filtrar, ver detalle, editar, eliminar
│   ├─ RegistrarTareaForm   ← crear / editar tarea
│   └─ SharedNav.cs         ← panel de navegación reutilizable
├─ Models/
│   └─ Models.cs            ← Tarea, Planta, Usuario
├─ NotificationService.cs   ← globos de notificación (system tray)
└─ Program.cs
```

---

## Funcionalidades

- **Login** con validación de usuario/contraseña (SHA-256 en BD).
- **Dashboard** con contador de plantas totales y tareas pendientes.
- **Gestor de Tareas** — lista filtrable por estado (Pendiente / Completada / Cancelada),
  buscador de texto, vista detalle, marcar como completada, editar, eliminar.
- **Registrar Tarea** — formulario completo con validación, selector de icono,
  prioridad, repetición, hora de recuerdo y adjuntar fotos.
- **Notificaciones** en la bandeja del sistema cuando una tarea con hora de
  recuerdo está a punto de comenzar (comprobación cada minuto).
- **Icono en la barra de tareas** con menú contextual para Abrir / Salir.

---

## Paquetes NuGet usados

| Paquete | Propósito |
|---|---|
| `MySql.Data` 8.0.33 | Conector oficial MariaDB/MySQL |
