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
    
    public partial class BEACON
    {
        public string Id_Beacon { get; set; }
        public int Id_Nodo_Beacon { get; set; }
        public string Status_Nodo { get; set; }
    
        public virtual NODO NODO { get; set; }
    }
}
