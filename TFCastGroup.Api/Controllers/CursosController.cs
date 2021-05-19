using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using TFCastGroup.Dto;
using TFCastGroup.Service;
using TFCastGroup.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TFCastGroup.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }


        // GET: api/<CursosController>
        /// <summary>
        /// Obter todos os cursos
        /// </summary>
        /// <remarks>
        /// 
        /// 
        ///     Obtém todos os cursos cadastrados.
        ///     
        /// </remarks>
        [HttpGet]
        [Route("GetAllCursos")]
        public async Task<IEnumerable<DtoCurso>> GetAllCursos()
        {
            return await _cursoService.GetAll();
        }

        // GET api/<CursosController>/5
        /// <summary>
        /// Obtém curso por id
        /// </summary>
        /// <remarks>
        /// 
        /// 
        ///     Obtém o curso por id. Caso você não tenha o id consulte o endpoint GetAllCursos
        ///     
        /// </remarks>
        [HttpGet]
        [Route("GetByIdCurso")]
        public async Task<DtoCurso> GetByIdCurso([FromQuery] int idCurso)
        {
            return await _cursoService.GetCursoById(idCurso);
        }

        // POST api/<CursosController>
        /// <summary>
        /// Cadastrar curso
        /// </summary>
        /// <remarks>
        /// 
        ///  Cadastra curso de acordo com as seguintes regras
        ///     Não é permitido a inclusão de cursos dentro do mesmo período.
        ///     Não será permitido a inclusão de cursos com a data de início menor que a data atual
        /// </remarks>
        [HttpPost]
        [Route("Cadastrar")]
        public async Task<ActionResult> Cadastrar([FromBody] DtoCurso dtoCursoRequest)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid) return BadRequest(allErrors);



            if (dtoCursoRequest.DataInicio < DateTime.Now)
                return BadRequest("Data início menor que a data atual.");

            var dtoCurso = await _cursoService.Cadastrar(dtoCursoRequest);
            return Ok(dtoCurso);

        }
        /// <summary>
        /// Editar um Curso
        /// </summary>
        /// <remarks>
        /// Edita um curso já cadastrado
        /// 
        ///     A edição do curso segue a regra do endpoint Cadastrar.
        ///     
        /// </remarks>
        // PUT api/<CursosController>/5
        [HttpPut]
        [Route("EditarCurso")]
        public async Task<ActionResult> EditarCurso([FromBody] DtoCurso dtoCurso)
        {
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid) return BadRequest(allErrors);


            if (dtoCurso == null) return BadRequest(new { Mensagem = "Objeto nulo" });
            if (dtoCurso.DataInicio < DateTime.Now) return BadRequest(new { Mensagem = "Data deve ser maior que hoje." });
            var update = _cursoService.UpdateCurso(dtoCurso).Result;

            return Ok(update);
        }

        // DELETE api/<CursosController>/5
        /// <summary>
        /// Deletar um Curso
        /// </summary>
        /// <remarks>
        /// Para deletar um curso passe o Id como parâmetro. Caso você não tenha o id consulte o endpoint GetAllCursos
        /// 
        ///     Assim que o endpoint DeleteCurso for executado essa operação não terá como ser desfeita.
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("DeleteCurso")]
        public async Task<ActionResult> DeleteCurso([FromQuery] int id)
        {
            if (id == 0) return BadRequest(new { Mensagem = "id deve ser diferente de zero." });

            var delete = await _cursoService.DeleteCurso(id);

            return Ok(delete);

        }
    }
}
