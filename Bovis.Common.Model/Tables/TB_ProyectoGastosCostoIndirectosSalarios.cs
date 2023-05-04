using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto_gastos_costo_indirectos_salarios")]
	public class TB_ProyectoGastosCostoIndirectosSalarios
	{
		[Column("NunumProyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Nuanio"), NotNull] public short Anio { get; set; }
		[Column("Boreal"), NotNull] public bool Real { get; set; }
		[Column("NukidcostoIndirecto"), NotNull] public int IdCostoIndirecto { get; set; }
		[Column("Nuunidad"), NotNull] public byte Unidad { get; set; }
		[Column("Nucantidad"), NotNull] public byte Cantidad { get; set; }
		[Column("Boreembolsable"), NotNull] public bool Reembolsable { get; set; }
		[Column("Nuenero"), Nullable] public decimal? Enero { get; set; }
		[Column("Nufebrero"), Nullable] public decimal? Febrero { get; set; }
		[Column("Numarzo"), Nullable] public decimal? Marzo { get; set; }
		[Column("Nuabril"), Nullable] public decimal? Abril { get; set; }
		[Column("Numayo"), Nullable] public decimal? Mayo { get; set; }
		[Column("Nujunio"), Nullable] public decimal? Junio { get; set; }
		[Column("Nujulio"), Nullable] public decimal? Julio { get; set; }
		[Column("Nuagosto"), Nullable] public decimal? Agosto { get; set; }
		[Column("Nuoctubre"), Nullable] public decimal? Octubre { get; set; }
		[Column("Nunoviembre"), Nullable] public decimal? Noviembre { get; set; }
		[Column("Nudiciembre"), Nullable] public decimal? Diciembre { get; set; }
		[Column("Nusubtotal"), Nullable] public decimal? Subtotal { get; set; }
	}
}
