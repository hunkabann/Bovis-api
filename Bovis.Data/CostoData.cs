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
                var resultado = (decimal) await InsertEntityAsync<TB_CostoPorEmpleado>(registro);
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

        public async Task<List<TB_CostoPorEmpleado>> GetCostos(bool hist)
        {
            var resp = await GetAllFromEntityAsync<TB_CostoPorEmpleado>();
            if (hist)
            {
                return resp.ToList();
            }
            else
                return resp.Where(reg => reg.RegHistorico == false).ToList();
           
        }
        #endregion

        #region GetCosto
        public async Task<TB_CostoPorEmpleado> GetCosto(int IdCosto)
        {
            var resp = await GetEntityByPKAsync<TB_CostoPorEmpleado>(IdCosto);
            return resp; 
        }
        #endregion

        #region GetCostoEmpleado
        public async Task<Common.Response<List<TB_CostoPorEmpleado>>> GetCostosEmpleado(int NumEmpleadoRrHh, bool hist)
        {
            var resp = await GetAllEntititiesByPropertyValueAsync<TB_CostoPorEmpleado, int>(nameof(NumEmpleadoRrHh), NumEmpleadoRrHh);
            if (hist)
            {
                if (resp.Count > 0)
                {
                    return new Common.Response<List<TB_CostoPorEmpleado>>()
                    {
                        Success = true,
                        Data = resp,
                        Message = "Ok"
                    };

                }
                else
                    return new Common.Response<List<TB_CostoPorEmpleado>>()
                    {
                        Success = false,
                        Message = $"No se encontraron históricos de costos del empleado: {NumEmpleadoRrHh}"

                    };
                

            }
            else
            {
                var listaCostos = resp.Where(reg => reg.RegHistorico == false).ToList();
                if (listaCostos.Count != 0)
                    return new Common.Response<List<TB_CostoPorEmpleado>>()
                    {
                        Success = true,
                        Data = listaCostos,
                        Message = "Ok"
                    };
                else
                    return new Common.Response<List<TB_CostoPorEmpleado>>()
                    {
                        Success = false,
                        Message = $"No se encontraron registros de costos para el empleado: {NumEmpleadoRrHh}"
                    };
            }
            
             
        }
        #endregion

        #region GetCostosEmpleado
        public async Task<Common.Response<List<TB_CostoPorEmpleado>>> GetCostoEmpleado(int NumEmpleadoRrHh, int anno, int mes, bool hist)
        {
            var registros = await GetAllEntititiesByPropertyValueAsync<TB_CostoPorEmpleado, int>(nameof(NumEmpleadoRrHh), NumEmpleadoRrHh);
            if (registros.Count > 0)
            {
                if (hist)
                {
                    var costosEmpleado = registros.Where(reg => reg.NuAnno == anno && reg.NuMes == mes).ToList();
                    return new Common.Response<List<TB_CostoPorEmpleado>>()
                    {
                        Success = true,
                        Data = costosEmpleado,
                        Message = "Ok"
                    };

                }
                else
                {
                    var costoEmpleado = registros.Where(reg => reg.RegHistorico == false && reg.NuAnno == anno && reg.NuMes == mes).ToList();
                    if (costoEmpleado.Count > 0)
                    {
                        return new Common.Response<List<TB_CostoPorEmpleado>>()
                        {
                            Success = true,
                            Data = costoEmpleado,
                            Message = "Ok"

                        };

                    }
                    else
                        return new Common.Response<List<TB_CostoPorEmpleado>>()
                        {
                            Success = false,
                            Message = $"No existe registro de costo para el empleado {NumEmpleadoRrHh} en el año y mes solicitados"

                        };
                    
                }
            }
            else
            {
                return new Common.Response<List<TB_CostoPorEmpleado>>() 
                { Success = false, 
                  Message = $"No existen históricos de costos para el empleado: {NumEmpleadoRrHh}." 
                };
            }
        }
        #endregion

        #region GetCostoLaborable
        public async Task<Common.Response<decimal>> GetCostoLaborable(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max)
        {
            var costos = await GetAllEntititiesByPropertyValueAsync<TB_CostoPorEmpleado, int>(nameof(NumEmpleadoRrHh), NumEmpleadoRrHh);

            var costosEmpleado = costos.Where(reg => (reg.RegHistorico == false) && ((reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();

            if (costosEmpleado.Count > 0)
            {

                decimal costoTotalLaborable = 0.0M;
                foreach (var costo in costosEmpleado)
                    costoTotalLaborable = (decimal)costo.CostoMensualEmpleado + costoTotalLaborable;
                return new Common.Response<decimal>()
                {
                    Success = true,
                    Data = (decimal)costoTotalLaborable,
                    Message = "Ok"
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
        public async Task<Common.Response<List<TB_CostoPorEmpleado>>> GetCostosBetweenDates(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist)
        {
            var costos = await GetAllEntititiesByPropertyValueAsync<TB_CostoPorEmpleado, int>(nameof(NumEmpleadoRrHh), NumEmpleadoRrHh);
            if(costos.Count > 0)
            {
                if (hist)
                {
                    var costosEmpleado = costos.Where(reg => ((reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();
                    return new Common.Response<List<TB_CostoPorEmpleado>>()
                    {
                        Success = true,
                        Data = costosEmpleado,
                        Message = "Ok"
                    };
                }
                else
                {
                    var costosEmpleado = costos.Where(reg => (reg.RegHistorico == false) && ((reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();
                    return new Common.Response<List<TB_CostoPorEmpleado>>()
                    {
                        Success = true,
                        Data = costosEmpleado,
                        Message = "Ok"
                    };
                }
               
            }

            return new Common.Response<List<TB_CostoPorEmpleado>>()
            {
                Success = false,
                Message = $"No se encontraron registros históricos de costos para el Empleado: {NumEmpleadoRrHh} en las fechas proporcionadas"
            };
            

        }
        #endregion

        #region UpdateCostos
        public async Task<Common.Response<TB_CostoPorEmpleado>> UpdateCostos(int costoId, TB_CostoPorEmpleado registro)
        {
            if(costoId == registro.IdCosto)
            {
                var respuesta = await GetCostoEmpleado(registro.NumEmpleadoRrHh,registro.NuAnno, registro.NuMes,false);
                if (respuesta.Success)
                {
                    var registro_anterior = respuesta.Data[0];
                    registro_anterior.RegHistorico = true; //Actualiza el estatus del registro para ser histórico
                    var resBool = await UpdateEntityAsync<TB_CostoPorEmpleado>(registro_anterior);
                    var resDecimal = (decimal) await InsertEntityAsync<TB_CostoPorEmpleado>(registro);
                    return new Common.Response<TB_CostoPorEmpleado>
                    {
                        Data = registro,
                        Success = true,
                        Message = $"Actualización del registro de costos: {costoId} por el {resDecimal}"
                    };
                }
                return new Common.Response<TB_CostoPorEmpleado>
                {
                    Success = false,
                    Message = $"No se encontró el registro de costos: {costoId}."
                };
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
