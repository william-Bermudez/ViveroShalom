using System;
using System.Collections.Generic;

namespace ViveroShalom.Models
{
    public class Tarea
    {
        public int    Id           { get; set; }
        public int?   PlantaId     { get; set; }
        public string NombrePlanta { get; set; } = string.Empty;
        public string EspeciePlanta{ get; set; } = string.Empty;
        public string TipoAccion  { get; set; } = string.Empty;
        public string Icono       { get; set; } = "🌿";
        public string Prioridad   { get; set; } = "Media";
        public string Ubicacion   { get; set; } = string.Empty;
        public DateTime FechaInicio{ get; set; } = DateTime.Today;
        public DateTime? FechaFin  { get; set; }
        public string Repeticion  { get; set; } = string.Empty;
        public TimeSpan? HoraRecuerdo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string FotosJson   { get; set; } = string.Empty;
        public string Estado      { get; set; } = "Pendiente";
        public DateTime CreadaEn  { get; set; } = DateTime.Now;
    }

    public class Planta
    {
        public int    Id          { get; set; }
        public string Nombre      { get; set; } = string.Empty;
        public string Especie     { get; set; } = string.Empty;
        public string Ubicacion   { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool   Activa      { get; set; } = true;
    }

    public class Usuario
    {
        public int    Id       { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Nombre   { get; set; } = string.Empty;
        public string Rol      { get; set; } = "operario";
        public bool   Activo   { get; set; } = true;
    }
}
