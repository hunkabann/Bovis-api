using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System.Text.Json.Nodes;

namespace Bovis.Data
{
	public class CatalogoData : RepositoryLinq2DB<ConnectionDB>, ICatalogoData
	{
		private readonly string dbConfig = "DBConfig";
		public CatalogoData()
		{
			this.ConfigurationDB = dbConfig;
		}

        #region Destructor

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        #endregion



        #region Beneficio
        public async Task<List<TB_Cat_Beneficio>> GetBeneficio(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Beneficios
																		  where cat.Activo == activo
																		  orderby cat.Beneficio ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Beneficio>();
		}

		public Task<bool> AddBeneficio(TB_Cat_Beneficio beneficio) => InsertEntityIdAsync<TB_Cat_Beneficio>(beneficio);

		public Task<bool> UpdateBeneficio(TB_Cat_Beneficio beneficio) => UpdateEntityAsync<TB_Cat_Beneficio>(beneficio);

		public async Task<bool> DeleteBeneficio(TB_Cat_Beneficio beneficio)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Beneficios
					   .Where(x => x.IdBeneficio == beneficio.IdBeneficio)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Categoria

		public async Task<List<TB_Cat_Categoria>> GetCategoria(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Categorias
																		  where cat.Activo == activo
																		  orderby cat.Categoria ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Categoria>();
		}

		public Task<bool> AddCategoria(TB_Cat_Categoria categoria) => InsertEntityIdAsync<TB_Cat_Categoria>(categoria);

		public Task<bool> UpdateCategoria(TB_Cat_Categoria categoria) => UpdateEntityAsync<TB_Cat_Categoria>(categoria);

		public async Task<bool> DeleteCategoria(TB_Cat_Categoria categoria)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Categorias
					   .Where(x => x.IdCategoria == categoria.IdCategoria)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Clasificacion

		public async Task<List<TB_Cat_Clasificacion>> GetClasificacion(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Clasificacions
																		  where cat.Activo == activo
																		  orderby cat.Clasificacion ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Clasificacion>();
		}

		public Task<bool> AddClasificacion(TB_Cat_Clasificacion clasificacion) => InsertEntityIdAsync<TB_Cat_Clasificacion>(clasificacion);

		public Task<bool> UpdateClasificacion(TB_Cat_Clasificacion clasificacion) => UpdateEntityAsync<TB_Cat_Clasificacion>(clasificacion);

		public async Task<bool> DeleteClasificacion(TB_Cat_Clasificacion clasificacion)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Clasificacions
					   .Where(x => x.IdClasificacion == clasificacion.IdClasificacion)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Cliente

        public async Task<List<TB_Cliente>> GetCliente(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Clientes
																		  where cat.Activo == activo
																		  orderby cat.Cliente ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cliente>();
		}

		public async Task<(bool Success, string Message)> AddCliente(JsonObject registro)
		{
			string cliente = registro["cliente"].ToString();
			string rfc = registro["rfc"].ToString();

            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
			{
				var insert_cliente = await db.tB_Clientes
					.Value(x => x.Cliente, cliente)
					.Value(x => x.Rfc, rfc)
					.Value(x => x.Activo, true)
					.InsertAsync() > 0;

                resp.Success = insert_cliente;
                resp.Message = insert_cliente == default ? "Ocurrio un error al agregar registro." : string.Empty;

				return resp;
            }
        }

