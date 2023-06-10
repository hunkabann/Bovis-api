using Bovis.Common;
using MediatR;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Business.Interface;
using AutoMapper;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;

namespace Bovis.Service.EventHandlers;

#region Beneficio

public class AgregaBeneficioEventHandler : IRequestHandler<AgregarBeneficioCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaBeneficioEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarBeneficioCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddBeneficio(new TB_Cat_Beneficio { Activo = true, Beneficio = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaBeneficioEventHandler : IRequestHandler<EliminarBeneficioCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaBeneficioEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarBeneficioCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteBeneficio(new TB_Cat_Beneficio { IdBeneficio = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaBeneficioEventHandler : IRequestHandler<ActualizarBeneficioCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaBeneficioEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarBeneficioCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateBeneficio(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Beneficio { Beneficio = request.descripcion, IdBeneficio = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Categoria

public class AgregaCategoriaEventHandler : IRequestHandler<AgregarCategoriaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaCategoriaEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarCategoriaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddCategoria(new TB_Cat_Categoria { Activo = true, Categoria = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaCategoriaEventHandler : IRequestHandler<EliminarCategoriaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaCategoriaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarCategoriaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteCategoria(new TB_Cat_Categoria { IdCategoria = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaCategoriaEventHandler : IRequestHandler<ActualizarCategoriaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaCategoriaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarCategoriaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateCategoria(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Categoria { Categoria = request.descripcion, IdCategoria = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Clasificacion

public class AgregaClasificacionEventHandler : IRequestHandler<AgregarClasificacionCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaClasificacionEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarClasificacionCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddClasificacion(new TB_Cat_Clasificacion { Activo = true, Clasificacion = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaClasificacionEventHandler : IRequestHandler<EliminarClasificacionCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaClasificacionEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarClasificacionCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteClasificacion(new TB_Cat_Clasificacion { IdClasificacion = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaClasificacionEventHandler : IRequestHandler<ActualizarClasificacionCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaClasificacionEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarClasificacionCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateClasificacion(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Clasificacion { Clasificacion = request.descripcion, IdClasificacion = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Costo Indirecto Salarios

public class AgregaCostoIndirectoSalariosEventHandler : IRequestHandler<AgregarCostoIndirectoSalariosCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaCostoIndirectoSalariosEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarCostoIndirectoSalariosCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddCostoIndirectoSalarios(new TB_Cat_CostoIndirectoSalarios { Activo = true, CostoIndirecto = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaCostoIndirectoSalariosEventHandler : IRequestHandler<EliminarCostoIndirectoSalariosCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaCostoIndirectoSalariosEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarCostoIndirectoSalariosCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteCostoIndirectoSalarios(new TB_Cat_CostoIndirectoSalarios { IdCostoIndirecto = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaCostoIndirectoSalariosEventHandler : IRequestHandler<ActualizarCostoIndirectoSalariosCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaCostoIndirectoSalariosEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarCostoIndirectoSalariosCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateCostoIndirectoSalarios(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_CostoIndirectoSalarios { CostoIndirecto = request.descripcion, IdCostoIndirecto = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Departamento

public class AgregaDepartamentoEventHandler : IRequestHandler<AgregarDepartamentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaDepartamentoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarDepartamentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddDepartamento(new TB_Cat_Departamento { Activo = true, Departamento = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaDepartamentoEventHandler : IRequestHandler<EliminarDepartamentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaDepartamentoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarDepartamentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteDepartamento(new TB_Cat_Departamento { IdDepartamento = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaDepartamentoEventHandler : IRequestHandler<ActualizarDepartamentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaDepartamentoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarDepartamentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateDepartamento(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Departamento { Departamento = request.descripcion, IdDepartamento = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Documento

public class AgregaDocumentoEventHandler : IRequestHandler<AgregarDocumentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaDocumentoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarDocumentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddDocumento(new TB_Cat_Documento { Activo = true, Documento = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaDocumentoEventHandler : IRequestHandler<EliminarDocumentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaDocumentoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarDocumentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteDocumento(new TB_Cat_Documento { IdDocumento = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaDocumentoEventHandler : IRequestHandler<ActualizarDocumentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaDocumentoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarDocumentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateDocumento(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Documento { Documento = request.descripcion, IdDocumento = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Estado Civil

public class AgregaEdoCivilEventHandler : IRequestHandler<AgregarEdoCivilCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaEdoCivilEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarEdoCivilCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddEdoCivil(new TB_Cat_EdoCivil { Activo = true, EdoCivil = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaEdoCivilEventHandler : IRequestHandler<EliminarEdoCivilCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaEdoCivilEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarEdoCivilCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteEdoCivil(new TB_Cat_EdoCivil { IdEdoCivil = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaEdoCivilEventHandler : IRequestHandler<ActualizarEdoCivilCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaEdoCivilEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarEdoCivilCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateEdoCivil(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_EdoCivil { EdoCivil = request.descripcion, IdEdoCivil = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Estatus Proyecto
public class AgregaEstatusProyectoEventHandler : IRequestHandler<AgregarEstatusProyectoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaEstatusProyectoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarEstatusProyectoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddEstatusProyecto(new TB_Cat_EstatusProyecto { Activo = true, Estatus = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaEstatusProyectoEventHandler : IRequestHandler<EliminarEstatusProyectoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaEstatusProyectoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarEstatusProyectoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteEstatusProyecto(new TB_Cat_EstatusProyecto { IdEstatus = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaEstatusProyectoEventHandler : IRequestHandler<ActualizarEstatusProyectoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaEstatusProyectoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarEstatusProyectoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateEstatusProyecto(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_EstatusProyecto { Estatus = request.descripcion, IdEstatus = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Experiencia
public class AgregaExperienciaEventHandler : IRequestHandler<AgregarExperienciaCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;
    private readonly IMapper _mapper;

    public AgregaExperienciaEventHandler(ICatalogoBusiness _business, IMapper _mapper)
    {
        this._business = _business;
        this._mapper = _mapper;
    }

    public async Task<Response<bool>> Handle(AgregarExperienciaCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.AddExperiencia(new TB_Cat_Experiencia { Activo = true, Experiencia = request.descripcion });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}

public class EliminaExperienciaEventHandler : IRequestHandler<EliminarExperienciaCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;

    public EliminaExperienciaEventHandler(ICatalogoBusiness _business)
    {
        this._business = _business;
    }

    public async Task<Response<bool>> Handle(EliminarExperienciaCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.DeleteExperiencia(new TB_Cat_Experiencia { IdExperiencia = request.id });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}

public class ActualizaExperienciaEventHandler : IRequestHandler<ActualizarExperienciaCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;

    public ActualizaExperienciaEventHandler(ICatalogoBusiness _business)
    {
        this._business = _business;
    }

    public async Task<Response<bool>> Handle(ActualizarExperienciaCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.UpdateExperiencia(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Experiencia { Experiencia = request.descripcion, IdExperiencia = request.id, Activo = true });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}
#endregion Experiencia

#region Forma Pago

public class AgregaFormaPagoEventHandler : IRequestHandler<AgregarFormaPagoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaFormaPagoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarFormaPagoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddFormaPago(new TB_Cat_FormaPago { Activo = true, TipoDocumento = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaFormaPagoEventHandler : IRequestHandler<EliminarFormaPagoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaFormaPagoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarFormaPagoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteFormaPago(new TB_Cat_FormaPago { IdFormaPago = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaFormaPagoEventHandler : IRequestHandler<ActualizarFormaPagoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaFormaPagoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarFormaPagoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateFormaPago(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_FormaPago { TipoDocumento = request.descripcion, IdFormaPago = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Gasto

public class AgregaGastoEventHandler : IRequestHandler<AgregarGastoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaGastoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarGastoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddGasto(new TB_Cat_Gasto { Activo = true, Gasto = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaGastoEventHandler : IRequestHandler<EliminarGastoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaGastoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarGastoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteGasto(new TB_Cat_Gasto { IdGasto = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaGastoEventHandler : IRequestHandler<ActualizarGastoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaGastoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarGastoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateGasto(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Gasto { Gasto = request.descripcion, IdGasto = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Habilidad
public class AgregaHabilidadEventHandler : IRequestHandler<AgregarHabilidadCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;
    private readonly IMapper _mapper;

    public AgregaHabilidadEventHandler(ICatalogoBusiness _business, IMapper _mapper)
    {
        this._business = _business;
        this._mapper = _mapper;
    }

    public async Task<Response<bool>> Handle(AgregarHabilidadCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.AddHabilidad(new TB_Cat_Habilidad { Activo = true, Habilidad = request.descripcion });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}

public class EliminaHabilidadEventHandler : IRequestHandler<EliminarHabilidadCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;

    public EliminaHabilidadEventHandler(ICatalogoBusiness _business)
    {
        this._business = _business;
    }

    public async Task<Response<bool>> Handle(EliminarHabilidadCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.DeleteHabilidad(new TB_Cat_Habilidad { IdHabilidad = request.id });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}

public class ActualizaHabilidadEventHandler : IRequestHandler<ActualizarHabilidadCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;

    public ActualizaHabilidadEventHandler(ICatalogoBusiness _business)
    {
        this._business = _business;
    }

    public async Task<Response<bool>> Handle(ActualizarHabilidadCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.UpdateHabilidad(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Habilidad { Habilidad = request.descripcion, IdHabilidad = request.id, Activo = true });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}
#endregion Habilidad

#region Ingreso

public class AgregaIngresoEventHandler : IRequestHandler<AgregarIngresoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaIngresoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarIngresoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddIngreso(new TB_Cat_Ingreso { Activo = true, Ingreso = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaIngresoEventHandler : IRequestHandler<EliminarIngresoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaIngresoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarIngresoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteIngreso(new TB_Cat_Ingreso { IdIngreso = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaIngresoEventHandler : IRequestHandler<ActualizarIngresoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaIngresoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarIngresoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateIngreso(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Ingreso { Ingreso = request.descripcion, IdIngreso = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Jornada

public class AgregaJornadaEventHandler : IRequestHandler<AgregarJornadaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaJornadaEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarJornadaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddJornada(new TB_Cat_Jornada { Activo = true, Jornada = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaJornadaEventHandler : IRequestHandler<EliminarJornadaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaJornadaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarJornadaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteJornada(new TB_Cat_Jornada { IdJornada = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaJornadaEventHandler : IRequestHandler<ActualizarJornadaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaJornadaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarJornadaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateJornada(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Jornada { Jornada = request.descripcion, IdJornada = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Modena

public class AgregaModenaEventHandler : IRequestHandler<AgregarModenaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaModenaEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarModenaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddModena(new TB_Cat_Modena { Activo = true, Moneda = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaModenaEventHandler : IRequestHandler<EliminarModenaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaModenaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarModenaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteModena(new TB_Cat_Modena { IdMoneda = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaModenaEventHandler : IRequestHandler<ActualizarModenaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaModenaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarModenaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateModena(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Modena { Moneda = request.descripcion, IdMoneda = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Nivel Estudios

public class AgregaNivelEstudiosEventHandler : IRequestHandler<AgregarNivelEstudiosCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaNivelEstudiosEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarNivelEstudiosCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddNivelEstudios(new TB_Cat_NivelEstudios { Activo = true, NivelEstudios = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaNivelEstudiosEventHandler : IRequestHandler<EliminarNivelEstudiosCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaNivelEstudiosEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarNivelEstudiosCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteNivelEstudios(new TB_Cat_NivelEstudios { IdNivelEstudios = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaNivelEstudiosEventHandler : IRequestHandler<ActualizarNivelEstudiosCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaNivelEstudiosEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarNivelEstudiosCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateNivelEstudios(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_NivelEstudios { NivelEstudios = request.descripcion, IdNivelEstudios = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Nivel Puesto

public class AgregaNivelPuestoEventHandler : IRequestHandler<AgregarNivelPuestoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaNivelPuestoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarNivelPuestoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddNivelPuesto(new TB_Cat_NivelPuesto { Activo = true, NivelPuesto = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaNivelPuestoEventHandler : IRequestHandler<EliminarNivelPuestoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaNivelPuestoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarNivelPuestoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteNivelPuesto(new TB_Cat_NivelPuesto { IdNivelPuesto = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaNivelPuestoEventHandler : IRequestHandler<ActualizarNivelPuestoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaNivelPuestoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarNivelPuestoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateNivelPuesto(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_NivelPuesto { NivelPuesto = request.descripcion, IdNivelPuesto = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Pcs

public class AgregaPcsEventHandler : IRequestHandler<AgregarPcsCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaPcsEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarPcsCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddPcs(new TB_Cat_Pcs { Activo = true, Pcs = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaPcsEventHandler : IRequestHandler<EliminarPcsCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaPcsEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarPcsCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeletePcs(new TB_Cat_Pcs { IdPcs = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaPcsEventHandler : IRequestHandler<ActualizarPcsCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaPcsEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarPcsCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdatePcs(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Pcs { Pcs = request.descripcion, IdPcs = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Prestacion

public class AgregaPrestacionEventHandler : IRequestHandler<AgregarPrestacionCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaPrestacionEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarPrestacionCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddPrestacion(new TB_Cat_Prestacion { Activo = true, Viatico = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaPrestacionEventHandler : IRequestHandler<EliminarPrestacionCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaPrestacionEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarPrestacionCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeletePrestacion(new TB_Cat_Prestacion { IdPrestacion = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaPrestacionEventHandler : IRequestHandler<ActualizarPrestacionCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaPrestacionEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarPrestacionCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdatePrestacion(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Prestacion { Viatico = request.descripcion, IdPrestacion = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Profesion
public class AgregaProfesionEventHandler : IRequestHandler<AgregarProfesionCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;
    private readonly IMapper _mapper;

    public AgregaProfesionEventHandler(ICatalogoBusiness _business, IMapper _mapper)
    {
        this._business = _business;
        this._mapper = _mapper;
    }

    public async Task<Response<bool>> Handle(AgregarProfesionCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.AddProfesion(new TB_Cat_Profesion { Activo = true, Profesion = request.descripcion });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}

public class EliminaProfesionEventHandler : IRequestHandler<EliminarProfesionCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;

    public EliminaProfesionEventHandler(ICatalogoBusiness _business)
    {
        this._business = _business;
    }

    public async Task<Response<bool>> Handle(EliminarProfesionCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.DeleteProfesion(new TB_Cat_Profesion { IdProfesion = request.id });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}

public class ActualizaProfesionEventHandler : IRequestHandler<ActualizarProfesionCommand, Response<bool>>
{
    private readonly ICatalogoBusiness _business;

    public ActualizaProfesionEventHandler(ICatalogoBusiness _business)
    {
        this._business = _business;
    }

    public async Task<Response<bool>> Handle(ActualizarProfesionCommand request, CancellationToken cancellationToken)
    {
        var resp = new Response<bool>();
        (bool Success, string Message) tmpResp = await _business.UpdateProfesion(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Profesion { Profesion = request.descripcion, IdProfesion = request.id, Activo = true });
        if (!tmpResp.Success) resp.AddError(tmpResp.Message);
        else resp.Data = tmpResp.Success;
        return resp;
    }
}
#endregion Profesion

#region Puesto

public class AgregaPuestoEventHandler : IRequestHandler<AgregarPuestoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaPuestoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarPuestoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddPuesto(new TB_Cat_Puesto { Activo = true, Puesto = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaPuestoEventHandler : IRequestHandler<EliminarPuestoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaPuestoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarPuestoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeletePuesto(new TB_Cat_Puesto { IdPuesto = request.id.ToString() });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaPuestoEventHandler : IRequestHandler<ActualizarPuestoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaPuestoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarPuestoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdatePuesto(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Puesto { Puesto = request.descripcion, IdPuesto = request.id.ToString(), Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Rubro Ingreso Reembolsable

public class AgregaRubroIngresoReembolsableEventHandler : IRequestHandler<AgregarRubroIngresoReembolsableCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaRubroIngresoReembolsableEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarRubroIngresoReembolsableCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddRubroIngresoReembolsable(new TB_Cat_RubroIngresoReembolsable { Activo = true, Rubro = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaRubroIngresoReembolsableEventHandler : IRequestHandler<EliminarRubroIngresoReembolsableCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaRubroIngresoReembolsableEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarRubroIngresoReembolsableCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteRubroIngresoReembolsable(new TB_Cat_RubroIngresoReembolsable { IdRubroIngreso = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaRubroIngresoReembolsableEventHandler : IRequestHandler<ActualizarRubroIngresoReembolsableCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaRubroIngresoReembolsableEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarRubroIngresoReembolsableCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateRubroIngresoReembolsable(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_RubroIngresoReembolsable { Rubro = request.descripcion, IdRubroIngreso = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Sector

public class AgregaSectorEventHandler : IRequestHandler<AgregarSectorCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaSectorEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarSectorCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddSector(new TB_Cat_Sector { Activo = true, Sector = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaSectorEventHandler : IRequestHandler<EliminarSectorCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaSectorEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarSectorCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteSector(new TB_Cat_Sector { IdSector = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaSectorEventHandler : IRequestHandler<ActualizarSectorCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaSectorEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarSectorCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateSector(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Sector { Sector = request.descripcion, IdSector = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Cie

public class AgregaTipoCieEventHandler : IRequestHandler<AgregarTipoCieCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoCieEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoCieCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoCie(new TB_Cat_TipoCie { Activo = true, TipoCie = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoCieEventHandler : IRequestHandler<EliminarTipoCieCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoCieEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoCieCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoCie(new TB_Cat_TipoCie { IdTipoCie = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoCieEventHandler : IRequestHandler<ActualizarTipoCieCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoCieEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoCieCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoCie(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoCie { TipoCie = request.descripcion, IdTipoCie = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Contrato

public class AgregaTipoContratoEventHandler : IRequestHandler<AgregarTipoContratoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoContratoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoContratoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoContrato(new TB_Cat_TipoContrato { Activo = true, Contrato = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoContratoEventHandler : IRequestHandler<EliminarTipoContratoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoContratoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoContratoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoContrato(new TB_Cat_TipoContrato { IdTipoContrato = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoContratoEventHandler : IRequestHandler<ActualizarTipoContratoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoContratoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoContratoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoContrato(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoContrato { Contrato = request.descripcion, IdTipoContrato = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Cta Contable

public class AgregaTipoCtaContableEventHandler : IRequestHandler<AgregarTipoCtaContableCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoCtaContableEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoCtaContableCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoCtaContable(new TB_Cat_TipoCtaContable { Activo = true, Concepto = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoCtaContableEventHandler : IRequestHandler<EliminarTipoCtaContableCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoCtaContableEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoCtaContableCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoCtaContable(new TB_Cat_TipoCtaContable { IdTipoCtaContable = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoCtaContableEventHandler : IRequestHandler<ActualizarTipoCtaContableCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoCtaContableEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoCtaContableCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoCtaContable(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoCtaContable { Concepto = request.descripcion, IdTipoCtaContable = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Cuenta

public class AgregaTipoCuentaEventHandler : IRequestHandler<AgregarTipoCuentaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoCuentaEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoCuentaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoCuenta(new TB_Cat_TipoCuenta { Activo = true, TipoCuenta = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoCuentaEventHandler : IRequestHandler<EliminarTipoCuentaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoCuentaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoCuentaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoCuenta(new TB_Cat_TipoCuenta { IdTipoCuenta = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoCuentaEventHandler : IRequestHandler<ActualizarTipoCuentaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoCuentaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoCuentaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoCuenta(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoCuenta { TipoCuenta = request.descripcion, IdTipoCuenta = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Documento

public class AgregaTipoDocumentoEventHandler : IRequestHandler<AgregarTipoDocumentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoDocumentoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoDocumentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoDocumento(new TB_Cat_TipoDocumento { Activo = true, TipoDocumento = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoDocumentoEventHandler : IRequestHandler<EliminarTipoDocumentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoDocumentoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoDocumentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoDocumento(new TB_Cat_TipoDocumento { IdTipoDocumento = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoDocumentoEventHandler : IRequestHandler<ActualizarTipoDocumentoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoDocumentoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoDocumentoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoDocumento(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoDocumento { TipoDocumento = request.descripcion, IdTipoDocumento = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Empleado

public class AgregaTipoEmpleadoEventHandler : IRequestHandler<AgregarTipoEmpleadoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoEmpleadoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoEmpleadoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoEmpleado(new TB_Cat_TipoEmpleado { Activo = true, TipoEmpleado = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoEmpleadoEventHandler : IRequestHandler<EliminarTipoEmpleadoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoEmpleadoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoEmpleadoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoEmpleado(new TB_Cat_TipoEmpleado { IdTipoEmpleado = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoEmpleadoEventHandler : IRequestHandler<ActualizarTipoEmpleadoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoEmpleadoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoEmpleadoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoEmpleado(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoEmpleado { TipoEmpleado = request.descripcion, IdTipoEmpleado = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Factura

public class AgregaTipoFacturaEventHandler : IRequestHandler<AgregarTipoFacturaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoFacturaEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoFacturaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoFactura(new TB_Cat_TipoFactura { Activo = true, TipoFactura = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoFacturaEventHandler : IRequestHandler<EliminarTipoFacturaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoFacturaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoFacturaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoFactura(new TB_Cat_TipoFactura { IdTipoFactura = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoFacturaEventHandler : IRequestHandler<ActualizarTipoFacturaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoFacturaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoFacturaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoFactura(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoFactura { TipoFactura = request.descripcion, IdTipoFactura = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Gasto

public class AgregaTipoGastoEventHandler : IRequestHandler<AgregarTipoGastoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoGastoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoGastoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoGasto(new TB_Cat_TipoGasto { Activo = true, TipoGasto = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoGastoEventHandler : IRequestHandler<EliminarTipoGastoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoGastoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoGastoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoGasto(new TB_Cat_TipoGasto { IdTipoGasto = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoGastoEventHandler : IRequestHandler<ActualizarTipoGastoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoGastoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoGastoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoGasto(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoGasto { TipoGasto = request.descripcion, IdTipoGasto = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Ingreso

public class AgregaTipoIngresoEventHandler : IRequestHandler<AgregarTipoIngresoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoIngresoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoIngresoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoIngreso(new TB_Cat_TipoIngreso { Activo = true, TipoIngreso = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoIngresoEventHandler : IRequestHandler<EliminarTipoIngresoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoIngresoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoIngresoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoIngreso(new TB_Cat_TipoIngreso { IdTipoIngreso = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoIngresoEventHandler : IRequestHandler<ActualizarTipoIngresoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoIngresoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoIngresoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoIngreso(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoIngreso { TipoIngreso = request.descripcion, IdTipoIngreso = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Pcs

public class AgregaTipoPcsEventHandler : IRequestHandler<AgregarTipoPcsCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoPcsEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoPcsCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoPcs(new TB_Cat_TipoPcs { Activo = true, TipoPcs = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoPcsEventHandler : IRequestHandler<EliminarTipoPcsCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoPcsEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoPcsCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoPcs(new TB_Cat_TipoPcs { IdTipoPcs = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoPcsEventHandler : IRequestHandler<ActualizarTipoPcsCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoPcsEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoPcsCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoPcs(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoPcs { TipoPcs = request.descripcion, IdTipoPcs = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Poliza

public class AgregaTipoPolizaEventHandler : IRequestHandler<AgregarTipoPolizaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoPolizaEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoPolizaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoPoliza(new TB_Cat_TipoPoliza { Activo = true, TipoPoliza = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoPolizaEventHandler : IRequestHandler<EliminarTipoPolizaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoPolizaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoPolizaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoPoliza(new TB_Cat_TipoPoliza { IdTipoPoliza = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoPolizaEventHandler : IRequestHandler<ActualizarTipoPolizaCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoPolizaEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoPolizaCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoPoliza(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoPoliza { TipoPoliza = request.descripcion, IdTipoPoliza = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Proyecto

public class AgregaTipoProyectoEventHandler : IRequestHandler<AgregarTipoProyectoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoProyectoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoProyectoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoProyecto(new TB_Cat_TipoProyecto { Activo = true, TipoProyecto = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoProyectoEventHandler : IRequestHandler<EliminarTipoProyectoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoProyectoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoProyectoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoProyecto(new TB_Cat_TipoProyecto { IdTipoProyecto = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoProyectoEventHandler : IRequestHandler<ActualizarTipoProyectoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoProyectoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoProyectoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoProyecto(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoProyecto { TipoProyecto = request.descripcion, IdTipoProyecto = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Resultado

public class AgregaTipoResultadoEventHandler : IRequestHandler<AgregarTipoResultadoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoResultadoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoResultadoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoResultado(new TB_Cat_TipoResultado { Activo = true, TipoResultado = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoResultadoEventHandler : IRequestHandler<EliminarTipoResultadoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoResultadoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoResultadoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoResultado(new TB_Cat_TipoResultado { IdTipoResultado = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoResultadoEventHandler : IRequestHandler<ActualizarTipoResultadoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoResultadoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoResultadoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoResultado(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoResultado { TipoResultado = request.descripcion, IdTipoResultado = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Tipo Sangre

public class AgregaTipoSangreEventHandler : IRequestHandler<AgregarTipoSangreCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaTipoSangreEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarTipoSangreCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddTipoSangre(new TB_Cat_TipoSangre { Activo = true, TipoSangre = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaTipoSangreEventHandler : IRequestHandler<EliminarTipoSangreCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaTipoSangreEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarTipoSangreCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteTipoSangre(new TB_Cat_TipoSangre { IdTipoSangre = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaTipoSangreEventHandler : IRequestHandler<ActualizarTipoSangreCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaTipoSangreEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarTipoSangreCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateTipoSangre(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_TipoSangre { TipoSangre = request.descripcion, IdTipoSangre = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Unidad Negocio

public class AgregaUnidadNegocioEventHandler : IRequestHandler<AgregarUnidadNegocioCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaUnidadNegocioEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarUnidadNegocioCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddUnidadNegocio(new TB_Cat_UnidadNegocio { Activo = true, UnidadNegocio = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaUnidadNegocioEventHandler : IRequestHandler<EliminarUnidadNegocioCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaUnidadNegocioEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarUnidadNegocioCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteUnidadNegocio(new TB_Cat_UnidadNegocio { IdUnidadNegocio = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaUnidadNegocioEventHandler : IRequestHandler<ActualizarUnidadNegocioCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaUnidadNegocioEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarUnidadNegocioCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateUnidadNegocio(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_UnidadNegocio { UnidadNegocio = request.descripcion, IdUnidadNegocio = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

#region Viatico

public class AgregaViaticoEventHandler : IRequestHandler<AgregarViaticoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;
	private readonly IMapper _mapper;

	public AgregaViaticoEventHandler(ICatalogoBusiness _business, IMapper _mapper)
	{
		this._business = _business;
		this._mapper = _mapper;
	}

	public async Task<Response<bool>> Handle(AgregarViaticoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.AddViatico(new TB_Cat_Viatico { Activo = true, Viatico = request.descripcion });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class EliminaViaticoEventHandler : IRequestHandler<EliminarViaticoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public EliminaViaticoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(EliminarViaticoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.DeleteViatico(new TB_Cat_Viatico { IdViatico = request.id });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

public class ActualizaViaticoEventHandler : IRequestHandler<ActualizarViaticoCommand, Response<bool>>
{
	private readonly ICatalogoBusiness _business;

	public ActualizaViaticoEventHandler(ICatalogoBusiness _business)
	{
		this._business = _business;
	}

	public async Task<Response<bool>> Handle(ActualizarViaticoCommand request, CancellationToken cancellationToken)
	{
		var resp = new Response<bool>();
		(bool Success, string Message) tmpResp = await _business.UpdateViatico(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new TB_Cat_Viatico { Viatico = request.descripcion, IdViatico = request.id, Activo = true });
		if (!tmpResp.Success) resp.AddError(tmpResp.Message);
		else resp.Data = tmpResp.Success;
		return resp;
	}
}

#endregion

