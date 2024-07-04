using Bovis.Common;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using Bovis.Common.Model;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bovis.Common.Model.DTO;
using Microsoft.Identity.Client;

namespace Bovis.Data
{
    public class BeneficiosData : RepositoryLinq2DB<ConnectionDB>, IBeneficiosData
    {
        private readonly string dbConfig = "DBConfig";
        public BeneficiosData()
        {
            this.ConfigurationDB = dbConfig;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #region AddBeneficio
        public async Task<Response<object>> AddBeneficio(TB_EmpleadoBeneficio registro)
        {
            registro.Mes = DateTime.Now.Month;
            registro.Anno = DateTime.Now.Year;
            registro.FechaActualizacion = DateTime.Now;
            //Revisa que el registro no se encuentre en la tabla Beneficios Empleados
            var res = await GetBeneficio(registro.IdBeneficio, registro.NumEmpleadoRrHh, registro.Mes, registro.Anno);
            if (res.Success != false)
            {
                return new()
                {
                    Success = false,
                    Data = res.Data,
                    Message = $"Error: Este beneficio ya está registrado para el empleado: {registro.NumEmpleadoRrHh}"
                };
            }
            var respuesta = (object)await InsertEntityAsync<TB_EmpleadoBeneficio>(registro);
            return new Response<object>
            {
                Data = respuesta,
                Success = true,
                Message = $"Beneficio agregado con éxito para empleado: {registro.NumEmpleadoRrHh}."
            };

        }
        #endregion

        #region GetBeneficios
        public async Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficios(string NumEmpleado)
        {
            var respuesta = await GetAllEntititiesByPropertyValueAsync<TB_EmpleadoBeneficio, string>(nameof(TB_EmpleadoBeneficio.NumEmpleadoRrHh), NumEmpleado);


            if (respuesta.Count != 0)
            {
                respuesta = respuesta.Where(s => s.RegHistorico == false).ToList();
                if (respuesta.Count != 0)
                {
                    return new Response<List<TB_EmpleadoBeneficio>>
                    {
                        Success = true,
                        Data = respuesta,
                        Message = "Ok"
                    };
                }
            }
           
            return new Response<List<TB_EmpleadoBeneficio>>
            {
                Success = false,
                Data = respuesta,
                Message = $"Error: No se encontraron beneficios para el empleado {NumEmpleado}."

            };
        }
        #endregion

        #region GetBeneficio
        public async Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficio(int idBeneficio, string NumEmpleado, int Mes, int Anno)
        {
            var listaBeneficios = await GetAllEntititiesByPropertyValueAsync<TB_EmpleadoBeneficio, string>(nameof(TB_EmpleadoBeneficio.NumEmpleadoRrHh), NumEmpleado);
           
            if (listaBeneficios.Count > 0)
            {
                var respuesta = listaBeneficios.Where(s => s.IdBeneficio == idBeneficio && s.Mes == Mes && s.Anno == Anno && s.RegHistorico == false).ToList<TB_EmpleadoBeneficio>();

                if(respuesta.Count > 0)
                {
                    return new()
                    {
                        Data = respuesta,
                        Success = true,
                        Message = "Ok"
                    }; 
                }
                return new()
                {
                    Success = false,
                    Message = $"Error: No se encontró beneficio para el empleado: {NumEmpleado} en el mes y año solicitados"
                };
            }
            return new()
            {
                Success = false,
                Message = $"Error: No se encontró al empleado: {NumEmpleado} en tabla Beneficios-Empleado."
            };
        }
        #endregion

        #region GetBeneficioProyecto
        public async Task<Response<List<TB_EmpleadoProyectoBeneficio>>> GetBeneficioProyecto(int idBeneficio, string NumEmpleado)
        {
            var listaBeneficios = await GetAllEntititiesByPropertyValueAsync<TB_EmpleadoProyectoBeneficio, string>(nameof(TB_EmpleadoProyectoBeneficio.NumEmpleadoRrHh), NumEmpleado);

            if (listaBeneficios.Count > 0)
            {
                var respuesta = listaBeneficios.Where(s => s.IdBeneficio == idBeneficio ).ToList<TB_EmpleadoProyectoBeneficio>();

                if (respuesta.Count > 0)
                {
                    return new()
                    {
                        Data = respuesta,
                        Success = true,
                        Message = "Ok"
                    };
                }
                return new()
                {
                    Success = false,
                    Message = $"Error: No se encontró beneficio para el empleado: {NumEmpleado} en el mes y año solicitados"
                };
            }
            return new()
            {
                Success = false,
                Message = $"Error: No se encontró al empleado: {NumEmpleado} en tabla Beneficios-Empleado."
            };
        }
        #endregion


        #region UpdateBeneficio
        public async Task<Response<int>> UpdateBeneficio(TB_EmpleadoBeneficio registro, int idBeneficio, string numEmpleado)
        {
            TB_EmpleadoBeneficio regBeneficioAnt = new();
            if (registro.IdBeneficio == idBeneficio)
            {
                if(registro.NumEmpleadoRrHh == numEmpleado)
                {
                    //Checar si el registro existe en la base de datos.
                    var resultado = await GetAllEntititiesByPropertyValueAsync<TB_EmpleadoBeneficio, string>(nameof(TB_EmpleadoBeneficio.NumEmpleadoRrHh), numEmpleado);
                    resultado = resultado.Where(s => s.IdBeneficio == idBeneficio && s.RegHistorico == false).ToList();
                    
                    if(resultado.Count > 0)
                    {
                        regBeneficioAnt = await GetEntityByPKAsync<TB_EmpleadoBeneficio>(resultado[0].Id);
                        if(regBeneficioAnt.Costo == registro.Costo)
                        {
                            return new()
                            {
                                Success = false,
                                Data = 0,
                                Message = $"Error: Nada para actualizar en el registro del empleado {numEmpleado}"
                               
                            };
                        }

                        //Convierte a estatus de histórico este registro y actualiza su estado en la tabla. Asegurar que el nuevo registro sea el actual poniendo su estatus de histórico en Falso.


                        regBeneficioAnt.RegHistorico = true;
                        registro.RegHistorico = false;
                        registro.FechaActualizacion = DateTime.Now;
                        var res = await UpdateEntityAsync<TB_EmpleadoBeneficio>(regBeneficioAnt);
                        var res2 = await InsertEntityAsync<TB_EmpleadoBeneficio>(registro);
                        return new()
                        {
                            Success = true,
                            Data = 1,
                            Message = $"Registro de beneficio actualizado con éxito"
                        };
                    }
                    else
                    {
                        return new()
                        {
                            Success = false,
                            Data = 0,
                            Message = $"No existe ese beneficio para el empleado {numEmpleado}" 
                        };
                    }

                }
                else
                {
                    return new Response<int>
                    {
                        Data = 0,
                        Success = false,
                        Message = $"Error: El NumEmpleado {numEmpleado}, no coincide con el del registro proporcionado para actualizar."
                    };
                }
            }
            else
            {
                return new Response<int>
                {
                    Data = 0,
                    Success = false,
                    Message = $"Error: El idBeneficio {idBeneficio}, no coincide con el del registro proporcionado para actualizar."
                };
                
            }
            
        }
        #endregion

        #region UpdateBeneficioProyecto
        public async Task<Response<int>> UpdateBeneficioProyecto(TB_EmpleadoProyectoBeneficio registro, int idBeneficio, string numEmpleado)
        {
            TB_EmpleadoProyectoBeneficio regBeneficioAnt = new();
            if (registro.IdBeneficio == idBeneficio)
            {
                if (registro.NumEmpleadoRrHh == numEmpleado)
                {
                    //Checar si el registro existe en la base de datos.
                    var resultado = await GetAllEntititiesByPropertyValueAsync<TB_EmpleadoProyectoBeneficio, string>(nameof(TB_EmpleadoProyectoBeneficio.NumEmpleadoRrHh), numEmpleado);
                    //resultado = resultado.Where(s => s.IdBeneficio == idBeneficio && s.RegHistorico == false).ToList();
                    resultado = resultado.Where(s => s.IdBeneficio == idBeneficio ).ToList();

                    if (resultado.Count > 0)
                    {
                        regBeneficioAnt = await GetEntityByPKAsync<TB_EmpleadoProyectoBeneficio>(resultado[0].IdBeneficio);
                        if (regBeneficioAnt.nucostobeneficio == registro.nucostobeneficio)
                        {
                            return new()
                            {
                                Success = false,
                                Data = 0,
                                Message = $"Error: Nada para actualizar en el registro del empleado {numEmpleado}"

                            };
                        }

                        //Convierte a estatus de histórico este registro y actualiza su estado en la tabla. Asegurar que el nuevo registro sea el actual poniendo su estatus de histórico en Falso.


                        //regBeneficioAnt.RegHistorico = true;
                        //registro.RegHistorico = false;
                        //registro.FechaActualizacion = DateTime.Now;
                        var res = await UpdateEntityAsync<TB_EmpleadoProyectoBeneficio>(regBeneficioAnt);
                        var res2 = await InsertEntityAsync<TB_EmpleadoProyectoBeneficio>(registro);
                        return new()
                        {
                            Success = true,
                            Data = 1,
                            Message = $"Registro de beneficio actualizado con éxito"
                        };
                    }
                    else
                    {
                        return new()
                        {
                            Success = false,
                            Data = 0,
                            Message = $"No existe ese beneficio para el empleado {numEmpleado}"
                        };
                    }

                }
                else
                {
                    return new Response<int>
                    {
                        Data = 0,
                        Success = false,
                        Message = $"Error: El NumEmpleado {numEmpleado}, no coincide con el del registro proporcionado para actualizar."
                    };
                }
            }
            else
            {
                return new Response<int>
                {
                    Data = 0,
                    Success = false,
                    Message = $"Error: El idBeneficio {idBeneficio}, no coincide con el del registro proporcionado para actualizar."
                };

            }

        }
        #endregion

        #region AddBeneficio
        public async Task<Response<object>> AddBeneficioProyecto(TB_EmpleadoProyectoBeneficio registro)
        {
            //registro.Mes = DateTime.Now.Month;
            //registro.Anno = DateTime.Now.Year;
            //registro.FechaActualizacion = DateTime.Now;
            //Revisa que el registro no se encuentre en la tabla Beneficios Empleados
            var res = await GetBeneficioProyecto(registro.IdBeneficio, registro.NumEmpleadoRrHh);
            if (res.Success != false)
            {
                return new()
                {
                    Success = false,
                    Data = res.Data,
                    Message = $"Error: Este beneficio ya está registrado para el empleado: {registro.NumEmpleadoRrHh}"
                };
            }
            var respuesta = (object)await InsertEntityAsync<TB_EmpleadoProyectoBeneficio>(registro);
            return new Response<object>
            {
                Data = respuesta,
                Success = true,
                Message = $"Beneficio agregado con éxito para empleado: {registro.NumEmpleadoRrHh}."
            };

        }
        #endregion


    }
}
