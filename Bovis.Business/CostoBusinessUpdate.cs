using AutoMapper;
using Bovis.Common.Mapper;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;


namespace Bovis.Business
{
    public static class CostoBusinessUpdate
    {
        public class CostoLaboral
        {
            public decimal? CostoMensualEmpleado { get; set; }
            public decimal? CostoMensualProyecto { get; set; }
            public decimal? CostoAnualEmpleado { get; set; }
            public decimal? IndiceCostoLaboral { get; set; } // CostoMensualEmpleado/SueldoBruto
            public decimal? IndiceCargaLaboral { get; set; } // CostoMensualEmpleado/SueldoNeto

        }

        public static TB_CostoPorEmpleado ValueFields(CostoPorEmpleadoDTO source)
        {
            
            TB_CostoPorEmpleado destination = new();

            destination = MapToTbCostoEmpleado<NotNullMappingProfile>(source, destination); 
             

            #region Antiguedad
            if (destination.FechaIngreso == null)
            {
                destination.FechaIngreso = DateTime.Now; 
            }
            destination.NuMes = DateTime.Now.Month; 
            destination.NuAnno = DateTime.Now.Year;

            //destination.Antiguedad = Convert.ToDecimal(((DateTime.Now - destination.FechaIngreso).Days) / 365);
            TimeSpan diferencia = (TimeSpan) (DateTime.Now - destination.FechaIngreso); 
            destination.Antiguedad = diferencia.Days / 365;
            #endregion

            #region DescuentoMensualEmpleado
            destination.MontoDescuentoMensual = destination.RetencionImss + destination.Ispt;
            #endregion

            if (destination.Antiguedad != 0)
            {
                if (destination.Antiguedad < 5)
                {
                    destination.AguinaldoCantMeses = 0.5M;
                }
                else
                    destination.AguinaldoCantMeses = 1.0M;
            }

            if (destination.SueldoBruto.HasValue && destination.SueldoBruto > 1)
            {
                destination.AvgDescuentoEmpleado = destination.MontoDescuentoMensual / destination.SueldoBruto;
                destination.SueldoNetoPercibidoMensual = destination.SueldoBruto + CostoBusinessConstants.BonoAdicionalReubicacion + CostoBusinessConstants.ViaticosAComprobar - destination.MontoDescuentoMensual;
                destination.Anual = destination.SueldoBruto * 12;
                destination.AguinaldoMontoProvisionMensual = destination.AguinaldoCantMeses * destination.SueldoBruto / 30M;
                destination.IndemProvisionMensual = destination.SueldoBruto / 30M * 20M / 12M;
                destination.PtuProvision = destination.SueldoBruto * 1116000M / 8053945M / 12M;

            }

           

            if (destination.SueldoBruto == 0)
            {
                destination.PvDiasVacasAnuales = 0;
            }
            else
            {
                if (Math.Round(destination.Antiguedad ?? 0.0M) > 4.15M)
                {
                    destination.PvDiasVacasAnuales = 20;
                }
                else if (Math.Round(destination.Antiguedad ?? 0.0M) == 4.0M)
                {
                    destination.PvDiasVacasAnuales = 18;
                }
                else if (Math.Round(destination.Antiguedad ?? 0.0M) == 3.0M)
                {
                    destination.PvDiasVacasAnuales = 16;
                }
                else if (Math.Round(destination.Antiguedad ?? 0.0M) == 2.0M)
                {
                    destination.PvDiasVacasAnuales = 14;
                }
                else
                    destination.PvDiasVacasAnuales = 12;
            }

            if (destination.SueldoBruto > 0)
            {
                destination.PvProvisionMensual = destination.SueldoBruto / 30 * destination.PvDiasVacasAnuales * 0.25M / 12;
            }
            else
                destination.PvProvisionMensual = 0.0M;

            if (destination.SgmmCostoTotalAnual.HasValue)
            {
                destination.SgmmCostoMensual = destination.SgmmCostoTotalAnual / 12.0M;
            }

            if (destination.SvCostoTotalAnual.HasValue && destination.SvCostoTotalAnual != 0)
            {
                destination.SvCostoMensual = destination.SvCostoTotalAnual / 12.0M;
            }
            else
                destination.SvCostoMensual = 0.0M;

            if (destination.VaidCostoMensual.HasValue && destination.VaidCostoMensual != 0)
            {
                destination.VaidComisionCostoMensual = destination.VaidCostoMensual * 0.015M;
            }
            else
                destination.VaidComisionCostoMensual = 0.0M;

            destination.Impuesto3sNomina = destination.SueldoBruto + destination.AguinaldoMontoProvisionMensual + destination.PvProvisionMensual + CostoBusinessConstants.Be_BonoAdicional + CostoBusinessConstants.Be_AyudaTransporte + destination.BonoAnualProvisionMensual * 0.03M;

            destination.CargasSociales = destination.Impuesto3sNomina + destination.RetencionImss + destination.Retiro2 + destination.CesantesVejez + destination.Infonavit;

            CostoLaboral costoLab = CostoTotalLaboral(destination);
            destination.CostoMensualEmpleado = costoLab.CostoMensualEmpleado;
            destination.CostoAnualEmpleado = costoLab.CostoAnualEmpleado;
            destination.IndiceCostoLaboral = costoLab.IndiceCostoLaboral;
            destination.IndiceCargaLaboral = costoLab.IndiceCargaLaboral;

            destination.FechaActualizacion = DateTime.Now;
            destination.RegHistorico = false; 

            return destination;

        } 

        private static CostoLaboral CostoTotalLaboral(TB_CostoPorEmpleado costo)
        {
            CostoLaboral costoLab = new();
            costoLab.CostoMensualEmpleado = costo.SueldoBruto + costo.AguinaldoMontoProvisionMensual + costo.PvProvisionMensual + costo.IndemProvisionMensual + costo.BonoAnualProvisionMensual + costo.SgmmCostoMensual + costo.SvCostoMensual + costo.VaidCostoMensual + costo.VaidComisionCostoMensual + costo.PtuProvision + costo.CargasSociales;

            costoLab.CostoMensualProyecto = 0.0M;
            costoLab.CostoAnualEmpleado = costoLab.CostoMensualEmpleado * 12;
            costoLab.IndiceCostoLaboral = costoLab.CostoMensualEmpleado / costo.SueldoBruto;
            costoLab.IndiceCargaLaboral = costoLab.CostoMensualEmpleado / costo.SueldoNetoPercibidoMensual;

            return costoLab; 
        }
        
        private static TB_CostoPorEmpleado MapToTbCostoEmpleado<TProfile>(CostoPorEmpleadoDTO source, TB_CostoPorEmpleado destination) where TProfile : Profile, new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile>();
            });

            IMapper mapper = config.CreateMapper(); 
            destination = mapper.Map(source, destination);

            return destination;
            
        }
        
    }
}
