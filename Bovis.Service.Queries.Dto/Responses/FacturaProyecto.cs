﻿using Bovis.Common.Model.Tables;

namespace Bovis.Service.Queries.Dto.Responses
{
    public class FacturaProyecto
    {
        public int NumProyecto { get; set; }
        public string Nombre { get; set; }
        public string RfcBaseEmisor { get; set;}
        public List<string> RfcBaseReceptor { get; set;}

    }
    public class DetallesFactura
    {
        public int Id { get; set; } 
        public string Uuid { get; set; }
        public int NumProyecto { get; set; }
        public string IdTipoFactura { get; set; }
        public string IdMoneda { get; set; }
        public decimal Importe { get; set; }
        public decimal? Iva { get; set; }
        public decimal? IvaRet { get; set; }
        public decimal? Total { get; set; }
        public string Concepto { get; set; }
        public byte? Mes { get; set; }
        public short Anio { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string? NoFactura { get; set; }
        public decimal? TipoCambio { get; set; }
        public string? MotivoCancelacion { get; set; }
        public List<NotaDetalle> Notas { get; set; }
        public List<CobranzaDetalle> Cobranzas { get; set; }
        public int TotalNotasCredito { set; get; }
        public int TotalCobranzas { get; set; }
        ////nota credito
        //public string? NC_UuidNotaCredito { set; get; }
        //public string? NC_IdMoneda { set; get; }
        //public string? NC_IdTipoRelacion { set; get; }
        //public string? NC_NotaCredito { set; get; }
        //public decimal? NC_Importe { set; get; }
        //public decimal? NC_Iva { set; get; }
        //public decimal? NC_Total { set; get; }
        //public string? NC_Concepto { set; get; }
        //public byte? NC_Mes { set; get; }
        //public short NC_Anio { set; get; }
        //public decimal? NC_TipoCambio { set; get; }
        //public DateTime? NC_FechaNotaCredito { set; get; }
        ////cobranza
        //public string? C_UuidCobranza { set; get; }
        //public string? C_IdMonedaP { set; get; }
        //public decimal? C_ImportePagado { set; get; }
        //public decimal? C_ImpSaldoAnt { set; get; }
        //public decimal? C_ImporteSaldoInsoluto { set; get; }
        //public decimal? C_IvaP { set; get; }
        //public decimal? C_TipoCambioP { set; get; }
        //public DateTime? C_FechaPago { set; get; }
    }

    public class NotaDetalle
    {
        public string? NC_UuidNotaCredito { set; get; }
        public string? NC_IdMoneda { set; get; }
        public string? NC_IdTipoRelacion { set; get; }
        public string? NC_NotaCredito { set; get; }
        public decimal? NC_Importe { set; get; }
        public decimal? NC_Iva { set; get; }
        public decimal? NC_Total { set; get; }
        public string? NC_Concepto { set; get; }
        public byte? NC_Mes { set; get; }
        public short NC_Anio { set; get; }
        public decimal? NC_TipoCambio { set; get; }
        public DateTime? NC_FechaNotaCredito { set; get; }
    }

    public class CobranzaDetalle
    {
        public string? C_UuidCobranza { set; get; }
        public string? C_IdMonedaP { set; get; }
        public decimal? C_ImportePagado { set; get; }
        public decimal? C_ImpSaldoAnt { set; get; }
        public decimal? C_ImporteSaldoInsoluto { set; get; }
        public decimal? C_IvaP { set; get; }
        public decimal? C_TipoCambioP { set; get; }
        public DateTime? C_FechaPago { set; get; }
    }
}
