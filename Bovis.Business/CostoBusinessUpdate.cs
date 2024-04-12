using AutoMapper;
using Bovis.Common.Mapper;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;


namespace Bovis.Business
{
    public static class CostoBusinessUpdate
    {
        public class CostoLaboral
        {
            public decimal? CostoMensualEmpleado { get; set; }
            public decimal? CostoMensualProyecto { get; set; }
            public decimal? CostoAnualEmpleado { get; set; }
            public decimal? CostoSalarioBruto { get; set; } // CostoMensualEmpleado/SueldoBruto
            public decimal? CostoSalarioNeto { get; set; } // CostoMensualEmpleado/SueldoNeto

        }

        public static TB_CostoPorEmpleado ValueFields(CostoPorEmpleadoDTO source)
        {            

            TB_CostoPorEmpleado destination = new();

            destination = MapToTbCostoEmpleado<NotNullMappingProfile>(source, destination); 
             

            #region Seniority
            if (destination.FechaIngreso == null || destination.FechaIngreso == DateTime.MinValue)
            {
                DateTime fechaHoraActual = DateTime.Now;
                string fechaHoraFormateada = fechaHoraActual.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                DateTime fechaHoraFormateadaDateTime = DateTime.ParseExact(fechaHoraFormateada, "yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                destination.FechaIngreso = fechaHoraFormateadaDateTime; 
            }
            destination.NuMes = DateTime.Now.Month; 
            destination.NuAnno = DateTime.Now.Year;
            //destination.Antiguedad = Convert.ToDecimal(((DateTime.Now - destination.FechaIngreso).Days) / 365);
            TimeSpan diferencia = (TimeSpan) (DateTime.Now - destination.FechaIngreso); 
            destination.Antiguedad = diferencia.Days / 365;
            #endregion Seniority

            #region Aguinaldo
            if (destination.Antiguedad != 0)
            {
                if (destination.Antiguedad < 5)
                {
                    destination.AguinaldoCantMeses = 0.5M;
                }
                else
                    destination.AguinaldoCantMeses = 1.0M;
            }
            #endregion Aguinaldo

            #region IMSS
            //destination.RetencionImss = 
            #endregion IMSS

            if (destination.SueldoBruto.HasValue && destination.SueldoBruto > 1)
            {
                #region Sueldo Neto Mensual
                destination.MontoDescuentoMensual = destination.RetencionImss + destination.Ispt;
                destination.AvgDescuentoEmpleado = destination.SueldoBruto > 0 ? destination.MontoDescuentoMensual / destination.SueldoBruto : 0;
                destination.SueldoNetoPercibidoMensual = (destination.SueldoBruto + CostoBusinessConstants.BonoAdicionalReubicacion / CostoBusinessConstants.ViaticosAComprobar) - destination.MontoDescuentoMensual;
                #endregion Sueldo Neto Mensual

                destination.Anual = destination.SueldoBruto * 12;

                #region Aguinaldo
                destination.AguinaldoMontoProvisionMensual = destination.AguinaldoCantMeses * destination.SueldoBruto / 30M;
                #endregion Aguinaldo

                #region Indemnizacion
                destination.IndemProvisionMensual = ((destination.SueldoBruto / 30M) * 20M) / 12M;
                #endregion Indemnizacion

                #region PTU
                destination.PtuProvision = destination.SueldoBruto * 1116000M / 8053945M / 12M;
                #endregion PTU
            }
            else
            {
                #region Sueldo Neto Mensual
                destination.AvgDescuentoEmpleado = 0;
                #endregion Sueldo Neto Mensual
            }


            #region Prima Vacacional
            if (destination.SueldoBruto == 0)
            {
                destination.PvDiasVacasAnuales = 0;
            }
            else
            {
                switch (destination.Antiguedad)
                {
                    case 1:
                        destination.PvDiasVacasAnuales = 12;
                        break;
                    case 2:
                        destination.PvDiasVacasAnuales = 14;
                        break;
                    case 3:
                        destination.PvDiasVacasAnuales = 16;
                        break;
                    case 4:
                        destination.PvDiasVacasAnuales = 18;
                        break;
                    case 5:
                        destination.PvDiasVacasAnuales = 20;
                        break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        destination.PvDiasVacasAnuales = 22;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        destination.PvDiasVacasAnuales = 24;
                        break;
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                        destination.PvDiasVacasAnuales = 26;
                        break;
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                        destination.PvDiasVacasAnuales = 28;
                        break;
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                        destination.PvDiasVacasAnuales = 30;
                        break;
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                        destination.PvDiasVacasAnuales = 32;
                        break;
                }
            }

            if (destination.SueldoBruto > 0)
            {
                destination.PvProvisionMensual = destination.PvDiasVacasAnuales > 0 ? (destination.SueldoBruto / 30 * destination.PvDiasVacasAnuales * 0.25M / 12) * 0.75M : 0;
            }
            else
                destination.PvProvisionMensual = 0.0M;
            #endregion Prima Vacacional

            #region Gastos Médicos Mayores
            if (destination.SgmmCostoTotalAnual.HasValue)
            {
                destination.SgmmCostoMensual = destination.SgmmCostoTotalAnual / 12.0M;
            }
            #endregion Gastos Médicos Mayores

            #region Seguro de Vida
            if (destination.SvCostoTotalAnual.HasValue && destination.SvCostoTotalAnual != 0)
            {
                destination.SvCostoMensual = destination.SvCostoTotalAnual / 12.0M;
            }
            else
                destination.SvCostoMensual = 0.0M;
            #endregion Seguro de Vida

            #region Vales de Despensa
            if (destination.VaidCostoMensual.HasValue && destination.VaidCostoMensual != 0)
            {
                destination.VaidComisionCostoMensual = destination.VaidCostoMensual * 0.015M;
            }
            else
                destination.VaidComisionCostoMensual = 0.0M;
            #endregion Vales de Despensa

            #region Cargas sociales e impuestos laborales
            destination.Impuesto3sNomina = (destination.SueldoBruto + destination.AguinaldoMontoProvisionMensual + destination.PvProvisionMensual + CostoBusinessConstants.Be_BonoAdicional + CostoBusinessConstants.Be_AyudaTransporte + destination.BonoAnualProvisionMensual + CostoBusinessConstants.BonoAdicionalReubicacion) * source.ImpuestoNomina; // * 0.03M;
            destination.CargasSociales = destination.Impuesto3sNomina + destination.RetencionImss + destination.Retiro2 + destination.CesantesVejez + destination.Infonavit;
            #endregion Cargas sociales e impuestos laborales

            #region Costo total laboral BLL
            destination.CostoMensualEmpleado = destination.SueldoBruto + destination.AguinaldoMontoProvisionMensual + destination.PvProvisionMensual + destination.IndemProvisionMensual + destination.BonoAnualProvisionMensual + destination.SgmmCostoMensual + destination.SvCostoMensual + destination.VaidCostoMensual + destination.VaidComisionCostoMensual + destination.PtuProvision + destination.CargasSociales;
            destination.CostoMensualProyecto = 0.0M;
            destination.CostoAnualEmpleado = destination.CostoMensualEmpleado * 12;
            destination.CostoSalarioBruto = destination.SueldoBruto > 0 ? destination.CostoMensualEmpleado / destination.SueldoBruto : 0;
            destination.CostoSalarioNeto = destination.SueldoNetoPercibidoMensual > 0 ? destination.CostoMensualEmpleado / destination.SueldoNetoPercibidoMensual : 0;
            #endregion Costo total laboral BLL


            destination.FechaActualizacion = DateTime.Now;
            destination.RegHistorico = false;


            return destination;

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
