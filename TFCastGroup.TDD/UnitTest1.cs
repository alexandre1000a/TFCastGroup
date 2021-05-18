using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TFCastGroup.Domain.Model;
using TFCastGroup.Dto;
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
        public void DeveCadastrarUmCurso()
        {
            var cursoBuilder = new CursoTestBuilder();
            DtoResult<DtoCurso> cursoEntity = null;
            Mock<ICursoService> cursoService = new Mock<ICursoService>();
            var mock = new Mock<DtoResult<DtoCurso>>();
            var curso = cursoService.Setup(x => x.Cadastrar(cursoBuilder.Default().ComCategoria(1).Build())).Returns(Task.FromResult(mock.Object));
            curso.Callback<DtoResult<DtoCurso>>((DtoResult<DtoCurso> curso) => cursoEntity = curso);
            Assert.AreEqual("test", cursoEntity, "Wrong Description");

        }
    }
}
