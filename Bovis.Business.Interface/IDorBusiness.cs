﻿using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Common;
using System.Text.Json.Nodes;

namespace Bovis.Business.Interface
{
    public interface IDorBusiness : IDisposable
    {
        Task<TB_DorEmpleados?> GetDorEjecutivoCorreo(string email);
        Task<Dor_Subordinados?> GetDorEmpleadoCorreo(string email);
        Task<List<Dor_Subordinados>> GetDorListaSubordinados(string name);
        Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(int nivel, string unidadNegocio, int mes, int anio, string seccion);

        //Task<List<DOR_ObjetivosDesepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado);
        Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado,int nivel, int? acepto, int mes);
        Task<(bool Success, string Message)> AddDorObjetivo(TB_DorObjetivosDesepeno objetivo);
        Task<(bool Success, string Message)> UpdDorObjetivo(TB_DorObjetivosDesepeno objetivo);
        Task<List<Dor_ObjetivosGenerales>> GetDorGpmProyecto(int proyecto);
        Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes, int anio, int empleado, string seccion);
        Task<(bool Success, string Message)> UpdateReal(JsonObject registro);
        Task<(bool Success, string Message)> UpdateObjetivoPersonal(JsonObject registro);
        Task<(bool Success, string Message)> UpdateAcepto(JsonObject registro);
    }
}
