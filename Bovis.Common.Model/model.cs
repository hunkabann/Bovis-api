using Bovis.Common.Model.Tables;
using LinqToDB;
using LinqToDB.Data;

namespace Bovis.Common.Model
{
	public partial class ConnectionDB : DataConnection
	{
		public ITable<DOR_Empleados> dOR_Empleados { get { return this.GetTable<DOR_Empleados>(); } }
        public ITable<DOR_Gpm_Proyecto> dOR_Gpm_Proyecto { get { return this.GetTable<DOR_Gpm_Proyecto>(); } }
        public ITable<DOR_Metas_Proyecto> dOR_Meta_Proyecto { get { return this.GetTable<DOR_Metas_Proyecto>(); } }
        public ITable<DOR_Objetivos_Gral> dOR_Objetivos_Gral { get { return this.GetTable<DOR_Objetivos_Gral>(); } }
        public ITable<DOR_Objetivos_Nivel> dOR_Objetivos_Nivel { get { return this.GetTable<DOR_Objetivos_Nivel>(); } }
        public ITable<DOR_ObjetivosDesepeno> dOR_ObjetivosDesepenos { get { return this.GetTable<DOR_ObjetivosDesepeno>(); } }
        public ITable<DOR_Tooltip> dOR_Tooltip { get { return this.GetTable<DOR_Tooltip>(); } }
        public ITable<TB_Banco> tB_Bancos { get { return this.GetTable<TB_Banco>(); } }
		public ITable<TB_Bitacora> tB_Bitacoras { get { return this.GetTable<TB_Bitacora>(); } }
		public ITable<TB_Cat_Beneficio> tB_Cat_Beneficios { get { return this.GetTable<TB_Cat_Beneficio>(); } }
		public ITable<TB_Cat_Categoria> tB_Cat_Categorias { get { return this.GetTable<TB_Cat_Categoria>(); } }
		public ITable<TB_Cat_Clasificacion> tB_Cat_Clasificacions { get { return this.GetTable<TB_Cat_Clasificacion>(); } }
		public ITable<TB_Cat_CostoIndirectoSalarios> tB_Cat_CostoIndirectoSalarios { get { return this.GetTable<TB_Cat_CostoIndirectoSalarios>(); } }
		public ITable<TB_Cat_Departamento> tB_Cat_Departamentos { get { return this.GetTable<TB_Cat_Departamento>(); } }
		public ITable<TB_Cat_Documento> tB_Cat_Documentos { get { return this.GetTable<TB_Cat_Documento>(); } }
        public ITable<TB_Cat_Dor_PuestoNivel> tB_Cat_Dor_PuestoNivel { get { return this.GetTable<TB_Cat_Dor_PuestoNivel>(); } }
        public ITable<TB_Cat_EdoCivil> tB_Cat_EdoCivils { get { return this.GetTable<TB_Cat_EdoCivil>(); } }
        public ITable<TB_Cat_Experiencia> tB_Cat_Experiencias { get { return this.GetTable<TB_Cat_Experiencia>(); } }
        public ITable<TB_Cat_EstatusProyecto> tB_Cat_EstatusProyectos { get { return this.GetTable<TB_Cat_EstatusProyecto>(); } }
		public ITable<TB_Cat_FormaPago> tB_Cat_FormaPagos { get { return this.GetTable<TB_Cat_FormaPago>(); } }
		public ITable<TB_Cat_Gasto> tB_Cat_Gastos { get { return this.GetTable<TB_Cat_Gasto>(); } }
        public ITable<TB_Cat_Habilidad> tB_Cat_Habilidades { get { return this.GetTable<TB_Cat_Habilidad>(); } }
        public ITable<TB_Cat_Ingreso> tB_Cat_Ingresos { get { return this.GetTable<TB_Cat_Ingreso>(); } }
		public ITable<TB_Cat_Jornada> tB_Cat_Jornadas { get { return this.GetTable<TB_Cat_Jornada>(); } }
		public ITable<TB_Cat_Modena> tB_Cat_Modenas { get { return this.GetTable<TB_Cat_Modena>(); } }
		public ITable<TB_Cat_NivelEstudios> tB_Cat_NivelEstudios { get { return this.GetTable<TB_Cat_NivelEstudios>(); } }
		public ITable<TB_Cat_NivelPuesto> tB_Cat_NivelPuestos { get { return this.GetTable<TB_Cat_NivelPuesto>(); } }
		public ITable<TB_Cat_Pcs> tB_Cat_Pcs { get { return this.GetTable<TB_Cat_Pcs>(); } }
		public ITable<TB_Cat_Prestacion> tB_Cat_Prestacions { get { return this.GetTable<TB_Cat_Prestacion>(); } }
        public ITable<TB_Cat_Profesion> tB_Cat_Profesiones { get { return this.GetTable<TB_Cat_Profesion>(); } }
        public ITable<TB_Cat_Puesto> tB_Cat_Puestos { get { return this.GetTable<TB_Cat_Puesto>(); } }
		public ITable<TB_Cat_RubroIngresoReembolsable> tB_Cat_RubroIngresoReembolsables { get { return this.GetTable<TB_Cat_RubroIngresoReembolsable>(); } }
		public ITable<TB_Cat_Sector> tB_Cat_Sectors { get { return this.GetTable<TB_Cat_Sector>(); } }
        public ITable<TB_Cat_Sexo> tB_Cat_Sexos { get { return this.GetTable<TB_Cat_Sexo>(); } }
        public ITable<TB_Cat_TipoCie> tB_Cat_TipoCies { get { return this.GetTable<TB_Cat_TipoCie>(); } }
		public ITable<TB_Cat_TipoContrato> tB_Cat_TipoContratos { get { return this.GetTable<TB_Cat_TipoContrato>(); } }
        public ITable<TB_Cat_TipoContrato_Sat> tB_Cat_TipoContrato_Sats { get { return this.GetTable<TB_Cat_TipoContrato_Sat>(); } }
        public ITable<TB_Cat_TipoCtaContable> tB_Cat_TipoCtaContables { get { return this.GetTable<TB_Cat_TipoCtaContable>(); } }
		public ITable<TB_Cat_TipoCuenta> tB_Cat_TipoCuentas { get { return this.GetTable<TB_Cat_TipoCuenta>(); } }
		public ITable<TB_Cat_TipoDocumento> tB_Cat_TipoDocumentos { get { return this.GetTable<TB_Cat_TipoDocumento>(); } }
		public ITable<TB_Cat_TipoEmpleado> tB_Cat_TipoEmpleados { get { return this.GetTable<TB_Cat_TipoEmpleado>(); } }
		public ITable<TB_Cat_TipoFactura> tB_Cat_TipoFacturas { get { return this.GetTable<TB_Cat_TipoFactura>(); } }
		public ITable<TB_Cat_TipoGasto> tB_Cat_TipoGastos { get { return this.GetTable<TB_Cat_TipoGasto>(); } }
		public ITable<TB_Cat_TipoIngreso> tB_Cat_TipoIngresos { get { return this.GetTable<TB_Cat_TipoIngreso>(); } }
		public ITable<TB_Cat_TipoPcs> tB_Cat_TipoPcs { get { return this.GetTable<TB_Cat_TipoPcs>(); } }
        public ITable<TB_Cat_TipoPcs2> tB_Cat_TipoPcs2 { get { return this.GetTable<TB_Cat_TipoPcs2>(); } }
        public ITable<TB_Cat_TipoPersona> tB_Cat_TipoPersonas { get { return this.GetTable<TB_Cat_TipoPersona>(); } }
        public ITable<TB_Cat_TipoPoliza> tB_Cat_TipoPolizas { get { return this.GetTable<TB_Cat_TipoPoliza>(); } }
		public ITable<TB_Cat_TipoProyecto> tB_Cat_TipoProyectos { get { return this.GetTable<TB_Cat_TipoProyecto>(); } }
		public ITable<TB_Cat_TipoResultado> tB_Cat_TipoResultados { get { return this.GetTable<TB_Cat_TipoResultado>(); } }
		public ITable<TB_Cat_TipoSangre> tB_Cat_TipoSangres { get { return this.GetTable<TB_Cat_TipoSangre>(); } }
		public ITable<TB_Cat_UnidadNegocio> tB_Cat_UnidadNegocios { get { return this.GetTable<TB_Cat_UnidadNegocio>(); } }
		public ITable<TB_Cat_Viatico> tB_Cat_Viaticos { get { return this.GetTable<TB_Cat_Viatico>(); } }
		public ITable<TB_Categ_Prestacion> tB_Categ_Prestacions { get { return this.GetTable<TB_Categ_Prestacion>(); } }
		public ITable<TB_Cent_Costos> tB_Cent_Costos { get { return this.GetTable<TB_Cent_Costos>(); } }
		public ITable<TB_Cie> tB_Cies { get { return this.GetTable<TB_Cie>(); } }
        public ITable<TB_Cie_Data> tB_Cie_Datas { get { return this.GetTable<TB_Cie_Data>(); } }
        public ITable<TB_Ciudad> tB_Ciudads { get { return this.GetTable<TB_Ciudad>(); } }
		public ITable<TB_Cliente> tB_Clientes { get { return this.GetTable<TB_Cliente>(); } }
		public ITable<TB_ClienteEmpresa> tB_ClienteEmpresas { get { return this.GetTable<TB_ClienteEmpresa>(); } }
		public ITable<TB_Cobranza> tB_Cobranzas { get { return this.GetTable<TB_Cobranza>(); } }
		public ITable<TB_Colonia> tB_Colonias { get { return this.GetTable<TB_Colonia>(); } }
		public ITable<TB_Contacto> tB_Contactos { get { return this.GetTable<TB_Contacto>(); } }
		public ITable<TB_CuentaBanco> tB_CuentaBancos { get { return this.GetTable<TB_CuentaBanco>(); } }
        public ITable<TB_Dias_Timesheet> tB_Dias_Timesheets { get { return this.GetTable<TB_Dias_Timesheet>(); } }
        public ITable<TB_Direccion> tB_Direccions { get { return this.GetTable<TB_Direccion>(); } }
		public ITable<TB_Empleado> tB_Empleados { get { return this.GetTable<TB_Empleado>(); } }
		public ITable<TB_EmpleadoBeneficio> tB_EmpleadoBeneficios { get { return this.GetTable<TB_EmpleadoBeneficio>(); } }
		public ITable<TB_EmpleadoContrato> tB_EmpleadoContratos { get { return this.GetTable<TB_EmpleadoContrato>(); } }
		public ITable<TB_EmpleadoCorreos> tB_EmpleadoCorreos { get { return this.GetTable<TB_EmpleadoCorreos>(); } }
		public ITable<TB_EmpleadoCuenta> tB_EmpleadoCuentas { get { return this.GetTable<TB_EmpleadoCuenta>(); } }
		public ITable<TB_EmpleadoDocumento> tB_EmpleadoDocumentos { get { return this.GetTable<TB_EmpleadoDocumento>(); } }
        public ITable<TB_Empleado_Experiencia> tB_Empleado_Experiencias { get { return this.GetTable<TB_Empleado_Experiencia>(); } }
        public ITable<TB_Empleado_Habilidad> tB_Empleado_Habilidades { get { return this.GetTable<TB_Empleado_Habilidad>(); } }
        public ITable<TB_EmpleadoProyecto> tB_EmpleadoProyectos { get { return this.GetTable<TB_EmpleadoProyecto>(); } }
		public ITable<TB_EmpleadoProyectoBeneficio> tB_EmpleadoProyectoBeneficios { get { return this.GetTable<TB_EmpleadoProyectoBeneficio>(); } }
		public ITable<TB_Empresa> tB_Empresas { get { return this.GetTable<TB_Empresa>(); } }
		public ITable<TB_EmpresaCuenta> tB_EmpresaCuentas { get { return this.GetTable<TB_EmpresaCuenta>(); } }
		public ITable<TB_Estado> tB_Estados { get { return this.GetTable<TB_Estado>(); } }
		public ITable<TB_EstimadoConstruccion> tB_EstimadoConstruccions { get { return this.GetTable<TB_EstimadoConstruccion>(); } }
		public ITable<TB_Hist_EmpleadoPuesto> tB_Hist_EmpleadoPuestos { get { return this.GetTable<TB_Hist_EmpleadoPuesto>(); } }
		public ITable<TB_Modulo> tB_Modulos { get { return this.GetTable<TB_Modulo>(); } }
		public ITable<TB_Operacion> tB_Operacions { get { return this.GetTable<TB_Operacion>(); } }
		public ITable<TB_Pais> tB_Pais { get { return this.GetTable<TB_Pais>(); } }
		public ITable<TB_Perfil> tB_Perfils { get { return this.GetTable<TB_Perfil>(); } }
		public ITable<TB_PerfilModulo> tB_PerfilModulos { get { return this.GetTable<TB_PerfilModulo>(); } }
		public ITable<TB_PermisoUsuarioPerfilModulo> tB_PermisoUsuarioPerfilModulos { get { return this.GetTable<TB_PermisoUsuarioPerfilModulo>(); } }
		public ITable<TB_Persona> tB_Personas { get { return this.GetTable<TB_Persona>(); } }
		public ITable<TB_Proyecto> tB_Proyectos { get { return this.GetTable<TB_Proyecto>(); } }
		public ITable<TB_ProyectoFactura> tB_ProyectoFacturas { get { return this.GetTable<TB_ProyectoFactura>(); } }
        public ITable<TB_Proyecto_Factura_Nota_Credito> tB_ProyectoFacturasNotaCredito { get { return this.GetTable<TB_Proyecto_Factura_Nota_Credito>(); } }
        public ITable<TB_Proyecto_Factura_Cobranza> tB_ProyectoFacturasCobranza { get { return this.GetTable<TB_Proyecto_Factura_Cobranza>(); } }
        public ITable<TB_ProyectoFase> tB_ProyectoFases { get { return this.GetTable<TB_ProyectoFase>(); } }
		public ITable<TB_ProyectoGastos> tB_ProyectoGastos { get { return this.GetTable<TB_ProyectoGastos>(); } }
		public ITable<TB_ProyectoGastosCostoDirectosSalarios> tB_ProyectoGastosCostoDirectosSalarios { get { return this.GetTable<TB_ProyectoGastosCostoDirectosSalarios>(); } }
		public ITable<TB_ProyectoGastosCostoIndirectosSalarios> tB_ProyectoGastosCostoIndirectosSalarios { get { return this.GetTable<TB_ProyectoGastosCostoIndirectosSalarios>(); } }
		public ITable<TB_ProyectoIngresos> tB_ProyectoIngresos { get { return this.GetTable<TB_ProyectoIngresos>(); } }
		public ITable<TB_ProyectoIngresosCobranza> tB_ProyectoIngresosCobranzas { get { return this.GetTable<TB_ProyectoIngresosCobranza>(); } }
		public ITable<TB_ProyectoIngresosFactura> tB_ProyectoIngresosFacturas { get { return this.GetTable<TB_ProyectoIngresosFactura>(); } }
		public ITable<TB_ProyectoIngresosReembolsables> tB_ProyectoIngresosReembolsables { get { return this.GetTable<TB_ProyectoIngresosReembolsables>(); } }
		public ITable<TB_ProyectoParticipacionPersonal> tB_ProyectoParticipacionPersonals { get { return this.GetTable<TB_ProyectoParticipacionPersonal>(); } }
		public ITable<TB_ProyectoViatico> tB_ProyectoViaticos { get { return this.GetTable<TB_ProyectoViatico>(); } }
		public ITable<TB_PuestoProyecto> tB_PuestoProyectos{ get { return this.GetTable<TB_PuestoProyecto>(); } }
		public ITable<TB_Requerimiento> tB_Requerimientos { get { return this.GetTable<TB_Requerimiento>(); } }
        public ITable<TB_Requerimiento_Experiencia> tB_Requerimiento_Experiencias { get { return this.GetTable<TB_Requerimiento_Experiencia>(); } }
        public ITable<TB_Requerimiento_Habilidad> tB_Requerimiento_Habilidades { get { return this.GetTable<TB_Requerimiento_Habilidad>(); } }
        public ITable<TB_Timesheet> tB_Timesheets { get { return this.GetTable<TB_Timesheet>(); } }
        public ITable<TB_Timesheet_Otro> tB_Timesheet_Otros { get { return this.GetTable<TB_Timesheet_Otro>(); } }
        public ITable<TB_Timesheet_Proyecto> tB_Timesheet_Proyectos { get { return this.GetTable<TB_Timesheet_Proyecto>(); } }
        public ITable<TB_Usuario> tB_Usuarios { get { return this.GetTable<TB_Usuario>(); } }

		public ConnectionDB()
		{
			InitDataContext();
			InitMappingSchema();
		}
		public ConnectionDB(string configuration) : base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext();
		partial void InitMappingSchema();
	}

	public static partial class TableExtensions
	{
	}
}
