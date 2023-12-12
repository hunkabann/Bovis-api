using AutoMapper;
using Bovis.Common.Model.DTO;
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

        CreateMap<TB_RequerimientoHabilidad, Habilidad>()
            .ForMember(c => c.nukidrequerimiento, dto => dto.MapFrom(src => src.IdRequerimiento))
            .ForMember(c => c.nukidhabilidad, dto => dto.MapFrom(src => src.IdHabilidad));

        CreateMap<TB_RequerimientoExperiencia, Experiencia>()
            .ForMember(c => c.nukidrequerimiento, dto => dto.MapFrom(src => src.IdRequerimiento))
            .ForMember(c => c.nukidexperiencia, dto => dto.MapFrom(src => src.IdExperiencia));
        #endregion Requerimientos

        #region Cie
        CreateMap<TB_Empresa, EmpresaRegistro>()
            .ForMember(c => c.nukidempresa, dto => dto.MapFrom(src => src.IdEmpresa))
            .ForMember(c => c.chempresa, dto => dto.MapFrom(src => src.Empresa))
            .ForMember(c => c.rfc, dto => dto.MapFrom(src => src.Rfc))
            .ForMember(c => c.nucoi, dto => dto.MapFrom(src => src.Coi))
            .ForMember(c => c.nunoi, dto => dto.MapFrom(src => src.Noi))
            .ForMember(c => c.nusae, dto => dto.MapFrom(src => src.Sae))
            .ForMember(c => c.boactivo, dto => dto.MapFrom(src => src.Activo));

        CreateMap<TB_Cie, CieRegistro>()
            .ForMember(c => c.IdCie, dto => dto.MapFrom(src => src.IdCie))
            .ForMember(c => c.NumProyecto, dto => dto.MapFrom(src => src.NumProyecto))
            .ForMember(c => c.IdTipoCie, dto => dto.MapFrom(src => src.IdTipoCie))
            .ForMember(c => c.IdTipoPoliza, dto => dto.MapFrom(src => src.IdTipoPoliza))
            .ForMember(c => c.Fecha, dto => dto.MapFrom(src => src.Fecha))
            .ForMember(c => c.Concepto, dto => dto.MapFrom(src => src.Concepto))
            .ForMember(c => c.SaldoIni, dto => dto.MapFrom(src => src.SaldoIni))
            .ForMember(c => c.Debe, dto => dto.MapFrom(src => src.Debe))
            .ForMember(c => c.Haber, dto => dto.MapFrom(src => src.Haber))
            .ForMember(c => c.Movimiento, dto => dto.MapFrom(src => src.Movimiento))
            .ForMember(c => c.EdoResultados, dto => dto.MapFrom(src => src.EdoResultados))
            .ForMember(c => c.Mes, dto => dto.MapFrom(src => src.Mes))
            .ForMember(c => c.IdCentroCostos, dto => dto.MapFrom(src => src.IdCentroCostos))
            .ForMember(c => c.IdTipoCtaContable, dto => dto.MapFrom(src => src.IdTipoCtaContable))
            .ForMember(c => c.Estatus, dto => dto.MapFrom(src => src.Estatus));
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

        CreateMap<TB_Cat_Moneda, Catalogo>()
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
        CreateMap<TB_DorEmpleados, DorEmpleadoCorreo>()
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

        //CreateMap<Dor_ObjetivosGenerales, DorObjetivoGeneral>()
        //    .ForMember(c => c.Id, dto => dto.MapFrom(src => src.Id))
        //    .ForMember(c => c.UnidadDeNegocio, dto => dto.MapFrom(src => src.UnidadDeNegocio))
        //    .ForMember(c => c.Concepto, dto => dto.MapFrom(src => src.Concepto))
        //    .ForMember(c => c.Descripcion, dto => dto.MapFrom(src => src.Descripcion))
        //    .ForMember(c => c.Meta, dto => dto.MapFrom(src => src.Meta))
        //    .ForMember(c => c.Real, dto => dto.MapFrom(src => src.Real))
        //    .ForMember(c => c.PorcentajeEstimado, dto => dto.MapFrom(src => src.PorcentajeEstimado))
        //    .ForMember(c => c.PorcentajeReal, dto => dto.MapFrom(src => src.PorcentajeReal))
        //    .ForMember(c => c.Ingreso, dto => dto.MapFrom(src => src.Ingreso))
        //    .ForMember(c => c.Gasto, dto => dto.MapFrom(src => src.Gasto))
        //    .ForMember(c => c.Nivel, dto => dto.MapFrom(src => src.Nivel))
        //    .ForMember(c => c.Valor, dto => dto.MapFrom(src => src.Valor))
        //    .ForMember(c => c.Tooltip, dto => dto.MapFrom(src => src.Tooltip))
        //    .ForMember(c => c.ENE, dto => dto.MapFrom(src => src.ENE))
        //    .ForMember(c => c.FEB, dto => dto.MapFrom(src => src.FEB))
        //    .ForMember(c => c.MAR, dto => dto.MapFrom(src => src.MAR))
        //    .ForMember(c => c.ABR, dto => dto.MapFrom(src => src.ABR))
        //    .ForMember(c => c.MAY, dto => dto.MapFrom(src => src.MAY))
        //    .ForMember(c => c.JUN, dto => dto.MapFrom(src => src.JUN))
        //    .ForMember(c => c.JUL, dto => dto.MapFrom(src => src.JUL))
        //    .ForMember(c => c.AGO, dto => dto.MapFrom(src => src.AGO))
        //    .ForMember(c => c.SEP, dto => dto.MapFrom(src => src.SEP))
        //    .ForMember(c => c.OCT, dto => dto.MapFrom(src => src.OCT))
        //    .ForMember(c => c.NOV, dto => dto.MapFrom(src => src.NOV))
        //    .ForMember(c => c.DIC, dto => dto.MapFrom(src => src.DIC));

        CreateMap<Dor_ObjetivosEmpleado, DorObjetivoDesepeno>()
           .ForMember(c => c.IdEmpOb, dto => dto.MapFrom(src => src.IdEmpOb))
           .ForMember(c => c.UnidadDeNegocio, dto => dto.MapFrom(src => src.UnidadDeNegocio))
           .ForMember(c => c.Concepto, dto => dto.MapFrom(src => src.Concepto))
           .ForMember(c => c.Descripcion, dto => dto.MapFrom(src => src.Descripcion))
           .ForMember(c => c.Meta, dto => dto.MapFrom(src => src.Meta))
           .ForMember(c => c.Real, dto => dto.MapFrom(src => src.Real))
           .ForMember(c => c.PromedioReal, dto => dto.MapFrom(src => src.PromedioReal))
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

        #region Beneficios
        CreateMap<TB_EmpleadoBeneficio, EmpleadoBeneficioDTO>().ReverseMap();
        #endregion
    }
}

