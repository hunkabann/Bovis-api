using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Autorizacion_Detalle
    {
    }

    #region Usuarios
    public class Usuario_Detalle
    {
        public int IdUsuario { get; set; }
        public string NumEmpleado { get; set; }
        public string Empleado { get; set; }
        public bool Activo { get; set; }
        public DateTime? UltimaSesion { get; set; }
    }

    public class Usuario_Perfiles_Detalle
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public List<Perfil_Detalle> Perfiles { get; set; }
    }
    #endregion Usuarios


    #region Módulos
    public class Modulo_Detalle
    {
        public int IdModulo { get; set; }
        public string Modulo { get; set; }        
        public bool Activo { get; set; }
        public List<Submodulo_Detalle> Submodulos { get; set; }
    }

    public class Submodulo_Detalle
    {
        public int IdSubmodulo { get; set; }
        public string SubModulo { get; set; }
        public bool Activo { get; set; }
        public List<Tab_Detalle> Tabs { get; set; }
    }

    public class Tab_Detalle
    {
        public int IdTab { get; set; }
        public string Tab { get; set; }
        public bool IsTab { get; set; }
        public bool Activo { get; set; }
    }

    public class Modulo_Perfiles_Detalle
    {
        public int IdModulo { get; set; }
        public string Modulo { get; set; }
        public string SubModulo { get; set; }
        public List<Perfil_Detalle> Perfiles { get; set; }
    }
    #endregion Módulos


    #region Perfiles
    public class Perfil_Detalle
    {
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public class Perfil_Permisos_Detalle
    {
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public string Descripcion { get; set; }
        public List<Permiso_Detalle> Permisos { get; set; }
    }

    public class Perfil_Modulos_Detalle
    {
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public string Descripcion { get; set; }
        public List<Modulo_Detalle> Modulos { get; set; }
    }
    #endregion Perfiles


    #region Permisos
    public class Permiso_Detalle
    {
        public int IdPermiso { get; set; }
        public string Permiso { get; set; }
        public bool Activo { get; set; }
    }
    #endregion Permisos



}
