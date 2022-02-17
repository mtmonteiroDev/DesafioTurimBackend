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
    public class FuncionarioController:ControllerBase
    {
        /// <summary>
        /// ListarFuncionarios - Recupera todos os registros dos funcionários
        /// </summary>
        [HttpGet("ListarFuncionarios")]
        public IActionResult ListarFuncionarios()
        {
           try
            {
                return new JsonResult(FuncionarioModel.ModelToEntidadeList(new FuncionarioDominio(new FuncionarioDAL()).ListarFuncionariosRepositorio().Result));
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
        /// BuscarFuncionario - Recupera um registro especício do funcionário, recebendo como parametro o id 
        /// </summary>
        [HttpGet("BuscarFuncionario/{id}")]
        public IActionResult BuscarFuncionario(string id)
        {
           try
            {
               return new JsonResult(FuncionarioModel.EntidadeToModel(new FuncionarioDominio(new FuncionarioDAL()).BuscarFuncionarioPorId(id).Result));
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
        /// IncluirFuncionario - Insere um novo registro de funcionário
        /// </summary>
        [HttpPost("IncluirFuncionario")]
        public IActionResult IncluirFuncionario([FromBody] FuncionarioModel funcionarioModel)
        {
            try
            {
                var funcionarioDAL = new FuncionarioDAL();
                var areaAtuacaoDAL = new AreaAtuacaoDAL();
                var funcionarioDominio = new FuncionarioDominio(funcionarioDAL, areaAtuacaoDAL);
                var retorno=funcionarioDominio.InserirFuncionario(FuncionarioModel.ModelToEntidade(funcionarioModel));
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
        /// AlterarFuncionario - Altera o registro de um funcionário
        /// </summary>
        [HttpPut("AlterarFuncionario")]
        public IActionResult AlterarFuncionario([FromBody] FuncionarioModel funcionarioModel)
        {
            try
            {
                var funcionarioDAL = new FuncionarioDAL();
                var areaAtuacaoDAL = new AreaAtuacaoDAL();
                var funcionarioDominio = new FuncionarioDominio(funcionarioDAL, areaAtuacaoDAL);
                var retorno=funcionarioDominio.AlterarFuncionario(FuncionarioModel.ModelToEntidade(funcionarioModel));
                

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
        /// ExcluirFuncionario - Exclui um registro especício do funcionário, recebendo como parametro o id 
        /// </summary>
        [HttpDelete("ExcluirFuncionario/{id}")]
        public IActionResult ExcluirFuncionario(string id)
        {
            try
            {
                var retorno = new FuncionarioDominio(new FuncionarioDAL()).ExcluirFuncionario(id);
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

        [HttpPut("CalculaDistribuicaoDeLucros")]
        public IActionResult  CalculaDistribuicaoDeLucros()
        {
            try
            {
                var funcionarioDAL = new FuncionarioDAL();
                var funcionarioDominio = new FuncionarioDominio(funcionarioDAL);
                funcionarioDominio.calculaDivisaoLucros();
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
       
    }
    
}