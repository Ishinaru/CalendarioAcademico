using API.Data;
using API.DTO.Calendarios;
using API.DTO.Historicos;
using API.Model;
using API.Services.Historicos;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Calendarios
{
    public class CalendarioService : ICalendarioInterface
    {
        private readonly AppDbContext _context;
        private readonly HistoricoService _historicoService;
        private const string MSG_CRIACAO = "Calendário Criado";
        private const string MSG_APROVACAO = "Calendário Aprovado";
        private const string MSG_DESATIVACAO = "Calendário Desativado";
        public CalendarioService(AppDbContext context, HistoricoService historicoService)
        {
            _context = context;
            _historicoService = historicoService;
        }

        public async Task<ResponseModel<Calendario>> AprovarCalendario(int idCalendario)
        {
            ResponseModel<Calendario> response = new ResponseModel<Calendario>();
            try
            {
                var calendario = await _context.Calendarios.FirstOrDefaultAsync(calendarioDB => calendarioDB.IdCalendario == idCalendario);
                if (calendario == null || calendario.Status == Status.Desativado)
                {
                    response.Mensagem = "Calendário não encontrado";
                    response.Status = false;
                    return response;
                }

                if (calendario.Status == Status.Aprovado)
                {
                    response.Mensagem = "Calendário já foi aprovado anteriormente";
                    response.Status = false;
                    return response;
                }

                calendario.Status = Status.Aprovado;
                _context.Calendarios.Update(calendario);
                await _context.SaveChangesAsync();

                CriarHistoricoDTO historicoDTO = new CriarHistoricoDTO();

                var historico = new Historico()
                {
                    Status = calendario.Status,
                    Descricao = $"{MSG_APROVACAO}: pelo usuário : {calendario.IdUsuario}",
                    IdUsuario = calendario.IdUsuario,
                    DataMudanca = historicoDTO.DataMudanca,
                    IdCalendario = calendario.IdCalendario
                };

                _context.Add(historico);
                await _context.SaveChangesAsync();

                response.Dados = calendario;
                response.Mensagem = $"{MSG_APROVACAO}: {calendario.IdUsuario}";
                return response;

            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<Calendario>> CalendarioPorId(int idCalendario)
        {
            ResponseModel<Calendario> response = new ResponseModel<Calendario>();
            try
            {
                var calendario = await _context.Calendarios.FirstOrDefaultAsync(calendarioDb => calendarioDb.IdCalendario == idCalendario);
                if (calendario == null)
                {
                    response.Mensagem = "Calendário não encontrado";
                    return response;
                }
                response.Dados = calendario;
                response.Mensagem = "Calendário encontrado";
                return response;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Calendario>> CriarCalendario(CriarCalendarioDTO criarCalendarioDTO)
        {
            var response = new ResponseModel<Calendario>();

            try
            {
                var calendario = new Calendario()
                {
                    Ano = criarCalendarioDTO.Ano,
                    IdUsuario = criarCalendarioDTO.IdUsuario,
                    DataAtualizacao = criarCalendarioDTO.DataAtualizacao,
                    NumeroResolucao = $"000/{criarCalendarioDTO.Ano}",
                    Observacao = criarCalendarioDTO.Observacao
                };
                var calendarioDB = await _context.Calendarios.Where(calendarioDB => calendarioDB.Ano == calendario.Ano).ToListAsync();

                if (calendarioDB.Any(calendarioDB => calendarioDB.Status != Status.Desativado))
                {
                    response.Mensagem = "Calendário com mesmo ano já consta no banco de dados";
                    response.Status = false;
                    return response;
                }

                _context.Add(calendario);
                await _context.SaveChangesAsync();

                CriarHistoricoDTO historicoDTO = new();

                var historico = new Historico()
                {
                    Status = calendario.Status,
                    Descricao = MSG_CRIACAO,
                    IdUsuario = historicoDTO.IdUsuario,
                    DataMudanca = historicoDTO.DataMudanca,
                    IdCalendario = calendario.IdCalendario
                };

                _context.Add(historico);
                await _context.SaveChangesAsync();

                response.Dados = calendario;
                response.Mensagem = "Calendário criado com sucesso";
                return response;

            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<Calendario>>> ListarCalendarios()
        {
            ResponseModel<List<Calendario>> response = new ResponseModel<List<Calendario>>();
            try
            {
                var calendarios = await _context.Calendarios.Where(calendarioDB => calendarioDB.Status != Status.Desativado).ToListAsync();
                response.Dados = calendarios;
                response.Mensagem = "Calendários encontrados";
                return response;

            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Calendario>> DesativarCalendario(int idCalendario)
        {
            ResponseModel<Calendario> response = new ResponseModel<Calendario>();
            try
            {
                var calendario = await _context.Calendarios.FirstOrDefaultAsync(calendarioDB => calendarioDB.IdCalendario == idCalendario);
                if (calendario.Status == Status.Desativado)
                {
                    response.Mensagem = "Calendário já foi desativado anteriormente";
                    response.Status = false;
                    return response;
                }
                calendario.Status = Status.Desativado;
                _context.Calendarios.Update(calendario);
                await _context.SaveChangesAsync();

                CriarHistoricoDTO historicoDTO = new();

                var historico = new Historico()
                {
                    Status = calendario.Status,
                    Descricao = MSG_DESATIVACAO,
                    IdUsuario = historicoDTO.IdUsuario,
                    DataMudanca = historicoDTO.DataMudanca,
                    IdCalendario = calendario.IdCalendario
                };

                _context.Add(historico);
                await _context.SaveChangesAsync();

                response.Dados = calendario;
                response.Mensagem = "Calendário desativado com sucesso";
                return response;

            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<string> GerarNumeroResolucao(int ano)
        {
            var calendario = await _context.Calendarios
                .Where(calendarioDB => calendarioDB.Ano == ano)
                .OrderByDescending(calendarioDB => calendarioDB.NumeroResolucao)
                .FirstOrDefaultAsync();

            var num = 0;

            if (calendario != null)
            {
                var numRes = calendario.NumeroResolucao;
                num = int.Parse(numRes.Split('/')[0]);
                num++;
            }

            string numResolucao = $"{num:D3}/{ano}";
            return numResolucao;
        }
    }
}
