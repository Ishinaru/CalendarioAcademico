using API.Data;
using API.DTO.Historicos;
using API.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace API.Services.Historicos
{
    public class HistoricoService : IHistoricoInterface
    {
        private readonly AppDbContext _context; 

        public HistoricoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<Historico>>> CriarHistorico(CriarHistoricoDTO? historicoDTO, int idCalendario)
        {
            ResponseModel<List<Historico>> response = new ();

            var calendario = await _context.Calendarios.FirstOrDefaultAsync(calendarioDb => calendarioDb.IdCalendario == idCalendario);

            if (calendario == null) {
                response.Mensagem = "Calendário não encontrado";
                response.Status = false;
                return response;
            }

            try
            {
                var historico = new Historico()
                {
                    Status = calendario.Status,
                    Descricao = historicoDTO.Descricao,
                    IdUsuario = historicoDTO.IdUsuario,
                    DataMudanca = historicoDTO.DataMudanca,
                    IdCalendario = historicoDTO.IdCalendario,
                    IdEvento = historicoDTO.IdEvento,
                    IdPortaria = historicoDTO.IdPortaria,
                };

                await _context.AddAsync(historico);
                await _context.SaveChangesAsync();
                response.Dados = await _context.Historicos.ToListAsync();
                response.Mensagem = "Histórico criado com sucesso";
                return response;

    }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<Historico>>> ListarHistoricos()
        {
            ResponseModel<List<Historico>> response = new ResponseModel<List<Historico>>();
            try
            {
                var historicos = await _context.Historicos.ToListAsync();
                response.Dados = historicos;
                response.Mensagem = "Históricos encontrados";
                return response;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
