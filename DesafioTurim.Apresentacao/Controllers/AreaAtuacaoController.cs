using DesafioTurim.Dominio.Repositorios;
using DesafioTurim.Apresentacao.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DesafioTurim.Dominio.Dominio;
using DesafioTurim.InfraEstrutura.Data;
using DesafioTurim.Dominio.Excepition;
using System;

namespace DesafioTurim.Apresentacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaAtuacaoController:ControllerBase
    {
        
        /// <summary>
        /// ListarAerasdeAtuacao - Listas todas as áreas de atuação 
        /// </summary>
        [HttpGet("ListarAerasdeAtuacao")]
        public IActionResult ListarAerasdeAtuacao()
        {
           
           try
            {
                return new JsonResult(AreaAtuacaoModel.ModelToEntidadeList(new AreaAtuacaoDominio(new AreaAtuacaoDAL()).ListarAreasdeAtuacao().Result));
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
        /// BuscaAerasdeAtuacaoPorId - Busca área de atuação por id
        /// </summary>
        [HttpGet("BuscaAerasdeAtuacaoPorId/{id}")]
        public IActionResult BuscaAerasdeAtuacaoPorId(string id)
        {
            try
            {
               return new JsonResult(AreaAtuacaoModel.EntidadeToModel(new AreaAtuacaoDominio(new AreaAtuacaoDAL()).BuscaAerasdeAtuacaoPorId(id).Result));
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