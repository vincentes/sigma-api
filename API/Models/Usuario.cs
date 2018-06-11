using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Administrador = new HashSet<Administrador>();
            Adscripto = new HashSet<Adscripto>();
            Alumno = new HashSet<Alumno>();
            Docente = new HashSet<Docente>();
            Encuesta = new HashSet<Encuesta>();
            NotificacionDestinatario = new HashSet<NotificacionDestinatario>();
            RespuestaEncuesta = new HashSet<RespuestaEncuesta>();
        }

        public int IdUsr { get; set; }
        public string CedulaUsr { get; set; }
        public byte[] PassHashUsr { get; set; }
        public Guid? Salt { get; set; }
        public string CelularUsr { get; set; }
        public string TokenUsr { get; set; }
        public string NombreUsr { get; set; }
        public string ApellidoUsr { get; set; }

        public ICollection<Administrador> Administrador { get; set; }
        public ICollection<Adscripto> Adscripto { get; set; }
        public ICollection<Alumno> Alumno { get; set; }
        public ICollection<Docente> Docente { get; set; }
        public ICollection<Encuesta> Encuesta { get; set; }
        public ICollection<NotificacionDestinatario> NotificacionDestinatario { get; set; }
        public ICollection<RespuestaEncuesta> RespuestaEncuesta { get; set; }
    }
}
