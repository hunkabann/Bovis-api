using AutoMapper;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Responses;

namespace Bovis.Common.Mapper;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        #region TimeSheet
        CreateMap<Dias_Timesheet_Detalle, Detalle_Dias_Timesheet>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.id))
            .ForMember(c => c.mes, dto => dto.MapFrom(src => src.mes))
            .ForMember(c => c.dias, dto => dto.MapFrom(src => src.dias))
            .ForMember(c => c.feriados, dto => dto.MapFrom(src => src.feriados))
            .ForMember(c => c.sabados, dto => dto.MapFrom(src => src.sabados))
            .ForMember(c => c.anio, dto => dto.MapFrom(src => src.anio))
            .ForMember(c => c.dias_habiles, dto => dto.MapFrom(src => src.dias_habiles));

        CreateMap<TimeSheet_Detalle, Detalle_TimeSheet>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.id))
            .ForMember(c => c.id_empleado, dto => dto.MapFrom(src => src.id_empleado))
            .ForMember(c => c.mes, dto => dto.MapFrom(src => src.mes))
            .ForMember(c => c.anio, dto => dto.MapFrom(src => src.anio))
            .ForMember(c => c.id_responsable, dto => dto.MapFrom(src => src.id_responsable))
            .ForMember(c => c.sabados, dto => dto.MapFrom(src => src.sabados))
            .ForMember(c => c.dias_trabajo, dto => dto.MapFrom(src => src.dias_trabajo))
            .ForMember(c => c.otros, dto => dto.MapFrom(src => src.otros))
            .ForMember(c => c.proyectos, dto => dto.MapFrom(src => src.proyectos));
        #endregion TimeSheet

        #region Requerimientos
        CreateMap<TB_Requerimiento, Requerimiento>()
            .ForMember(c => c.nukidrequerimiento, dto => dto.MapFrom(src => src.IdRequerimiento))
            .ForMember(c => c.nukidcategoria, dto => dto.MapFrom(src => src.IdCategoria))
            .ForMember(c => c.nukidpuesto, dto => dto.MapFrom(src => src.IdPuesto))
            .ForMember(c => c.nukidnivel_estudios, dto => dto.MapFrom(src => src.IdNivelEstudios))
            .ForMember(c => c.nukidprofesion, dto => dto.MapFrom(src => src.IdProfesion))
            .ForMember(c => c.nukidjornada, dto => dto.MapFrom(src => src.IdJornada))
            .ForMember(c => c.nusueldo_min, dto => dto.MapFrom(src => src.SueldoMin))
            .ForMember(c => c.nusueldo_max, dto => dto.MapFrom(src => src.SueldoMax))
            .ForMember(c => c.boactivo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Requerimiento_Habilidad, Habilidad>()
            .ForMember(c => c.nukidrequerimiento, dto => dto.MapFrom(src => src.IdRequerimiento))
            .ForMember(c => c.nukidhabilidad, dto => dto.MapFrom(src => src.IdHabilidad));

        CreateMap<TB_Requerimiento_Experiencia, Experiencia>()
            .ForMember(c => c.nukidrequerimiento, dto => dto.MapFrom(src => src.IdRequerimiento))
            .ForMember(c => c.nukidexperiencia, dto => dto.MapFrom(src => src.IdExperiencia));
        #endregion Requerimientos

        #region Empleados
        CreateMap<Empleado_Detalle, Detalle_Empleado>()
            .ForMember(c => c.nunum_empleado_rr_hh, dto => dto.MapFrom(src => src.nunum_empleado_rr_hh))
            .ForMember(c => c.nukidpersona, dto => dto.MapFrom(src => src.nukidpersona))
            .ForMember(c => c.nombre_persona, dto => dto.MapFrom(src => src.nombre_persona))
            .ForMember(c => c.nukidtipo_empleado, dto => dto.MapFrom(src => src.nukidtipo_empleado))
            .ForMember(c => c.nukidcategoria, dto => dto.MapFrom(src => src.nukidcategoria))
            .ForMember(c => c.nukidtipo_contrato, dto => dto.MapFrom(src => src.nukidtipo_contrato))
            .ForMember(c => c.chcve_puesto, dto => dto.MapFrom(src => src.chcve_puesto))
            .ForMember(c => c.nukidempresa, dto => dto.MapFrom(src => src.nukidempresa))
            .ForMember(c => c.nukidciudad, dto => dto.MapFrom(src => src.nukidciudad))
            .ForMember(c => c.nukidnivel_estudios, dto => dto.MapFrom(src => src.nukidnivel_estudios))
            .ForMember(c => c.nukidforma_pago, dto => dto.MapFrom(src => src.nukidforma_pago))
            .ForMember(c => c.nukidjornada, dto => dto.MapFrom(src => src.nukidjornada))
            .ForMember(c => c.nukiddepartamento, dto => dto.MapFrom(src => src.nukiddepartamento))
            .ForMember(c => c.nukidclasificacion, dto => dto.MapFrom(src => src.nukidclasificacion))
            .ForMember(c => c.nukidjefe_directo, dto => dto.MapFrom(src => src.nukidjefe_directo))
            .ForMember(c => c.nukidunidad_negocio, dto => dto.MapFrom(src => src.nukidunidad_negocio))
            .ForMember(c => c.nukidtipo_contrato_sat, dto => dto.MapFrom(src => src.nukidtipo_contrato_sat))
            .ForMember(c => c.nunum_empleado, dto => dto.MapFrom(src => src.nunum_empleado))
            .ForMember(c => c.dtfecha_ingreso, dto => dto.MapFrom(src => src.dtfecha_ingreso))
            .ForMember(c => c.dtfecha_salida, dto => dto.MapFrom(src => src.dtfecha_salida))
            .ForMember(c => c.dtfecha_ultimo_reingreso, dto => dto.MapFrom(src => src.dtfecha_ultimo_reingreso))
            .ForMember(c => c.chnss, dto => dto.MapFrom(src => src.chnss))
            .ForMember(c => c.chemail_bovis, dto => dto.MapFrom(src => src.chemail_bovis))
            .ForMember(c => c.chexperiencias, dto => dto.MapFrom(src => src.chexperiencias))
            .ForMember(c => c.chhabilidades, dto => dto.MapFrom(src => src.chhabilidades))
            .ForMember(c => c.churl_repositorio, dto => dto.MapFrom(src => src.churl_repositorio))
            .ForMember(c => c.nusalario, dto => dto.MapFrom(src => src.nusalario))
            .ForMember(c => c.chprofesion, dto => dto.MapFrom(src => src.chprofesion))
            .ForMember(c => c.nuantiguedad, dto => dto.MapFrom(src => src.nuantiguedad))
            .ForMember(c => c.chturno, dto => dto.MapFrom(src => src.chturno))
            .ForMember(c => c.nuunidad_medica, dto => dto.MapFrom(src => src.nuunidad_medica))
            .ForMember(c => c.chregistro_patronal, dto => dto.MapFrom(src => src.chregistro_patronal))
            .ForMember(c => c.chcotizacion, dto => dto.MapFrom(src => src.chcotizacion))
            .ForMember(c => c.nuduracion, dto => dto.MapFrom(src => src.nuduracion))
            .ForMember(c => c.boactivo, dto => dto.MapFrom(src => src.boactivo))
            .ForMember(c => c.bodescuento_pension, dto => dto.MapFrom(src => src.bodescuento_pension))
            .ForMember(c => c.nuporcentaje_pension, dto => dto.MapFrom(src => src.nuporcentaje_pension))
            .ForMember(c => c.nufondo_fijo, dto => dto.MapFrom(src => src.nufondo_fijo))
            .ForMember(c => c.nucredito_infonavit, dto => dto.MapFrom(src => src.nucredito_infonavit))
            .ForMember(c => c.chtipo_descuento, dto => dto.MapFrom(src => src.chtipo_descuento))
            .ForMember(c => c.nuvalor_descuento, dto => dto.MapFrom(src => src.nuvalor_descuento))
            .ForMember(c => c.nuno_empleado_noi, dto => dto.MapFrom(src => src.nuno_empleado_noi))
            .ForMember(c => c.chrol, dto => dto.MapFrom(src => src.chrol));

        CreateMap<TB_Empleado, Empleado>()
            .ForMember(c => c.nunum_empleado_rr_hh, dto => dto.MapFrom(src => src.NumEmpleadoRrHh))
            .ForMember(c => c.nukidpersona, dto => dto.MapFrom(src => src.IdPersona))
            .ForMember(c => c.nukidtipo_empleado, dto => dto.MapFrom(src => src.IdTipoEmpleado))
            .ForMember(c => c.nukidcategoria, dto => dto.MapFrom(src => src.IdCategoria))
            .ForMember(c => c.nukidtipo_contrato, dto => dto.MapFrom(src => src.IdTipoContrato))
            .ForMember(c => c.chcve_puesto, dto => dto.MapFrom(src => src.CvePuesto))
            .ForMember(c => c.nukidempresa, dto => dto.MapFrom(src => src.IdEmpresa))
            .ForMember(c => c.nukidciudad, dto => dto.MapFrom(src => src.IdCiudad))
            .ForMember(c => c.nukidnivel_estudios, dto => dto.MapFrom(src => src.IdNivelEstudios))
            .ForMember(c => c.nukidforma_pago, dto => dto.MapFrom(src => src.IdFormaPago))
            .ForMember(c => c.nukidjornada, dto => dto.MapFrom(src => src.IdJornada))
            .ForMember(c => c.nukiddepartamento, dto => dto.MapFrom(src => src.IdDepartamento))
            .ForMember(c => c.nukidclasificacion, dto => dto.MapFrom(src => src.IdClasificacion))
            .ForMember(c => c.nukidjefe_directo, dto => dto.MapFrom(src => src.IdJefeDirecto))
            .ForMember(c => c.nukidunidad_negocio, dto => dto.MapFrom(src => src.IdUnidadNegocio))
            .ForMember(c => c.nukidtipo_contrato_sat, dto => dto.MapFrom(src => src.IdTipoContrato_sat))
            .ForMember(c => c.nunum_empleado, dto => dto.MapFrom(src => src.NumEmpleado))
            .ForMember(c => c.dtfecha_ingreso, dto => dto.MapFrom(src => src.FechaIngreso))
            .ForMember(c => c.dtfecha_salida, dto => dto.MapFrom(src => src.FechaSalida))
            .ForMember(c => c.dtfecha_ultimo_reingreso, dto => dto.MapFrom(src => src.FechaUltimoReingreso))
            .ForMember(c => c.chnss, dto => dto.MapFrom(src => src.Nss))
            .ForMember(c => c.chemail_bovis, dto => dto.MapFrom(src => src.EmailBovis))
            .ForMember(c => c.chexperiencias, dto => dto.MapFrom(src => src.Experiencias))
            .ForMember(c => c.chhabilidades, dto => dto.MapFrom(src => src.Habilidades))
            .ForMember(c => c.churl_repositorio, dto => dto.MapFrom(src => src.UrlRepositorio))
            .ForMember(c => c.nusalario, dto => dto.MapFrom(src => src.Salario))
            .ForMember(c => c.nukidprofesion, dto => dto.MapFrom(src => src.IdProfesion))
            .ForMember(c => c.nuantiguedad, dto => dto.MapFrom(src => src.Antiguedad))
            .ForMember(c => c.nukidturno, dto => dto.MapFrom(src => src.IdTurno))
            .ForMember(c => c.nuunidad_medica, dto => dto.MapFrom(src => src.UnidadMedica))
            .ForMember(c => c.chregistro_patronal, dto => dto.MapFrom(src => src.RegistroPatronal))
            .ForMember(c => c.chcotizacion, dto => dto.MapFrom(src => src.Cotizacion))
            .ForMember(c => c.nuduracion, dto => dto.MapFrom(src => src.Duracion))
            .ForMember(c => c.boactivo, dto => dto.MapFrom(src => src.Activo))
            .ForMember(c => c.bodescuento_pension, dto => dto.MapFrom(src => src.DescuentoPension))
            .ForMember(c => c.nuporcentaje_pension, dto => dto.MapFrom(src => src.PorcentajePension))
            .ForMember(c => c.nufondo_fijo, dto => dto.MapFrom(src => src.FondoFijo))
            .ForMember(c => c.nucredito_infonavit, dto => dto.MapFrom(src => src.CreditoInfonavit))
            .ForMember(c => c.chtipo_descuento, dto => dto.MapFrom(src => src.TipoDescuento))
            .ForMember(c => c.nuvalor_descuento, dto => dto.MapFrom(src => src.ValorDescuento))
            .ForMember(c => c.nuno_empleado_noi, dto => dto.MapFrom(src => src.NoEmpleadoNoi))
            .ForMember(c => c.chrol, dto => dto.MapFrom(src => src.Rol));
        #endregion Empleados

        #region Cie
        CreateMap<TB_Empresa, Empresa>()
            .ForMember(c => c.nukidempresa, dto => dto.MapFrom(src => src.IdEmpresa))
            .ForMember(c => c.chempresa, dto => dto.MapFrom(src => src.Empresa))
            .ForMember(c => c.rfc, dto => dto.MapFrom(src => src.Rfc))
            .ForMember(c => c.nucoi, dto => dto.MapFrom(src => src.Coi))
            .ForMember(c => c.nunoi, dto => dto.MapFrom(src => src.Noi))
            .ForMember(c => c.nusae, dto => dto.MapFrom(src => src.Sae))
            .ForMember(c => c.boactivo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cie, Cie>()
            .ForMember(c => c.nukidcie, dto => dto.MapFrom(src => src.IdCie))
            .ForMember(c => c.nunum_proyecto, dto => dto.MapFrom(src => src.NumProyecto))
            .ForMember(c => c.nukidtipo_cie, dto => dto.MapFrom(src => src.IdTipoCie))
            .ForMember(c => c.nukidtipo_poliza, dto => dto.MapFrom(src => src.IdTipoPoliza))
            .ForMember(c => c.dtfecha, dto => dto.MapFrom(src => src.Fecha))
            .ForMember(c => c.chconcepto, dto => dto.MapFrom(src => src.Concepto))
            .ForMember(c => c.nusaldo_ini, dto => dto.MapFrom(src => src.SaldoIni))
            .ForMember(c => c.nudebe, dto => dto.MapFrom(src => src.Debe))
            .ForMember(c => c.nuhaber, dto => dto.MapFrom(src => src.Haber))
            .ForMember(c => c.numovimiento, dto => dto.MapFrom(src => src.Movimiento))
            .ForMember(c => c.chedo_resultados, dto => dto.MapFrom(src => src.EdoResultados))
            .ForMember(c => c.numes, dto => dto.MapFrom(src => src.Mes))
            .ForMember(c => c.nukidcentro_costos, dto => dto.MapFrom(src => src.IdCentroCostos))
            .ForMember(c => c.nukidtipo_cta_contable, dto => dto.MapFrom(src => src.IdTipoCtaContable))
            .ForMember(c => c.nuestatus, dto => dto.MapFrom(src => src.Estatus));
        #endregion Cie

        #region Catalogos                
        CreateMap<TB_Cat_Beneficio, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdBeneficio))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Beneficio))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Categoria, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdCategoria))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Categoria))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Clasificacion, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdClasificacion))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Clasificacion))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_CostoIndirectoSalarios, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdCostoIndirecto))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.CostoIndirecto))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Departamento, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdDepartamento))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Departamento))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Documento, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdDocumento))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Documento))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_EdoCivil, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdEdoCivil))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.EdoCivil))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_EstatusProyecto, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdEstatus))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Estatus))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Experiencia, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdExperiencia))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Experiencia))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_FormaPago, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdFormaPago))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoDocumento))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Gasto, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoGasto))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Gasto))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Habilidad, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdHabilidad))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Habilidad))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Ingreso, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdIngreso))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Ingreso))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Jornada, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdJornada))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Jornada))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Modena, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdMoneda))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Moneda))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_NivelEstudios, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdNivelEstudios))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.NivelEstudios))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_NivelPuesto, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdNivelPuesto))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.NivelPuesto))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Pcs, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdPcs))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Pcs))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Prestacion, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdPrestacion))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Viatico))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Puesto, Puesto_Detalle>()
            .ForMember(c => c.nukid_puesto, dto => dto.MapFrom(src => src.IdPuesto))
            .ForMember(c => c.chpuesto, dto => dto.MapFrom(src => src.Puesto))
            .ForMember(c => c.nusalario_min, dto => dto.MapFrom(src => src.SalarioMin))
            .ForMember(c => c.nusalario_max, dto => dto.MapFrom(src => src.SalarioMax))
            .ForMember(c => c.nusalario_prom, dto => dto.MapFrom(src => src.SalarioProm))
            .ForMember(c => c.boactivo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Profesion, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdProfesion))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Profesion))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_RubroIngresoReembolsable, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdRubroIngreso))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Rubro))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Sector, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdSector))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Sector))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Sexo, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdSexo))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Sexo))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoCie, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoCie))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoCie))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoContrato, TipoContrato_Detalle>()
            .ForMember(c => c.nukid_contrato, dto => dto.MapFrom(src => src.IdTipoContrato))
            .ForMember(c => c.chcontrato, dto => dto.MapFrom(src => src.Contrato))
            .ForMember(c => c.chve_contrato, dto => dto.MapFrom(src => src.VeContrato))
            .ForMember(c => c.boactivo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoCtaContable, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoCuenta))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Concepto))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoCuenta, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoCuenta))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoCuenta))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoDocumento, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoDocumento))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoDocumento))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoEmpleado, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoEmpleado))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoEmpleado))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoFactura, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoFactura))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoFactura))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoGasto, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoGasto))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoGasto))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoIngreso, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoIngreso))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoIngreso))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoPcs, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoPcs))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoPcs))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoPersona, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoPersona))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoPersona))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoPoliza, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoPoliza))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoPoliza))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoProyecto, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoProyecto))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoProyecto))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoResultado, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoResultado))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoResultado))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_TipoSangre, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTipoSangre))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.TipoSangre))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Turno, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdTurno))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Turno))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_UnidadNegocio, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdUnidadNegocio))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.UnidadNegocio))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cat_Viatico, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdViatico))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Viatico))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Estado, TB_Estado>()
            .ForMember(c => c.IdEstado, dto => dto.MapFrom(src => src.IdEstado))
            .ForMember(c => c.IdPais, dto => dto.MapFrom(src => src.IdPais))
            .ForMember(c => c.Estado, dto => dto.MapFrom(src => src.Estado))
            .ForMember(c => c.CveEntidad, dto => dto.MapFrom(src => src.CveEntidad))
            .ForMember(c => c.Activo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Pais, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.IdPais))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.Pais))
            .ForMember(c => c.activo, dto => dto.MapFrom(src => src.Activo));
        #endregion

        #region DOR
        CreateMap<DOR_Empleados, DorEmpleadoCorreo>()
            .ForMember(c => c.Proyecto, dto => dto.MapFrom(src => src.Proyecto))
            .ForMember(c => c.Puesto, dto => dto.MapFrom(src => src.Puesto))
            .ForMember(c => c.CentrosdeCostos, dto => dto.MapFrom(src => src.CentrosdeCostos))
            .ForMember(c => c.Nombre, dto => dto.MapFrom(src => src.Nombre));

        CreateMap<Dor_Subordinados, DorSubordinado>()
            .ForMember(c => c.NoEmpleado, dto => dto.MapFrom(src => src.NoEmpleado))
            .ForMember(c => c.Puesto, dto => dto.MapFrom(src => src.Puesto))
            .ForMember(c => c.CentrosdeCostos, dto => dto.MapFrom(src => src.CentrosdeCostos))
            .ForMember(c => c.Proyecto, dto => dto.MapFrom(src => src.Proyecto))
            .ForMember(c => c.DireccionEjecutiva, dto => dto.MapFrom(src => src.DireccionEjecutiva))
            .ForMember(c => c.UnidadDeNegocio, dto => dto.MapFrom(src => src.UnidadDeNegocio))
            .ForMember(c => c.Nivel, dto => dto.MapFrom(src => src.Nivel))
            .ForMember(c => c.JefeDirecto, dto => dto.MapFrom(src => src.JefeDirecto))
            .ForMember(c => c.Nombre, dto => dto.MapFrom(src => src.Nombre));

        CreateMap<Dor_ObjetivosGenerales, DorObjetivoGeneral>()
            .ForMember(c => c.Id, dto => dto.MapFrom(src => src.Id))
            .ForMember(c => c.UnidadDeNegocio, dto => dto.MapFrom(src => src.UnidadDeNegocio))
            .ForMember(c => c.Concepto, dto => dto.MapFrom(src => src.Concepto))
            .ForMember(c => c.Descripcion, dto => dto.MapFrom(src => src.Descripcion))
            .ForMember(c => c.Meta, dto => dto.MapFrom(src => src.Meta))
            .ForMember(c => c.Real, dto => dto.MapFrom(src => src.Real))
            .ForMember(c => c.PorcentajeEstimado, dto => dto.MapFrom(src => src.PorcentajeEstimado))
            .ForMember(c => c.PorcentajeReal, dto => dto.MapFrom(src => src.PorcentajeReal))
            .ForMember(c => c.Nivel, dto => dto.MapFrom(src => src.Nivel))
            .ForMember(c => c.Valor, dto => dto.MapFrom(src => src.Valor))
            .ForMember(c => c.Tooltip, dto => dto.MapFrom(src => src.Tooltip));

        CreateMap<Dor_ObjetivosEmpleado, DorObjetivoDesepeno>()
           .ForMember(c => c.IdEmpOb, dto => dto.MapFrom(src => src.IdEmpOb))
           .ForMember(c => c.UnidadDeNegocio, dto => dto.MapFrom(src => src.UnidadDeNegocio))
           .ForMember(c => c.Concepto, dto => dto.MapFrom(src => src.Concepto))
           .ForMember(c => c.Descripcion, dto => dto.MapFrom(src => src.Descripcion))
           .ForMember(c => c.Meta, dto => dto.MapFrom(src => src.Meta))
           .ForMember(c => c.Real, dto => dto.MapFrom(src => src.Real))
           .ForMember(c => c.PorcentajeEstimado, dto => dto.MapFrom(src => src.PorcentajeEstimado))
           .ForMember(c => c.PorcentajeReal, dto => dto.MapFrom(src => src.PorcentajeReal))
           .ForMember(c => c.Acepto, dto => dto.MapFrom(src => src.Acepto))
           .ForMember(c => c.MotivoR, dto => dto.MapFrom(src => src.MotivoR))
           .ForMember(c => c.FechaCarga, dto => dto.MapFrom(src => src.FechaCarga))
           .ForMember(c => c.FechaAceptado, dto => dto.MapFrom(src => src.FechaAceptado))
           .ForMember(c => c.FechaRechazo, dto => dto.MapFrom(src => src.FechaRechazo))
           .ForMember(c => c.Nivel, dto => dto.MapFrom(src => src.Nivel))
           .ForMember(c => c.Valor, dto => dto.MapFrom(src => src.Valor))
           .ForMember(c => c.Tooltip, dto => dto.MapFrom(src => src.Tooltip));
        #endregion

        #region Pcs
        CreateMap<TB_Proyecto, Proyecto>()
            .ForMember(c => c.NumProyecto, dto => dto.MapFrom(src => src.NumProyecto))
            .ForMember(c => c.Nombre, dto => dto.MapFrom(src => src.Proyecto))
            .ForMember(c => c.Alcance, dto => dto.MapFrom(src => src.Alcance))
            .ForMember(c => c.Cp, dto => dto.MapFrom(src => src.Cp))
            .ForMember(c => c.Ciudad, dto => dto.MapFrom(src => src.Ciudad))
            .ForMember(c => c.IdEstatus, dto => dto.MapFrom(src => src.IdEstatus))
            .ForMember(c => c.IdSector, dto => dto.MapFrom(src => src.IdSector))
            .ForMember(c => c.IdTipoProyecto, dto => dto.MapFrom(src => src.IdTipoProyecto))
            .ForMember(c => c.IdResponsablePreconstruccion, dto => dto.MapFrom(src => src.IdResponsablePreconstruccion))
            .ForMember(c => c.IdResponsableConstruccion, dto => dto.MapFrom(src => src.IdResponsableConstruccion))
            .ForMember(c => c.IdResponsableEhs, dto => dto.MapFrom(src => src.IdResponsableEhs))
            .ForMember(c => c.IdCliente, dto => dto.MapFrom(src => src.IdCliente))
            .ForMember(c => c.IdEmpresa, dto => dto.MapFrom(src => src.IdEmpresa))
            .ForMember(c => c.IdPais, dto => dto.MapFrom(src => src.IdPais))
            .ForMember(c => c.IdDirectorEjecutivo, dto => dto.MapFrom(src => src.IdDirectorEjecutivo))
            .ForMember(c => c.CostoPromedioM2, dto => dto.MapFrom(src => src.CostoPromedioM2))
            .ForMember(c => c.FechaIni, dto => dto.MapFrom(src => src.FechaIni))
            .ForMember(c => c.FechaFin, dto => dto.MapFrom(src => src.FechaFin));

        CreateMap<TB_Empresa, InfoEmpresa>()
            .ForMember(c => c.IdEmpresa, dto => dto.MapFrom(src => src.IdEmpresa))
            .ForMember(c => c.Empresa, dto => dto.MapFrom(src => src.Empresa))
            .ForMember(c => c.Rfc, dto => dto.MapFrom(src => src.Rfc));

        CreateMap<TB_Cliente, InfoCliente>()
            .ForMember(c => c.IdCliente, dto => dto.MapFrom(src => src.IdCliente))
            .ForMember(c => c.Cliente, dto => dto.MapFrom(src => src.Cliente))
            .ForMember(c => c.Rfc, dto => dto.MapFrom(src => src.Rfc));
        #endregion

        #region Factura

        CreateMap<BaseCFDI, InfoFactura>()
            .ForMember(c => c.Serie, dto => dto.MapFrom(src => src.Serie))
            .ForMember(c => c.Folio, dto => dto.MapFrom(src => src.Folio))
            .ForMember(c => c.Fecha, dto => dto.MapFrom(src => src.Fecha))
            .ForMember(c => c.SubTotal, dto => dto.MapFrom(src => src.SubTotal))
            .ForMember(c => c.Moneda, dto => dto.MapFrom(src => src.Moneda))
            .ForMember(c => c.TipoCambio, dto => dto.MapFrom(src => src.TipoCambio))
            .ForMember(c => c.Total, dto => dto.MapFrom(src => src.Total))
            .ForMember(c => c.TipoDeComprobante, dto => dto.MapFrom(src => src.TipoDeComprobante))
            .ForMember(c => c.RfcEmisor, dto => dto.MapFrom(src => src.RfcEmisor))
            .ForMember(c => c.RfcReceptor, dto => dto.MapFrom(src => src.RfcReceptor))
            .ForMember(c => c.Conceptos, dto => dto.MapFrom(src => src.Conceptos))
            .ForMember(c => c.TipoRelacion, dto => dto.MapFrom(src => src.TipoRelacion))
            .ForMember(c => c.CfdiRelacionados, dto => dto.MapFrom(src => src.CfdiRelacionados))
            .ForMember(c => c.TotalImpuestosTrasladados, dto => dto.MapFrom(src => src.TotalImpuestosTrasladados))
            .ForMember(c => c.Uuid, dto => dto.MapFrom(src => src.UUID))
            .ForMember(c => c.IsVersionValida, dto => dto.MapFrom(src => src.IsVersionValida))
            .ForMember(c => c.Pagos, dto => dto.MapFrom(src => src.Pagos));

        CreateMap<CfdiPagos, FacturaPagos>()
           .ForMember(c => c.FechaPago, dto => dto.MapFrom(src => src.FechaPago))
           .ForMember(c => c.TipoCambioP, dto => dto.MapFrom(src => src.TipoCambioP))
           .ForMember(c => c.DoctosRelacionados, dto => dto.MapFrom(src => src.DoctosRelacionados));

        CreateMap<CfdiPagoDocto, FacturaPagoDocto>()
           .ForMember(c => c.Uuid, dto => dto.MapFrom(src => src.Uuid))
           .ForMember(c => c.Serie, dto => dto.MapFrom(src => src.Serie))
           .ForMember(c => c.Folio, dto => dto.MapFrom(src => src.Folio))
           .ForMember(c => c.MonedaDR, dto => dto.MapFrom(src => src.MonedaDR))
           .ForMember(c => c.ImportePagado, dto => dto.MapFrom(src => src.ImportePagado))
           .ForMember(c => c.ImporteSaldoAnt, dto => dto.MapFrom(src => src.ImporteSaldoAnt))
           .ForMember(c => c.ImporteSaldoInsoluto, dto => dto.MapFrom(src => src.ImporteSaldoInsoluto))
           .ForMember(c => c.ImporteDR, dto => dto.MapFrom(src => src.ImporteDR));


        CreateMap<Factura_Proyecto, FacturaProyecto>()
           .ForMember(c => c.NumProyecto, dto => dto.MapFrom(src => src.NumProyecto))
           .ForMember(c => c.Nombre, dto => dto.MapFrom(src => src.Nombre))
           .ForMember(c => c.RfcBaseReceptor, dto => dto.MapFrom(src => src.RfcBaseReceptor))
           .ForMember(c => c.RfcBaseEmisor, dto => dto.MapFrom(src => src.RfcBaseEmisor));

        CreateMap<TB_Proyecto, Catalogo>()
            .ForMember(c => c.id, dto => dto.MapFrom(src => src.NumProyecto))
            .ForMember(c => c.descripcion, dto => dto.MapFrom(src => src.NumProyecto));

        CreateMap<FacturaDetalles, DetallesFactura>()
           .ForMember(c => c.Id, dto => dto.MapFrom(src => src.Id))
           .ForMember(c => c.NumProyecto, dto => dto.MapFrom(src => src.NumProyecto))
           .ForMember(c => c.IdTipoFactura, dto => dto.MapFrom(src => src.IdTipoFactura))
           .ForMember(c => c.IdMoneda, dto => dto.MapFrom(src => src.IdMoneda))
           .ForMember(c => c.Uuid, dto => dto.MapFrom(src => src.Uuid))
           .ForMember(c => c.Importe, dto => dto.MapFrom(src => src.Importe))
           .ForMember(c => c.Iva, dto => dto.MapFrom(src => src.Iva))
           .ForMember(c => c.IvaRet, dto => dto.MapFrom(src => src.IvaRet))
           .ForMember(c => c.Total, dto => dto.MapFrom(src => src.Total))
           .ForMember(c => c.Concepto, dto => dto.MapFrom(src => src.Concepto))
           .ForMember(c => c.Mes, dto => dto.MapFrom(src => src.Mes))
           .ForMember(c => c.Anio, dto => dto.MapFrom(src => src.Anio))
           .ForMember(c => c.FechaEmision, dto => dto.MapFrom(src => src.FechaEmision))
           .ForMember(c => c.FechaPago, dto => dto.MapFrom(src => src.FechaPago))
           .ForMember(c => c.FechaCancelacion, dto => dto.MapFrom(src => src.FechaCancelacion))
           .ForMember(c => c.NoFactura, dto => dto.MapFrom(src => src.NoFactura))
           .ForMember(c => c.TipoCambio, dto => dto.MapFrom(src => src.TipoCambio))
           .ForMember(c => c.MotivoCancelacion, dto => dto.MapFrom(src => src.MotivoCancelacion))
           .ForMember(c => c.Notas, dto => dto.MapFrom(src => src.Notas))
           .ForMember(c => c.Cobranzas, dto => dto.MapFrom(src => src.Cobranzas))           
           //.ForMember(c => c.NC_UuidNotaCredito, dto => dto.MapFrom(src => src.NC_UuidNotaCredito))
           //.ForMember(c => c.NC_IdMoneda, dto => dto.MapFrom(src => src.NC_IdMoneda))
           //.ForMember(c => c.NC_IdTipoRelacion, dto => dto.MapFrom(src => src.NC_IdTipoRelacion))
           //.ForMember(c => c.NC_NotaCredito, dto => dto.MapFrom(src => src.NC_NotaCredito))
           //.ForMember(c => c.NC_Importe, dto => dto.MapFrom(src => src.NC_Importe))
           //.ForMember(c => c.NC_Iva, dto => dto.MapFrom(src => src.NC_Iva))
           //.ForMember(c => c.NC_Total, dto => dto.MapFrom(src => src.NC_Total))
           //.ForMember(c => c.NC_Concepto, dto => dto.MapFrom(src => src.NC_Concepto))
           //.ForMember(c => c.NC_Mes, dto => dto.MapFrom(src => src.NC_Mes))
           //.ForMember(c => c.NC_Anio, dto => dto.MapFrom(src => src.NC_Anio))
           //.ForMember(c => c.NC_TipoCambio, dto => dto.MapFrom(src => src.NC_TipoCambio))
           //.ForMember(c => c.NC_FechaNotaCredito, dto => dto.MapFrom(src => src.NC_FechaNotaCredito))
           //.ForMember(c => c.C_UuidCobranza, dto => dto.MapFrom(src => src.C_UuidCobranza))
           //.ForMember(c => c.C_IdMonedaP, dto => dto.MapFrom(src => src.C_IdMonedaP))
           //.ForMember(c => c.C_ImportePagado, dto => dto.MapFrom(src => src.C_ImportePagado))
           //.ForMember(c => c.C_ImpSaldoAnt, dto => dto.MapFrom(src => src.C_ImpSaldoAnt))
           //.ForMember(c => c.C_ImporteSaldoInsoluto, dto => dto.MapFrom(src => src.C_ImporteSaldoInsoluto))
           //.ForMember(c => c.C_IvaP, dto => dto.MapFrom(src => src.C_IvaP))
           //.ForMember(c => c.C_TipoCambioP, dto => dto.MapFrom(src => src.C_TipoCambioP))
           //.ForMember(c => c.C_FechaPago, dto => dto.MapFrom(src => src.C_FechaPago))
           .ForMember(c => c.TotalCobranzas, dto => dto.MapFrom(src => src.TotalCobranzas))
           .ForMember(c => c.TotalNotasCredito, dto => dto.MapFrom(src => src.TotalNotasCredito));

        #endregion
    }
}

