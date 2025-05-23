﻿using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Transactions;
using static LinqToDB.SqlQuery.SqlPredicate;

namespace Bovis.Data
{
    public class EmpleadoData : RepositoryLinq2DB<ConnectionDB>, IEmpleadoData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public EmpleadoData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Empleados
        public async Task<List<Empleado_Detalle>> GetEmpleados(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig))
                {
                    var resp = await (from emp in db.tB_Empleados
                                      join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                      from perItem in perJoin.DefaultIfEmpty()
                                      join sex in db.tB_Cat_Sexos on perItem.IdSexo equals sex.IdSexo into sexJoin
                                      from sexItem in sexJoin.DefaultIfEmpty()
                                      join tipo_emp in db.tB_Cat_TipoEmpleados on emp.IdTipoEmpleado equals tipo_emp.IdTipoEmpleado into tipo_empJoin
                                      from tipo_empItem in tipo_empJoin.DefaultIfEmpty()
                                      join cat in db.tB_Cat_Categorias on emp.IdCategoria equals cat.IdCategoria into catJoin
                                      from catItem in catJoin.DefaultIfEmpty()
                                      join contrato in db.tB_Cat_TipoContratos on emp.IdTipoContrato equals contrato.IdTipoContrato into contratoJoin
                                      from contratoItem in contratoJoin.DefaultIfEmpty()
                                      join puesto in db.tB_Cat_Puestos on emp.CvePuesto equals puesto.IdPuesto into puestoJoin
                                      from puestoItem in puestoJoin.DefaultIfEmpty()
                                      join empresa in db.tB_Empresas on emp.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                      from empresaItem in empresaJoin.DefaultIfEmpty()
                                      join ciudad in db.tB_Ciudads on emp.IdCiudad equals ciudad.IdCiudad into ciudadJoin
                                      from ciudadItem in ciudadJoin.DefaultIfEmpty()
                                      join estado in db.tB_Estados on emp.IdEstado equals estado.IdEstado into estadoJoin
                                      from estadoItem in estadoJoin.DefaultIfEmpty()
                                      join pais in db.tB_Pais on emp.IdPais equals pais.IdPais into paisJoin
                                      from paisItem in paisJoin.DefaultIfEmpty()
                                      join estudios in db.tB_Cat_NivelEstudios on emp.IdNivelEstudios equals estudios.IdNivelEstudios into estudiosJoin
                                      from estudiosItem in estudiosJoin.DefaultIfEmpty()
                                      join pago in db.tB_Cat_FormaPagos on emp.IdFormaPago equals pago.IdFormaPago into pagoJoin
                                      from pagoItem in pagoJoin.DefaultIfEmpty()
                                      join jornada in db.tB_Cat_Jornadas on emp.IdJornada equals jornada.IdJornada into jornadaJoin
                                      from jornadaItem in jornadaJoin.DefaultIfEmpty()
                                      join depto in db.tB_Cat_Departamentos on emp.IdDepartamento equals depto.IdDepartamento into deptoJoin
                                      from deptoItem in deptoJoin.DefaultIfEmpty()
                                      join clasif in db.tB_Cat_Clasificacions on emp.IdClasificacion equals clasif.IdClasificacion into clasifJoin
                                      from clasifItem in clasifJoin.DefaultIfEmpty()
                                      join emp1 in db.tB_Empleados on emp.IdJefeDirecto equals emp1.NumEmpleadoRrHh
                                      join jefe in db.tB_Personas on emp1.IdPersona equals jefe.IdPersona into jefeJoin
                                      from jefeItem in jefeJoin.DefaultIfEmpty()
                                      join usertime in db.tB_Usuario_Timesheets on emp.NumProyectoPrincipal equals usertime.NumProyecto into usertimeJoin
                                      //join usertime in db.tB_Usuario_Timesheets on emp.NumEmpleado equals usertime.NumEmpleadoRrHh into usertimeJoin
                                      //from usertimeItem in usertimeJoin.DefaultIfEmpty()
                                      //join emplusertime in db.tB_Empleados on usertime.NumProyecto equals emplusertime.NumProyectoPrincipal
                                      //join responsa in db.tB_Personas on emplusertime.IdPersona equals responsa.IdPersona into responsaJoin
                                      from responsaItem in usertimeJoin.DefaultIfEmpty()
                                      join unidad in db.tB_Cat_UnidadNegocios on emp.IdUnidadNegocio equals unidad.IdUnidadNegocio into unidadJoin
                                      from unidadItem in unidadJoin.DefaultIfEmpty()
                                      join contrato_sat in db.tB_Cat_TipoContrato_Sats on emp.IdTipoContrato_sat equals contrato_sat.IdTipoContratoSat into contrato_satJoin
                                      from contrato_satItem in contrato_satJoin.DefaultIfEmpty()
                                      join profesion in db.tB_Cat_Profesiones on emp.IdProfesion equals profesion.IdProfesion into profesionJoin
                                      from profesionItem in profesionJoin.DefaultIfEmpty()
                                      join turno in db.tB_Cat_Turnos on emp.IdTurno equals turno.IdTurno into turnoJoin
                                      from turnoItem in turnoJoin.DefaultIfEmpty()
                                      join proyectoPrin in db.tB_Proyectos on emp.NumProyectoPrincipal equals proyectoPrin.NumProyecto into proyectoPrinJoin
                                      from proyectoPrinItem in proyectoPrinJoin.DefaultIfEmpty()
                                      where emp.Activo == activo
                                      orderby perItem.Nombre ascending
                                      select new Empleado_Detalle
                                      {
                                          nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                                          nukidpersona = emp.IdPersona,
                                          nombre_persona = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty,
                                          dtfecha_nacimiento = perItem.FechaNacimiento,
                                          nukidsexo = perItem.IdSexo,
                                          chsexo = sexItem != null ? sexItem.Sexo : string.Empty,
                                          chcurp = perItem.Curp,
                                          chrfc = perItem.Rfc,
                                          chnacionalidad = perItem.Nacionalidad,
                                          nukidtipo_empleado = emp.IdTipoEmpleado,
                                          chtipo_emplado = tipo_empItem.TipoEmpleado != null ? tipo_empItem.TipoEmpleado : string.Empty,
                                          nukidcategoria = emp.IdCategoria,
                                          chcategoria = catItem != null ? catItem.Categoria : string.Empty,
                                          nukidtipo_contrato = emp.IdTipoContrato,
                                          chtipo_contrato = contratoItem != null ? contratoItem.VeContrato : string.Empty,
                                          chcve_puesto = emp.CvePuesto,
                                          chpuesto = puestoItem != null ? puestoItem.Puesto : string.Empty,
                                          nukidempresa = emp.IdEmpresa,
                                          chempresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                          chcalle = emp.Calle,
                                          nunumero_interior = emp.NumeroInterior,
                                          nunumero_exterior = emp.NumeroExterior,
                                          chcolonia = emp.Colonia,
                                          chalcaldia = emp.Alcaldia,
                                          nukidciudad = emp.IdCiudad,
                                          chciudad = ciudadItem != null ? ciudadItem.Ciudad : string.Empty,
                                          nukidestado = emp.IdEstado,
                                          chestado = estadoItem != null ? estadoItem.Estado : string.Empty,
                                          chcp = emp.CP,
                                          nukidpais = emp.IdPais,
                                          chpais = paisItem != null ? paisItem.Pais : string.Empty,
                                          nukidnivel_estudios = emp.IdNivelEstudios,
                                          chnivel_estudios = estudiosItem != null ? estudiosItem.NivelEstudios : string.Empty,
                                          nukidforma_pago = emp.IdFormaPago,
                                          chforma_pago = pagoItem != null ? pagoItem.TipoDocumento : string.Empty,
                                          nukidjornada = emp.IdJornada,
                                          chjornada = jornadaItem != null ? jornadaItem.Jornada : string.Empty,
                                          nukiddepartamento = emp.IdDepartamento,
                                          chdepartamento = deptoItem != null ? deptoItem.Departamento : string.Empty,
                                          nukidclasificacion = emp.IdClasificacion,
                                          chclasificacion = clasifItem != null ? clasifItem.Clasificacion : string.Empty,
                                          nukidjefe_directo = emp.IdJefeDirecto,
                                          chjefe_directo = jefeItem != null ? jefeItem.Nombre + " " + jefeItem.ApPaterno + " " + jefeItem.ApMaterno : string.Empty,
                                          nukidresponsable = responsaItem.NumEmpleadoRrHh,
                                          chresponsable = responsaItem != null ? responsaItem.Usuario : string.Empty,
                                          nukidunidad_negocio = emp.IdUnidadNegocio,
                                          chunidad_negocio = unidadItem != null ? unidadItem.UnidadNegocio : string.Empty,
                                          nukidtipo_contrato_sat = emp.IdTipoContrato_sat,
                                          chtipo_contrato_sat = contrato_satItem != null ? contrato_satItem.ContratoSat : string.Empty,
                                          nunum_empleado = emp.NumEmpleado,
                                          dtfecha_ingreso = emp.FechaIngreso.ToString("yyyy-MM-dd"),
                                          dtfecha_salida = emp.FechaSalida != null ? Convert.ToDateTime(emp.FechaSalida).ToString("yyyy-MM-dd") : string.Empty,
                                          dtfecha_ultimo_reingreso = emp.FechaUltimoReingreso != null ? Convert.ToDateTime(emp.FechaUltimoReingreso).ToString("yyyy-MM-dd") : string.Empty,
                                          chnss = emp.Nss,
                                          chemail_bovis = emp.EmailBovis,
                                          chexperiencias = emp.Experiencias,
                                          chhabilidades = emp.Habilidades,
                                          churl_repositorio = emp.UrlRepositorio,
                                          nusalario = emp.Salario,
                                          nukidprofesion = emp.IdProfesion,
                                          chprofesion = profesionItem != null ? profesionItem.Profesion : string.Empty,
                                          nukidturno = emp.IdTurno,
                                          chturno = turnoItem != null ? turnoItem.Turno : string.Empty,
                                          nuunidad_medica = emp.UnidadMedica,
                                          chregistro_patronal = emp.RegistroPatronal,
                                          chcotizacion = emp.Cotizacion,
                                          nuduracion = emp.Duracion,
                                          boactivo = emp.Activo,
                                          boempleado = perItem != null ? perItem.EsEmpleado : false,
                                          nudescuento_pension = emp.DescuentoPension,
                                          chporcentaje_pension = emp.ChPorcentajePension,
                                          nufondo_fijo = emp.FondoFijo,
                                          nucredito_infonavit = emp.CreditoInfonavit,
                                          chtipo_descuento = emp.TipoDescuento,
                                          nuvalor_descuento = emp.ValorDescuento,
                                          nuno_empleado_noi = emp.NoEmpleadoNoi,
                                          chrol = emp.Rol,
                                          nuproyecto_principal = emp.NumProyectoPrincipal,
                                          chproyecto_principal = proyectoPrinItem != null ? proyectoPrinItem.Proyecto : string.Empty,
                                          dtvigencia = emp.FechaIngreso.AddDays(30).ToString("yyyy-MM-dd"),
                                          dtvigencia90 = emp.FechaIngreso.AddDays(90).ToString("yyyy-MM-dd"),
                                         // dtvigencia90Letras = emp.FechaIngreso.AddDays(90).ToString("d") + " --- " + emp.FechaIngreso.AddDays(90).ToString("D") + " --- " + emp.FechaIngreso.AddDays(90).ToString("F") ,
                                          SalarioenLetras = emp.Salario.ToString("C") + " " + enletras(emp.Salario.ToString()) + " " + "M.N."
                                      }).ToListAsync();

                    // Se calcula la antigüedad y edad
                    foreach (var emp in resp)
                    {
                        DateTime fecha_ingreso = (emp.dtfecha_ultimo_reingreso.IsNullOrEmpty()) ? Convert.ToDateTime(emp.dtfecha_ingreso) : Convert.ToDateTime(emp.dtfecha_ultimo_reingreso);
                        DateTime fecha_salida = (emp.dtfecha_salida.IsNullOrEmpty()) ? DateTime.Now : Convert.ToDateTime(emp.dtfecha_salida);
                        TimeSpan diferencia = fecha_salida - fecha_ingreso;
                        int años_antiguedad = diferencia.Days / 365;
                        int meses_antiguedad = (diferencia.Days % 365) / 30;
                        int dias_antiguedad = diferencia.Days % 30;
                        emp.nuantiguedad = $"{años_antiguedad} años, {meses_antiguedad} meses, {dias_antiguedad} días";

                        TimeSpan diferenciaEdad = DateTime.Now - emp.dtfecha_nacimiento;
                        int años_edad = diferenciaEdad.Days / 365;
                        emp.chedad = $"{años_edad} años";
                    }

                    return resp;
                }
            }
            else return await GetAllFromEntityAsync<Empleado_Detalle>();
        }

        public async Task<List<Empleado_Detalle>> GetEmpleadosAll(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig))
                {
                    var resp = await (from emp in db.tB_Empleados
                                      join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                      from perItem in perJoin.DefaultIfEmpty()
                                      join sex in db.tB_Cat_Sexos on perItem.IdSexo equals sex.IdSexo into sexJoin
                                      from sexItem in sexJoin.DefaultIfEmpty()
                                      join tipo_emp in db.tB_Cat_TipoEmpleados on emp.IdTipoEmpleado equals tipo_emp.IdTipoEmpleado into tipo_empJoin
                                      from tipo_empItem in tipo_empJoin.DefaultIfEmpty()
                                      join cat in db.tB_Cat_Categorias on emp.IdCategoria equals cat.IdCategoria into catJoin
                                      from catItem in catJoin.DefaultIfEmpty()
                                      join contrato in db.tB_Cat_TipoContratos on emp.IdTipoContrato equals contrato.IdTipoContrato into contratoJoin
                                      from contratoItem in contratoJoin.DefaultIfEmpty()
                                      join puesto in db.tB_Cat_Puestos on emp.CvePuesto equals puesto.IdPuesto into puestoJoin
                                      from puestoItem in puestoJoin.DefaultIfEmpty()
                                      join empresa in db.tB_Empresas on emp.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                      from empresaItem in empresaJoin.DefaultIfEmpty()
                                      join ciudad in db.tB_Ciudads on emp.IdCiudad equals ciudad.IdCiudad into ciudadJoin
                                      from ciudadItem in ciudadJoin.DefaultIfEmpty()
                                      join estado in db.tB_Estados on emp.IdEstado equals estado.IdEstado into estadoJoin
                                      from estadoItem in estadoJoin.DefaultIfEmpty()
                                      join pais in db.tB_Pais on emp.IdPais equals pais.IdPais into paisJoin
                                      from paisItem in paisJoin.DefaultIfEmpty()
                                      join estudios in db.tB_Cat_NivelEstudios on emp.IdNivelEstudios equals estudios.IdNivelEstudios into estudiosJoin
                                      from estudiosItem in estudiosJoin.DefaultIfEmpty()
                                      join pago in db.tB_Cat_FormaPagos on emp.IdFormaPago equals pago.IdFormaPago into pagoJoin
                                      from pagoItem in pagoJoin.DefaultIfEmpty()
                                      join jornada in db.tB_Cat_Jornadas on emp.IdJornada equals jornada.IdJornada into jornadaJoin
                                      from jornadaItem in jornadaJoin.DefaultIfEmpty()
                                      join depto in db.tB_Cat_Departamentos on emp.IdDepartamento equals depto.IdDepartamento into deptoJoin
                                      from deptoItem in deptoJoin.DefaultIfEmpty()
                                      join clasif in db.tB_Cat_Clasificacions on emp.IdClasificacion equals clasif.IdClasificacion into clasifJoin
                                      from clasifItem in clasifJoin.DefaultIfEmpty()
                                      join emp1 in db.tB_Empleados on emp.IdJefeDirecto equals emp1.NumEmpleadoRrHh
                                      join jefe in db.tB_Personas on emp1.IdPersona equals jefe.IdPersona into jefeJoin
                                      from jefeItem in jefeJoin.DefaultIfEmpty()
                                      join unidad in db.tB_Cat_UnidadNegocios on emp.IdUnidadNegocio equals unidad.IdUnidadNegocio into unidadJoin
                                      from unidadItem in unidadJoin.DefaultIfEmpty()
                                      join contrato_sat in db.tB_Cat_TipoContrato_Sats on emp.IdTipoContrato_sat equals contrato_sat.IdTipoContratoSat into contrato_satJoin
                                      from contrato_satItem in contrato_satJoin.DefaultIfEmpty()
                                      join profesion in db.tB_Cat_Profesiones on emp.IdProfesion equals profesion.IdProfesion into profesionJoin
                                      from profesionItem in profesionJoin.DefaultIfEmpty()
                                      join turno in db.tB_Cat_Turnos on emp.IdTurno equals turno.IdTurno into turnoJoin
                                      from turnoItem in turnoJoin.DefaultIfEmpty()
                                      join proyectoPrin in db.tB_Proyectos on emp.NumProyectoPrincipal equals proyectoPrin.NumProyecto into proyectoPrinJoin
                                      from proyectoPrinItem in proyectoPrinJoin.DefaultIfEmpty()
                                     // where emp.Activo == activo
                                      orderby perItem.Nombre ascending
                                      select new Empleado_Detalle
                                      {
                                          nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                                          nukidpersona = emp.IdPersona,
                                          nombre_persona = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty,
                                          dtfecha_nacimiento = perItem.FechaNacimiento,
                                          nukidsexo = perItem.IdSexo,
                                          chsexo = sexItem != null ? sexItem.Sexo : string.Empty,
                                          chcurp = perItem.Curp,
                                          chrfc = perItem.Rfc,
                                          chnacionalidad = perItem.Nacionalidad,
                                          nukidtipo_empleado = emp.IdTipoEmpleado,
                                          chtipo_emplado = tipo_empItem.TipoEmpleado != null ? tipo_empItem.TipoEmpleado : string.Empty,
                                          nukidcategoria = emp.IdCategoria,
                                          chcategoria = catItem != null ? catItem.Categoria : string.Empty,
                                          nukidtipo_contrato = emp.IdTipoContrato,
                                          chtipo_contrato = contratoItem != null ? contratoItem.VeContrato : string.Empty,
                                          chcve_puesto = emp.CvePuesto,
                                          chpuesto = puestoItem != null ? puestoItem.Puesto : string.Empty,
                                          nukidempresa = emp.IdEmpresa,
                                          chempresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                          chcalle = emp.Calle,
                                          nunumero_interior = emp.NumeroInterior,
                                          nunumero_exterior = emp.NumeroExterior,
                                          chcolonia = emp.Colonia,
                                          chalcaldia = emp.Alcaldia,
                                          nukidciudad = emp.IdCiudad,
                                          chciudad = ciudadItem != null ? ciudadItem.Ciudad : string.Empty,
                                          nukidestado = emp.IdEstado,
                                          chestado = estadoItem != null ? estadoItem.Estado : string.Empty,
                                          chcp = emp.CP,
                                          nukidpais = emp.IdPais,
                                          chpais = paisItem != null ? paisItem.Pais : string.Empty,
                                          nukidnivel_estudios = emp.IdNivelEstudios,
                                          chnivel_estudios = estudiosItem != null ? estudiosItem.NivelEstudios : string.Empty,
                                          nukidforma_pago = emp.IdFormaPago,
                                          chforma_pago = pagoItem != null ? pagoItem.TipoDocumento : string.Empty,
                                          nukidjornada = emp.IdJornada,
                                          chjornada = jornadaItem != null ? jornadaItem.Jornada : string.Empty,
                                          nukiddepartamento = emp.IdDepartamento,
                                          chdepartamento = deptoItem != null ? deptoItem.Departamento : string.Empty,
                                          nukidclasificacion = emp.IdClasificacion,
                                          chclasificacion = clasifItem != null ? clasifItem.Clasificacion : string.Empty,
                                          nukidjefe_directo = emp.IdJefeDirecto,
                                          chjefe_directo = jefeItem != null ? jefeItem.Nombre + " " + jefeItem.ApPaterno + " " + jefeItem.ApMaterno : string.Empty,
                                          nukidunidad_negocio = emp.IdUnidadNegocio,
                                          chunidad_negocio = unidadItem != null ? unidadItem.UnidadNegocio : string.Empty,
                                          nukidtipo_contrato_sat = emp.IdTipoContrato_sat,
                                          chtipo_contrato_sat = contrato_satItem != null ? contrato_satItem.ContratoSat : string.Empty,
                                          nunum_empleado = emp.NumEmpleado,
                                          dtfecha_ingreso = emp.FechaIngreso.ToString("yyyy-MM-dd"),
                                          dtfecha_salida = emp.FechaSalida != null ? Convert.ToDateTime(emp.FechaSalida).ToString("yyyy-MM-dd") : string.Empty,
                                          dtfecha_ultimo_reingreso = emp.FechaUltimoReingreso != null ? Convert.ToDateTime(emp.FechaUltimoReingreso).ToString("yyyy-MM-dd") : string.Empty,
                                          chnss = emp.Nss,
                                          chemail_bovis = emp.EmailBovis,
                                          chexperiencias = emp.Experiencias,
                                          chhabilidades = emp.Habilidades,
                                          churl_repositorio = emp.UrlRepositorio,
                                          nusalario = emp.Salario,
                                          nukidprofesion = emp.IdProfesion,
                                          chprofesion = profesionItem != null ? profesionItem.Profesion : string.Empty,
                                          nukidturno = emp.IdTurno,
                                          chturno = turnoItem != null ? turnoItem.Turno : string.Empty,
                                          nuunidad_medica = emp.UnidadMedica,
                                          chregistro_patronal = emp.RegistroPatronal,
                                          chcotizacion = emp.Cotizacion,
                                          nuduracion = emp.Duracion,
                                          boactivo = emp.Activo,
                                          boempleado = perItem != null ? perItem.EsEmpleado : false,
                                          nudescuento_pension = emp.DescuentoPension,
                                          chporcentaje_pension = emp.ChPorcentajePension,
                                          nufondo_fijo = emp.FondoFijo,
                                          nucredito_infonavit = emp.CreditoInfonavit,
                                          chtipo_descuento = emp.TipoDescuento,
                                          nuvalor_descuento = emp.ValorDescuento,
                                          nuno_empleado_noi = emp.NoEmpleadoNoi,
                                          chrol = emp.Rol,
                                          nuproyecto_principal = emp.NumProyectoPrincipal,
                                          chproyecto_principal = proyectoPrinItem != null ? proyectoPrinItem.Proyecto : string.Empty,
                                          dtvigencia = emp.FechaIngreso.AddDays(30).ToString("yyyy-MM-dd"),
                                          dtvigencia90 = emp.FechaIngreso.AddDays(90).ToString("yyyy-MM-dd"),
                                          //dtvigencia90Letras = emp.FechaIngreso.AddDays(90).ToString("d") + " --- " + emp.FechaIngreso.AddDays(90).ToString("D") + " --- " + emp.FechaIngreso.AddDays(90).ToString("F"),
                                          SalarioenLetras = emp.Salario.ToString("C") + " " + enletras(emp.Salario.ToString()) + " " + "M.N."
                                      }).ToListAsync();

                    // Se calcula la antigüedad y edad
                    foreach (var emp in resp)
                    {
                        DateTime fecha_ingreso = (emp.dtfecha_ultimo_reingreso.IsNullOrEmpty()) ? Convert.ToDateTime(emp.dtfecha_ingreso) : Convert.ToDateTime(emp.dtfecha_ultimo_reingreso);
                        DateTime fecha_salida = (emp.dtfecha_salida.IsNullOrEmpty()) ? DateTime.Now : Convert.ToDateTime(emp.dtfecha_salida);
                        TimeSpan diferencia = fecha_salida - fecha_ingreso;
                        int años_antiguedad = diferencia.Days / 365;
                        int meses_antiguedad = (diferencia.Days % 365) / 30;
                        int dias_antiguedad = diferencia.Days % 30;
                        emp.nuantiguedad = $"{años_antiguedad} años, {meses_antiguedad} meses, {dias_antiguedad} días";

                        TimeSpan diferenciaEdad = DateTime.Now - emp.dtfecha_nacimiento;
                        int años_edad = diferenciaEdad.Days / 365;
                        emp.chedad = $"{años_edad} años";
                    }

                    return resp;
                }
            }
            else return await GetAllFromEntityAsync<Empleado_Detalle>();
        }

        //ATC 03-12-2024
        public async Task<List<Empleado_Detalle>> GetEmpleadosAllFiltro(bool? activo, int? idEstado, int? idPuesto, int? idProyecto, int? idEmpresa, int? idUnidadNegocio)
        {
            bool estadocombo = false;
            if (idEstado == 1)
            {
                estadocombo = true;
            }
            if (idEstado == 2)
            {
                estadocombo = false;
            }

            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig))
                {
                    var resp = await (from emp in db.tB_Empleados
                                      join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                      from perItem in perJoin.DefaultIfEmpty()
                                      join sex in db.tB_Cat_Sexos on perItem.IdSexo equals sex.IdSexo into sexJoin
                                      from sexItem in sexJoin.DefaultIfEmpty()
                                      join tipo_emp in db.tB_Cat_TipoEmpleados on emp.IdTipoEmpleado equals tipo_emp.IdTipoEmpleado into tipo_empJoin
                                      from tipo_empItem in tipo_empJoin.DefaultIfEmpty()
                                      join cat in db.tB_Cat_Categorias on emp.IdCategoria equals cat.IdCategoria into catJoin
                                      from catItem in catJoin.DefaultIfEmpty()
                                      join contrato in db.tB_Cat_TipoContratos on emp.IdTipoContrato equals contrato.IdTipoContrato into contratoJoin
                                      from contratoItem in contratoJoin.DefaultIfEmpty()
                                      join puesto in db.tB_Cat_Puestos on emp.CvePuesto equals puesto.IdPuesto into puestoJoin
                                      from puestoItem in puestoJoin.DefaultIfEmpty()
                                      join empresa in db.tB_Empresas on emp.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                      from empresaItem in empresaJoin.DefaultIfEmpty()
                                      join ciudad in db.tB_Ciudads on emp.IdCiudad equals ciudad.IdCiudad into ciudadJoin
                                      from ciudadItem in ciudadJoin.DefaultIfEmpty()
                                      join estado in db.tB_Estados on emp.IdEstado equals estado.IdEstado into estadoJoin
                                      from estadoItem in estadoJoin.DefaultIfEmpty()
                                      join pais in db.tB_Pais on emp.IdPais equals pais.IdPais into paisJoin
                                      from paisItem in paisJoin.DefaultIfEmpty()
                                      join estudios in db.tB_Cat_NivelEstudios on emp.IdNivelEstudios equals estudios.IdNivelEstudios into estudiosJoin
                                      from estudiosItem in estudiosJoin.DefaultIfEmpty()
                                      join pago in db.tB_Cat_FormaPagos on emp.IdFormaPago equals pago.IdFormaPago into pagoJoin
                                      from pagoItem in pagoJoin.DefaultIfEmpty()
                                      join jornada in db.tB_Cat_Jornadas on emp.IdJornada equals jornada.IdJornada into jornadaJoin
                                      from jornadaItem in jornadaJoin.DefaultIfEmpty()
                                      join depto in db.tB_Cat_Departamentos on emp.IdDepartamento equals depto.IdDepartamento into deptoJoin
                                      from deptoItem in deptoJoin.DefaultIfEmpty()
                                      join clasif in db.tB_Cat_Clasificacions on emp.IdClasificacion equals clasif.IdClasificacion into clasifJoin
                                      from clasifItem in clasifJoin.DefaultIfEmpty()
                                      join emp1 in db.tB_Empleados on emp.IdJefeDirecto equals emp1.NumEmpleadoRrHh
                                      join jefe in db.tB_Personas on emp1.IdPersona equals jefe.IdPersona into jefeJoin
                                      from jefeItem in jefeJoin.DefaultIfEmpty()
                                      join unidad in db.tB_Cat_UnidadNegocios on emp.IdUnidadNegocio equals unidad.IdUnidadNegocio into unidadJoin
                                      from unidadItem in unidadJoin.DefaultIfEmpty()
                                      join contrato_sat in db.tB_Cat_TipoContrato_Sats on emp.IdTipoContrato_sat equals contrato_sat.IdTipoContratoSat into contrato_satJoin
                                      from contrato_satItem in contrato_satJoin.DefaultIfEmpty()
                                      join profesion in db.tB_Cat_Profesiones on emp.IdProfesion equals profesion.IdProfesion into profesionJoin
                                      from profesionItem in profesionJoin.DefaultIfEmpty()
                                      join turno in db.tB_Cat_Turnos on emp.IdTurno equals turno.IdTurno into turnoJoin
                                      from turnoItem in turnoJoin.DefaultIfEmpty()
                                      join proyectoPrin in db.tB_Proyectos on emp.NumProyectoPrincipal equals proyectoPrin.NumProyecto into proyectoPrinJoin
                                      from proyectoPrinItem in proyectoPrinJoin.DefaultIfEmpty()
                                      where (idProyecto == 0 || emp.NumProyectoPrincipal == idProyecto)
                                       && (idPuesto == 0 || emp.CvePuesto == idPuesto)
                                       && (idEmpresa == 0 || emp.IdEmpresa == idEmpresa)
                                       && (idUnidadNegocio == 0 || emp.IdUnidadNegocio == idUnidadNegocio)
                                       && (idEstado == 0 || emp.Activo == estadocombo)
                                      // where emp.Activo == activo
                                      orderby perItem.Nombre ascending
                                      select new Empleado_Detalle
                                      {
                                          nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                                          nukidpersona = emp.IdPersona,
                                          nombre_persona = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty,
                                          dtfecha_nacimiento = perItem.FechaNacimiento,
                                          nukidsexo = perItem.IdSexo,
                                          chsexo = sexItem != null ? sexItem.Sexo : string.Empty,
                                          chcurp = perItem.Curp,
                                          chrfc = perItem.Rfc,
                                          chnacionalidad = perItem.Nacionalidad,
                                          nukidtipo_empleado = emp.IdTipoEmpleado,
                                          chtipo_emplado = tipo_empItem.TipoEmpleado != null ? tipo_empItem.TipoEmpleado : string.Empty,
                                          nukidcategoria = emp.IdCategoria,
                                          chcategoria = catItem != null ? catItem.Categoria : string.Empty,
                                          nukidtipo_contrato = emp.IdTipoContrato,
                                          chtipo_contrato = contratoItem != null ? contratoItem.VeContrato : string.Empty,
                                          chcve_puesto = emp.CvePuesto,
                                          chpuesto = puestoItem != null ? puestoItem.Puesto : string.Empty,
                                          nukidempresa = emp.IdEmpresa,
                                          chempresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                          chcalle = emp.Calle,
                                          nunumero_interior = emp.NumeroInterior,
                                          nunumero_exterior = emp.NumeroExterior,
                                          chcolonia = emp.Colonia,
                                          chalcaldia = emp.Alcaldia,
                                          nukidciudad = emp.IdCiudad,
                                          chciudad = ciudadItem != null ? ciudadItem.Ciudad : string.Empty,
                                          nukidestado = emp.IdEstado,
                                          chestado = estadoItem != null ? estadoItem.Estado : string.Empty,
                                          chcp = emp.CP,
                                          nukidpais = emp.IdPais,
                                          chpais = paisItem != null ? paisItem.Pais : string.Empty,
                                          nukidnivel_estudios = emp.IdNivelEstudios,
                                          chnivel_estudios = estudiosItem != null ? estudiosItem.NivelEstudios : string.Empty,
                                          nukidforma_pago = emp.IdFormaPago,
                                          chforma_pago = pagoItem != null ? pagoItem.TipoDocumento : string.Empty,
                                          nukidjornada = emp.IdJornada,
                                          chjornada = jornadaItem != null ? jornadaItem.Jornada : string.Empty,
                                          nukiddepartamento = emp.IdDepartamento,
                                          chdepartamento = deptoItem != null ? deptoItem.Departamento : string.Empty,
                                          nukidclasificacion = emp.IdClasificacion,
                                          chclasificacion = clasifItem != null ? clasifItem.Clasificacion : string.Empty,
                                          nukidjefe_directo = emp.IdJefeDirecto,
                                          chjefe_directo = jefeItem != null ? jefeItem.Nombre + " " + jefeItem.ApPaterno + " " + jefeItem.ApMaterno : string.Empty,
                                          nukidunidad_negocio = emp.IdUnidadNegocio,
                                          chunidad_negocio = unidadItem != null ? unidadItem.UnidadNegocio : string.Empty,
                                          nukidtipo_contrato_sat = emp.IdTipoContrato_sat,
                                          chtipo_contrato_sat = contrato_satItem != null ? contrato_satItem.ContratoSat : string.Empty,
                                          nunum_empleado = emp.NumEmpleado,
                                          dtfecha_ingreso = emp.FechaIngreso.ToString("yyyy-MM-dd"),
                                          dtfecha_salida = emp.FechaSalida != null ? Convert.ToDateTime(emp.FechaSalida).ToString("yyyy-MM-dd") : string.Empty,
                                          dtfecha_ultimo_reingreso = emp.FechaUltimoReingreso != null ? Convert.ToDateTime(emp.FechaUltimoReingreso).ToString("yyyy-MM-dd") : string.Empty,
                                          chnss = emp.Nss,
                                          chemail_bovis = emp.EmailBovis,
                                          chexperiencias = emp.Experiencias,
                                          chhabilidades = emp.Habilidades,
                                          churl_repositorio = emp.UrlRepositorio,
                                          nusalario = emp.Salario,
                                          nukidprofesion = emp.IdProfesion,
                                          chprofesion = profesionItem != null ? profesionItem.Profesion : string.Empty,
                                          nukidturno = emp.IdTurno,
                                          chturno = turnoItem != null ? turnoItem.Turno : string.Empty,
                                          nuunidad_medica = emp.UnidadMedica,
                                          chregistro_patronal = emp.RegistroPatronal,
                                          chcotizacion = emp.Cotizacion,
                                          nuduracion = emp.Duracion,
                                          boactivo = emp.Activo,
                                          boempleado = perItem != null ? perItem.EsEmpleado : false,
                                          nudescuento_pension = emp.DescuentoPension,
                                          chporcentaje_pension = emp.ChPorcentajePension,
                                          nufondo_fijo = emp.FondoFijo,
                                          nucredito_infonavit = emp.CreditoInfonavit,
                                          chtipo_descuento = emp.TipoDescuento,
                                          nuvalor_descuento = emp.ValorDescuento,
                                          nuno_empleado_noi = emp.NoEmpleadoNoi,
                                          chrol = emp.Rol,
                                          nuproyecto_principal = emp.NumProyectoPrincipal,
                                          chproyecto_principal = proyectoPrinItem != null ? proyectoPrinItem.Proyecto : string.Empty,
                                          dtvigencia = emp.FechaIngreso.AddDays(30).ToString("yyyy-MM-dd"),
                                          dtvigencia90 = emp.FechaIngreso.AddDays(90).ToString("yyyy-MM-dd"),
                                          SalarioenLetras = emp.Salario.ToString("C") + " " + enletras(emp.Salario.ToString()) + " " + "M.N."
                                      }).ToListAsync();

                    // Se calcula la antigüedad y edad
                    foreach (var emp in resp)
                    {
                        DateTime fecha_ingreso = (emp.dtfecha_ultimo_reingreso.IsNullOrEmpty()) ? Convert.ToDateTime(emp.dtfecha_ingreso) : Convert.ToDateTime(emp.dtfecha_ultimo_reingreso);
                        DateTime fecha_salida = (emp.dtfecha_salida.IsNullOrEmpty()) ? DateTime.Now : Convert.ToDateTime(emp.dtfecha_salida);
                        TimeSpan diferencia = fecha_salida - fecha_ingreso;
                        int años_antiguedad = diferencia.Days / 365;
                        int meses_antiguedad = (diferencia.Days % 365) / 30;
                        int dias_antiguedad = diferencia.Days % 30;
                        emp.nuantiguedad = $"{años_antiguedad} años, {meses_antiguedad} meses, {dias_antiguedad} días";

                        TimeSpan diferenciaEdad = DateTime.Now - emp.dtfecha_nacimiento;
                        int años_edad = diferenciaEdad.Days / 365;
                        emp.chedad = $"{años_edad} años";
                    }

                    return resp;
                }
            }
            else return await GetAllFromEntityAsync<Empleado_Detalle>();
        }

        public async Task<List<Empleado_Detalle>> GetEmpleadosByIDPuesto(string idPuesto)
        {

            int id_Puesto = Convert.ToInt32(idPuesto);
           // if (activo.HasValue)
            //{
                using (var db = new ConnectionDB(dbConfig))
                {
                    var resp = await (from emp in db.tB_Empleados
                                      join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                      from perItem in perJoin.DefaultIfEmpty()
                                      join sex in db.tB_Cat_Sexos on perItem.IdSexo equals sex.IdSexo into sexJoin
                                      from sexItem in sexJoin.DefaultIfEmpty()
                                      join tipo_emp in db.tB_Cat_TipoEmpleados on emp.IdTipoEmpleado equals tipo_emp.IdTipoEmpleado into tipo_empJoin
                                      from tipo_empItem in tipo_empJoin.DefaultIfEmpty()
                                      join cat in db.tB_Cat_Categorias on emp.IdCategoria equals cat.IdCategoria into catJoin
                                      from catItem in catJoin.DefaultIfEmpty()
                                      join contrato in db.tB_Cat_TipoContratos on emp.IdTipoContrato equals contrato.IdTipoContrato into contratoJoin
                                      from contratoItem in contratoJoin.DefaultIfEmpty()
                                      join puesto in db.tB_Cat_Puestos on emp.CvePuesto equals puesto.IdPuesto into puestoJoin
                                      from puestoItem in puestoJoin.DefaultIfEmpty()
                                      join empresa in db.tB_Empresas on emp.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                      from empresaItem in empresaJoin.DefaultIfEmpty()
                                      join ciudad in db.tB_Ciudads on emp.IdCiudad equals ciudad.IdCiudad into ciudadJoin
                                      from ciudadItem in ciudadJoin.DefaultIfEmpty()
                                      join estado in db.tB_Estados on emp.IdEstado equals estado.IdEstado into estadoJoin
                                      from estadoItem in estadoJoin.DefaultIfEmpty()
                                      join pais in db.tB_Pais on emp.IdPais equals pais.IdPais into paisJoin
                                      from paisItem in paisJoin.DefaultIfEmpty()
                                      join estudios in db.tB_Cat_NivelEstudios on emp.IdNivelEstudios equals estudios.IdNivelEstudios into estudiosJoin
                                      from estudiosItem in estudiosJoin.DefaultIfEmpty()
                                      join pago in db.tB_Cat_FormaPagos on emp.IdFormaPago equals pago.IdFormaPago into pagoJoin
                                      from pagoItem in pagoJoin.DefaultIfEmpty()
                                      join jornada in db.tB_Cat_Jornadas on emp.IdJornada equals jornada.IdJornada into jornadaJoin
                                      from jornadaItem in jornadaJoin.DefaultIfEmpty()
                                      join depto in db.tB_Cat_Departamentos on emp.IdDepartamento equals depto.IdDepartamento into deptoJoin
                                      from deptoItem in deptoJoin.DefaultIfEmpty()
                                      join clasif in db.tB_Cat_Clasificacions on emp.IdClasificacion equals clasif.IdClasificacion into clasifJoin
                                      from clasifItem in clasifJoin.DefaultIfEmpty()
                                      join emp1 in db.tB_Empleados on emp.IdJefeDirecto equals emp1.NumEmpleadoRrHh
                                      join jefe in db.tB_Personas on emp1.IdPersona equals jefe.IdPersona into jefeJoin
                                      from jefeItem in jefeJoin.DefaultIfEmpty()
                                      join unidad in db.tB_Cat_UnidadNegocios on emp.IdUnidadNegocio equals unidad.IdUnidadNegocio into unidadJoin
                                      from unidadItem in unidadJoin.DefaultIfEmpty()
                                      join contrato_sat in db.tB_Cat_TipoContrato_Sats on emp.IdTipoContrato_sat equals contrato_sat.IdTipoContratoSat into contrato_satJoin
                                      from contrato_satItem in contrato_satJoin.DefaultIfEmpty()
                                      join profesion in db.tB_Cat_Profesiones on emp.IdProfesion equals profesion.IdProfesion into profesionJoin
                                      from profesionItem in profesionJoin.DefaultIfEmpty()
                                      join turno in db.tB_Cat_Turnos on emp.IdTurno equals turno.IdTurno into turnoJoin
                                      from turnoItem in turnoJoin.DefaultIfEmpty()
                                      join proyectoPrin in db.tB_Proyectos on emp.NumProyectoPrincipal equals proyectoPrin.NumProyecto into proyectoPrinJoin
                                      from proyectoPrinItem in proyectoPrinJoin.DefaultIfEmpty()
                                      where emp.CvePuesto == id_Puesto
                                      orderby perItem.Nombre ascending
                                      select new Empleado_Detalle
                                      {
                                          nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                                          nukidpersona = emp.IdPersona,
                                          nombre_persona = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty,
                                          dtfecha_nacimiento = perItem.FechaNacimiento,
                                          nukidsexo = perItem.IdSexo,
                                          chsexo = sexItem != null ? sexItem.Sexo : string.Empty,
                                          chcurp = perItem.Curp,
                                          chrfc = perItem.Rfc,
                                          chnacionalidad = perItem.Nacionalidad,
                                          nukidtipo_empleado = emp.IdTipoEmpleado,
                                          chtipo_emplado = tipo_empItem.TipoEmpleado != null ? tipo_empItem.TipoEmpleado : string.Empty,
                                          nukidcategoria = emp.IdCategoria,
                                          chcategoria = catItem != null ? catItem.Categoria : string.Empty,
                                          nukidtipo_contrato = emp.IdTipoContrato,
                                          chtipo_contrato = contratoItem != null ? contratoItem.VeContrato : string.Empty,
                                          chcve_puesto = emp.CvePuesto,
                                          chpuesto = puestoItem != null ? puestoItem.Puesto : string.Empty,
                                          nukidempresa = emp.IdEmpresa,
                                          chempresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                          chcalle = emp.Calle,
                                          nunumero_interior = emp.NumeroInterior,
                                          nunumero_exterior = emp.NumeroExterior,
                                          chcolonia = emp.Colonia,
                                          chalcaldia = emp.Alcaldia,
                                          nukidciudad = emp.IdCiudad,
                                          chciudad = ciudadItem != null ? ciudadItem.Ciudad : string.Empty,
                                          nukidestado = emp.IdEstado,
                                          chestado = estadoItem != null ? estadoItem.Estado : string.Empty,
                                          chcp = emp.CP,
                                          nukidpais = emp.IdPais,
                                          chpais = paisItem != null ? paisItem.Pais : string.Empty,
                                          nukidnivel_estudios = emp.IdNivelEstudios,
                                          chnivel_estudios = estudiosItem != null ? estudiosItem.NivelEstudios : string.Empty,
                                          nukidforma_pago = emp.IdFormaPago,
                                          chforma_pago = pagoItem != null ? pagoItem.TipoDocumento : string.Empty,
                                          nukidjornada = emp.IdJornada,
                                          chjornada = jornadaItem != null ? jornadaItem.Jornada : string.Empty,
                                          nukiddepartamento = emp.IdDepartamento,
                                          chdepartamento = deptoItem != null ? deptoItem.Departamento : string.Empty,
                                          nukidclasificacion = emp.IdClasificacion,
                                          chclasificacion = clasifItem != null ? clasifItem.Clasificacion : string.Empty,
                                          nukidjefe_directo = emp.IdJefeDirecto,
                                          chjefe_directo = jefeItem != null ? jefeItem.Nombre + " " + jefeItem.ApPaterno + " " + jefeItem.ApMaterno : string.Empty,
                                          nukidunidad_negocio = emp.IdUnidadNegocio,
                                          chunidad_negocio = unidadItem != null ? unidadItem.UnidadNegocio : string.Empty,
                                          nukidtipo_contrato_sat = emp.IdTipoContrato_sat,
                                          chtipo_contrato_sat = contrato_satItem != null ? contrato_satItem.ContratoSat : string.Empty,
                                          nunum_empleado = emp.NumEmpleado,
                                          dtfecha_ingreso = emp.FechaIngreso.ToString("yyyy-MM-dd"),
                                          dtfecha_salida = emp.FechaSalida != null ? Convert.ToDateTime(emp.FechaSalida).ToString("yyyy-MM-dd") : string.Empty,
                                          dtfecha_ultimo_reingreso = emp.FechaUltimoReingreso != null ? Convert.ToDateTime(emp.FechaUltimoReingreso).ToString("yyyy-MM-dd") : string.Empty,
                                          chnss = emp.Nss,
                                          chemail_bovis = emp.EmailBovis,
                                          chexperiencias = emp.Experiencias,
                                          chhabilidades = emp.Habilidades,
                                          churl_repositorio = emp.UrlRepositorio,
                                          nusalario = emp.Salario,
                                          nukidprofesion = emp.IdProfesion,
                                          chprofesion = profesionItem != null ? profesionItem.Profesion : string.Empty,
                                          nukidturno = emp.IdTurno,
                                          chturno = turnoItem != null ? turnoItem.Turno : string.Empty,
                                          nuunidad_medica = emp.UnidadMedica,
                                          chregistro_patronal = emp.RegistroPatronal,
                                          chcotizacion = emp.Cotizacion,
                                          nuduracion = emp.Duracion,
                                          boactivo = emp.Activo,
                                          boempleado = perItem != null ? perItem.EsEmpleado : false,
                                          nudescuento_pension = emp.DescuentoPension,
                                          chporcentaje_pension = emp.ChPorcentajePension,
                                          nufondo_fijo = emp.FondoFijo,
                                          nucredito_infonavit = emp.CreditoInfonavit,
                                          chtipo_descuento = emp.TipoDescuento,
                                          nuvalor_descuento = emp.ValorDescuento,
                                          nuno_empleado_noi = emp.NoEmpleadoNoi,
                                          chrol = emp.Rol,
                                          nuproyecto_principal = emp.NumProyectoPrincipal,
                                          chproyecto_principal = proyectoPrinItem != null ? proyectoPrinItem.Proyecto : string.Empty,
                                          dtvigencia = emp.FechaIngreso.AddDays(30).ToString("yyyy-MM-dd"),
                                          dtvigencia90 = emp.FechaIngreso.AddDays(90).ToString("yyyy-MM-dd"),
                                          SalarioenLetras = emp.Salario.ToString("C") + " " + enletras(emp.Salario.ToString()) + " " + "M.N."
                                      }).ToListAsync();

                    // Se calcula la antigüedad y edad
                    foreach (var emp in resp)
                    {
                        DateTime fecha_ingreso = (emp.dtfecha_ultimo_reingreso.IsNullOrEmpty()) ? Convert.ToDateTime(emp.dtfecha_ingreso) : Convert.ToDateTime(emp.dtfecha_ultimo_reingreso);
                        DateTime fecha_salida = (emp.dtfecha_salida.IsNullOrEmpty()) ? DateTime.Now : Convert.ToDateTime(emp.dtfecha_salida);
                        TimeSpan diferencia = fecha_salida - fecha_ingreso;
                        int años_antiguedad = diferencia.Days / 365;
                        int meses_antiguedad = (diferencia.Days % 365) / 30;
                        int dias_antiguedad = diferencia.Days % 30;
                        emp.nuantiguedad = $"{años_antiguedad} años, {meses_antiguedad} meses, {dias_antiguedad} días";

                        TimeSpan diferenciaEdad = DateTime.Now - emp.dtfecha_nacimiento;
                        int años_edad = diferenciaEdad.Days / 365;
                        emp.chedad = $"{años_edad} años";
                    }

                    return resp;
                }
            //}
            //else return await GetAllFromEntityAsync<Empleado_Detalle>();
        }

        public async Task<Empleado_Detalle> GetEmpleado(string idEmpleado)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from emp in db.tB_Empleados
                                 join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                 from perItem in perJoin.DefaultIfEmpty()
                                 join sex in db.tB_Cat_Sexos on perItem.IdSexo equals sex.IdSexo into sexJoin
                                 from sexItem in sexJoin.DefaultIfEmpty()
                                 join tipo_emp in db.tB_Cat_TipoEmpleados on emp.IdTipoEmpleado equals tipo_emp.IdTipoEmpleado into tipo_empJoin
                                 from tipo_empItem in tipo_empJoin.DefaultIfEmpty()
                                 join cat in db.tB_Cat_Categorias on emp.IdCategoria equals cat.IdCategoria into catJoin
                                 from catItem in catJoin.DefaultIfEmpty()
                                 join contrato in db.tB_Cat_TipoContratos on emp.IdTipoContrato equals contrato.IdTipoContrato into contratoJoin
                                 from contratoItem in contratoJoin.DefaultIfEmpty()
                                 join puesto in db.tB_Cat_Puestos on emp.CvePuesto equals puesto.IdPuesto into puestoJoin
                                 from puestoItem in puestoJoin.DefaultIfEmpty()
                                 join empresa in db.tB_Empresas on emp.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                 from empresaItem in empresaJoin.DefaultIfEmpty()
                                 join ciudad in db.tB_Ciudads on emp.IdCiudad equals ciudad.IdCiudad into ciudadJoin
                                 from ciudadItem in ciudadJoin.DefaultIfEmpty()
                                 join estado in db.tB_Estados on emp.IdEstado equals estado.IdEstado into estadoJoin
                                 from estadoItem in estadoJoin.DefaultIfEmpty()
                                 join pais in db.tB_Pais on emp.IdPais equals pais.IdPais into paisJoin
                                 from paisItem in paisJoin.DefaultIfEmpty()
                                 join estudios in db.tB_Cat_NivelEstudios on emp.IdNivelEstudios equals estudios.IdNivelEstudios into estudiosJoin
                                 from estudiosItem in estudiosJoin.DefaultIfEmpty()
                                 join pago in db.tB_Cat_FormaPagos on emp.IdFormaPago equals pago.IdFormaPago into pagoJoin
                                 from pagoItem in pagoJoin.DefaultIfEmpty()
                                 join jornada in db.tB_Cat_Jornadas on emp.IdJornada equals jornada.IdJornada into jornadaJoin
                                 from jornadaItem in jornadaJoin.DefaultIfEmpty()
                                 join depto in db.tB_Cat_Departamentos on emp.IdDepartamento equals depto.IdDepartamento into deptoJoin
                                 from deptoItem in deptoJoin.DefaultIfEmpty()
                                 join clasif in db.tB_Cat_Clasificacions on emp.IdClasificacion equals clasif.IdClasificacion into clasifJoin
                                 from clasifItem in clasifJoin.DefaultIfEmpty()
                                 join emp1 in db.tB_Empleados on emp.IdJefeDirecto equals emp1.NumEmpleadoRrHh
                                 join jefe in db.tB_Personas on emp1.IdPersona equals jefe.IdPersona into jefeJoin
                                 from jefeItem in jefeJoin.DefaultIfEmpty()
                                 join unidad in db.tB_Cat_UnidadNegocios on emp.IdUnidadNegocio equals unidad.IdUnidadNegocio into unidadJoin
                                 from unidadItem in unidadJoin.DefaultIfEmpty()
                                 join contrato_sat in db.tB_Cat_TipoContrato_Sats on emp.IdTipoContrato_sat equals contrato_sat.IdTipoContratoSat into contrato_satJoin
                                 from contrato_satItem in contrato_satJoin.DefaultIfEmpty()
                                 join profesion in db.tB_Cat_Profesiones on emp.IdProfesion equals profesion.IdProfesion into profesionJoin
                                 from profesionItem in profesionJoin.DefaultIfEmpty()
                                 join turno in db.tB_Cat_Turnos on emp.IdTurno equals turno.IdTurno into turnoJoin
                                 from turnoItem in turnoJoin.DefaultIfEmpty()
                                 join sexo in db.tB_Cat_Sexos on perItem.IdSexo equals sexo.IdSexo into sexoJoin
                                 from sexoItem in sexoJoin.DefaultIfEmpty()
                                 join proyectoPrin in db.tB_Proyectos on emp.NumProyectoPrincipal equals proyectoPrin.NumProyecto into proyectoPrinJoin
                                 from proyectoPrinItem in proyectoPrinJoin.DefaultIfEmpty()
                                 where emp.NumEmpleadoRrHh == idEmpleado
                                 select new Empleado_Detalle
                                 {
                                     nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                                     nukidpersona = emp.IdPersona,
                                     nombre_persona = perItem != null ? perItem.Nombre.ToUpper() + " " + perItem.ApPaterno.ToUpper() + " " + perItem.ApMaterno.ToUpper() : string.Empty,
                                     dtfecha_nacimiento = perItem.FechaNacimiento,
                                     nukidsexo = perItem.IdSexo,
                                     chsexo = sexItem != null ? sexItem.Sexo : string.Empty,
                                     chcurp = perItem.Curp,
                                     chrfc = perItem.Rfc,
                                     chnacionalidad = perItem.Nacionalidad,
                                     nukidtipo_empleado = emp.IdTipoEmpleado,
                                     chtipo_emplado = tipo_empItem.TipoEmpleado != null ? tipo_empItem.TipoEmpleado : string.Empty,
                                     nukidcategoria = emp.IdCategoria,
                                     chcategoria = catItem != null ? catItem.Categoria : string.Empty,
                                     nukidtipo_contrato = emp.IdTipoContrato,
                                     chtipo_contrato = contratoItem != null ? contratoItem.VeContrato : string.Empty,
                                     chcve_puesto = emp.CvePuesto,
                                     chpuesto = puestoItem != null ? puestoItem.Puesto : string.Empty,
                                     nukidempresa = emp.IdEmpresa,
                                     chempresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                     chcalle = emp.Calle,
                                     nunumero_interior = emp.NumeroInterior,
                                     nunumero_exterior = emp.NumeroExterior,
                                     chcolonia = emp.Colonia,
                                     chalcaldia = emp.Alcaldia,
                                     nukidciudad = emp.IdCiudad,
                                     chciudad = ciudadItem != null ? ciudadItem.Ciudad : string.Empty,
                                     nukidestado = emp.IdEstado,
                                     chestado = estadoItem != null ? estadoItem.Estado : string.Empty,
                                     chcp = emp.CP,
                                     nukidpais = emp.IdPais,
                                     chpais = paisItem != null ? paisItem.Pais : string.Empty,
                                     nukidnivel_estudios = emp.IdNivelEstudios,
                                     chnivel_estudios = estudiosItem != null ? estudiosItem.NivelEstudios : string.Empty,
                                     nukidforma_pago = emp.IdFormaPago,
                                     chforma_pago = pagoItem != null ? pagoItem.TipoDocumento : string.Empty,
                                     nukidjornada = emp.IdJornada,
                                     chjornada = jornadaItem != null ? jornadaItem.Jornada : string.Empty,
                                     nukiddepartamento = emp.IdDepartamento,
                                     chdepartamento = deptoItem != null ? deptoItem.Departamento : string.Empty,
                                     nukidclasificacion = emp.IdClasificacion,
                                     chclasificacion = clasifItem != null ? clasifItem.Clasificacion : string.Empty,
                                     nukidjefe_directo = emp.IdJefeDirecto,
                                     chjefe_directo = jefeItem != null ? jefeItem.Nombre + " " + jefeItem.ApPaterno + " " + jefeItem.ApMaterno : string.Empty,
                                     nukidunidad_negocio = emp.IdUnidadNegocio,
                                     chunidad_negocio = unidadItem != null ? unidadItem.UnidadNegocio : string.Empty,
                                     nukidtipo_contrato_sat = emp.IdTipoContrato_sat,
                                     chtipo_contrato_sat = contrato_satItem != null ? contrato_satItem.ContratoSat : string.Empty,
                                     nunum_empleado = emp.NumEmpleado,
                                     dtfecha_ingreso = emp.FechaIngreso.ToString("yyyy-MM-dd"),
                                     dtfecha_ingresoLetras = emp.FechaIngreso.ToString("D"),
                                     dtfecha_salida = emp.FechaSalida != null ? Convert.ToDateTime(emp.FechaSalida).ToString("yyyy-MM-dd") : string.Empty,
                                     dtfecha_salidaLetras = emp.FechaSalida != null ? Convert.ToDateTime(emp.FechaSalida).ToString("D") : string.Empty,
                                     dtfecha_ultimo_reingreso = emp.FechaUltimoReingreso != null ? Convert.ToDateTime(emp.FechaUltimoReingreso).ToString("yyyy-MM-dd") : string.Empty,
                                     dtfecha_ultimo_reingresoLetras = emp.FechaUltimoReingreso != null ? Convert.ToDateTime(emp.FechaUltimoReingreso).ToString("D") : string.Empty,
                                     chnss = emp.Nss,
                                     chemail_bovis = emp.EmailBovis,
                                     chexperiencias = emp.Experiencias,
                                     chhabilidades = emp.Habilidades,
                                     churl_repositorio = emp.UrlRepositorio,
                                     nusalario = emp.Salario,
                                     nukidprofesion = emp.IdProfesion,
                                     chprofesion = profesionItem != null ? profesionItem.Profesion : string.Empty,
                                     nukidturno = emp.IdTurno,
                                     chturno = turnoItem != null ? turnoItem.Turno : string.Empty,
                                     nuunidad_medica = emp.UnidadMedica,
                                     chregistro_patronal = emp.RegistroPatronal,
                                     chcotizacion = emp.Cotizacion,
                                     nuduracion = emp.Duracion,
                                     boactivo = emp.Activo,
                                     boempleado = perItem != null ? perItem.EsEmpleado : false,
                                     nudescuento_pension = emp.DescuentoPension,
                                     chporcentaje_pension = emp.ChPorcentajePension,
                                     nufondo_fijo = emp.FondoFijo,
                                     nucredito_infonavit = emp.CreditoInfonavit,
                                     chtipo_descuento = emp.TipoDescuento,
                                     nuvalor_descuento = emp.ValorDescuento,
                                     nuno_empleado_noi = emp.NoEmpleadoNoi,
                                     chrol = emp.Rol,
                                     nuproyecto_principal = emp.NumProyectoPrincipal,
                                     chproyecto_principal = proyectoPrinItem != null ? proyectoPrinItem.Proyecto : string.Empty,
                                     dtvigencia = emp.FechaIngreso.AddDays(30).ToString("yyyy-MM-dd"),
                                     dtvigenciaLetras = emp.FechaIngreso.AddDays(30).ToString("D"),
                                     dtvigencia90 = emp.FechaIngreso.AddDays(90).ToString("yyyy-MM-dd"),
                                     //ATC GET EMPLEADO
                                     dtvigencia90Letras = emp.FechaIngreso.AddDays(90).ToString("D"),
                                     SalarioenLetras = emp.Salario.ToString("C") + " " + enletras(emp.Salario.ToString()) + " " + "M.N."
                                 }).FirstOrDefaultAsync();

                if (res != null)
                {
                    // Se calcula la antigüedad y edad
                    DateTime fecha_ingreso = (res.dtfecha_ultimo_reingreso.IsNullOrEmpty()) ? Convert.ToDateTime(res.dtfecha_ingreso) : Convert.ToDateTime(res.dtfecha_ultimo_reingreso);
                    DateTime fecha_salida = (res.dtfecha_salida.IsNullOrEmpty()) ? DateTime.Now : Convert.ToDateTime(res.dtfecha_salida);
                    TimeSpan diferencia = fecha_salida - fecha_ingreso;
                    int años_antiguedad = diferencia.Days / 365;
                    int meses_antiguedad = (diferencia.Days % 365) / 30;
                    int dias_antiguedad = diferencia.Days % 30;
                    res.nuantiguedad = $"{años_antiguedad} años, {meses_antiguedad} meses, {dias_antiguedad} días";

                    TimeSpan diferenciaEdad = DateTime.Now - res.dtfecha_nacimiento;
                    int años_edad = diferenciaEdad.Days / 365;
                    res.chedad = $"{años_edad} años";



                    res.experiencias = await (from exp in db.tB_Empleado_Experiencias
                                              join cat in db.tB_Cat_Experiencias on exp.IdExperiencia equals cat.IdExperiencia
                                              where exp.IdEmpleado == idEmpleado
                                              && exp.Activo == true
                                              select new Experiencia_Detalle
                                              {
                                                  IdEmpleado = exp.IdEmpleado,
                                                  IdExperiencia = exp.IdExperiencia,
                                                  Experiencia = cat.Experiencia,
                                                  Activo = exp.Activo
                                              }).ToListAsync();

                    res.experiencias = res.experiencias.DistinctBy(x => x.IdExperiencia).ToList();

                    foreach (var exp in res.experiencias)
                    {
                        res.chexperiencias += (res.chexperiencias == null) ? exp.Experiencia : ", " + exp.Experiencia;
                    }

                    res.habilidades = await (from hab in db.tB_Empleado_Habilidades
                                             join cat in db.tB_Cat_Habilidades on hab.IdHabilidad equals cat.IdHabilidad
                                             where hab.IdEmpleado == idEmpleado
                                             && hab.Activo == true
                                             select new Habilidad_Detalle
                                             {
                                                 IdEmpleado = hab.IdEmpleado,
                                                 IdHabilidad = hab.IdHabilidad,
                                                 Habilidad = cat.Habilidad,
                                                 Activo = hab.Activo
                                             }).ToListAsync();

                    res.habilidades = res.habilidades.DistinctBy(x => x.IdHabilidad).ToList();

                    foreach (var hab in res.habilidades)
                    {
                        res.chhabilidades += (res.chhabilidades == null) ? hab.Habilidad : ", " + hab.Habilidad;
                    }
                }

                res.proyecto = await (from proy in db.tB_Proyectos
                                      join rel in db.tB_EmpleadoProyectos on proy.NumProyecto equals rel.NumProyecto
                                      where rel.NumEmpleadoRrHh == res.nunum_empleado_rr_hh
                                      select new Empleado_Proyecto_Info
                                      {
                                          NumProyecto = proy.NumProyecto,
                                          Proyecto = proy.Proyecto,
                                          FechaInicio = proy.FechaIni,
                                          FechaFin = proy.FechaFin
                                      }).FirstOrDefaultAsync();

                return res;

            }
        }

        public async Task<Empleado_BasicData> GetEmpleadoByEmail(string email)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from emp in db.tB_Empleados
                                 join per in db.tB_Personas on emp.IdPersona equals per.IdPersona
                                 where emp.EmailBovis == email
                                 orderby per.Nombre ascending
                                 select new Empleado_BasicData
                                 {
                                     nukid_empleado = emp.NumEmpleadoRrHh,
                                     chnombre = per.Nombre,
                                     chap_paterno = per.ApPaterno,
                                     chap_materno = per.ApMaterno
                                 }).FirstOrDefaultAsync();

                return res;
            }
        }

        public async Task<List<Empleado_BasicData>> GetEmpleadoDetalle()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from emp in db.tB_Empleados
                                 join per in db.tB_Personas on emp.IdPersona equals per.IdPersona
                                 join puesto in db.tB_Cat_Puestos on emp.CvePuesto equals puesto.IdPuesto into puestoJoin
                                 from puestoItem in puestoJoin.DefaultIfEmpty()
                                 where puestoItem.IdNivel == 3
                                 select new Empleado_BasicData
                                 {
                                     nukid_empleado = emp.NumEmpleadoRrHh,
                                     chnombre = per.Nombre,
                                     chap_paterno = per.ApPaterno,
                                     chap_materno = per.ApMaterno,
                                     chpuesto = puestoItem != null ? puestoItem.Puesto : string.Empty
                                 }).ToListAsync();

                return res;
            }
        }

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string num_empleado_rr_hh = registro["num_empleado_rr_hh"].ToString();
            int id_persona = Convert.ToInt32(registro["id_persona"].ToString());
            int id_tipo_empleado = Convert.ToInt32(registro["id_tipo_empleado"].ToString());
            int id_categoria = Convert.ToInt32(registro["id_categoria"].ToString());
            int id_tipo_contrato = Convert.ToInt32(registro["id_tipo_contrato"].ToString());
            int cve_puesto = Convert.ToInt32(registro["cve_puesto"].ToString());
            int id_empresa = Convert.ToInt32(registro["id_empresa"].ToString());
            string? calle = registro["calle"] != null ? registro["calle"].ToString() : null;
            string? numero_exterior = registro["numero_exterior"] != null ? registro["numero_exterior"].ToString() : null;
            string? numero_interior = registro["numero_interior"] != null ? registro["numero_interior"].ToString() : null;
            string? colonia = registro["colonia"] != null ? registro["colonia"].ToString() : null;
            string? alcaldia = registro["alcaldia"] != null ? registro["alcaldia"].ToString() : null;
            int id_ciudad = Convert.ToInt32(registro["id_ciudad"].ToString());
            int? id_estado = registro["id_estado"] != null ? Convert.ToInt32(registro["id_estado"].ToString()) : null;
            string? codigo_postal = registro["codigo_postal"] != null ? registro["codigo_postal"].ToString() : null;
            int? id_pais = registro["id_pais"] != null ? Convert.ToInt32(registro["id_pais"].ToString()) : null;
            int id_nivel_estudios = registro["id_nivel_estudios"] != null ? Convert.ToInt32(registro["id_nivel_estudios"].ToString()) : 0;
            int id_forma_pago = Convert.ToInt32(registro["id_forma_pago"].ToString());
            int? id_jornada = registro["id_jornada"] != null ? Convert.ToInt32(registro["id_jornada"].ToString()) : null;
            int? id_departamento = registro["id_departamento"] != null ? Convert.ToInt32(registro["id_departamento"].ToString()) : null;
            int? id_clasificacion = registro["id_clasificacion"] != null ? Convert.ToInt32(registro["id_clasificacion"].ToString()) : null;
            string? id_jefe_directo = registro["id_jefe_directo"] != null ? registro["id_jefe_directo"].ToString() : null;
            int? id_unidad_negocio = registro["id_unidad_negocio"] != null ? Convert.ToInt32(registro["id_unidad_negocio"].ToString()) : null;
            int? id_tipo_contrato_sat = registro["id_tipo_contrato_sat"] != null ? Convert.ToInt32(registro["id_tipo_contrato_sat"].ToString()) : null;
            string? num_empleado = registro["num_empleado"] != null ? registro["num_empleado"].ToString() : null;
            DateTime fecha_ingreso = Convert.ToDateTime(registro["fecha_ingreso"].ToString());
            DateTime? fecha_salida = registro["fecha_salida"] != null ? Convert.ToDateTime(registro["fecha_salida"].ToString()) : null;
            DateTime? fecha_ultimo_reingreso = registro["fecha_ultimo_reingreso"] != null ? Convert.ToDateTime(registro["fecha_ultimo_reingreso"].ToString()) : null;
            string? nss = registro["nss"] != null ? registro["nss"].ToString() : null;
            string? email_bovis = registro["email_bovis"] != null ? registro["email_bovis"].ToString() : null;
            string? url_repo = registro["url_repo"] != null ? registro["url_repo"].ToString() : null;
            decimal salario = Convert.ToDecimal(registro["salario"].ToString());
            int? id_profesion = registro["id_profesion"] != null ? Convert.ToInt32(registro["id_profesion"].ToString()) : null;
            int antiguedad = registro["antiguedad"] != null ? Convert.ToInt32(registro["antiguedad"].ToString()) : 0;
            int? id_turno = registro["id_turno"] != null ? Convert.ToInt32(registro["id_turno"].ToString()) : null;
            int? unidad_medica = registro["unidad_medica"] != null ? Convert.ToInt32(registro["unidad_medica"].ToString()) : null;
            string? registro_patronal = registro["registro_patronal"] != null ? registro["registro_patronal"].ToString() : null;
            string? cotizacion = registro["cotizacion"] != null ? registro["cotizacion"].ToString() : null;
            int? duracion = registro["duracion"] != null ? Convert.ToInt32(registro["duracion"].ToString()) : null;
            decimal descuento_pension = registro["descuento_pension"] != null ? Convert.ToDecimal(registro["descuento_pension"].ToString()) : 0;
            string? porcentaje_pension = registro["porcentaje_pension"] != null ? registro["porcentaje_pension"].ToString() : null;
            decimal? fondo_fijo = registro["fondo_fijo"] != null ? Convert.ToDecimal(registro["fondo_fijo"].ToString()) : null;
            string? credito_infonavit = registro["credito_infonavit"] != null ? registro["credito_infonavit"].ToString() : null;
            string? tipo_descuento = registro["tipo_descuento"] != null ? registro["tipo_descuento"].ToString() : null;
            decimal? valor_descuento = registro["valor_descuento"] != null ? Convert.ToDecimal(registro["valor_descuento"].ToString()) : null;
            string? no_empleado_noi = registro["no_empleado_noi"] != null ? registro["no_empleado_noi"].ToString() : null;
            string? rol = registro["rol"] != null ? registro["rol"].ToString() : null;
            int id_requerimiento = Convert.ToInt32(registro["id_requerimiento"].ToString());
            int num_proyecto_principal = Convert.ToInt32(registro["num_proyecto_principal"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from emp in db.tB_Empleados
                                 where emp.NumEmpleadoRrHh == num_empleado_rr_hh
                                 select emp).FirstOrDefaultAsync();

                if (res != null)
                {
                    resp.Success = true;
                    resp.Message = String.Format("Ya existe un registro de empleado con el número RR HH: {0}", num_empleado_rr_hh);
                    return resp;
                }

                string last_inserted_id = string.Empty;

                var insert_empleado = await db.tB_Empleados
                    .Value(x => x.NumEmpleadoRrHh, num_empleado_rr_hh)
                    .Value(x => x.IdPersona, id_persona)
                    .Value(x => x.IdTipoEmpleado, id_tipo_empleado)
                    .Value(x => x.IdCategoria, id_categoria)
                    .Value(x => x.IdTipoContrato, id_tipo_contrato)
                    .Value(x => x.CvePuesto, cve_puesto)
                    .Value(x => x.IdEmpresa, id_empresa)
                    .Value(x => x.Calle, calle)
                    .Value(x => x.NumeroInterior, numero_interior)
                    .Value(x => x.NumeroExterior, numero_exterior)
                    .Value(x => x.Colonia, colonia)
                    .Value(x => x.Alcaldia, alcaldia)
                    .Value(x => x.IdCiudad, id_ciudad)
                    .Value(x => x.IdEstado, id_estado)
                    .Value(x => x.CP, codigo_postal)
                    .Value(x => x.IdPais, id_pais)
                    .Value(x => x.IdNivelEstudios, id_nivel_estudios)
                    .Value(x => x.IdFormaPago, id_forma_pago)
                    .Value(x => x.IdJornada, id_jornada)
                    .Value(x => x.IdDepartamento, id_departamento)
                    .Value(x => x.IdClasificacion, id_clasificacion)
                    .Value(x => x.IdJefeDirecto, id_jefe_directo)
                    .Value(x => x.IdUnidadNegocio, id_unidad_negocio)
                    .Value(x => x.IdTipoContrato_sat, id_tipo_contrato_sat != 0 ? id_tipo_contrato_sat : (int?)null)
                    .Value(x => x.NumEmpleado, num_empleado)
                    .Value(x => x.FechaIngreso, fecha_ingreso)
                    .Value(x => x.FechaSalida, fecha_salida)
                    .Value(x => x.FechaUltimoReingreso, fecha_ultimo_reingreso)
                    .Value(x => x.Nss, nss)
                    .Value(x => x.EmailBovis, email_bovis)
                    .Value(x => x.UrlRepositorio, url_repo)
                    .Value(x => x.Salario, salario)
                    .Value(x => x.IdProfesion, id_profesion)
                    .Value(x => x.Antiguedad, antiguedad)
                    .Value(x => x.IdTurno, id_turno)
                    .Value(x => x.UnidadMedica, unidad_medica)
                    .Value(x => x.RegistroPatronal, registro_patronal)
                    .Value(x => x.Cotizacion, cotizacion)
                    .Value(x => x.Duracion, duracion)
                    .Value(x => x.DescuentoPension, descuento_pension)
                    .Value(x => x.ChPorcentajePension, porcentaje_pension)
                    .Value(x => x.FondoFijo, fondo_fijo)
                    .Value(x => x.CreditoInfonavit, credito_infonavit)
                    .Value(x => x.TipoDescuento, tipo_descuento)
                    .Value(x => x.ValorDescuento, valor_descuento)
                    .Value(x => x.NoEmpleadoNoi, no_empleado_noi)
                    .Value(x => x.Rol, rol)
                    .Value(x => x.NumProyectoPrincipal, num_proyecto_principal)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_empleado;
                resp.Message = insert_empleado == default ? "Ocurrio un error al agregar registro de Empleado." : string.Empty;

                if (insert_empleado != null)
                {
                    var lastInsertedRecord = db.tB_Empleados.OrderByDescending(x => x.NumEmpleadoRrHh).FirstOrDefault();
                    last_inserted_id = lastInsertedRecord.NumEmpleadoRrHh;
                }

                var res_update_persona = await db.tB_Personas.Where(x => x.IdPersona == id_persona)
                    .UpdateAsync(x => new TB_Persona
                    {
                        EsEmpleado = true
                    }) > 0;

                resp.Success = res_update_persona;
                resp.Message = res_update_persona == default ? "Ocurrio un error al actualizar registro." : string.Empty;


                /*
                 * Se inserta también en tb_dor_emplado
                 */
                var persona = await (from p in db.tB_Personas
                                     where p.IdPersona == id_persona
                                     select p).FirstOrDefaultAsync();

                var puesto = await (from p in db.tB_Cat_Puestos
                                    where p.IdNivel == cve_puesto
                                    select p).FirstOrDefaultAsync();

                var jefe_directo = await (from e in db.tB_Empleados
                                          join p in db.tB_Personas on e.IdPersona equals p.IdPersona into pJoin
                                          from pItem in pJoin.DefaultIfEmpty()
                                          select pItem).FirstOrDefaultAsync();

                var unidad_negocio = await (from u in db.tB_Cat_UnidadNegocios
                                            where u.IdUnidadNegocio == id_unidad_negocio
                                            select u).FirstOrDefaultAsync();

                var proyecto = await (from ep in db.tB_EmpleadoProyectos
                                      join p in db.tB_Proyectos on ep.NumProyecto equals p.NumProyecto into pJoin
                                      from pItem in pJoin.DefaultIfEmpty()
                                      where ep.NumEmpleadoRrHh == num_empleado_rr_hh
                                      select pItem).FirstOrDefaultAsync();

                var insert_dor_empleado = await db.tB_Dor_Empleados
                    .Value(x => x.Usuario, email_bovis)
                    .Value(x => x.Rol, rol)
                    .Value(x => x.Nombre, persona != null ? persona.Nombre + " " + persona.ApPaterno + " " + persona.ApMaterno : string.Empty)
                    .Value(x => x.NoEmpleado, num_empleado_rr_hh)
                    .Value(x => x.CorreoElec, email_bovis)
                    .Value(x => x.Puesto, puesto != null ? puesto.Puesto : string.Empty)
                    .Value(x => x.FechaIngreso, fecha_ingreso.ToString("dd/MM/yyyy"))
                    .Value(x => x.JefeDirecto, jefe_directo != null ? jefe_directo.ApPaterno + " " + jefe_directo.ApMaterno + " " + jefe_directo.Nombre : string.Empty)
                    .Value(x => x.UnidadDeNegocio, unidad_negocio != null ? unidad_negocio.UnidadNegocio : string.Empty)
                    .Value(x => x.Proyecto, proyecto != null ? proyecto.Proyecto : string.Empty)
                    .Value(x => x.CentrosdeCostos, proyecto != null ? proyecto.NumProyecto.ToString() : string.Empty)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_dor_empleado;
                resp.Message = insert_dor_empleado == default ? "Ocurrio un error al agregar registro de Empleado." : string.Empty;


                /*
                 * Se insertan habilidades y experiencias.
                 */
                if (registro["habilidades"] != null)
                {
                    foreach (var habilidad in registro["habilidades"].AsArray())
                    {
                        int id_habilidad = Convert.ToInt32(habilidad.ToString());

                        var insert_habilidad = await db.tB_Empleado_Habilidades
                            .Value(x => x.IdEmpleado, num_empleado_rr_hh)
                            .Value(x => x.IdHabilidad, id_habilidad)
                            .Value(x => x.Activo, true)
                            .InsertAsync() > 0;

                        resp.Success = insert_habilidad;
                        resp.Message = insert_habilidad == default ? "Ocurrio un error al agregar registro de la habilidad." : string.Empty;
                    }
                }

                if (registro["experiencias"] != null)
                {
                    foreach (var experiencia in registro["experiencias"].AsArray())
                    {
                        int id_experiencia = Convert.ToInt32(experiencia.ToString());

                        var insert_experiencia = await db.tB_Empleado_Experiencias
                                                .Value(x => x.IdEmpleado, num_empleado_rr_hh)
                                                .Value(x => x.IdExperiencia, id_experiencia)
                                                .Value(x => x.Activo, true)
                                                .InsertAsync() > 0;

                        resp.Success = insert_experiencia;
                        resp.Message = insert_experiencia == default ? "Ocurrio un error al agregar registro de la experiencia." : string.Empty;
                    }
                }

                if (id_requerimiento > 0)
                {
                    var res_update_requerimiento = await db.tB_Requerimientos.Where(x => x.IdRequerimiento == id_requerimiento)
                    .UpdateAsync(x => new TB_Requerimiento
                    {
                        NumEmpleadoRrHh = num_empleado_rr_hh
                    }) > 0;

                    resp.Success = res_update_requerimiento;
                    resp.Message = res_update_requerimiento == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }



                //
                // Se inserta también como nuevo usuario.
                //
                var insert_usuario = await db.tB_Usuarios
                        .Value(x => x.NumEmpleadoRrHh, num_empleado_rr_hh)
                        .Value(x => x.Activo, true)
                        .InsertWithIdentityAsync();

                resp.Success = insert_usuario != null;
                resp.Message = insert_usuario == default ? "Ocurrio un error al agregar registro." : string.Empty;

                // Se le asigna por default el perfil Inicial.
                var perfil_inicial = await (from perfil in db.tB_Perfils
                                            where perfil.Perfil == "General / TimeSheet / PEC"
                                            && perfil.Activo == true
                                            select perfil).FirstOrDefaultAsync();

                var insert_perfil_usuario = await db.tB_PerfilUsuarios
                                                    .Value(x => x.IdPerfil, perfil_inicial.IdPerfil)
                                                    .Value(x => x.IdUsuario, Convert.ToInt32(insert_usuario))
                                                    .InsertAsync() > 0;

                resp.Success = insert_perfil_usuario;
                resp.Message = insert_perfil_usuario == default ? "Ocurrio un error al agregar registro de la experiencia." : string.Empty;



                //ATC
                var insert_Proyecto_empleado = await db.tB_EmpleadoProyectos
                    .Value(x => x.NumEmpleadoRrHh, num_empleado_rr_hh)
                    .Value(x => x.NumProyecto, num_proyecto_principal)
                    .Value(x => x.PorcentajeParticipacion, Convert.ToDecimal(0.0))
                    .Value(x => x.AliasPuesto, "Alias")
                    .Value(x => x.GrupoProyecto, "Grupo")
                    .Value(x => x.FechaIni, fecha_ingreso)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_Proyecto_empleado;
                resp.Message = insert_Proyecto_empleado == default ? "Ocurrio un error al agregar registro de Proyecto Empleado." : string.Empty;

                // termina ATC

            }
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string num_empleado_rr_hh = registro["num_empleado_rr_hh"].ToString();
            int id_persona = Convert.ToInt32(registro["id_persona"].ToString());
            int id_tipo_empleado = Convert.ToInt32(registro["id_tipo_empleado"].ToString());
            int id_categoria = Convert.ToInt32(registro["id_categoria"].ToString());
            int id_tipo_contrato = Convert.ToInt32(registro["id_tipo_contrato"].ToString());
            int cve_puesto = Convert.ToInt32(registro["cve_puesto"].ToString());
            int id_empresa = Convert.ToInt32(registro["id_empresa"].ToString());
            string? calle = registro["calle"] != null ? registro["calle"].ToString() : null;
            string? numero_exterior = registro["numero_exterior"] != null ? registro["numero_exterior"].ToString() : null;
            string? numero_interior = registro["numero_interior"] != null ? registro["numero_interior"].ToString() : null;
            string? colonia = registro["colonia"] != null ? registro["colonia"].ToString() : null;
            string? alcaldia = registro["alcaldia"] != null ? registro["alcaldia"].ToString() : null;
            int id_ciudad = Convert.ToInt32(registro["id_ciudad"].ToString());
            int? id_estado = registro["id_estado"] != null ? Convert.ToInt32(registro["id_estado"].ToString()) : null;
            string? codigo_postal = registro["codigo_postal"] != null ? registro["codigo_postal"].ToString() : null;
            int? id_pais = registro["id_pais"] != null ? Convert.ToInt32(registro["id_pais"].ToString()) : null;
            int id_nivel_estudios = registro["id_nivel_estudios"] != null ? Convert.ToInt32(registro["id_nivel_estudios"].ToString()) : 0;
            int id_forma_pago = Convert.ToInt32(registro["id_forma_pago"].ToString());
            int? id_jornada = registro["id_jornada"] != null ? Convert.ToInt32(registro["id_jornada"].ToString()) : null;
            int? id_departamento = registro["id_departamento"] != null ? Convert.ToInt32(registro["id_departamento"].ToString()) : null;
            int? id_clasificacion = registro["id_clasificacion"] != null ? Convert.ToInt32(registro["id_clasificacion"].ToString()) : null;
            string? id_jefe_directo = registro["id_jefe_directo"] != null ? registro["id_jefe_directo"].ToString() : null;
            int? id_unidad_negocio = registro["id_unidad_negocio"] != null ? Convert.ToInt32(registro["id_unidad_negocio"].ToString()) : null;
            int? id_tipo_contrato_sat = registro["id_tipo_contrato_sat"] != null ? Convert.ToInt32(registro["id_tipo_contrato_sat"].ToString()) : null;
            string? num_empleado = registro["num_empleado"] != null ? registro["num_empleado"].ToString() : null;
            DateTime fecha_ingreso = Convert.ToDateTime(registro["fecha_ingreso"].ToString());
            DateTime? fecha_salida = registro["fecha_salida"] != null ? Convert.ToDateTime(registro["fecha_salida"].ToString()) : null;
            DateTime? fecha_ultimo_reingreso = registro["fecha_ultimo_reingreso"] != null ? Convert.ToDateTime(registro["fecha_ultimo_reingreso"].ToString()) : null;
            string? nss = registro["nss"] != null ? registro["nss"].ToString() : null;
            string? email_bovis = registro["email_bovis"] != null ? registro["email_bovis"].ToString() : null;
            string? url_repo = registro["url_repo"] != null ? registro["url_repo"].ToString() : null;
            decimal salario = Convert.ToDecimal(registro["salario"].ToString());
            int? id_profesion = registro["id_profesion"] != null ? Convert.ToInt32(registro["id_profesion"].ToString()) : null;
            int antiguedad = registro["antiguedad"] != null ? Convert.ToInt32(registro["antiguedad"].ToString()) : 0;
            int? id_turno = registro["id_turno"] != null ? Convert.ToInt32(registro["id_turno"].ToString()) : null;
            int? unidad_medica = registro["unidad_medica"] != null ? Convert.ToInt32(registro["unidad_medica"].ToString()) : null;
            string? registro_patronal = registro["registro_patronal"] != null ? registro["registro_patronal"].ToString() : null;
            string? cotizacion = registro["cotizacion"] != null ? registro["cotizacion"].ToString() : null;
            int? duracion = registro["duracion"] != null ? Convert.ToInt32(registro["duracion"].ToString()) : null;
            decimal descuento_pension = registro["descuento_pension"] != null ? Convert.ToDecimal(registro["descuento_pension"].ToString()) : 0;
            string? porcentaje_pension = registro["porcentaje_pension"] != null ? registro["porcentaje_pension"].ToString() : null;
            decimal? fondo_fijo = registro["fondo_fijo"] != null ? Convert.ToDecimal(registro["fondo_fijo"].ToString()) : null;
            string? credito_infonavit = registro["credito_infonavit"] != null ? registro["credito_infonavit"].ToString() : null;
            string? tipo_descuento = registro["tipo_descuento"] != null ? registro["tipo_descuento"].ToString() : null;
            decimal? valor_descuento = registro["valor_descuento"] != null ? Convert.ToDecimal(registro["valor_descuento"].ToString()) : null;
            string? no_empleado_noi = registro["no_empleado_noi"] != null ? registro["no_empleado_noi"].ToString() : null;
            string? rol = registro["rol"] != null ? registro["rol"].ToString() : null;
            int num_proyecto_principal = Convert.ToInt32(registro["num_proyecto_principal"].ToString());
            int index = 0;

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_update_empleado = await db.tB_Empleados.Where(x => x.NumEmpleadoRrHh == num_empleado_rr_hh)
                    .UpdateAsync(x => new TB_Empleado
                    {
                        NumEmpleadoRrHh = num_empleado_rr_hh,
                        IdPersona = id_persona,
                        IdTipoEmpleado = id_tipo_empleado,
                        IdCategoria = id_categoria,
                        IdTipoContrato = id_tipo_contrato,
                        CvePuesto = cve_puesto,
                        IdEmpresa = id_empresa,
                        Calle = calle,
                        NumeroInterior = numero_interior,
                        NumeroExterior = numero_exterior,
                        Colonia = colonia,
                        Alcaldia = alcaldia,
                        IdCiudad = id_ciudad,
                        IdEstado = id_estado,
                        CP = codigo_postal,
                        IdPais = id_pais,
                        IdNivelEstudios = id_nivel_estudios,
                        IdFormaPago = id_forma_pago,
                        IdJornada = id_jornada,
                        IdDepartamento = id_departamento,
                        IdClasificacion = id_clasificacion,
                        IdJefeDirecto = id_jefe_directo,
                        IdUnidadNegocio = id_unidad_negocio,
                        IdTipoContrato_sat = id_tipo_contrato_sat,
                        NumEmpleado = num_empleado,
                        FechaIngreso = fecha_ingreso,
                        FechaSalida = fecha_salida != null ? Convert.ToDateTime(fecha_salida) : null,
                        FechaUltimoReingreso = fecha_ultimo_reingreso,
                        Nss = nss,
                        EmailBovis = email_bovis,
                        UrlRepositorio = url_repo,
                        Salario = salario,
                        IdProfesion = id_profesion,
                        Antiguedad = antiguedad,
                        IdTurno = id_turno,
                        UnidadMedica = unidad_medica,
                        RegistroPatronal = registro_patronal,
                        Cotizacion = cotizacion,
                        Duracion = duracion,
                        DescuentoPension = descuento_pension,
                        ChPorcentajePension = porcentaje_pension,
                        FondoFijo = fondo_fijo,
                        CreditoInfonavit = credito_infonavit,
                        TipoDescuento = tipo_descuento,
                        ValorDescuento = valor_descuento,
                        NoEmpleadoNoi = no_empleado_noi,
                        Rol = rol,
                        NumProyectoPrincipal = num_proyecto_principal
                    }) > 0;

                resp.Success = res_update_empleado;
                resp.Message = res_update_empleado == default ? "Ocurrio un error al actualizar registro." : string.Empty;


                /*
                 * Se actualiza también en tb_dor_emplado
                 */
                var persona = await (from p in db.tB_Personas
                                     where p.IdPersona == id_persona
                                     select p).FirstOrDefaultAsync();

                var puesto = await (from p in db.tB_Cat_Puestos
                                    where p.IdNivel == cve_puesto
                                    select p).FirstOrDefaultAsync();

                var jefe_directo = await (from e in db.tB_Empleados
                                          join p in db.tB_Personas on e.IdPersona equals p.IdPersona into pJoin
                                          from pItem in pJoin.DefaultIfEmpty()
                                          select pItem).FirstOrDefaultAsync();

                var unidad_negocio = await (from u in db.tB_Cat_UnidadNegocios
                                            where u.IdUnidadNegocio == id_unidad_negocio
                                            select u).FirstOrDefaultAsync();

                var proyecto = await (from ep in db.tB_EmpleadoProyectos
                                      join p in db.tB_Proyectos on ep.NumProyecto equals p.NumProyecto into pJoin
                                      from pItem in pJoin.DefaultIfEmpty()
                                      where ep.NumEmpleadoRrHh == num_empleado_rr_hh
                                      select pItem).FirstOrDefaultAsync();

                var res_update_dor_empleado = await db.tB_Dor_Empleados.Where(x => x.NoEmpleado == num_empleado_rr_hh)
                    .UpdateAsync(x => new TB_DorEmpleados
                    {
                        Usuario = email_bovis,
                        Rol = rol,
                        Nombre = persona != null ? persona.Nombre + " " + persona.ApPaterno + " " + persona.ApMaterno : string.Empty,
                        NoEmpleado = num_empleado_rr_hh,
                        CorreoElec = email_bovis,
                        Puesto = puesto != null ? puesto.Puesto : string.Empty,
                        FechaIngreso = fecha_ingreso.ToString("dd/MM/yyyy"),
                        JefeDirecto = jefe_directo != null ? jefe_directo.ApPaterno + " " + jefe_directo.ApMaterno + " " + jefe_directo.Nombre : string.Empty,
                        UnidadDeNegocio = unidad_negocio != null ? unidad_negocio.UnidadNegocio : string.Empty,
                        Proyecto = proyecto != null ? proyecto.Proyecto : string.Empty,
                        CentrosdeCostos = proyecto != null ? proyecto.NumProyecto.ToString() : string.Empty
                    }) > 0;


                /*
                 * Se actualizan habilidades.
                 */
                if (registro["habilidades"] != null)
                {
                    var res_empleado_habilidades = await (from emp_hab in db.tB_Empleado_Habilidades
                                                          where emp_hab.IdEmpleado == num_empleado_rr_hh
                                                          select emp_hab).ToListAsync();

                    int[] ids_habilidades_db = new int[res_empleado_habilidades.Count()];
                    index = 0;
                    foreach (var r in res_empleado_habilidades)
                    {
                        ids_habilidades_db[index] = r.IdHabilidad;
                        index++;
                    }
                    int[] ids_habilidades_request = new int[registro["habilidades"].AsArray().Count()];
                    index = 0;
                    foreach (var r in registro["habilidades"].AsArray())
                    {
                        ids_habilidades_request[index] = Convert.ToInt32(r.ToString());
                        index++;
                    }
                    HashSet<int> ids_habilidades = new HashSet<int>(ids_habilidades_db.Concat(ids_habilidades_request));

                    foreach (int id in ids_habilidades)
                    {
                        if (ids_habilidades_db.Contains(id))
                        {
                            if (ids_habilidades_request.Contains(id))
                            {
                                // Se actualiza
                                var res_update_empleado_habilidad = await db.tB_Empleado_Habilidades.Where(x => x.IdHabilidad == id && x.IdEmpleado == num_empleado_rr_hh)
                                    .UpdateAsync(x => new TB_EmpleadoHabilidad
                                    {
                                        IdHabilidad = id,
                                        Activo = true
                                    }) > 0;

                                resp.Success = res_update_empleado_habilidad;
                                resp.Message = res_update_empleado_habilidad == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                            }
                            else
                            {
                                // Se elimina
                                var res_delete_empleado_habilidad = await (db.tB_Empleado_Habilidades
                                   .Where(x => x.IdHabilidad == id && x.IdEmpleado == num_empleado_rr_hh)
                                   .Set(x => x.Activo, false))
                                   .UpdateAsync() >= 0;

                                resp.Success = res_delete_empleado_habilidad;
                                resp.Message = res_delete_empleado_habilidad == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                            }
                        }
                        else
                        {
                            // Se agrega
                            var res_insert_empleado_habilidad = await db.tB_Empleado_Habilidades
                            .Value(x => x.IdEmpleado, num_empleado_rr_hh)
                            .Value(x => x.IdHabilidad, id)
                            .Value(x => x.Activo, true)
                            .InsertAsync() > 0;

                            resp.Success = res_insert_empleado_habilidad;
                            resp.Message = res_insert_empleado_habilidad == default ? "Ocurrio un error al agregar registro." : string.Empty;
                        }
                        Console.WriteLine();
                    }
                }

                /*
                 * Se actualizan experiencias.
                 */
                if (registro["experiencias"] != null)
                {
                    var res_empleado_experiencias = await (from emp_exp in db.tB_Empleado_Experiencias
                                                           where emp_exp.IdEmpleado == num_empleado_rr_hh
                                                           select emp_exp)
                                                               .ToListAsync();

                    int[] ids_experiencias_db = new int[res_empleado_experiencias.Count()];
                    index = 0;
                    foreach (var r in res_empleado_experiencias)
                    {
                        ids_experiencias_db[index] = r.IdExperiencia;
                        index++;
                    }
                    int[] ids_experiencias_request = new int[registro["experiencias"].AsArray().Count()];
                    index = 0;
                    foreach (var r in registro["experiencias"].AsArray())
                    {
                        ids_experiencias_request[index] = Convert.ToInt32(r.ToString());
                        index++;
                    }
                    HashSet<int> ids_experiencias = new HashSet<int>(ids_experiencias_db.Concat(ids_experiencias_request));

                    foreach (int id in ids_experiencias)
                    {
                        if (ids_experiencias_db.Contains(id))
                        {
                            if (ids_experiencias_request.Contains(id))
                            {
                                // Se actualiza
                                var res_update_empleado_experiencia = await db.tB_Empleado_Experiencias.Where(x => x.IdExperiencia == id && x.IdEmpleado == num_empleado_rr_hh)
                                    .UpdateAsync(x => new TB_EmpleadoExperiencia
                                    {
                                        IdExperiencia = id,
                                        Activo = true
                                    }) > 0;

                                resp.Success = res_update_empleado_experiencia;
                                resp.Message = res_update_empleado_experiencia == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                            }
                            else
                            {
                                // Se elimina
                                var res_delete_empleado_experiencia = await (db.tB_Empleado_Experiencias
                                   .Where(x => x.IdExperiencia == id && x.IdEmpleado == num_empleado_rr_hh)
                                   .Set(x => x.Activo, false))
                                   .UpdateAsync() >= 0;

                                resp.Success = res_delete_empleado_experiencia;
                                resp.Message = res_delete_empleado_experiencia == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                            }
                        }
                        else
                        {
                            // Se agrega
                            var res_insert_empleado_experiencia = await db.tB_Empleado_Experiencias
                            .Value(x => x.IdEmpleado, num_empleado_rr_hh)
                            .Value(x => x.IdExperiencia, id)
                            .Value(x => x.Activo, true)
                            .InsertAsync() > 0;

                            resp.Success = res_insert_empleado_experiencia;
                            resp.Message = res_insert_empleado_experiencia == default ? "Ocurrio un error al agregar registro." : string.Empty;
                        }
                        Console.WriteLine();
                    }
                }
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateEstatus(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string num_empleado_rr_hh = registro["id"].ToString();
            bool activo = Convert.ToBoolean(registro["boactivo"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_update_empleado = await db.tB_Empleados.Where(x => x.NumEmpleadoRrHh == num_empleado_rr_hh)
                    .UpdateAsync(x => new TB_Empleado
                    {
                        Activo = activo
                    }) > 0;

                resp.Success = res_update_empleado;
                resp.Message = res_update_empleado == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                /*
                 * Se actualiza también en tb_dor_empleado.
                 */
                var res_update_dor_empleado = await db.tB_Dor_Empleados.Where(x => x.NoEmpleado == num_empleado_rr_hh)
                    .UpdateAsync(x => new TB_DorEmpleados
                    {
                        Activo = activo
                    }) > 0;

                /*
                 * Se actualiza también el estatus del usuario.
                 */
                var res_disable_usuario = await db.tB_Usuarios.Where(x => x.NumEmpleadoRrHh == num_empleado_rr_hh)
                                .UpdateAsync(x => new TB_Usuario
                                {
                                    Activo = activo
                                }) > 0;

                resp.Success = res_disable_usuario;
                resp.Message = res_disable_usuario == default ? "Ocurrio un error al actualizar registro." : string.Empty;


                //ATC 20-12-2024  OBTIENE EL VALOR DEL EMPLEADO

                var empleado = await (from p in db.tB_Empleados
                                     where p.NumEmpleadoRrHh == num_empleado_rr_hh
                                     select p).FirstOrDefaultAsync();

                //ATC 20-12-2024  UTILIZA EL VALOR DE PERSONA PARA ACTUALIZAR A FALSE O 0

                var res_update_persona = await db.tB_Personas.Where(x => x.IdPersona == empleado.IdPersona)
                    .UpdateAsync(x => new TB_Persona
                    {
                        EsEmpleado = false
                    }) > 0;

                resp.Success = res_update_persona;
                resp.Message = res_update_persona == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }
            return resp;
        }
        #endregion Empleados

        #region Proyectos
        public async Task<List<Proyecto_Detalle>> GetProyectos(string idEmpleado)
        {
            List<Proyecto_Detalle> proyectos = new List<Proyecto_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                proyectos = await (from proy in db.tB_Proyectos
                                      join emp_proy in db.tB_EmpleadoProyectos on proy.NumProyecto equals emp_proy.NumProyecto into emp_proyJoin
                                      from emp_proyItem in emp_proyJoin.DefaultIfEmpty()
                                   join usertime in db.tB_Usuario_Timesheets on proy.NumProyecto equals usertime.NumProyecto into usertimeJoin
                                   from responsaItem in usertimeJoin.DefaultIfEmpty()
                                   where emp_proyItem.NumEmpleadoRrHh == idEmpleado
                                      select new Proyecto_Detalle
                                      {
                                          nunum_proyecto = proy.NumProyecto,
                                          chproyecto = proy.Proyecto,
                                          chalcance = proy.Alcance,
                                          chcp = proy.Cp,
                                          chciudad = proy.Ciudad,
                                          nukidestatus = proy.IdEstatus,
                                          nukidsector = proy.IdSector,
                                          nukidtipo_proyecto = proy.IdTipoProyecto,
                                          nukidresponsable_preconstruccion = proy.IdResponsablePreconstruccion,
                                          nukidresponsable_construccion = proy.IdResponsableConstruccion,
                                          nukidresponsable_ehs = proy.IdResponsableEhs,
                                          nukidresponsable_supervisor = proy.IdResponsableSupervisor,
                                          nukidempresa = proy.IdEmpresa,
                                          nukidpais = proy.IdPais,
                                          nukiddirector_ejecutivo = proy.IdDirectorEjecutivo,
                                          nucosto_promedio_m2 = proy.CostoPromedioM2,
                                          dtfecha_ini = proy.FechaIni,
                                          dtfecha_fin = proy.FechaFin,
                                          nunum_empleado_rr_hh = idEmpleado,
                                          nuporcantaje_participacion = emp_proyItem.PorcentajeParticipacion,
                                          chalias_puesto = emp_proyItem.AliasPuesto,
                                          chgrupo_proyecto = emp_proyItem.GrupoProyecto,
                                          nukidunidadnegocio = proy.IdUnidadDeNegocio,  //atc  
                                          nukidresponsable = responsaItem.NumEmpleadoRrHh,  //atc  
                                          chresponsable = responsaItem.Usuario  //atc  
                                      }).ToListAsync();

                foreach (var proy in proyectos)
                {
                    var timesheet = await (from time_proy in db.tB_Timesheet_Proyectos
                                            join time in db.tB_Timesheets on time_proy.IdTimesheet equals time.IdTimesheet into timeJoin
                                            from timeItem in timeJoin.DefaultIfEmpty()
                                            where time_proy.IdProyecto == proy.nunum_proyecto
                                            && timeItem.IdEmpleado == idEmpleado
                                            && timeItem.Mes == DateTime.Now.Month
                                            && timeItem.Anio == DateTime.Now.Year
                                            select new InfoCosto
                                            {
                                                nudias = time_proy.Dias != null ? time_proy.Dias : (float)0,
                                                nudedicacion = time_proy.TDedicacion != null ? time_proy.TDedicacion : (float)0,
                                                nucosto = time_proy.Costo != null ? time_proy.Costo : 0.0M
                                            }).FirstOrDefaultAsync();


                    proy.nudias = timesheet != null ? timesheet.nudias : (float)0;
                    proy.nudedicacion = timesheet != null ? timesheet.nudedicacion : (float)0;
                    proy.nucosto = timesheet != null ? timesheet.nucosto : 0.0M; 
                }

                //var proyectos = await (from emp_proj in db.tB_EmpleadoProyectos
                //                       join proj in db.tB_Proyectos on emp_proj.NumProyecto equals proj.NumProyecto    
                //                       join time_proy in db.tB_Timesheet_Proyectos on proj.NumProyecto equals time_proy.IdProyecto into time_proyJoin
                //                       from time_proyItem in time_proyJoin.DefaultIfEmpty()
                //                       join time in db.tB_Timesheets on time_proyItem.IdTimesheet equals time.IdTimesheet into timeJoin
                //                       from timeItem in timeJoin.DefaultIfEmpty()
                //                       where (idEmpleado == "0" || emp_proj.NumEmpleadoRrHh == idEmpleado)
                //                       && timeItem.IdEmpleado == idEmpleado
                //                       //&& timeItem.Mes == DateTime.Now.Month
                //                       //&& timeItem.Anio == DateTime.Now.Year
                //                       && emp_proj.Activo == true
                //                       select new Proyecto_Detalle
                //                       {
                //                           nunum_proyecto = proj.NumProyecto,
                //                           chproyecto = proj.Proyecto,
                //                           chalcance = proj.Alcance,
                //                           chcp = proj.Cp,
                //                           chciudad = proj.Ciudad,
                //                           nukidestatus = proj.IdEstatus,
                //                           nukidsector = proj.IdSector,
                //                           nukidtipo_proyecto = proj.IdTipoProyecto,
                //                           nukidresponsable_preconstruccion = proj.IdResponsablePreconstruccion,
                //                           nukidresponsable_construccion = proj.IdResponsableConstruccion,
                //                           nukidresponsable_ehs = proj.IdResponsableEhs,
                //                           nukidresponsable_supervisor = proj.IdResponsableSupervisor,
                //                           nukidempresa = proj.IdEmpresa,
                //                           nukidpais = proj.IdPais,
                //                           nukiddirector_ejecutivo = proj.IdDirectorEjecutivo,
                //                           nucosto_promedio_m2 = proj.CostoPromedioM2,
                //                           dtfecha_ini = proj.FechaIni,
                //                           dtfecha_fin = proj.FechaFin,
                //                           nunum_empleado_rr_hh = emp_proj.NumEmpleadoRrHh,
                //                           nuporcantaje_participacion = emp_proj.PorcentajeParticipacion,
                //                           chalias_puesto = emp_proj.AliasPuesto,
                //                           chgrupo_proyecto = emp_proj.GrupoProyecto,
                //                           nudias = time_proyItem.Dias,
                //                           nudedicacion = time_proyItem.TDedicacion,
                //                           nucosto = time_proyItem.Costo
                //                       }).ToListAsync();

                //foreach (var proyecto in proyectos)
                //{
                //    var costos = await (from time_proy in db.tB_Timesheet_Proyectos
                //                       join time in db.tB_Timesheets on time_proy.IdTimesheet equals time.IdTimesheet into timeJoin
                //                       from time_item in timeJoin.DefaultIfEmpty()
                //                       where time_proy.IdProyecto == proyecto.nunum_proyecto
                //                       select new InfoCosto
                //                       {
                //                           nudias = time_item.DiasTrabajo,
                //                           nudedicacion = time_proy.TDedicacion,
                //                           nucosto = time_proy.Costo
                //                       }).ToListAsync();

                //    proyecto.Costos = new List<InfoCosto>();
                //    proyecto.Costos.AddRange(costos);
                //}

                return proyectos;
            }
        }
        #endregion Proyectos

        #region Ciudades
        public async Task<List<TB_Ciudad>> GetCiudades(bool? activo, int? IdEstado)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from ciudad in db.tB_Ciudads
                                                                          where (ciudad.Activo == activo)
                                                                          && (IdEstado == 0 || ciudad.IdEstado == IdEstado)
                                                                          select ciudad).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Ciudad>();
        }
        #endregion Ciudades


        public string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }

    }


}