		public async Task<(bool Success, string Message)> UpdateCliente(JsonObject registro)
		{
            (bool Success, string Message) resp = (true, string.Empty);

            int id_cliente = Convert.ToInt32(registro["id_cliente"].ToString());
            string cliente = registro["cliente"].ToString();
            string rfc = registro["rfc"].ToString();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_cliente = await (db.tB_Clientes.Where(x => x.IdCliente == id_cliente)
                    .UpdateAsync(x => new TB_Cliente
                    {
                        Cliente = cliente,
						Rfc = rfc
                    })) > 0;

                resp.Success = res_update_cliente;
                resp.Message = res_update_cliente == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

		public async Task<(bool Success, string Message)> DeleteCliente(int idCliente)
		{
            (bool Success, string Message) resp = (true, string.Empty);

			using (ConnectionDB db = new ConnectionDB(dbConfig))
			{
				var res_update_cliente = await (db.tB_Clientes.Where(x => x.IdCliente == idCliente)
					.UpdateAsync(x => new TB_Cliente
                    {
                        Activo = false
                    })) > 0;

                resp.Success = res_update_cliente;
                resp.Message = res_update_cliente == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

        #endregion Cliente

        #region Costo Indirecto Salarios

        public async Task<List<TB_Cat_CostoIndirectoSalarios>> GetCostoIndirectoSalarios(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_CostoIndirectoSalarios
																		  where cat.Activo == activo
																		  orderby cat.CostoIndirecto ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_CostoIndirectoSalarios>();
		}

		public Task<bool> AddCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios) => InsertEntityIdAsync<TB_Cat_CostoIndirectoSalarios>(costoIndirectoSalarios);

		public Task<bool> UpdateCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios) => UpdateEntityAsync<TB_Cat_CostoIndirectoSalarios>(costoIndirectoSalarios);

		public async Task<bool> DeleteCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_CostoIndirectoSalarios
					   .Where(x => x.IdCostoIndirecto == costoIndirectoSalarios.IdCostoIndirecto)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Departamento

		public async Task<List<TB_Cat_Departamento>> GetDepartamento(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Departamentos
																		  where cat.Activo == activo
																		  orderby cat.Departamento ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Departamento>();
		}

		public Task<bool> AddDepartamento(TB_Cat_Departamento departamento) => InsertEntityIdAsync<TB_Cat_Departamento>(departamento);

		public Task<bool> UpdateDepartamento(TB_Cat_Departamento departamento) => UpdateEntityAsync<TB_Cat_Departamento>(departamento);

		public async Task<bool> DeleteDepartamento(TB_Cat_Departamento Departamento)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Departamentos
					   .Where(x => x.IdDepartamento == Departamento.IdDepartamento)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Documento

		public async Task<List<TB_Cat_Documento>> GetDocumento(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Documentos
																		  where cat.Activo == activo
																		  orderby cat.Documento ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Documento>();
		}

		public Task<bool> AddDocumento(TB_Cat_Documento documento) => InsertEntityIdAsync<TB_Cat_Documento>(documento);

		public Task<bool> UpdateDocumento(TB_Cat_Documento documento) => UpdateEntityAsync<TB_Cat_Documento>(documento);

		public async Task<bool> DeleteDocumento(TB_Cat_Documento documento)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Documentos
					   .Where(x => x.IdDocumento == documento.IdDocumento)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Estado
        public async Task<List<TB_Estado>> GetEdo(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Estados
                                                                          where cat.Activo == activo
																		  orderby cat.Estado ascending
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Estado>();
        }

        public Task<bool> AddEdo(TB_Estado edo) => InsertEntityIdAsync<TB_Estado>(edo);

        public Task<bool> UpdateEdo(TB_Estado edo) => UpdateEntityAsync<TB_Estado>(edo);

        public async Task<bool> DeleteEdo(TB_Estado edo)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var qry = db.tB_Estados
                       .Where(x => x.IdEstado == edo.IdEstado)
                       .Set(x => x.Activo, false);
                return await qry.UpdateAsync() >= 0;
            }
        }

        #endregion Estado

        #region Estado Civil
        public async Task<List<TB_Cat_EdoCivil>> GetEdoCivil(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_EdoCivils
																		  where cat.Activo == activo
																		  orderby cat.EdoCivil ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_EdoCivil>();
		}

		public Task<bool> AddEdoCivil(TB_Cat_EdoCivil edoCivil) => InsertEntityIdAsync<TB_Cat_EdoCivil>(edoCivil);

		public Task<bool> UpdateEdoCivil(TB_Cat_EdoCivil edoCivil) => UpdateEntityAsync<TB_Cat_EdoCivil>(edoCivil);

		public async Task<bool> DeleteEdoCivil(TB_Cat_EdoCivil edoCivil)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_EdoCivils
					   .Where(x => x.IdEdoCivil == edoCivil.IdEdoCivil)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Estatus Proyecto

		public async Task<List<TB_Cat_EstatusProyecto>> GetEstatusProyecto(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_EstatusProyectos
																		  where cat.Activo == activo
																		  orderby cat.Estatus ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_EstatusProyecto>();
		}

