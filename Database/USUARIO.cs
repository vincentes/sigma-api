//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.ADMINISTRADOR = new HashSet<ADMINISTRADOR>();
            this.ADSCRIPTO = new HashSet<ADSCRIPTO>();
            this.ALUMNO = new HashSet<ALUMNO>();
            this.DOCENTE = new HashSet<DOCENTE>();
            this.ENCUESTA = new HashSet<ENCUESTA>();
            this.NOTIFICACION_DESTINATARIO = new HashSet<NOTIFICACION_DESTINATARIO>();
            this.RESPUESTA_ENCUESTA = new HashSet<RESPUESTA_ENCUESTA>();
        }
    
        public int Id_Usr { get; set; }
        public string Cedula_Usr { get; set; }
        public byte[] PassHash_Usr { get; set; }
        public Nullable<System.Guid> Salt { get; set; }
        public string Celular_Usr { get; set; }
        public string Token_Usr { get; set; }
        public string Nombre_Usr { get; set; }
        public string Apellido_Usr { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADMINISTRADOR> ADMINISTRADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADSCRIPTO> ADSCRIPTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ALUMNO> ALUMNO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCENTE> DOCENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENCUESTA> ENCUESTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICACION_DESTINATARIO> NOTIFICACION_DESTINATARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESPUESTA_ENCUESTA> RESPUESTA_ENCUESTA { get; set; }
    }
}
