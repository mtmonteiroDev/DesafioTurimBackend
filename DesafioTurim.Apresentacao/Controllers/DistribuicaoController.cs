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
    public class DistribuicaoController:ControllerBase
    {
        private readonly IAreaAtuacaoRepositorio _areasAtuacao;
        public DistribuicaoController(IAreaAtuacaoRepositorio areasAtuacao)
        {
            _areasAtuacao = areasAtuacao;
        }
       
        /// <summary>
        /// AtualizarValorDistribuicao - Atualiza o valor de distribuição para os funcionarios 
        /// </summary>
        [HttpPut("AtualizarValorDistribuicao")]
        public IActionResult AtualizarValorDistribuicao([FromBody] DistribuicaoModel distribuicaoModel)
        {
            try
            {
                new DistribuicaoDominio(new DistribuicaoDAL()).AtualizaValorDistrinuicao(DistribuicaoModel.ModelToEntidade(distribuicaoModel));
                return Ok();
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
        /// BuscarValorParaDistribuicao - Retorna o valor para efetuar a distribuição entre os funcionarios
        /// </summary>
        [HttpGet("BuscarValorParaDistribuicao")]
        public IActionResult  BuscarValorParaDistribuicao()
        {
            try
            {
               return new JsonResult(DistribuicaoModel.ModelToEntidadeList(new DistribuicaoDominio(new DistribuicaoDAL()).BuscarValorDistribuicao().Result));
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