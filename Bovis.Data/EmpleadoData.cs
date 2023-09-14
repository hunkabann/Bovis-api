using Bovis.Common.Model;
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
                                          //where emp.Activo == activo
                                      orderby perItem.Nombre ascending
                                      select new Empleado_Detalle
                                      {
                                          nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                                          nukidpersona = emp.IdPersona,
                                          nombre_persona = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty,
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
                                          //nuantiguedad = emp.Antiguedad,
                                          nukidturno = emp.IdTurno,
                                          chturno = turnoItem != null ? turnoItem.Turno : string.Empty,
                                          nuunidad_medica = emp.UnidadMedica,
                                          chregistro_patronal = emp.RegistroPatronal,
                                          chcotizacion = emp.Cotizacion,
                                          nuduracion = emp.Duracion,
                                          boactivo = emp.Activo,
                                          nudescuento_pension = emp.DescuentoPension,
                                          chporcentaje_pension = emp.ChPorcentajePension,
                                          nufondo_fijo = emp.FondoFijo,
                                          nucredito_infonavit = emp.CreditoInfonavit,
                                          chtipo_descuento = emp.TipoDescuento,
                                          nuvalor_descuento = emp.ValorDescuento,
                                          nuno_empleado_noi = emp.NoEmpleadoNoi,
                                          chrol = emp.Rol
                                      }).ToListAsync();

                    // Se calcula la antigüedad
                    foreach (var emp in resp)
                    {
                        DateTime fecha_ingreso = (emp.dtfecha_ultimo_reingreso.IsNullOrEmpty()) ? Convert.ToDateTime(emp.dtfecha_ingreso) : Convert.ToDateTime(emp.dtfecha_ultimo_reingreso);
                        DateTime fecha_salida = (emp.dtfecha_salida.IsNullOrEmpty()) ? DateTime.Now : Convert.ToDateTime(emp.dtfecha_salida);
                        TimeSpan diferencia = fecha_salida - fecha_ingreso;
                        int años = diferencia.Days / 365;
                        int meses = (diferencia.Days % 365) / 30;
                        int dias = diferencia.Days % 30;

                        emp.nuantiguedad = $"{años} años, {meses} meses, {dias} días";
                    }

                    return resp;
                }
            }
            else return await GetAllFromEntityAsync<Empleado_Detalle>();
        }

        public async Task<Empleado_Detalle> GetEmpleado(int idEmpleado)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from emp in db.tB_Empleados
                                 join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                 from perItem in perJoin.DefaultIfEmpty()
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
                                 join jefe in db.tB_Personas on emp.IdJefeDirecto equals jefe.IdPersona into jefeJoin
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
                                 where emp.NumEmpleadoRrHh == idEmpleado
                                 select new Empleado_Detalle
                                 {
                                     nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                                     nukidpersona = emp.IdPersona,
                                     nombre_persona = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty,
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
                                     //nuantiguedad = emp.Antiguedad,
                                     nukidturno = emp.IdTurno,
                                     chturno = turnoItem != null ? turnoItem.Turno : string.Empty,
                                     nuunidad_medica = emp.UnidadMedica,
                                     chregistro_patronal = emp.RegistroPatronal,
                                     chcotizacion = emp.Cotizacion,
                                     nuduracion = emp.Duracion,
                                     boactivo = emp.Activo,
                                     nudescuento_pension = emp.DescuentoPension,
                                     chporcentaje_pension = emp.ChPorcentajePension,
                                     nufondo_fijo = emp.FondoFijo,
                                     nucredito_infonavit = emp.CreditoInfonavit,
                                     chtipo_descuento = emp.TipoDescuento,
                                     nuvalor_descuento = emp.ValorDescuento,
                                     nuno_empleado_noi = emp.NoEmpleadoNoi,
                                     chrol = emp.Rol,
                                     dtfecha_nacimiento = perItem != null ? perItem.FechaNacimiento.ToString("yyyy-MM-dd") : null,
                                     nukidsexo = perItem != null ? perItem.IdSexo : null,
                                     chsexo = sexoItem != null ? sexoItem.Sexo : null
                                 }).FirstOrDefaultAsync();

                if (res != null)
                {
                    // Se calcula la antigüedad
                    DateTime fecha_ingreso = (res.dtfecha_ultimo_reingreso.IsNullOrEmpty()) ? Convert.ToDateTime(res.dtfecha_ingreso) : Convert.ToDateTime(res.dtfecha_ultimo_reingreso);
                    DateTime fecha_salida = (res.dtfecha_salida.IsNullOrEmpty()) ? DateTime.Now : Convert.ToDateTime(res.dtfecha_salida);
                    TimeSpan diferencia = fecha_salida - fecha_ingreso;
                    int años = diferencia.Days / 365;
                    int meses = (diferencia.Days % 365) / 30;
                    int dias = diferencia.Days % 30;

                    res.nuantiguedad = $"{años} años, {meses} meses, {dias} días";

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

                    foreach (var hab in res.habilidades)
                    {
                        res.chhabilidades += (res.chhabilidades == null) ? hab.Habilidad : ", " + hab.Habilidad;
                    }
                }
                return res;

            }
        }

        public async Task<Empleado_BasicData> GetEmpleadoByEmail(string email)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = from emp in db.tB_Empleados
                          join per in db.tB_Personas on emp.IdPersona equals per.IdPersona
                          where emp.EmailBovis == email
                          select new Empleado_BasicData
                          {
                              nukid_empleado = emp.NumEmpleadoRrHh,
                              chnombre = per.Nombre,
                              chap_paterno = per.ApPaterno,
                              chap_materno = per.ApMaterno
                          };

                return await res.FirstOrDefaultAsync();

            }
        }

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int num_empleado_rr_hh = Convert.ToInt32(registro["num_empleado_rr_hh"].ToString());
            int id_persona = Convert.ToInt32(registro["id_persona"].ToString());
            int id_tipo_empleado = Convert.ToInt32(registro["id_tipo_empleado"].ToString());
            int id_categoria = Convert.ToInt32(registro["id_categoria"].ToString());
            int id_tipo_contrato = Convert.ToInt32(registro["id_tipo_contrato"].ToString());
            int cve_puesto = Convert.ToInt32(registro["cve_puesto"].ToString());
            int id_empresa = Convert.ToInt32(registro["id_empresa"].ToString());
            string calle = registro["calle"].ToString();
            string numero_interior = registro["numero_interior"].ToString();
            string numero_exterior = registro["numero_exterior"].ToString();
            string colonia = registro["colonia"].ToString();
            string alcaldia = registro["alcaldia"].ToString();
            int id_ciudad = Convert.ToInt32(registro["id_ciudad"].ToString());
            int id_estado = Convert.ToInt32(registro["id_estado"].ToString());
            string codigo_postal = registro["codigo_postal"].ToString();
            int id_pais = Convert.ToInt32(registro["id_pais"].ToString());
            int id_nivel_estudios = registro["id_nivel_estudios"] != null ? Convert.ToInt32(registro["id_nivel_estudios"].ToString()) : 0;
            int id_forma_pago = Convert.ToInt32(registro["id_forma_pago"].ToString());
            int id_jornada = Convert.ToInt32(registro["id_jornada"].ToString());
            int id_departamento = Convert.ToInt32(registro["id_departamento"].ToString());
            int id_clasificacion = registro["id_clasificacion"] != null ? Convert.ToInt32(registro["id_clasificacion"].ToString()) : 0;
            int id_jefe_directo = Convert.ToInt32(registro["id_jefe_directo"].ToString());
            int id_unidad_negocio = Convert.ToInt32(registro["id_unidad_negocio"].ToString());
            int id_tipo_contrato_sat = Convert.ToInt32(registro["id_tipo_contrato_sat"].ToString());
            int num_empleado = Convert.ToInt32(registro["num_empleado"].ToString());
            DateTime fecha_ingreso = Convert.ToDateTime(registro["fecha_ingreso"].ToString());
            DateTime? fecha_salida = registro["fecha_salida"] != null ? Convert.ToDateTime(registro["fecha_salida"].ToString()) : null;
            DateTime? fecha_ultimo_reingreso = registro["fecha_ultimo_reingreso"] != null ? Convert.ToDateTime(registro["fecha_ultimo_reingreso"].ToString()) : null;
            string nss = registro["nss"].ToString();
            string email_bovis = registro["email_bovis"].ToString();
            string url_repo = registro["url_repo"] != null ? registro["url_repo"].ToString() : string.Empty;
            decimal salario = Convert.ToDecimal(registro["salario"].ToString());
            int id_profesion = registro["id_profesion"] != null ? Convert.ToInt32(registro["id_profesion"].ToString()) : 0;
            int antiguedad = registro["antiguedad"] != null ? Convert.ToInt32(registro["antiguedad"].ToString()) : 0;
            int id_turno = registro["id_turno"] != null ? Convert.ToInt32(registro["id_turno"].ToString()) : 0;
            int unidad_medica = registro["unidad_medica"] != null ? Convert.ToInt32(registro["unidad_medica"].ToString()) : 0;
            string registro_patronal = registro["registro_patronal"].ToString();
            string cotizacion = registro["cotizacion"].ToString();
            int duracion = registro["duracion"] != null ? Convert.ToInt32(registro["duracion"].ToString()) : 0;
            decimal descuento_pension = registro["descuento_pension"] != null ? Convert.ToDecimal(registro["descuento_pension"].ToString()) : 0;
            string porcentaje_pension = registro["porcentaje_pension"] != null ? registro["porcentaje_pension"].ToString() : string.Empty;
            decimal fondo_fijo = registro["fondo_fijo"] != null ? Convert.ToDecimal(registro["fondo_fijo"].ToString()) : 0;
            string credito_infonavit = registro["credito_infonavit"] != null ? registro["credito_infonavit"].ToString() : string.Empty;
            string tipo_descuento = registro["tipo_descuento"] != null ? registro["tipo_descuento"].ToString() : string.Empty;
            decimal valor_descuento = registro["valor_descuento"] != null ? Convert.ToDecimal(registro["valor_descuento"].ToString()) : 0;
            string no_empleado_noi = registro["no_empleado_noi"] != null ? registro["no_empleado_noi"].ToString() : string.Empty;
            string rol = registro["rol"] != null ? registro["rol"].ToString() : string.Empty;
            int id_requerimiento = Convert.ToInt32(registro["id_requerimiento"].ToString());

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

                int last_inserted_id = 0;                

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
                    .Value(x => x.IdJefeDirecto, id_jefe_directo != 0 ? id_jefe_directo : (int?)null)
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
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_empleado;
                resp.Message = insert_empleado == default ? "Ocurrio un error al agregar registro de Empleado." : string.Empty;

                if (insert_empleado != null)
                {
                    var lastInsertedRecord = db.tB_Empleados.OrderByDescending(x => x.NumEmpleadoRrHh).FirstOrDefault();
                    last_inserted_id = lastInsertedRecord.NumEmpleadoRrHh;
                }

                if (registro["habilidades"] != null) {
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

                if(id_requerimiento > 0)
                {
                    var res_update_requerimiento = await db.tB_Requerimientos.Where(x => x.IdRequerimiento == id_requerimiento)
                    .UpdateAsync(x => new TB_Requerimiento
                    {
                        NumEmpleadoRrHh = num_empleado_rr_hh
                    }) > 0;

                    resp.Success = res_update_requerimiento;
                    resp.Message = res_update_requerimiento == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }
            }
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int num_empleado_rr_hh = Convert.ToInt32(registro["num_empleado_rr_hh"].ToString());
            int id_persona = Convert.ToInt32(registro["id_persona"].ToString());
            int id_tipo_empleado = Convert.ToInt32(registro["id_tipo_empleado"].ToString());
            int id_categoria = Convert.ToInt32(registro["id_categoria"].ToString());
            int id_tipo_contrato = Convert.ToInt32(registro["id_tipo_contrato"].ToString());
            int cve_puesto = Convert.ToInt32(registro["cve_puesto"].ToString());
            int id_empresa = Convert.ToInt32(registro["id_empresa"].ToString());
            string calle = registro["calle"].ToString();
            string numero_interior = registro["numero_interior"].ToString();
            string numero_exterior = registro["numero_exterior"].ToString();
            string colonia = registro["colonia"].ToString();
            string alcaldia = registro["alcaldia"].ToString();
            int id_ciudad = Convert.ToInt32(registro["id_ciudad"].ToString());
            int id_estado = Convert.ToInt32(registro["id_estado"].ToString());
            string codigo_postal = registro["codigo_postal"].ToString();
            int id_pais = Convert.ToInt32(registro["id_pais"].ToString());
            int id_nivel_estudios = registro["id_nivel_estudios"] != null ? Convert.ToInt32(registro["id_nivel_estudios"].ToString()) : 0;
            int id_forma_pago = Convert.ToInt32(registro["id_forma_pago"].ToString());
            int id_jornada = Convert.ToInt32(registro["id_jornada"].ToString());
            int id_departamento = Convert.ToInt32(registro["id_departamento"].ToString());
            int id_clasificacion = registro["id_clasificacion"] != null ? Convert.ToInt32(registro["id_clasificacion"].ToString()) : 0;
            int id_jefe_directo = Convert.ToInt32(registro["id_jefe_directo"].ToString());
            int id_unidad_negocio = Convert.ToInt32(registro["id_unidad_negocio"].ToString());
            int id_tipo_contrato_sat = Convert.ToInt32(registro["id_tipo_contrato_sat"].ToString());
            int num_empleado = Convert.ToInt32(registro["num_empleado"].ToString());
            DateTime fecha_ingreso = Convert.ToDateTime(registro["fecha_ingreso"].ToString());
            DateTime? fecha_salida = registro["fecha_salida"] != null ? Convert.ToDateTime(registro["fecha_salida"].ToString()) : null;
            DateTime? fecha_ultimo_reingreso = registro["fecha_ultimo_reingreso"] != null ? Convert.ToDateTime(registro["fecha_ultimo_reingreso"].ToString()) : null;
            string nss = registro["nss"].ToString();
            string email_bovis = registro["email_bovis"].ToString();
            string url_repo = registro["url_repo"] != null ? registro["url_repo"].ToString() : string.Empty;
            decimal salario = Convert.ToDecimal(registro["salario"].ToString());
            int id_profesion = registro["id_profesion"] != null ? Convert.ToInt32(registro["id_profesion"].ToString()) : 0;
            int antiguedad = registro["antiguedad"] != null ? Convert.ToInt32(registro["antiguedad"].ToString()) : 0;
            int id_turno = registro["id_turno"] != null ? Convert.ToInt32(registro["id_turno"].ToString()) : 0;
            int unidad_medica = registro["unidad_medica"] != null ? Convert.ToInt32(registro["unidad_medica"].ToString()) : 0;
            string registro_patronal = registro["registro_patronal"].ToString();
            string cotizacion = registro["cotizacion"].ToString();
            int duracion = registro["duracion"] != null ? Convert.ToInt32(registro["duracion"].ToString()) : 0;
            decimal descuento_pension = registro["descuento_pension"] != null ? Convert.ToDecimal(registro["descuento_pension"].ToString()) : 0;
            string porcentaje_pension = registro["porcentaje_pension"] != null ? registro["porcentaje_pension"].ToString() : string.Empty;
            decimal fondo_fijo = registro["fondo_fijo"] != null ? Convert.ToDecimal(registro["fondo_fijo"].ToString()) : 0;
            string credito_infonavit = registro["credito_infonavit"] != null ? registro["credito_infonavit"].ToString() : string.Empty;
            string tipo_descuento = registro["tipo_descuento"] != null ? registro["tipo_descuento"].ToString() : string.Empty;
            decimal valor_descuento = registro["valor_descuento"] != null ? Convert.ToDecimal(registro["valor_descuento"].ToString()) : 0;
            string no_empleado_noi = registro["no_empleado_noi"] != null ? registro["no_empleado_noi"].ToString() : string.Empty;
            string rol = registro["rol"] != null ? registro["rol"].ToString() : string.Empty;
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
                        FechaSalida = fecha_salida,
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
                        Rol = rol
                    }) > 0;

                resp.Success = res_update_empleado;
                resp.Message = res_update_empleado == default ? "Ocurrio un error al actualizar registro." : string.Empty;


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
                                    .UpdateAsync(x => new TB_Empleado_Habilidad
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
                                    .UpdateAsync(x => new TB_Empleado_Experiencia
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
            
            int num_empleado_rr_hh = Convert.ToInt32(registro["id"].ToString());
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
            }
            return resp;
        }
        #endregion Empleados

        #region Proyectos
        public async Task<List<Proyecto_Detalle>> GetProyectos(int idEmpleado)
        {
            if (idEmpleado > 0)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from emp_proj in db.tB_EmpleadoProyectos
                                                                          join proj in db.tB_Proyectos on emp_proj.NumProyecto equals proj.NumProyecto
                                                                          where emp_proj.NumEmpleadoRrHh == idEmpleado
                                                                          select new Proyecto_Detalle
                                                                          {
                                                                              nunum_proyecto = proj.NumProyecto,
                                                                              chproyecto = proj.Proyecto,
                                                                              chalcance = proj.Alcance,
                                                                              chcp = proj.Cp,
                                                                              chciudad = proj.Ciudad,
                                                                              nukidestatus = proj.IdEstatus,
                                                                              nukidsector = proj.IdSector,
                                                                              nukidtipo_proyecto = proj.IdTipoProyecto,
                                                                              nukidresponsable_preconstruccion = proj.IdResponsablePreconstruccion,
                                                                              nukidresponsable_construccion = proj.IdResponsableConstruccion,
                                                                              nukidresponsable_ehs = proj.IdResponsableEhs,
                                                                              nukidresponsable_supervisor = proj.IdResponsableSupervisor,
                                                                              nukidcliente = proj.IdCliente,
                                                                              nukidempresa = proj.IdEmpresa,
                                                                              nukidpais = proj.IdPais,
                                                                              nukiddirector_ejecutivo = proj.IdDirectorEjecutivo,
                                                                              nucosto_promedio_m2 = proj.CostoPromedioM2,
                                                                              dtfecha_ini = proj.FechaIni,
                                                                              dtfecha_fin = proj.FechaFin,
                                                                              nunum_empleado_rr_hh = emp_proj.NumEmpleadoRrHh,
                                                                              nuporcantaje_participacion = emp_proj.PorcentajeParticipacion,
                                                                              chalias_puesto = emp_proj.AliasPuesto,
                                                                              chgrupo_proyecto = emp_proj.GrupoProyecto
                                                                          }).ToListAsync();
            }
            else return await GetAllFromEntityAsync<Proyecto_Detalle>();
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
    }
}
