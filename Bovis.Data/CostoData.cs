using Azure;
using Bovis.Common;
using Bovis.Common.Model;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        #region AddCosto
        public async Task<Common.Response<decimal>> AddCosto(TB_CostoPorEmpleado registro)
        {
            var registro_anterior = await GetCostoEmpleado(registro.NumEmpleadoRrHh, registro.NuAnno, registro.NuMes, false); //Verifica que no existe registro previo de costos para este empleado en el año y mes solicitados.

            if (!registro_anterior.Success) // Puede llevarse a cabo la inserción de un nuevo registro.
            {
                using (var db = new ConnectionDB(dbConfig))
                {
                    var isr_record = await (from isr in db.tB_Cat_Tabla_ISRs
                                            where isr.Anio == registro.NuAnno
                                            && isr.Mes == registro.NuMes
                                            && (isr.LimiteInferior <= registro.SueldoBruto && isr.LimiteSuperior >= registro.SueldoBruto)
                                            select isr).FirstOrDefaultAsync();

                    if (isr_record != null)
                    {
                        registro.Ispt = ((registro.SueldoBruto - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;
                    }
                }

                var resultado = (decimal)await InsertEntityAsync<TB_CostoPorEmpleado>(registro);
                return new Common.Response<decimal>()
                {
                    Data = resultado,
                    Success = true,
                    Message = $"Se creó nuevo registro con id: {resultado}"
                };
            }

            return new Common.Response<decimal>()
            {
                Success = false,
                Message = $"Error: Ya existe el registro: {registro_anterior.Data[0].IdCosto} en tabla costos para el empleado {registro.NumEmpleadoRrHh}."
            };

        }
        #endregion

        #region GetCostos

        public async Task<List<Costo_Detalle>> GetCostos(bool? hist)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();

            if ((bool)hist)
            {
                return costos;
            }
            else
                return costos.Where(reg => reg.RegHistorico == false).ToList();

        }
        #endregion

        #region GetCosto
        public async Task<Costo_Detalle> GetCosto(int IdCosto)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();
            var resp = costos.SingleOrDefault(costo => costo.IdCosto == IdCosto);

            return resp;
        }
        #endregion

        #region GetCostosEmpleado
        public async Task<Common.Response<List<Costo_Detalle>>> GetCostosEmpleado(string NumEmpleadoRrHh, bool hist)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();
            var resp = costos.Where(costo => costo.NumEmpleadoRrHh == NumEmpleadoRrHh).ToList<Costo_Detalle>();

            if (hist)
            {
                if (resp.Count > 0)
                {
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = resp,
                        Message = "Ok"
                    };

                }
                else
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = false,
                        Message = $"No se encontraron históricos de costos del empleado: {NumEmpleadoRrHh}"

                    };


            }
            else
            {
                var listaCostos = resp.Where(reg => reg.RegHistorico == false).ToList();
                if (listaCostos.Count > 0)
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = listaCostos,
                        Message = "Ok"
                    };
                else
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = false,
                        Message = $"No se encontraron registros de costos para el empleado: {NumEmpleadoRrHh}"
                    };
            }


        }
        #endregion

        #region GetCostoEmpleado
        public async Task<Common.Response<List<Costo_Detalle>>> GetCostoEmpleado(string NumEmpleadoRrHh, int anno, int mes, bool hist)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();
            var result = costos.Where(costo => costo.NumEmpleadoRrHh ==  NumEmpleadoRrHh && costo.NuAnno == anno && costo.NuMes == mes).ToList();

            if (result.Count > 0)
            {
                if (hist)
                {
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = result,
                        Message = "Ok"
                    };

                }
                else
                {
                    var costoEmpleado = result.Where(reg => reg.RegHistorico == false).ToList();
                    if (costoEmpleado.Count > 0)
                    {
                        return new Common.Response<List<Costo_Detalle>>()
                        {
                            Success = true,
                            Data = costoEmpleado,
                            Message = "Ok"

                        };

                    }
                    else
                        return new Common.Response<List<Costo_Detalle>>()
                        {
                            Success = false,
                            Message = $"No existe registro de costo para el empleado {NumEmpleadoRrHh} en el año y mes solicitados"

                        };

                }
            }
            else
            {
                return new Common.Response<List<Costo_Detalle>>()
                {
                    Success = false,
                    Message = $"No existen históricos de costos para el empleado: {NumEmpleadoRrHh}."
                };
            }
        }
        #endregion

        #region GetCostoLaborable
        public async Task<Common.Response<decimal>> GetCostoLaborable(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max)
        {
            CostoQueries QueryBase = new(dbConfig);
            var result = await QueryBase.CostosEmpleados();

            var costosEmpleado = result.Where(reg => (reg.NumEmpleadoRrHh == NumEmpleadoRrHh) &&  (reg.RegHistorico == false) && ((reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();

            if (costosEmpleado.Count > 0)
            {

                decimal costoTotalLaborable = 0.0M;
                foreach (var costo in costosEmpleado)
                    costoTotalLaborable = (decimal)costo.CostoMensualEmpleado + costoTotalLaborable;
                return new Common.Response<decimal>()
                {
                    Success = true,
                    Data = (decimal)costoTotalLaborable,
                    Message = $"El CTL - Costo Total Laborable es de: {costoTotalLaborable.ToString("C2")}"
                };
            }
            else
                return new Common.Response<decimal>()
                {
                    Success = false,
                    Data = 0.0M,
                    Message = $"No se encontraron históricos de costos para el empleado: {NumEmpleadoRrHh}"
                };

        }
        #endregion

        #region GetCostosBetweenDates
        public async Task<Common.Response<List<Costo_Detalle>>> GetCostosBetweenDates(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();
            if (costos.Count > 0)
            {
                if (hist)
                {
                    var costosEmpleado = costos.Where(reg => ((reg.NumEmpleadoRrHh == NumEmpleadoRrHh) && (reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = costosEmpleado,
                        Message = "Ok"
                    };
                }
                else
                {
                    var costosEmpleado = costos.Where(reg => (reg.NumEmpleadoRrHh == NumEmpleadoRrHh) &&(reg.RegHistorico == false) && ((reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = costosEmpleado,
                        Message = "Ok"
                    };
                }

            }

            return new Common.Response<List<Costo_Detalle>>()
            {
                Success = false,
                Message = $"No se encontraron registros históricos de costos para el Empleado: {NumEmpleadoRrHh} en las fechas proporcionadas"
            };


        }
        #endregion

        #region UpdateCostos
        public async Task<Common.Response<TB_CostoPorEmpleado>> UpdateCostos(int costoId, TB_CostoPorEmpleado registro)
        {
            if (costoId == registro.IdCosto)
            {
                //var respuesta = await GetCostoEmpleado(registro.NumEmpleadoRrHh, registro.NuAnno, registro.NuMes, false);
                var registros = await GetAllEntititiesByPropertyValueAsync<TB_CostoPorEmpleado, string>(nameof(registro.NumEmpleadoRrHh), registro.NumEmpleadoRrHh);
                var registro_anterior = registros.Where(costo => costo.NuAnno == registro.NuAnno && costo.NuMes == registro.NuMes && costo.RegHistorico == false).SingleOrDefault(); 
                if(registro_anterior != null)
                {
                    using (var db = new ConnectionDB(dbConfig))
                    {
                        var isr_record = await (from isr in db.tB_Cat_Tabla_ISRs
                                                where isr.Anio == registro.NuAnno
                                                && isr.Mes == registro.NuMes
                                                && (isr.LimiteInferior <= registro.SueldoBruto && isr.LimiteSuperior >= registro.SueldoBruto)
                                                select isr).FirstOrDefaultAsync();

                        if (isr_record != null)
                        {
                            registro.Ispt = ((registro.SueldoBruto - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;
                        }
                    }

                    registro_anterior.RegHistorico = true; //Actualiza el estatus del registro para convertirse en histórico.
                    var resBool = await UpdateEntityAsync<TB_CostoPorEmpleado>(registro_anterior);
                    var resDecimal = (decimal)await InsertEntityAsync<TB_CostoPorEmpleado>(registro);
                    return new Common.Response<TB_CostoPorEmpleado>
                    {
                        Data = registro,
                        Success = true,
                        Message = $"Actualización del registro de costos: {costoId} por el {resDecimal}"
                    };
                }
                else
                {
                    return new Common.Response<TB_CostoPorEmpleado>
                    {
                        Success = false,
                        Message = $"No se encontró el registro de costos: {costoId}."
                    };
                }
                
            }
            return new Common.Response<TB_CostoPorEmpleado>()
            {
                Success = false,
                Message = $"Identificador del Costo {costoId} no coincide con registro {registro.IdCosto}!"
            };

        }
        #endregion

        #region DeleteCosto
        public async Task<Common.Response<bool>> DeleteCosto(int costoId)
        {
            var entity = await GetEntityByPKAsync<TB_CostoPorEmpleado>(costoId);
            if (entity != null)
            {
                var respuesta = await DeleteEntityAsync<TB_CostoPorEmpleado>(entity);
                if (respuesta)
                {
                    return new Common.Response<bool>
                    {
                        Success = true,
                        Data = respuesta,
                        Message = $"Registro {costoId} borrado exitosamente."
                    };
                }
                else
                {
                    return new Common.Response<bool>()
                    {
                        Success = false,
                        Data = respuesta,
                        Message = $"Error: Se encontró una falla al intentar borrar el registro {costoId}."
                    };
                }
            }
            else
            {
                return new Common.Response<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Error: Registro de costo no existe!"
                };
            }

        }
        #endregion 

    }
}
public class CostoQueries :  RepositoryLinq2DB<ConnectionDB>
{
    private readonly string _dbConfig = "DBConfig";
    public CostoQueries(string dbConfig)
    {
        _dbConfig = dbConfig;
    }
    public async Task<List<Costo_Detalle>> CostosEmpleados()
    {
        using (var db = new ConnectionDB(_dbConfig))
        {
            var result = await (from costos in db.tB_Costo_Por_Empleados
                                join personaEmp in db.tB_Personas on costos.IdPersona equals personaEmp.IdPersona into personaEmpJoin
                                from personaEmpItem in personaEmpJoin.DefaultIfEmpty()
                                join puesto in db.tB_Cat_Puestos on costos.IdPuesto equals puesto.IdPuesto into puestoJoin
                                from puestoItem in puestoJoin.DefaultIfEmpty()
                                join proyecto in db.tB_Proyectos on costos.NumProyecto equals proyecto.NumProyecto into proyectoJoin
                                from proyectoItem in proyectoJoin.DefaultIfEmpty()
                                join unidadN in db.tB_Cat_UnidadNegocios on costos.IdUnidadNegocio equals unidadN.IdUnidadNegocio into unidadNJoin
                                from unidadNItem in unidadNJoin.DefaultIfEmpty()
                                join empresa in db.tB_Empresas on costos.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                from empresaItem in empresaJoin.DefaultIfEmpty()
                                join empleado in db.tB_Empleados on costos.IdEmpleadoJefe equals empleado.NumEmpleadoRrHh into empleadoJoin
                                from empleadoItem in empleadoJoin.DefaultIfEmpty()
                                join personaJefe in db.tB_Personas on empleadoItem.IdPersona equals personaJefe.IdPersona into personaJefeJoin
                                from personaJefeItem in personaJefeJoin.DefaultIfEmpty()
                                select new Costo_Detalle
                                {
                                    IdCosto = costos.IdCosto,
                                    NuAnno = costos.NuAnno,
                                    NuMes = costos.NuMes,
                                    NumEmpleadoRrHh = costos.NumEmpleadoRrHh,
                                    NumEmpleadoNoi = costos.NumEmpleadoNoi,
                                    IdPersona = costos.IdPersona,
                                    NombrePersona = personaEmpItem != null ? personaEmpItem.Nombre + " " + personaEmpItem.ApPaterno + " " + personaEmpItem.ApMaterno : string.Empty,
                                    Reubicacion = costos.Reubicacion,
                                    IdPuesto = costos.IdPuesto,
                                    Puesto = puestoItem != null ? puestoItem.Puesto : string.Empty,
                                    NumProyecto = costos.NumProyecto,
                                    Proyecto = proyectoItem != null ? proyectoItem.Proyecto : string.Empty,
                                    IdUnidadNegocio = costos.IdUnidadNegocio,
                                    UnidadNegocio = unidadNItem != null ? unidadNItem.UnidadNegocio : string.Empty,
                                    IdEmpresa = costos.IdEmpresa,
                                    Empresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                    Timesheet = costos.Timesheet,
                                    IdEmpleadoJefe = costos.IdEmpleadoJefe,
                                    NombreJefe = personaJefeItem != null ? personaJefeItem.Nombre + " " + personaJefeItem.ApPaterno + " " + personaJefeItem.ApMaterno : string.Empty,
                                    FechaIngreso = costos.FechaIngreso,
                                    Antiguedad = costos.Antiguedad,
                                    AvgDescuentoEmpleado = costos.AvgDescuentoEmpleado,
                                    MontoDescuentoMensual = costos.MontoDescuentoMensual,
                                    SueldoNetoMensual = costos.SueldoNetoPercibidoMensual,
                                    RetencionImss = costos.RetencionImss,
                                    Ispt = costos.Ispt,
                                    SueldoBruto = costos.SueldoBruto,
                                    Anual = costos.Anual,
                                    AguinaldoCantidadMeses = costos.AguinaldoCantMeses,
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
                                    CostoMensualEmpleado = costos.CostoMensualEmpleado,
                                    CostoMensualProyecto = costos.CostoMensualProyecto,
                                    CostoAnualEmpleado = costos.CostoAnualEmpleado,
                                    IndiceCostoLaboral = costos.IndiceCostoLaboral,
                                    IndiceCargaLaboral = costos.IndiceCargaLaboral,
                                    FechaActualizacion = costos.FechaActualizacion,
                                    RegHistorico = costos.RegHistorico
                                }).ToListAsync();

            return result;

        }

    }
    
}

