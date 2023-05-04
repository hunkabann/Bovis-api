using Bovis.Business.Interface;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Bovis.Business
{
	public class CatalogoBusiness : ICatalogoBusiness
	{
		private readonly ICatalogoData _catalogoData;
		private readonly ITransactionData _transactionData;
		public CatalogoBusiness(ICatalogoData catalogoData, ITransactionData _transactionData)
		{
			_catalogoData = catalogoData;
			this._transactionData = _transactionData;
		}

		#region Beneficio

		public Task<List<TB_Cat_Beneficio>> GetBeneficio(bool? Actio) => _catalogoData.GetBeneficio(Actio);
		public async Task<(bool Success, string Message)> AddBeneficio(TB_Cat_Beneficio beneficio)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddBeneficio(beneficio);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteBeneficio(TB_Cat_Beneficio beneficio)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteBeneficio(beneficio);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateBeneficio(InsertMovApi MovAPI, TB_Cat_Beneficio beneficio)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateBeneficio(beneficio);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(beneficio) });
			return resp;
		}

		#endregion

		#region Categoria

		public Task<List<TB_Cat_Categoria>> GetCategoria(bool? Actio) => _catalogoData.GetCategoria(Actio);
		public async Task<(bool Success, string Message)> AddCategoria(TB_Cat_Categoria categoria)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddCategoria(categoria);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteCategoria(TB_Cat_Categoria categoria)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteCategoria(categoria);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateCategoria(InsertMovApi MovAPI, TB_Cat_Categoria categoria)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateCategoria(categoria);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(categoria) });
			return resp;
		}

		#endregion

		#region Clasificacion

		public Task<List<TB_Cat_Clasificacion>> GetClasificacion(bool? Actio) => _catalogoData.GetClasificacion(Actio);
		public async Task<(bool Success, string Message)> AddClasificacion(TB_Cat_Clasificacion clasificacion)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddClasificacion(clasificacion);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteClasificacion(TB_Cat_Clasificacion clasificacion)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteClasificacion(clasificacion);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateClasificacion(InsertMovApi MovAPI, TB_Cat_Clasificacion clasificacion)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateClasificacion(clasificacion);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(clasificacion) });
			return resp;
		}

		#endregion

		#region Costo Indirecto Salarios

		public Task<List<TB_Cat_CostoIndirectoSalarios>> GetCostoIndirectoSalarios(bool? Actio) => _catalogoData.GetCostoIndirectoSalarios(Actio);
		public async Task<(bool Success, string Message)> AddCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddCostoIndirectoSalarios(costoIndirectoSalarios);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteCostoIndirectoSalarios(costoIndirectoSalarios);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateCostoIndirectoSalarios(InsertMovApi MovAPI, TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateCostoIndirectoSalarios(costoIndirectoSalarios);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(costoIndirectoSalarios) });
			return resp;
		}

		#endregion

		#region Departamento

		public Task<List<TB_Cat_Departamento>> GetDepartamento(bool? Actio) => _catalogoData.GetDepartamento(Actio);
		public async Task<(bool Success, string Message)> AddDepartamento(TB_Cat_Departamento departamento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddDepartamento(departamento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteDepartamento(TB_Cat_Departamento departamento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteDepartamento(departamento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateDepartamento(InsertMovApi MovAPI, TB_Cat_Departamento departamento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateDepartamento(departamento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(departamento) });
			return resp;
		}

		#endregion

		#region Documento

		public Task<List<TB_Cat_Documento>> GetDocumento(bool? Actio) => _catalogoData.GetDocumento(Actio);
		public async Task<(bool Success, string Message)> AddDocumento(TB_Cat_Documento documento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddDocumento(documento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteDocumento(TB_Cat_Documento documento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteDocumento(documento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateDocumento(InsertMovApi MovAPI, TB_Cat_Documento documento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateDocumento(documento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(documento) });
			return resp;
		}

		#endregion

		#region Estado Civil

		public Task<List<TB_Cat_EdoCivil>> GetEdoCivil(bool? Actio) => _catalogoData.GetEdoCivil(Actio);
		public async Task<(bool Success, string Message)> AddEdoCivil(TB_Cat_EdoCivil edoCivil)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddEdoCivil(edoCivil);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteEdoCivil(TB_Cat_EdoCivil edoCivil)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteEdoCivil(edoCivil);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateEdoCivil(InsertMovApi MovAPI, TB_Cat_EdoCivil edoCivil)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateEdoCivil(edoCivil);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(edoCivil) });
			return resp;
		}

		#endregion

		#region Estatus Proyecto

		public Task<List<TB_Cat_EstatusProyecto>> GetEstatusProyecto(bool? Actio) => _catalogoData.GetEstatusProyecto(Actio);
		public async Task<(bool Success, string Message)> AddEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddEstatusProyecto(estatusProyecto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteEstatusProyecto(estatusProyecto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateEstatusProyecto(InsertMovApi MovAPI, TB_Cat_EstatusProyecto estatusProyecto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateEstatusProyecto(estatusProyecto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(estatusProyecto) });
			return resp;
		}

		#endregion

		#region Forma Pago

		public Task<List<TB_Cat_FormaPago>> GetFormaPago(bool? Actio) => _catalogoData.GetFormaPago(Actio);
		public async Task<(bool Success, string Message)> AddFormaPago(TB_Cat_FormaPago formaPago)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddFormaPago(formaPago);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteFormaPago(TB_Cat_FormaPago formaPago)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteFormaPago(formaPago);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateFormaPago(InsertMovApi MovAPI, TB_Cat_FormaPago formaPago)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateFormaPago(formaPago);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(formaPago) });
			return resp;
		}

		#endregion

		#region Gasto

		public Task<List<TB_Cat_Gasto>> GetGasto(bool? Actio) => _catalogoData.GetGasto(Actio);
		public async Task<(bool Success, string Message)> AddGasto(TB_Cat_Gasto gasto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddGasto(gasto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteGasto(TB_Cat_Gasto gasto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteGasto(gasto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateGasto(InsertMovApi MovAPI, TB_Cat_Gasto gasto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateGasto(gasto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(gasto) });
			return resp;
		}

		#endregion

		#region Ingreso

		public Task<List<TB_Cat_Ingreso>> GetIngreso(bool? Actio) => _catalogoData.GetIngreso(Actio);
		public async Task<(bool Success, string Message)> AddIngreso(TB_Cat_Ingreso ingreso)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddIngreso(ingreso);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteIngreso(TB_Cat_Ingreso ingreso)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteIngreso(ingreso);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateIngreso(InsertMovApi MovAPI, TB_Cat_Ingreso ingreso)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateIngreso(ingreso);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(ingreso) });
			return resp;
		}

		#endregion

		#region Jornada

		public Task<List<TB_Cat_Jornada>> GetJornada(bool? Actio) => _catalogoData.GetJornada(Actio);
		public async Task<(bool Success, string Message)> AddJornada(TB_Cat_Jornada jornada)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddJornada(jornada);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteJornada(TB_Cat_Jornada jornada)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteJornada(jornada);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateJornada(InsertMovApi MovAPI, TB_Cat_Jornada jornada)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateJornada(jornada);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(jornada) });
			return resp;
		}

		#endregion

		#region Modena

		public Task<List<TB_Cat_Modena>> GetModena(bool? Actio) => _catalogoData.GetModena(Actio);
		public async Task<(bool Success, string Message)> AddModena(TB_Cat_Modena modena)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddModena(modena);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteModena(TB_Cat_Modena modena)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteModena(modena);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateModena(InsertMovApi MovAPI, TB_Cat_Modena modena)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateModena(modena);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(modena) });
			return resp;
		}

		#endregion

		#region Nivel Estudios

		public Task<List<TB_Cat_NivelEstudios>> GetNivelEstudios(bool? Actio) => _catalogoData.GetNivelEstudios(Actio);
		public async Task<(bool Success, string Message)> AddNivelEstudios(TB_Cat_NivelEstudios nivelEstudios)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddNivelEstudios(nivelEstudios);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteNivelEstudios(TB_Cat_NivelEstudios nivelEstudios)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteNivelEstudios(nivelEstudios);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateNivelEstudios(InsertMovApi MovAPI, TB_Cat_NivelEstudios nivelEstudios)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateNivelEstudios(nivelEstudios);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(nivelEstudios) });
			return resp;
		}

		#endregion

		#region Nivel Puesto

		public Task<List<TB_Cat_NivelPuesto>> GetNivelPuesto(bool? Actio) => _catalogoData.GetNivelPuesto(Actio);
		public async Task<(bool Success, string Message)> AddNivelPuesto(TB_Cat_NivelPuesto nivelPuesto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddNivelPuesto(nivelPuesto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteNivelPuesto(TB_Cat_NivelPuesto nivelPuesto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteNivelPuesto(nivelPuesto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateNivelPuesto(InsertMovApi MovAPI, TB_Cat_NivelPuesto nivelPuesto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateNivelPuesto(nivelPuesto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(nivelPuesto) });
			return resp;
		}

		#endregion

		#region Pcs

		public Task<List<TB_Cat_Pcs>> GetPcs(bool? Actio) => _catalogoData.GetPcs(Actio);
		public async Task<(bool Success, string Message)> AddPcs(TB_Cat_Pcs pcs)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddPcs(pcs);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeletePcs(TB_Cat_Pcs pcs)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeletePcs(pcs);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdatePcs(InsertMovApi MovAPI, TB_Cat_Pcs pcs)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdatePcs(pcs);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(pcs) });
			return resp;
		}

		#endregion

		#region Prestacion

		public Task<List<TB_Cat_Prestacion>> GetPrestacion(bool? Actio) => _catalogoData.GetPrestacion(Actio);
		public async Task<(bool Success, string Message)> AddPrestacion(TB_Cat_Prestacion prestacion)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddPrestacion(prestacion);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeletePrestacion(TB_Cat_Prestacion prestacion)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeletePrestacion(prestacion);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdatePrestacion(InsertMovApi MovAPI, TB_Cat_Prestacion prestacion)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdatePrestacion(prestacion);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(prestacion) });
			return resp;
		}

		#endregion

		#region Puesto

		public Task<List<TB_Cat_Puesto>> GetPuesto(bool? Actio) => _catalogoData.GetPuesto(Actio);
		public async Task<(bool Success, string Message)> AddPuesto(TB_Cat_Puesto puesto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddPuesto(puesto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeletePuesto(TB_Cat_Puesto puesto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeletePuesto(puesto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdatePuesto(InsertMovApi MovAPI, TB_Cat_Puesto puesto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdatePuesto(puesto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(puesto) });
			return resp;
		}

		#endregion

		#region Rubro Ingreso Reembolsable

		public Task<List<TB_Cat_RubroIngresoReembolsable>> GetRubroIngresoReembolsable(bool? Actio) => _catalogoData.GetRubroIngresoReembolsable(Actio);
		public async Task<(bool Success, string Message)> AddRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddRubroIngresoReembolsable(rubro);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteRubroIngresoReembolsable(rubro);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateRubroIngresoReembolsable(InsertMovApi MovAPI, TB_Cat_RubroIngresoReembolsable rubro)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateRubroIngresoReembolsable(rubro);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(rubro) });
			return resp;
		}

		#endregion

		#region Sector

		public Task<List<TB_Cat_Sector>> GetSector(bool? Actio) => _catalogoData.GetSector(Actio);
		public async Task<(bool Success, string Message)> AddSector(TB_Cat_Sector sector)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddSector(sector);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteSector(TB_Cat_Sector sector)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteSector(sector);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateSector(InsertMovApi MovAPI, TB_Cat_Sector sector)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateSector(sector);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(sector) });
			return resp;
		}

		#endregion

		#region Tipo Cie

		public Task<List<TB_Cat_TipoCie>> GetTipoCie(bool? Actio) => _catalogoData.GetTipoCie(Actio);
		public async Task<(bool Success, string Message)> AddTipoCie(TB_Cat_TipoCie tipoCie)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoCie(tipoCie);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoCie(TB_Cat_TipoCie tipoCie)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoCie(tipoCie);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoCie(InsertMovApi MovAPI, TB_Cat_TipoCie tipoCie)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoCie(tipoCie);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoCie) });
			return resp;
		}

		#endregion

		#region Tipo Contrato

		public Task<List<TB_Cat_TipoContrato>> GetTipoContrato(bool? Actio) => _catalogoData.GetTipoContrato(Actio);
		public async Task<(bool Success, string Message)> AddTipoContrato(TB_Cat_TipoContrato tipoContrato)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoContrato(tipoContrato);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoContrato(TB_Cat_TipoContrato tipoContrato)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoContrato(tipoContrato);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoContrato(InsertMovApi MovAPI, TB_Cat_TipoContrato tipoContrato)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoContrato(tipoContrato);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoContrato) });
			return resp;
		}

		#endregion

		#region Tipo Cta Contable

		public Task<List<TB_Cat_TipoCtaContable>> GetTipoCtaContable(bool? Actio) => _catalogoData.GetTipoCtaContable(Actio);
		public async Task<(bool Success, string Message)> AddTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoCtaContable(tipoCtaContable);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoCtaContable(tipoCtaContable);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoCtaContable(InsertMovApi MovAPI, TB_Cat_TipoCtaContable tipoCtaContable)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoCtaContable(tipoCtaContable);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoCtaContable) });
			return resp;
		}

		#endregion

		#region Tipo Cuenta

		public Task<List<TB_Cat_TipoCuenta>> GetTipoCuenta(bool? Actio) => _catalogoData.GetTipoCuenta(Actio);
		public async Task<(bool Success, string Message)> AddTipoCuenta(TB_Cat_TipoCuenta tipoCuenta)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoCuenta(tipoCuenta);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoCuenta(TB_Cat_TipoCuenta tipoCuenta)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoCuenta(tipoCuenta);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoCuenta(InsertMovApi MovAPI, TB_Cat_TipoCuenta tipoCuenta)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoCuenta(tipoCuenta);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoCuenta) });
			return resp;
		}

		#endregion

		#region Tipo Documento

		public Task<List<TB_Cat_TipoDocumento>> GetTipoDocumento(bool? Actio) => _catalogoData.GetTipoDocumento(Actio);
		public async Task<(bool Success, string Message)> AddTipoDocumento(TB_Cat_TipoDocumento tipoDocumento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoDocumento(tipoDocumento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoDocumento(TB_Cat_TipoDocumento tipoDocumento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoDocumento(tipoDocumento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoDocumento(InsertMovApi MovAPI, TB_Cat_TipoDocumento tipoDocumento)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoDocumento(tipoDocumento);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoDocumento) });
			return resp;
		}

		#endregion

		#region Tipo Empleado

		public Task<List<TB_Cat_TipoEmpleado>> GetTipoEmpleado(bool? Actio) => _catalogoData.GetTipoEmpleado(Actio);
		public async Task<(bool Success, string Message)> AddTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoEmpleado(tipoEmpleado);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoEmpleado(tipoEmpleado);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoEmpleado(InsertMovApi MovAPI, TB_Cat_TipoEmpleado tipoEmpleado)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoEmpleado(tipoEmpleado);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoEmpleado) });
			return resp;
		}

		#endregion

		#region Tipo Factura

		public Task<List<TB_Cat_TipoFactura>> GetTipoFactura(bool? Actio) => _catalogoData.GetTipoFactura(Actio);
		public async Task<(bool Success, string Message)> AddTipoFactura(TB_Cat_TipoFactura tipoFactura)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoFactura(tipoFactura);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoFactura(TB_Cat_TipoFactura tipoFactura)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoFactura(tipoFactura);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoFactura(InsertMovApi MovAPI, TB_Cat_TipoFactura tipoFactura)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoFactura(tipoFactura);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoFactura) });
			return resp;
		}

		#endregion

		#region Tipo Gasto

		public Task<List<TB_Cat_TipoGasto>> GetTipoGasto(bool? Actio) => _catalogoData.GetTipoGasto(Actio);
		public async Task<(bool Success, string Message)> AddTipoGasto(TB_Cat_TipoGasto tipoGasto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoGasto(tipoGasto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoGasto(TB_Cat_TipoGasto tipoGasto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoGasto(tipoGasto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoGasto(InsertMovApi MovAPI, TB_Cat_TipoGasto tipoGasto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoGasto(tipoGasto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoGasto) });
			return resp;
		}

		#endregion

		#region Tipo Ingreso

		public Task<List<TB_Cat_TipoIngreso>> GetTipoIngreso(bool? Actio) => _catalogoData.GetTipoIngreso(Actio);
		public async Task<(bool Success, string Message)> AddTipoIngreso(TB_Cat_TipoIngreso tipoIngreso)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoIngreso(tipoIngreso);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoIngreso(TB_Cat_TipoIngreso tipoIngreso)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoIngreso(tipoIngreso);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoIngreso(InsertMovApi MovAPI, TB_Cat_TipoIngreso tipoIngreso)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoIngreso(tipoIngreso);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoIngreso) });
			return resp;
		}

		#endregion

		#region Tipo Pcs

		public Task<List<TB_Cat_TipoPcs>> GetTipoPcs(bool? Actio) => _catalogoData.GetTipoPcs(Actio);
		public async Task<(bool Success, string Message)> AddTipoPcs(TB_Cat_TipoPcs tipoPcs)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoPcs(tipoPcs);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoPcs(TB_Cat_TipoPcs tipoPcs)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoPcs(tipoPcs);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoPcs(InsertMovApi MovAPI, TB_Cat_TipoPcs tipoPcs)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoPcs(tipoPcs);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoPcs) });
			return resp;
		}

		#endregion

		#region Tipo Poliza

		public Task<List<TB_Cat_TipoPoliza>> GetTipoPoliza(bool? Actio) => _catalogoData.GetTipoPoliza(Actio);
		public async Task<(bool Success, string Message)> AddTipoPoliza(TB_Cat_TipoPoliza tipoPoliza)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoPoliza(tipoPoliza);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoPoliza(TB_Cat_TipoPoliza tipoPoliza)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoPoliza(tipoPoliza);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoPoliza(InsertMovApi MovAPI, TB_Cat_TipoPoliza tipoPoliza)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoPoliza(tipoPoliza);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoPoliza) });
			return resp;
		}

		#endregion

		#region Tipo Proyecto

		public Task<List<TB_Cat_TipoProyecto>> GetTipoProyecto(bool? Actio) => _catalogoData.GetTipoProyecto(Actio);
		public async Task<(bool Success, string Message)> AddTipoProyecto(TB_Cat_TipoProyecto tipoProyecto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoProyecto(tipoProyecto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoProyecto(TB_Cat_TipoProyecto tipoProyecto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoProyecto(tipoProyecto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoProyecto(InsertMovApi MovAPI, TB_Cat_TipoProyecto tipoProyecto)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoProyecto(tipoProyecto);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoProyecto) });
			return resp;
		}

		#endregion

		#region Tipo Resultado

		public Task<List<TB_Cat_TipoResultado>> GetTipoResultado(bool? Actio) => _catalogoData.GetTipoResultado(Actio);
		public async Task<(bool Success, string Message)> AddTipoResultado(TB_Cat_TipoResultado tipoResultado)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoResultado(tipoResultado);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoResultado(TB_Cat_TipoResultado tipoResultado)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoResultado(tipoResultado);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoResultado(InsertMovApi MovAPI, TB_Cat_TipoResultado tipoResultado)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoResultado(tipoResultado);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoResultado) });
			return resp;
		}

		#endregion

		#region Tipo Sangre

		public Task<List<TB_Cat_TipoSangre>> GetTipoSangre(bool? Actio) => _catalogoData.GetTipoSangre(Actio);
		public async Task<(bool Success, string Message)> AddTipoSangre(TB_Cat_TipoSangre tipoSangre)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddTipoSangre(tipoSangre);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteTipoSangre(TB_Cat_TipoSangre tipoSangre)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteTipoSangre(tipoSangre);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateTipoSangre(InsertMovApi MovAPI, TB_Cat_TipoSangre tipoSangre)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateTipoSangre(tipoSangre);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(tipoSangre) });
			return resp;
		}

		#endregion

		#region UnidadNegocio

		public Task<List<TB_Cat_UnidadNegocio>> GetUnidadNegocio(bool? Actio) => _catalogoData.GetUnidadNegocio(Actio);
		public async Task<(bool Success, string Message)> AddUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddUnidadNegocio(unidadNegocio);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteUnidadNegocio(unidadNegocio);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateUnidadNegocio(InsertMovApi MovAPI, TB_Cat_UnidadNegocio unidadNegocio)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateUnidadNegocio(unidadNegocio);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(unidadNegocio) });
			return resp;
		}

		#endregion

		#region Viatico

		public Task<List<TB_Cat_Viatico>> GetViatico(bool? Actio) => _catalogoData.GetViatico(Actio);
		public async Task<(bool Success, string Message)> AddViatico(TB_Cat_Viatico viatico)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.AddViatico(viatico);
			if(!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> DeleteViatico(TB_Cat_Viatico viatico)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.DeleteViatico(viatico);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			return resp;
		}

		public async Task<(bool Success, string Message)> UpdateViatico(InsertMovApi MovAPI, TB_Cat_Viatico viatico)
		{
			(bool Success, string Message) resp = (true, string.Empty);
			var respData = await _catalogoData.UpdateViatico(viatico);
			if (!respData) { resp.Success = false; resp.Message = "No se pudo agregar el elemento del cataálogo a la base de datos"; return resp; }
			else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(viatico) });
			return resp;
		}

		#endregion

		#region Destructor
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion
	}
}
