using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public class CostoData : RepositoryLinq2DB<ConnectionDB>, ICostoData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public CostoData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<(bool Success, string Message)> AddCosto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int NumEmpleadoRrHh = Convert.ToInt32(registro["NumEmpleadoRrHh"].ToString());
            int? NumEmpleadoNoi = Convert.ToInt32(registro["NumEmpleadoNoi"].ToString());
            int? IdPersona = Convert.ToInt32(registro["IdPersona"].ToString());
            string? Reubicacion = registro["Reubicacion"].ToString();
            int? IdPuesto = Convert.ToInt32(registro["IdPuesto"].ToString());
            int? NumProyecto = Convert.ToInt32(registro["NumProyecto"].ToString());
            int? IdUnidadNegocio = Convert.ToInt32(registro["IdUnidadNegocio"].ToString());
            int? IdEmpresa = Convert.ToInt32(registro["IdEmpresa"].ToString());
            string? Timesheet = registro["Timesheet"].ToString();
            int? IdEmpleadoJefe = Convert.ToInt32(registro["IdEmpleadoJefe"].ToString());
            DateTime? FechaIngreso = Convert.ToDateTime(registro["FechaIngreso"].ToString());
            decimal? Antiguedad = Convert.ToDecimal(registro["Antiguedad"].ToString());
            decimal? AvgDescuentoEmpleado = Convert.ToDecimal(registro["AvgDescuentoEmpleado"].ToString());
            decimal? MontoDescuentoMensual = Convert.ToDecimal(registro["MontoDescuentoMensual"].ToString());
            decimal? SueldoNetoPercibidoMensual = Convert.ToDecimal(registro["SueldoNetoPercibidoMensual"].ToString());
            decimal? RetencionImss = Convert.ToDecimal(registro["RetencionImss"].ToString());
            decimal? Ispt = Convert.ToDecimal(registro["Ispt"].ToString());
            decimal? SueldoBruto = Convert.ToDecimal(registro["SueldoBruto"].ToString());
            decimal? Anual = Convert.ToDecimal(registro["Anual"].ToString());
            decimal? AguinaldoCantMeses = Convert.ToDecimal(registro["AguinaldoCantMeses"].ToString());
            decimal? AguinaldoMontoProvisionMensual = Convert.ToDecimal(registro["AguinaldoMontoProvisionMensual"].ToString());
            decimal? PvDiasVacasAnuales = Convert.ToDecimal(registro["PvDiasVacasAnuales"].ToString());
            decimal? PvProvisionMensual = Convert.ToDecimal(registro["PvProvisionMensual"].ToString());
            decimal? IndemProvisionMensual = Convert.ToDecimal(registro["IndemProvisionMensual"].ToString());
            decimal? AvgBonoAnualEstimado = Convert.ToDecimal(registro["AvgBonoAnualEstimado"].ToString());
            decimal? BonoAnualProvisionMensual = Convert.ToDecimal(registro["BonoAnualProvisionMensual"].ToString());
            decimal? SgmmCostoTotalAnual = Convert.ToDecimal(registro["SgmmCostoTotalAnual"].ToString());
            decimal? SgmmCostoMensual = Convert.ToDecimal(registro["SgmmCostoMensual"].ToString());
            decimal? SvCostoTotalAnual = Convert.ToDecimal(registro["SvCostoTotalAnual"].ToString());
            decimal? SvCostoMensual = Convert.ToDecimal(registro["SvCostoMensual"].ToString());
            decimal? VaidCostoMensual = Convert.ToDecimal(registro["VaidCostoMensual"].ToString());
            decimal? VaidComisionCostoMensual = Convert.ToDecimal(registro["VaidComisionCostoMensual"].ToString());
            decimal? PtuProvision = Convert.ToDecimal(registro["PtuProvision"].ToString());
            bool? Beneficios = Convert.ToBoolean(registro["Beneficios"].ToString());
            decimal? Impuesto3sNomina = Convert.ToDecimal(registro["Impuesto3sNomina"].ToString());
            decimal? Imss = Convert.ToDecimal(registro["Imss"].ToString());
            decimal? Retiro2 = Convert.ToDecimal(registro["Retiro2"].ToString());
            decimal? CesantesVejez = Convert.ToDecimal(registro["CesantesVejez"].ToString());
            decimal? Infonavit = Convert.ToDecimal(registro["Infonavit"].ToString());
            decimal? CargasSociales = Convert.ToDecimal(registro["CargasSociales"].ToString());
            decimal? CtlCostoMensualProyecto = Convert.ToDecimal(registro["CtlCostoMensualProyecto"].ToString());


            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_custom_query = await db.tB_Costo_Por_Empleados
                    .Value(x => x.NumEmpleadoRrHh, NumEmpleadoRrHh)
                    .Value(x => x.NumEmpleadoNoi, NumEmpleadoNoi)
                    .Value(x => x.IdPersona, IdPersona)
                    .Value(x => x.Reubicacion, Reubicacion)
                    .Value(x => x.IdPuesto, IdPuesto)
                    .Value(x => x.NumProyecto, NumProyecto)
                    .Value(x => x.IdUnidadNegocio, IdUnidadNegocio)
                    .Value(x => x.IdEmpresa, IdEmpresa)
                    .Value(x => x.Timesheet, Timesheet)
                    .Value(x => x.IdEmpleadoJefe, IdEmpleadoJefe)
                    .Value(x => x.FechaIngreso, FechaIngreso)
                    .Value(x => x.Antiguedad, Antiguedad)
                    .Value(x => x.AvgDescuentoEmpleado, AvgDescuentoEmpleado)
                    .Value(x => x.MontoDescuentoMensual, MontoDescuentoMensual)
                    .Value(x => x.SueldoNetoPercibidoMensual, SueldoNetoPercibidoMensual)
                    .Value(x => x.RetencionImss, RetencionImss)
                    .Value(x => x.Ispt, Ispt)
                    .Value(x => x.SueldoBruto, SueldoBruto)
                    .Value(x => x.Anual, Anual)
                    .Value(x => x.AguinaldoCantMeses, AguinaldoCantMeses)
                    .Value(x => x.AguinaldoMontoProvisionMensual, AguinaldoMontoProvisionMensual)
                    .Value(x => x.PvDiasVacasAnuales, PvDiasVacasAnuales)
                    .Value(x => x.PvProvisionMensual, PvProvisionMensual)
                    .Value(x => x.IndemProvisionMensual, IndemProvisionMensual)
                    .Value(x => x.AvgBonoAnualEstimado, AvgBonoAnualEstimado)
                    .Value(x => x.BonoAnualProvisionMensual, BonoAnualProvisionMensual)
                    .Value(x => x.SgmmCostoTotalAnual, SgmmCostoTotalAnual)
                    .Value(x => x.SgmmCostoMensual, SgmmCostoMensual)
                    .Value(x => x.SvCostoTotalAnual, SvCostoTotalAnual)
                    .Value(x => x.SvCostoMensual, SvCostoMensual)
                    .Value(x => x.VaidCostoMensual, VaidCostoMensual)
                    .Value(x => x.VaidComisionCostoMensual, VaidComisionCostoMensual)
                    .Value(x => x.PtuProvision, PtuProvision)
                    .Value(x => x.Beneficios, Beneficios)
                    .Value(x => x.Impuesto3sNomina, Impuesto3sNomina)
                    .Value(x => x.Imss, Imss)
                    .Value(x => x.Retiro2, Retiro2)
                    .Value(x => x.CesantesVejez, CesantesVejez)
                    .Value(x => x.Infonavit, Infonavit)
                    .Value(x => x.CargasSociales, CargasSociales)
                    .Value(x => x.CtlCostoMensualProyecto, CtlCostoMensualProyecto)
                    .InsertAsync() > 0;

                resp.Success = insert_custom_query;
                resp.Message = insert_custom_query == default ? "Ocurrio un error al agregar registro de costo por empleado." : string.Empty;

            }

            return resp;
        }
        public async Task<List<Costo_Detalle>> GetCostos(int IdCosto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var resp = await (from costos in db.tB_Costo_Por_Empleados
                                  where (IdCosto == 0 || costos.IdCosto == IdCosto)
                                  orderby costos.IdCosto ascending
                                  select new Costo_Detalle
                                  {
                                      IdCosto = costos.IdCosto,
                                      NumEmpleadoNoi = costos.NumEmpleadoRrHh,
                                      IdPersona = costos.IdPersona,
                                      Reubicacion = costos.Reubicacion,
                                      IdPuesto = costos.IdPuesto,
                                      NumProyecto = costos.NumProyecto,
                                      IdUnidadNegocio = costos.IdUnidadNegocio,
                                      IdEmpresa = costos.IdEmpresa,
                                      Timesheet = costos.Timesheet,
                                      IdEmpleadoJefe = costos.IdEmpleadoJefe,
                                      FechaIngreso = costos.FechaIngreso,
                                      Antiguedad = costos.Antiguedad,
                                      AvgDescuentoEmpleado = costos.AvgDescuentoEmpleado,
                                      MontoDescuentoMensual = costos.MontoDescuentoMensual,
                                      SueldoNetoPercibidoMensual = costos.SueldoNetoPercibidoMensual,
                                      RetencionImss = costos.RetencionImss,
                                      Ispt = costos.Ispt,
                                      SueldoBruto = costos.SueldoBruto,
                                      Anual = costos.Anual,
                                      AguinaldoCantMeses = costos.AguinaldoCantMeses,
                                      AguinaldoMontoProvisionMensual = costos.AguinaldoMontoProvisionMensual,
                                      PvDiasVacasAnuales = costos.PvDiasVacasAnuales,
                                      PvProvisionMensual = costos.PvProvisionMensual,
                                      IndemProvisionMensual = costos.IndemProvisionMensual,
                                      AvgBonoAnualEstimado = costos.AvgBonoAnualEstimado,
                                      BonoAnualProvisionMensual = costos.BonoAnualProvisionMensual,
                                      SgmmCostoTotalAnual = costos.SgmmCostoTotalAnual,
                                      SgmmCostoMensual = costos.SgmmCostoMensual,
                                      SvCostoTotalAnual = costos.SvCostoTotalAnual,
                                      SvCostoMensual = costos.SvCostoMensual,
                                      VaidCostoMensual = costos.VaidCostoMensual,
                                      VaidComisionCostoMensual = costos.VaidComisionCostoMensual,
                                      PtuProvision = costos.PtuProvision,
                                      Beneficios = costos.Beneficios,
                                      Impuesto3sNomina = costos.Impuesto3sNomina,
                                      Imss = costos.Imss,
                                      Retiro2 = costos.Retiro2,
                                      CesantesVejez = costos.CesantesVejez,
                                      Infonavit = costos.Infonavit,
                                      CargasSociales = costos.CargasSociales,
                                      CtlCostoMensualProyecto = costos.CtlCostoMensualProyecto
                                  }).ToListAsync();

                return resp;
            }
        }
    }
}
