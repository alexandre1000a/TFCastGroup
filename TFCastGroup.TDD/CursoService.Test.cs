using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TFCastGroup.Domain.Interface;
using TFCastGroup.Domain.Model;
using TFCastGroup.Dto;
using TFCastGroup.Infra.Repository;
using TFCastGroup.Service;
using TFCastGroup.Service.Interface;
using TFCastGroup.TDD.Builder;

namespace TFCastGroup.TDD
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeveValidarSeDataInicioEhMenorQueDataAtual()
        {
            Mock<ICursoRepository> mockCursoRepository = new Mock<ICursoRepository>();
            Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
            Mock<ICursoService> cursoService = new Mock<ICursoService>();

            CursoTestBuilder cursoBuilder = new CursoTestBuilder();

            CursoService cursoService1 = new CursoService(mockCursoRepository.Object, mockCategoriaRepository.Object);

            var cursoResult = cursoService1.Cadastrar(cursoBuilder.ComDatas(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(2)).ComCategoria(1).Build()).Result;
            Assert.AreEqual(cursoResult.Message, "Data início menor que a data atual.");

        }

        [Test]
        public void DeveValidarSeTemUmCursoPeriodoJaCadastradoParaOCurso()
        {
            CursoTestBuilder cursoBuilder = new CursoTestBuilder();

            Mock<ICursoRepository> mockCursoRepository = new Mock<ICursoRepository>();
            Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
            Mock<ICursoService> mockCursoService = new Mock<ICursoService>();

            mockCursoRepository.Setup(x => x.VerificaCursoPorPeriodo(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, false)).Returns(false);

            CursoService cursoService = new CursoService(mockCursoRepository.Object, mockCategoriaRepository.Object);

            var cursoResult = cursoService.Cadastrar(cursoBuilder.ComDatas(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3)).ComCategoria(1).Build()).Result;
            Assert.AreEqual(cursoResult.Message, "Existe(m) curso(s) planejados(s) dentro do período informado.");

        }


        [Test]
        public void DeveRetornarCategoriaNaoExiste()
        {
            CursoTestBuilder cursoBuilder = new CursoTestBuilder();

            Mock<ICursoRepository> mockCursoRepository = new Mock<ICursoRepository>();
            Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
            Mock<ICursoService> mockCursoService = new Mock<ICursoService>();

            mockCursoRepository.Setup(x => x.VerificaCursoPorPeriodo(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, false)).Returns(true);

            CursoService cursoService = new CursoService(mockCursoRepository.Object, mockCategoriaRepository.Object);

            var cursoResult = cursoService.Cadastrar(cursoBuilder.ComDatas(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3)).ComCategoria(1).Build()).Result;
            Assert.AreEqual(cursoResult.Message, "Categora não existe.");

        }


        [Test]
        public void DeveRetornarCursoCadastradoComSucesso()
        {
            CursoTestBuilder cursoBuilder = new CursoTestBuilder();
            Categoria categoria = new Categoria() { Codigo = 1, Descricao = "Programacao" };

            Mock<ICursoRepository> mockCursoRepository = new Mock<ICursoRepository>();
            Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
            Mock<ICursoService> mockCursoService = new Mock<ICursoService>();

            mockCursoRepository.Setup(x => x.VerificaCursoPorPeriodo(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null, false)).Returns(true);            

            mockCategoriaRepository.Setup(p => p.GetEntityById(It.IsAny<long>())).Returns(Task.FromResult(categoria));

            CursoService cursoService = new CursoService(mockCursoRepository.Object, mockCategoriaRepository.Object);

            var cursoResult = cursoService.Cadastrar(cursoBuilder.ComDatas(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3)).ComCategoria(1).Build()).Result;
            Assert.AreEqual(cursoResult.Message, "Curso cadastrado com sucesso.");

        }



    }
}
