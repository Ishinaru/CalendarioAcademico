using API.Data;
using API.DTO.Eventos;
using API.DTO.Historicos;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Eventos
{
    public class EventoService : IEventoInterface
    {
        private readonly AppDbContext _context;
        private const string MSG_CRIACAO = "Evento Criado : ";
        private const string MSG_EDICAO = "Evento Editado";
        private const string MSG_DESATIVACAO = "Evento Desativado";
        public EventoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<Evento>> CriarEvento(CriarEventoDTO eventoDTO, int idCalendario)
        {
            ResponseModel<Evento> response = new ResponseModel<Evento>();

            var calendario = await _context.Calendarios.FirstOrDefaultAsync(calendarioDB => calendarioDB.IdCalendario == idCalendario);

            if (calendario == null || calendario.Status == Status.Desativado)
            {
                response.Mensagem = "Calendário Inválido ou Inexistente";
                response.Status = false;
                return response;
            }
            else
            {
                if (calendario.Status == Status.Aprovado)
                {
                    response.Mensagem = "Não é possível criar um evento com um calendário em estado de aprovado";
                    response.Status = false;
                    return response;
                }

                if (eventoDTO.DataFinal < eventoDTO.DataInicio)
                {
                    response.Mensagem = "A data final não pode ser menor que a data inicial";
                    response.Status = false;
                    return response;
                }

                try
                {
                    var evento = new Evento()
                    {
                        DataInicio = eventoDTO.DataInicio,
                        DataFinal = eventoDTO.DataFinal,
                        Descricao = eventoDTO.Descricao,
                        UescFunciona = eventoDTO.UescFunciona,
                        Importante = eventoDTO.Importante,
                        TipoFeriado = eventoDTO.TipoFeriado,
                        IdUsuario = eventoDTO.IdUsuario,
                        DataAtualizacao = eventoDTO.DataAtualizacao,
                        IdCalendario = idCalendario

                    };
                    _context.Add(evento);
                    await _context.SaveChangesAsync();

                    CriarHistoricoDTO historicoDTO = new();

                    var historico = new Historico()
                    {
                        Status = calendario.Status,
                        Descricao = $"{MSG_CRIACAO}{eventoDTO.Descricao} pelo Usuário: {eventoDTO.IdUsuario}",
                        IdUsuario = evento.IdUsuario,
                        DataMudanca = historicoDTO.DataMudanca,
                        IdCalendario = calendario.IdCalendario,
                        IdEvento = evento.IdEvento
                    };

                    _context.Add(historico);
                    await _context.SaveChangesAsync();

                    response.Dados = evento;
                    response.Mensagem = "Evento criado com sucesso";
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

        public async Task<ResponseModel<Evento>> DesativarEvento(int idEvento)
        {
            ResponseModel<Evento> response = new ResponseModel<Evento>();
            try
            {
                var evento = await _context.Eventos.FirstOrDefaultAsync(eventoDB => eventoDB.IdEvento == idEvento);

                if (evento == null || !evento.IsAtivo)
                {
                    response.Mensagem = "Evento não encontrado";
                    response.Status = false;
                    return response;
                }
                var calendario = await _context.Calendarios.FirstOrDefaultAsync(calendarioDB => calendarioDB.IdCalendario == evento.IdCalendario);

                evento.IsAtivo = false;
                _context.Eventos.Update(evento);
                await _context.SaveChangesAsync();

                CriarHistoricoDTO historicoDTO = new CriarHistoricoDTO();

                var historico = new Historico()
                {
                    Status = calendario.Status,
                    Descricao = MSG_DESATIVACAO + $" pelo Usuário : {evento.IdUsuario}",
                    IdUsuario = evento.IdUsuario,
                    DataMudanca = historicoDTO.DataMudanca,
                    IdCalendario = calendario.IdCalendario,
                    IdEvento = evento.IdEvento
                };
                _context.Add(historico);
                await _context.SaveChangesAsync();

                response.Dados = evento;
                response.Mensagem = MSG_DESATIVACAO;
                return response;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Evento>> EditarEvento(EditarEventoDTO editarEvento, int idEvento)
        {
            ResponseModel<Evento> response = new ResponseModel<Evento>();

            try
            {
                if (editarEvento.DataFinal < editarEvento.DataInicio)
                {
                    response.Mensagem = "A data final não pode ser menor que a data inicial";
                    response.Status = false;
                    return response;
                }

                var evento = await _context.Eventos.FirstOrDefaultAsync(eventoDB => eventoDB.IdEvento == idEvento);
                if (evento == null || !evento.IsAtivo)
                {
                    response.Mensagem = "Evento não encontrado";
                    response.Status = false;
                    return response;
                }

                var calendario = await _context.Calendarios.FirstOrDefaultAsync(calendarioDB => calendarioDB.IdCalendario == evento.IdCalendario);

                if (calendario.Status == Status.Desativado || calendario.Status == Status.Aprovado)
                {
                    response.Mensagem = $"Não foi possível editar o evento devido ao status do calendário : {calendario.Status}";
                    response.Status = false;
                    return response;
                }

                evento.DataInicio = editarEvento.DataInicio;
                evento.DataFinal = editarEvento.DataFinal;
                evento.Descricao = editarEvento.Descricao;
                evento.UescFunciona = editarEvento.UescFunciona;
                evento.Importante = editarEvento.Importante;
                evento.TipoFeriado = editarEvento.TipoFeriado;

                _context.Update(evento);
                await _context.SaveChangesAsync();

                CriarHistoricoDTO historicoDTO = new CriarHistoricoDTO();
                var historico = new Historico()
                {
                    Status = calendario.Status,
                    Descricao = MSG_EDICAO + $" pelo Usuário : {evento.IdUsuario}",
                    IdUsuario = evento.IdUsuario,
                    DataMudanca = historicoDTO.DataMudanca,
                    IdCalendario = calendario.IdCalendario,
                    IdEvento = evento.IdEvento
                };
                _context.Add(historico);
                await _context.SaveChangesAsync();

                response.Dados = evento;
                response.Mensagem = $"Evento editado por ";
                return response;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Evento>> EventorPorID(int IdEvento)
        {
            ResponseModel<Evento> response = new ResponseModel<Evento>();

            try
            {
                var evento = await _context.Eventos.FirstOrDefaultAsync(eventoBD => eventoBD.IdEvento == IdEvento);
                if (evento == null)
                {
                    response.Mensagem = "Evento não encontrado";
                    return response;
                }
                response.Dados = evento;
                response.Mensagem = "Evento encontrado";
                return response;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<List<Evento>>> ListarEventos()
        {
            ResponseModel<List<Evento>> response = new ResponseModel<List<Evento>>();
            try
            {
                var eventos = await _context.Eventos.ToListAsync();
                response.Dados = eventos;
                response.Mensagem = "Eventos Disponíveis";

                return response;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<Evento>>> EventoPorCalendario(int idCalendario)
        {
            var response = new ResponseModel<List<Evento>>();
            try
            {
                var eventos = await _context.Eventos.Where(evento => evento.IdCalendario == idCalendario).ToListAsync();

                if (eventos == null)
                {
                    response.Mensagem = "Eventos não encontrados";
                    response.Status = false;
                    return response;
                }

                response.Dados = eventos;
                response.Mensagem = "Eventos encontrados";
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
