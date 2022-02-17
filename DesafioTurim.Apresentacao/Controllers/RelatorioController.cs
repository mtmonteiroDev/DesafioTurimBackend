using DesafioTurim.Dominio.Repositorios;
using DesafioTurim.Apresentacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioTurim.Dominio.Dominio;
using DesafioTurim.InfraEstrutura.Data;
using DesafioTurim.Dominio.Excepition;
using System;

namespace DesafioTurim.Apresentacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        /// <summary>
        /// ListarDistribuicaoDeLucroPorFuncionario - Recupera quanto cada colaborador irá receber
        /// </summary>
        [HttpGet("ListarDistribuicaoDeLucroPorFuncionario")]
        public IActionResult ListarDistribuicaoDeLucroPorFuncionario()
        {
           try
            {
                return new JsonResult(new RelatorioDominio(new RelatorioDAL()).ListarDistribuicaoDeLucroPorFuncionario().Result);
            }
            catch (DominioException e) 
            { 
                HttpContext.Response.StatusCode = 400;
                return new JsonResult(new { erro = e.Message }); 
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonResult(new { erro = "Ops! Ocorreu um erro no servidor." });
            }
        }    
        /// <summary>
        /// ListarFuncionariosCadastratos - Recupera o total de funcionários 
        /// </summary>
        [HttpGet("ListarFuncionariosCadastratos")]
        public IActionResult ListarFuncionariosCadastratos()
        {
           try
            {
                return new JsonResult(new{QuantidadeDeFuncionarios = new RelatorioDominio(new RelatorioDAL()).ListarFuncionariosCadastratos().Result.Count});
            }
            catch (DominioException e) 
            { 
                HttpContext.Response.StatusCode = 400;
                return new JsonResult(new { erro = e.Message }); 
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonResult(new { erro = "Ops! Ocorreu um erro no servidor." });
            }
        }   
        /// <summary>
        /// RetornaTotalDistribuido - Recupera o total distribuído 
        /// </summary>
        [HttpGet("RetornaTotalDistribuido")]
        public IActionResult RetornaTotalDistribuido()
        {
           try
            {
                return new JsonResult(new{TotalDistribuido = new RelatorioDominio(new RelatorioDAL()).RetornaTotalDistribuido().Result});
            }
            catch (DominioException e) 
            { 
                HttpContext.Response.StatusCode = 400;
                return new JsonResult(new { erro = e.Message }); 
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonResult(new { erro = "Ops! Ocorreu um erro no servidor." });
            }
        }   
        /// <summary>
        /// RetornaTotalDisPonibilizado - Recupera o total  disponibilizado 
        /// </summary>
        [HttpGet("RetornaTotalDisPonibilizado")]
        public IActionResult RetornaTotalDisPonibilizado()
        {
           try
            {
                return new JsonResult(new{TotalDisponibilizadoParaDistribucao = new RelatorioDominio(new RelatorioDAL()).RetornaTotalDisPonibilizado().Result});
            }
            catch (DominioException e) 
            { 
                HttpContext.Response.StatusCode = 400;
                return new JsonResult(new { erro = e.Message }); 
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonResult(new { erro = "Ops! Ocorreu um erro no servidor." });
            }
        }          
        /// <summary>
        /// RetornaSaldoDisponibilizadoDistribuido - Recupera o saldo entre disponibilizado e disponível
        /// </summary>
        [HttpGet("RetornaSaldoDisponibilizadoDistribuido")]
        public IActionResult RetornaSaldoDisponibilizadoDistribuido()
        {
           try
            {
                return new JsonResult(new{saldoDisponibilizadoDistribuido = new RelatorioDominio(new RelatorioDAL()).RetornaSaldoDisponibilizadoDistribuido().Result});
            }
            catch (DominioException e) 
            { 
                HttpContext.Response.StatusCode = 400;
                return new JsonResult(new { erro = e.Message }); 
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
                return new JsonResult(new { erro = "Ops! Ocorreu um erro no servidor." });
            }
        }          
    }
}