		public Task<bool> AddEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto) => InsertEntityIdAsync<TB_Cat_EstatusProyecto>(estatusProyecto);

		public Task<bool> UpdateEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto) => UpdateEntityAsync<TB_Cat_EstatusProyecto>(estatusProyecto);

		public async Task<bool> DeleteEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_EstatusProyectos
					   .Where(x => x.IdEstatus == estatusProyecto.IdEstatus)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Experiencia
        public async Task<List<TB_Cat_Experiencia>> GetExperiencia(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Experiencias
                                                                          where cat.Activo == activo
																		  orderby cat.Experiencia ascending
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Cat_Experiencia>();
        }
        public Task<bool> AddExperiencia(TB_Cat_Experiencia experiencia) => InsertEntityIdAsync<TB_Cat_Experiencia>(experiencia);

        public Task<bool> UpdateExperiencia(TB_Cat_Experiencia experiencia) => UpdateEntityAsync<TB_Cat_Experiencia>(experiencia);

        public async Task<bool> DeleteExperiencia(TB_Cat_Experiencia experiencia)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var qry = db.tB_Cat_Experiencias
                       .Where(x => x.IdExperiencia == experiencia.IdExperiencia)
                       .Set(x => x.Activo, false);
                return await qry.UpdateAsync() >= 0;
            }
        }
        #endregion Experiencia

        #region Forma Pago

        public async Task<List<TB_Cat_FormaPago>> GetFormaPago(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_FormaPagos
																		  where cat.Activo == activo
																		  orderby cat.TipoDocumento ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_FormaPago>();
		}

		public Task<bool> AddFormaPago(TB_Cat_FormaPago formaPago) => InsertEntityIdAsync<TB_Cat_FormaPago>(formaPago);

		public Task<bool> UpdateFormaPago(TB_Cat_FormaPago formaPago) => UpdateEntityAsync<TB_Cat_FormaPago>(formaPago);

		public async Task<bool> DeleteFormaPago(TB_Cat_FormaPago formaPago)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_FormaPagos
					   .Where(x => x.IdFormaPago == formaPago.IdFormaPago)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Gasto

		public async Task<List<TB_Cat_Gasto>> GetGasto(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Gastos
																		  where cat.Activo == activo
																		  orderby cat.Gasto ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Gasto>();
		}

		public Task<bool> AddGasto(TB_Cat_Gasto gasto) => InsertEntityIdAsync<TB_Cat_Gasto>(gasto);

		public Task<bool> UpdateGasto(TB_Cat_Gasto gasto) => UpdateEntityAsync<TB_Cat_Gasto>(gasto);

		public async Task<bool> DeleteGasto(TB_Cat_Gasto gasto)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Gastos
					   .Where(x => x.IdGasto == gasto.IdGasto)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Habilidad
        public async Task<List<TB_Cat_Habilidad>> GetHabilidad(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Habilidades
                                                                          where cat.Activo == activo
																		  orderby cat.Habilidad ascending
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Cat_Habilidad>();
        }
        public Task<bool> AddHabilidad(TB_Cat_Habilidad habilidad) => InsertEntityIdAsync<TB_Cat_Habilidad>(habilidad);

        public Task<bool> UpdateHabilidad(TB_Cat_Habilidad habilidad) => UpdateEntityAsync<TB_Cat_Habilidad>(habilidad);

        public async Task<bool> DeleteHabilidad(TB_Cat_Habilidad habilidad)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var qry = db.tB_Cat_Habilidades
                       .Where(x => x.IdHabilidad == habilidad.IdHabilidad)
                       .Set(x => x.Activo, false);
                return await qry.UpdateAsync() >= 0;
            }
        }
        #endregion Habilidad

        #region Ingreso

        public async Task<List<TB_Cat_Ingreso>> GetIngreso(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Ingresos
																		  where cat.Activo == activo
																		  orderby cat.Ingreso ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Ingreso>();
		}

		public Task<bool> AddIngreso(TB_Cat_Ingreso ingreso) => InsertEntityIdAsync<TB_Cat_Ingreso>(ingreso);

		public Task<bool> UpdateIngreso(TB_Cat_Ingreso ingreso) => UpdateEntityAsync<TB_Cat_Ingreso>(ingreso);

		public async Task<bool> DeleteIngreso(TB_Cat_Ingreso ingreso)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Ingresos
					   .Where(x => x.IdIngreso == ingreso.IdIngreso)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion
		
		#region Jornada

		public async Task<List<TB_Cat_Jornada>> GetJornada(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Jornadas
																		  where cat.Activo == activo
																		  orderby cat.Jornada ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Jornada>();
		}

		public Task<bool> AddJornada(TB_Cat_Jornada jornada) => InsertEntityIdAsync<TB_Cat_Jornada>(jornada);

		public Task<bool> UpdateJornada(TB_Cat_Jornada jornada) => UpdateEntityAsync<TB_Cat_Jornada>(jornada);

		public async Task<bool> DeleteJornada(TB_Cat_Jornada jornada)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Jornadas
					   .Where(x => x.IdJornada == jornada.IdJornada)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion
		
		#region Moneda

		public async Task<List<TB_Cat_Moneda>> GetModena(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Monedas
																		  where cat.Activo == activo
																		  orderby cat.Moneda ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Moneda>();
		}

		public Task<bool> AddModena(TB_Cat_Moneda modena) => InsertEntityIdAsync<TB_Cat_Moneda>(modena);

		public Task<bool> UpdateModena(TB_Cat_Moneda modena) => UpdateEntityAsync<TB_Cat_Moneda>(modena);

		public async Task<bool> DeleteModena(TB_Cat_Moneda modena)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Monedas
					   .Where(x => x.IdMoneda == modena.IdMoneda)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion
		
		#region Nivel Estudios

		public async Task<List<TB_Cat_NivelEstudios>> GetNivelEstudios(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_NivelEstudios
																		  where cat.Activo == activo
																		  orderby cat.NivelEstudios ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_NivelEstudios>();
		}

		public Task<bool> AddNivelEstudios(TB_Cat_NivelEstudios nivelEstudios) => InsertEntityIdAsync<TB_Cat_NivelEstudios>(nivelEstudios);

		public Task<bool> UpdateNivelEstudios(TB_Cat_NivelEstudios nivelEstudios) => UpdateEntityAsync<TB_Cat_NivelEstudios>(nivelEstudios);

		public async Task<bool> DeleteNivelEstudios(TB_Cat_NivelEstudios nivelEstudios)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_NivelEstudios
					   .Where(x => x.IdNivelEstudios == nivelEstudios.IdNivelEstudios)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Nivel Puesto

		public async Task<List<TB_Cat_NivelPuesto>> GetNivelPuesto(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_NivelPuestos
																		  where cat.Activo == activo
																		  orderby cat.NivelPuesto ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_NivelPuesto>();
		}

		public Task<bool> AddNivelPuesto(TB_Cat_NivelPuesto nivelPuesto) => InsertEntityIdAsync<TB_Cat_NivelPuesto>(nivelPuesto);

		public Task<bool> UpdateNivelPuesto(TB_Cat_NivelPuesto nivelPuesto) => UpdateEntityAsync<TB_Cat_NivelPuesto>(nivelPuesto);

		public async Task<bool> DeleteNivelPuesto(TB_Cat_NivelPuesto nivelPuesto)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_NivelPuestos
					   .Where(x => x.IdNivelPuesto == nivelPuesto.IdNivelPuesto)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Pais

        public async Task<List<TB_Pais>> GetPais(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Pais
                                                                          where cat.Activo == activo
																		  orderby cat.Pais ascending
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Pais>();
        }

        public Task<bool> AddPais(TB_Pais Pais) => InsertEntityIdAsync<TB_Pais>(Pais);

        public Task<bool> UpdatePais(TB_Pais Pais) => UpdateEntityAsync<TB_Pais>(Pais);

        public async Task<bool> DeletePais(TB_Pais Pais)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var qry = db.tB_Pais
                       .Where(x => x.IdPais == Pais.IdPais)
                       .Set(x => x.Activo, false);
                return await qry.UpdateAsync() >= 0;
            }
        }

        #endregion Pais

        #region Pcs

        public async Task<List<TB_Cat_Pcs>> GetPcs(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Pcs
																		  where cat.Activo == activo
																		  orderby cat.Pcs ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Pcs>();
		}

		public Task<bool> AddPcs(TB_Cat_Pcs pcs) => InsertEntityIdAsync<TB_Cat_Pcs>(pcs);

		public Task<bool> UpdatePcs(TB_Cat_Pcs pcs) => UpdateEntityAsync<TB_Cat_Pcs>(pcs);

		public async Task<bool> DeletePcs(TB_Cat_Pcs pcs)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Pcs
					   .Where(x => x.IdPcs == pcs.IdPcs)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Prestacion

		public async Task<List<TB_Cat_Prestacion>> GetPrestacion(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Prestacions
																		  where cat.Activo == activo
																		  orderby cat.Viatico ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Prestacion>();
		}

		public Task<bool> AddPrestacion(TB_Cat_Prestacion prestacion) => InsertEntityIdAsync<TB_Cat_Prestacion>(prestacion);

		public Task<bool> UpdatePrestacion(TB_Cat_Prestacion prestacion) => UpdateEntityAsync<TB_Cat_Prestacion>(prestacion);

		public async Task<bool> DeletePrestacion(TB_Cat_Prestacion prestacion)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Prestacions
					   .Where(x => x.IdPrestacion == prestacion.IdPrestacion)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Profesion
        public async Task<List<TB_Cat_Profesion>> GetProfesion(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Profesiones
                                                                          where cat.Activo == activo
																		  orderby cat.Profesion ascending
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Cat_Profesion>();
        }
        public Task<bool> AddProfesion(TB_Cat_Profesion profesion) => InsertEntityIdAsync<TB_Cat_Profesion>(profesion);

        public Task<bool> UpdateProfesion(TB_Cat_Profesion profesion) => UpdateEntityAsync<TB_Cat_Profesion>(profesion);

        public async Task<bool> DeleteProfesion(TB_Cat_Profesion profesion)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var qry = db.tB_Cat_Profesiones
                       .Where(x => x.IdProfesion == profesion.IdProfesion)
                       .Set(x => x.Activo, false);
                return await qry.UpdateAsync() >= 0;
            }
        }
		#endregion Profesion

		#region Puesto

		public async Task<List<Puesto_Detalle>> GetPuesto(bool? activo)
		{
			using (var db = new ConnectionDB(dbConfig)) return await (from puesto in db.tB_Cat_Puestos
																	  where puesto.Activo == activo
																	  orderby puesto.Puesto ascending
																	  select new Puesto_Detalle
																	  {
																		  nukid_puesto = puesto.IdPuesto,
																		  nukidnivel = puesto.IdNivel,
                                                                          chcvenoi = puesto.chcvenoi,
                                                                          chpuesto = puesto.Puesto,
																		  nusalario_min = puesto.SalarioMin,
																		  nusalario_max = puesto.SalarioMax,
																		  nusalario_prom = puesto.SalarioProm,
																		  boactivo = puesto.Activo
																	  }).ToListAsync();
		}

        //public Task<bool> AddPuesto(TB_Cat_Puesto puesto) => InsertEntityIdAsync<TB_Cat_Puesto>(puesto);

        public async Task<(bool Success, string Message)> AddPuesto(JsonObject registro)
        {

			//Atc
           // int nukid_puesto = Convert.ToInt32(registro["nukid_puesto"].ToString());
           
            string chpuesto = registro["chpuesto"].ToString();
            string chcvenoi = registro["chcvenoi"].ToString();
            decimal nusalario_min = Convert.ToDecimal(registro["nusalario_min"].ToString());
            decimal nusalario_max = Convert.ToDecimal(registro["nusalario_max"].ToString());
            decimal nusalario_prom = Convert.ToDecimal(registro["nusalario_prom"].ToString());
            int nukidnivel = Convert.ToInt32(registro["nukidnivel"].ToString());
            string boactivo = registro["boactivo"].ToString();

            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_Puesto = await db.tB_Cat_Puestos
                    //.Value(x => x.IdPuesto, nukid_puesto)
                    .Value(x => x.IdNivel, nukidnivel)
                     .Value(x => x.chcvenoi, chcvenoi)
                    .Value(x => x.Puesto, chpuesto)
                    .Value(x => x.SalarioMin, nusalario_min)
                    .Value(x => x.SalarioMax, nusalario_max)
                    .Value(x => x.SalarioProm, nusalario_prom)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_Puesto;
                resp.Message = insert_Puesto == default ? "Ocurrio un error al agregar registro." : string.Empty;

                return resp;
            }
        }

        //public Task<bool> UpdatePuesto(TB_Cat_Puesto puesto) => UpdateEntityAsync<TB_Cat_Puesto>(puesto);

        public async Task<(bool Success, string Message)> UpdatePuesto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int nukid_puesto = Convert.ToInt32(registro["nukid_puesto"].ToString());

            string chpuesto = registro["chpuesto"].ToString();
            string chcvenoi = registro["chcvenoi"].ToString();
            decimal nusalario_min = Convert.ToDecimal(registro["nusalario_min"].ToString());
            decimal nusalario_max = Convert.ToDecimal(registro["nusalario_max"].ToString());
            decimal nusalario_prom = Convert.ToDecimal(registro["nusalario_prom"].ToString());
            int nukidnivel = Convert.ToInt32(registro["nukidnivel"].ToString());
            string boactivo = registro["boactivo"].ToString();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_puesto = await (db.tB_Cat_Puestos.Where(x => x.IdPuesto == nukid_puesto)
                    .UpdateAsync(x => new TB_Cat_Puesto
                    {
                        chcvenoi = chcvenoi,
                        Puesto = chpuesto,
                        SalarioMin = nusalario_min,
                        SalarioMax = nusalario_max,
                        SalarioProm = nusalario_prom,
                    })) > 0;

                resp.Success = res_update_puesto;
                resp.Message = res_update_puesto == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> DeletePuesto(int nukid_puesto)
		{
			//using (var db = new ConnectionDB(dbConfig))
			//{
				//var qry = db.tB_Cat_Puestos
				//	   .Where(x => x.IdNivel == puesto.IdNivel)
				//	   .Set(x => x.Activo, false);
				//return await qry.UpdateAsync() >= 0;
			//}

            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_puesto = await (db.tB_Cat_Puestos.Where(x => x.IdPuesto == nukid_puesto)
                    .UpdateAsync(x => new TB_Cat_Puesto
                    {
                        Activo = false
                    })) > 0;

                resp.Success = res_update_puesto;
                resp.Message = res_update_puesto == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

		#endregion

		#region Rubro Ingreso Reembolsable

		public async Task<List<TB_Cat_RubroIngresoReembolsable>> GetRubroIngresoReembolsable(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_RubroIngresoReembolsables
																		  where cat.Activo == activo
																		  orderby cat.Rubro ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_RubroIngresoReembolsable>();
		}

		public Task<bool> AddRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro) => InsertEntityIdAsync<TB_Cat_RubroIngresoReembolsable>(rubro);

		public Task<bool> UpdateRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro) => UpdateEntityAsync<TB_Cat_RubroIngresoReembolsable>(rubro);

		public async Task<bool> DeleteRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_RubroIngresoReembolsables
					   .Where(x => x.IdRubroIngreso == rubro.IdRubroIngreso)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Sector

		public async Task<List<TB_Cat_Sector>> GetSector(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Sectors
																		  where cat.Activo == activo
																		  orderby cat.Sector ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Sector>();
		}

		public Task<bool> AddSector(TB_Cat_Sector sector) => InsertEntityIdAsync<TB_Cat_Sector>(sector);

		public Task<bool> UpdateSector(TB_Cat_Sector sector) => UpdateEntityAsync<TB_Cat_Sector>(sector);

		public async Task<bool> DeleteSector(TB_Cat_Sector sector)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Sectors
					   .Where(x => x.IdSector == sector.IdSector)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Sexo
        public async Task<List<TB_Cat_Sexo>> GetSexo(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Sexos
                                                                          where cat.Activo == activo
																		  orderby cat.Sexo ascending
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Cat_Sexo>();
        }

        public Task<bool> AddSexo(TB_Cat_Sexo Sexo) => InsertEntityIdAsync<TB_Cat_Sexo>(Sexo);

        public Task<bool> UpdateSexo(TB_Cat_Sexo Sexo) => UpdateEntityAsync<TB_Cat_Sexo>(Sexo);

        public async Task<bool> DeleteSexo(TB_Cat_Sexo Sexo)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var qry = db.tB_Cat_Sexos
                       .Where(x => x.IdSexo == Sexo.IdSexo)
                       .Set(x => x.Activo, false);
                return await qry.UpdateAsync() >= 0;
            }
        }

        #endregion Sexo

        #region Tipo Cie

        public async Task<List<TB_Cat_TipoCie>> GetTipoCie(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoCies
																		  where cat.Activo == activo
																		  orderby cat.TipoCie ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoCie>();
		}

		public Task<bool> AddTipoCie(TB_Cat_TipoCie tipoCie) => InsertEntityIdAsync<TB_Cat_TipoCie>(tipoCie);

		public Task<bool> UpdateTipoCie(TB_Cat_TipoCie tipoCie) => UpdateEntityAsync<TB_Cat_TipoCie>(tipoCie);

		public async Task<bool> DeleteTipoCie(TB_Cat_TipoCie tipoCie)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoCies
					   .Where(x => x.IdTipoCie == tipoCie.IdTipoCie)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Contrato

		public async Task<List<TipoContrato_Detalle>> GetTipoContrato(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from contrato in db.tB_Cat_TipoContratos
																		  where contrato.Activo == activo
																		  orderby contrato.Contrato ascending
																		  select new TipoContrato_Detalle
																		  {
																			  nukid_contrato = contrato.IdTipoContrato,
																			  chcontrato = contrato.Contrato,
																			  chve_contrato = contrato.VeContrato,
																			  boactivo = contrato.Activo
																		  }).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TipoContrato_Detalle>();
		}

		public Task<bool> AddTipoContrato(TB_Cat_TipoContrato tipoContrato) => InsertEntityIdAsync<TB_Cat_TipoContrato>(tipoContrato);

		public Task<bool> UpdateTipoContrato(TB_Cat_TipoContrato tipoContrato) => UpdateEntityAsync<TB_Cat_TipoContrato>(tipoContrato);

		public async Task<bool> DeleteTipoContrato(TB_Cat_TipoContrato tipoContrato)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoContratos
					   .Where(x => x.IdTipoContrato == tipoContrato.IdTipoContrato)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Cta Contable

		public async Task<List<TB_Cat_TipoCtaContable>> GetTipoCtaContable(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoCtaContables
																		  where cat.Activo == activo
																		  orderby cat.CtaContable ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoCtaContable>();
		}

		public Task<bool> AddTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable) => InsertEntityIdAsync<TB_Cat_TipoCtaContable>(tipoCtaContable);

		public Task<bool> UpdateTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable) => UpdateEntityAsync<TB_Cat_TipoCtaContable>(tipoCtaContable);

		public async Task<bool> DeleteTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoCtaContables
					   .Where(x => x.IdTipoCtaContable == tipoCtaContable.IdTipoCtaContable)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Cuenta

		public async Task<List<TB_Cat_TipoCuenta>> GetTipoCuenta(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoCuentas
																		  where cat.Activo == activo
																		  orderby cat.TipoCuenta ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoCuenta>();
		}

		public Task<bool> AddTipoCuenta(TB_Cat_TipoCuenta tipoCuenta) => InsertEntityIdAsync<TB_Cat_TipoCuenta>(tipoCuenta);

		public Task<bool> UpdateTipoCuenta(TB_Cat_TipoCuenta tipoCuenta) => UpdateEntityAsync<TB_Cat_TipoCuenta>(tipoCuenta);

		public async Task<bool> DeleteTipoCuenta(TB_Cat_TipoCuenta tipoCuenta)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoCuentas
					   .Where(x => x.IdTipoCuenta == tipoCuenta.IdTipoCuenta)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Documento

		public async Task<List<TB_Cat_TipoDocumento>> GetTipoDocumento(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoDocumentos
																		  where cat.Activo == activo
																		  orderby cat.TipoDocumento ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoDocumento>();
		}

		public Task<bool> AddTipoDocumento(TB_Cat_TipoDocumento tipoDocumento) => InsertEntityIdAsync<TB_Cat_TipoDocumento>(tipoDocumento);

		public Task<bool> UpdateTipoDocumento(TB_Cat_TipoDocumento tipoDocumento) => UpdateEntityAsync<TB_Cat_TipoDocumento>(tipoDocumento);

		public async Task<bool> DeleteTipoDocumento(TB_Cat_TipoDocumento tipoDocumento)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoDocumentos
					   .Where(x => x.IdTipoDocumento == tipoDocumento.IdTipoDocumento)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Empleado

		public async Task<List<TB_Cat_TipoEmpleado>> GetTipoEmpleado(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoEmpleados
																		  where cat.Activo == activo
																		  orderby cat.TipoEmpleado ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoEmpleado>();
		}

		public Task<bool> AddTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado) => InsertEntityIdAsync<TB_Cat_TipoEmpleado>(tipoEmpleado);

		public Task<bool> UpdateTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado) => UpdateEntityAsync<TB_Cat_TipoEmpleado>(tipoEmpleado);

		public async Task<bool> DeleteTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoEmpleados
					   .Where(x => x.IdTipoEmpleado == tipoEmpleado.IdTipoEmpleado)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Factura

		public async Task<List<TB_Cat_TipoFactura>> GetTipoFactura(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoFacturas
																		  where cat.Activo == activo
																		  orderby cat.TipoFactura ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoFactura>();
		}

		public Task<bool> AddTipoFactura(TB_Cat_TipoFactura tipoFactura) => InsertEntityIdAsync<TB_Cat_TipoFactura>(tipoFactura);

		public Task<bool> UpdateTipoFactura(TB_Cat_TipoFactura tipoFactura) => UpdateEntityAsync<TB_Cat_TipoFactura>(tipoFactura);

		public async Task<bool> DeleteTipoFactura(TB_Cat_TipoFactura tipoFactura)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoFacturas
					   .Where(x => x.IdTipoFactura == tipoFactura.IdTipoFactura)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Gasto

		public async Task<List<TB_Cat_TipoGasto>> GetTipoGasto(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoGastos
																		  where cat.Activo == activo
																		  orderby cat.TipoGasto ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoGasto>();
		}

		public Task<bool> AddTipoGasto(TB_Cat_TipoGasto tipoGasto) => InsertEntityIdAsync<TB_Cat_TipoGasto>(tipoGasto);

		public Task<bool> UpdateTipoGasto(TB_Cat_TipoGasto tipoGasto) => UpdateEntityAsync<TB_Cat_TipoGasto>(tipoGasto);

		public async Task<bool> DeleteTipoGasto(TB_Cat_TipoGasto tipoGasto)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoGastos
					   .Where(x => x.IdTipoGasto == tipoGasto.IdTipoGasto)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Ingreso

		public async Task<List<TB_Cat_TipoIngreso>> GetTipoIngreso(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoIngresos
																		  where cat.Activo == activo
																		  orderby cat.TipoIngreso ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoIngreso>();
		}

		public Task<bool> AddTipoIngreso(TB_Cat_TipoIngreso tipoIngreso) => InsertEntityIdAsync<TB_Cat_TipoIngreso>(tipoIngreso);

		public Task<bool> UpdateTipoIngreso(TB_Cat_TipoIngreso tipoIngreso) => UpdateEntityAsync<TB_Cat_TipoIngreso>(tipoIngreso);

		public async Task<bool> DeleteTipoIngreso(TB_Cat_TipoIngreso tipoIngreso)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoIngresos
					   .Where(x => x.IdTipoIngreso == tipoIngreso.IdTipoIngreso)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Pcs

		public async Task<List<TB_Cat_TipoPcs>> GetTipoPcs(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoPcs
																		  where cat.Activo == activo
																		  orderby cat.TipoPcs ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoPcs>();
		}

		public Task<bool> AddTipoPcs(TB_Cat_TipoPcs tipoPcs) => InsertEntityIdAsync<TB_Cat_TipoPcs>(tipoPcs);

		public Task<bool> UpdateTipoPcs(TB_Cat_TipoPcs tipoPcs) => UpdateEntityAsync<TB_Cat_TipoPcs>(tipoPcs);

		public async Task<bool> DeleteTipoPcs(TB_Cat_TipoPcs tipoPcs)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoPcs
					   .Where(x => x.IdTipoPcs == tipoPcs.IdTipoPcs)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Tipo Persona
        public async Task<List<TB_Cat_TipoPersona>> GetTipoPersona(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoPersonas
                                                                          where cat.Activo == activo
																		  orderby cat.TipoPersona ascending
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Cat_TipoPersona>();
        }

        public Task<bool> AddTipoPersona(TB_Cat_TipoPersona tipoPersona) => InsertEntityIdAsync<TB_Cat_TipoPersona>(tipoPersona);

        public Task<bool> UpdateTipoPersona(TB_Cat_TipoPersona tipoPersona) => UpdateEntityAsync<TB_Cat_TipoPersona>(tipoPersona);

        public async Task<bool> DeleteTipoPersona(TB_Cat_TipoPersona tipoPersona)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var qry = db.tB_Cat_TipoPersonas
                       .Where(x => x.IdTipoPersona == tipoPersona.IdTipoPersona)
                       .Set(x => x.Activo, false);
                return await qry.UpdateAsync() >= 0;
            }
        }

        #endregion Tipo Persona

        #region Tipo Poliza

        public async Task<List<TB_Cat_TipoPoliza>> GetTipoPoliza(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoPolizas
																		  where cat.Activo == activo
																		  orderby cat.TipoPoliza ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoPoliza>();
		}

		public Task<bool> AddTipoPoliza(TB_Cat_TipoPoliza tipoPoliza) => InsertEntityIdAsync<TB_Cat_TipoPoliza>(tipoPoliza);

		public Task<bool> UpdateTipoPoliza(TB_Cat_TipoPoliza tipoPoliza) => UpdateEntityAsync<TB_Cat_TipoPoliza>(tipoPoliza);

		public async Task<bool> DeleteTipoPoliza(TB_Cat_TipoPoliza tipoPoliza)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoPolizas
					   .Where(x => x.IdTipoPoliza == tipoPoliza.IdTipoPoliza)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Proyecto

		public async Task<List<TB_Cat_TipoProyecto>> GetTipoProyecto(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoProyectos
																		  where cat.Activo == activo
																		  orderby cat.TipoProyecto ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoProyecto>();
		}

		public Task<bool> AddTipoProyecto(TB_Cat_TipoProyecto tipoProyecto) => InsertEntityIdAsync<TB_Cat_TipoProyecto>(tipoProyecto);

		public Task<bool> UpdateTipoProyecto(TB_Cat_TipoProyecto tipoProyecto) => UpdateEntityAsync<TB_Cat_TipoProyecto>(tipoProyecto);

		public async Task<bool> DeleteTipoProyecto(TB_Cat_TipoProyecto tipoProyecto)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoProyectos
					   .Where(x => x.IdTipoProyecto == tipoProyecto.IdTipoProyecto)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Resultado

		public async Task<List<TB_Cat_TipoResultado>> GetTipoResultado(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoResultados
																		  where cat.Activo == activo
																		  orderby cat.TipoResultado ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoResultado>();
		}

		public Task<bool> AddTipoResultado(TB_Cat_TipoResultado tipoResultado) => InsertEntityIdAsync<TB_Cat_TipoResultado>(tipoResultado);

		public Task<bool> UpdateTipoResultado(TB_Cat_TipoResultado tipoResultado) => UpdateEntityAsync<TB_Cat_TipoResultado>(tipoResultado);

		public async Task<bool> DeleteTipoResultado(TB_Cat_TipoResultado tipoResultado)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoResultados
					   .Where(x => x.IdTipoResultado == tipoResultado.IdTipoResultado)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Tipo Sangre

		public async Task<List<TB_Cat_TipoSangre>> GetTipoSangre(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_TipoSangres
																		  where cat.Activo == activo
																		  orderby cat.TipoSangre ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_TipoSangre>();
		}

		public Task<bool> AddTipoSangre(TB_Cat_TipoSangre tipoSangre) => InsertEntityIdAsync<TB_Cat_TipoSangre>(tipoSangre);

		public Task<bool> UpdateTipoSangre(TB_Cat_TipoSangre tipoSangre) => UpdateEntityAsync<TB_Cat_TipoSangre>(tipoSangre);

		public async Task<bool> DeleteTipoSangre(TB_Cat_TipoSangre tipoSangre)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_TipoSangres
					   .Where(x => x.IdTipoSangre == tipoSangre.IdTipoSangre)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

        #endregion

        #region Turno
        public async Task<List<TB_Cat_Turno>> GetTurno(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from turno in db.tB_Cat_Turnos
                                                                          where turno.Activo == activo
																		  orderby turno.Turno ascending
                                                                          select turno).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Cat_Turno>();
        }

        public Task<bool> AddTurno(TB_Cat_Turno Turno) => InsertEntityIdAsync<TB_Cat_Turno>(Turno);

        public Task<bool> UpdateTurno(TB_Cat_Turno Turno) => UpdateEntityAsync<TB_Cat_Turno>(Turno);

        public async Task<bool> DeleteTurno(TB_Cat_Turno Turno)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var qry = db.tB_Cat_Turnos
                       .Where(x => x.IdTurno == Turno.IdTurno)
                       .Set(x => x.Activo, false);
                return await qry.UpdateAsync() >= 0;
            }
        }

        #endregion Turno

        #region Unidad Negocio

        public async Task<List<TB_Cat_UnidadNegocio>> GetUnidadNegocio(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_UnidadNegocios
																		  where cat.Activo == activo
																		  orderby cat.UnidadNegocio ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_UnidadNegocio>();
		}

		public Task<bool> AddUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio) => InsertEntityIdAsync<TB_Cat_UnidadNegocio>(unidadNegocio);

		public Task<bool> UpdateUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio) => UpdateEntityAsync<TB_Cat_UnidadNegocio>(unidadNegocio);

		public async Task<bool> DeleteUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_UnidadNegocios
					   .Where(x => x.IdUnidadNegocio == unidadNegocio.IdUnidadNegocio)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		#region Viatico

		public async Task<List<TB_Cat_Viatico>> GetViatico(bool? activo)
		{
			if (activo.HasValue)
			{
				using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Cat_Viaticos
																		  where cat.Activo == activo
																		  orderby cat.Viatico ascending
																		  select cat).ToListAsync();
			}
			else return await GetAllFromEntityAsync<TB_Cat_Viatico>();
		}

		public Task<bool> AddViatico(TB_Cat_Viatico viatico) => InsertEntityIdAsync<TB_Cat_Viatico>(viatico);

		public Task<bool> UpdateViatico(TB_Cat_Viatico viatico) => UpdateEntityAsync<TB_Cat_Viatico>(viatico);

		public async Task<bool> DeleteViatico(TB_Cat_Viatico viatico)
		{
			using (var db = new ConnectionDB(dbConfig))
			{
				var qry = db.tB_Cat_Viaticos
					   .Where(x => x.IdViatico == viatico.IdViatico)
					   .Set(x => x.Activo, false);
				return await qry.UpdateAsync() >= 0;
			}
		}

		#endregion

		


	}
}
