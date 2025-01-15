using API.DTO.Historicos;
using API.Model;

namespace API.Services.Historicos
{
    public interface IHistoricoInterface
    {
        Task<ResponseModel<List<Historico>>> CriarHistorico(CriarHistoricoDTO? historicoDTO, int idCalendario);
        Task<ResponseModel<List<Historico>>> ListarHistoricos();
    }
}
