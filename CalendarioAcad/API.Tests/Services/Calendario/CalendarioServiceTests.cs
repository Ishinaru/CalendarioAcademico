using API.Data;
using API.DTO.Calendarios;
using API.Services.Calendarios;
using API.Services.Historicos;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace API.Tests.Services.Calendario
{

    public class CalendarioServiceTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<HistoricoService> _mockHistoricoService;
        private readonly CalendarioService _calendarioService;

        public CalendarioServiceTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockHistoricoService = new Mock<HistoricoService>(_mockContext.Object);
            _calendarioService = new CalendarioService(_mockContext.Object, _mockHistoricoService.Object);
        }

        [Fact]
        public async Task CriarCalendarioDeveRetornarSucessoCasoAnoSejaUnico()
        {
            var calendario = new Model.Calendario() { Ano = 2030, IdUsuario = 0 };
            var calendarioTest = new CriarCalendarioDTO() { Ano = 2030, IdUsuario = 0 };

            var entityEntryMock = new Mock<EntityEntry<Model.Calendario>>();
            _mockContext.Setup(x => x.Calendarios.AddAsync(It.IsAny<Model.Calendario>(), default)).ReturnsAsync(entityEntryMock.Object);
            _mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

            var result = await _calendarioService.CriarCalendario(calendarioTest);

            Assert.True(result.Status);
            Assert.NotNull(result.Dados);
            Assert.Equal(2025, result.Dados.First().Ano);
        }
    }
}
