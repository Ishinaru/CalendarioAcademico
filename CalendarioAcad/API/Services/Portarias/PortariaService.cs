using API.Data;
using API.DTO.EventoPortarias;
using API.DTO.Historicos;
using API.DTO.Portarias;
using API.Model;
using API.Services.Calendarios;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Portarias
{
    public class PortariaService : IPortariaInterface
    {
        private readonly AppDbContext _context;
        private readonly ICalendarioInterface _calendarioInterface;
        private const string MSG_CRIACAO = "Portaria criada";
        private const string MSG_DESATIVACAO = "Portaria desativada";

        public PortariaService(AppDbContext context, ICalendarioInterface calendarioInterface)
        {
            _context = context;
            _calendarioInterface = calendarioInterface;
        }

        public async Task<ResponseModel<List<Portaria>>> CriarPortaria(CriarPortariaDTO portariaDTO, List<EventoPortariasDTO> eventoPortariasDTO)
        {

            ResponseModel<List<Portaria>> response = new ResponseModel<List<Portaria>>();

            Console.WriteLine($"IDs dos eventos recebidos: {string.Join(", ", eventoPortariasDTO.Select(ep => ep.EventoID))}");

            if (eventoPortariasDTO == null || eventoPortariasDTO.Count == 0)
            {
                response.Mensagem = "Eventos não encontrados";
                response.Status = false;
                return response;
            }

            if (eventoPortariasDTO.Any(ep => ep.DataFinal < ep.DataInicio))
            {
                response.Mensagem = "A data final não pode ser menor que a data inicial";
                response.Status = false;
                return response;
            }

            var eventos = await _context.Eventos
                .Where(e => eventoPortariasDTO.Select(ep => ep.EventoID).Contains(e.IdEvento) && e.IsAtivo)
                .ToListAsync();

            if (eventos.Count != eventoPortariasDTO.Count)
            {
                response.Mensagem = "Um ou mais eventos não encontrados";
                response.Status = false;
                return response;
            }

            var calendarioId = eventos.First().IdCalendario;

            if (eventos.Any(e => e.IdCalendario != calendarioId))
            {
                response.Mensagem = "Os eventos devem pertencer ao mesmo calendário";
                response.Status = false;
                return response;
            }

            var calendario = await _context.Calendarios.FirstOrDefaultAsync(calendarioDB => calendarioDB.IdCalendario == calendarioId);

            if (calendario == null || calendario.Status != Status.Aprovado)
            {
                response.Mensagem = "Calendário não encontrado ou não possui o status de \"Aprovado\"";
                response.Status = false;
                return response;
            }

            try
            {
                var backupNumRes = await _context.BackupNumerosResolucao.FirstOrDefaultAsync();
                string numeroResolucao;

                if (backupNumRes != null)
                {
                    numeroResolucao = backupNumRes.NumResolucao;
                    _context.BackupNumerosResolucao.Remove(backupNumRes);
                }
                else
                {
                    numeroResolucao = await _calendarioInterface.GerarNumeroResolucao(calendario.Ano);
                }

                var portaria = new Portaria
                {
                    NumPortaria = numeroResolucao,
                    AnoPortaria = calendario.Ano,
                    IdUsuario = portariaDTO.IdUsuario,
                    DataAtualizacao = portariaDTO.DataAtualizacao
                };

                _context.Portarias.Add(portaria);
                await _context.SaveChangesAsync();

                foreach (var eventoPortariaDTO in eventoPortariasDTO)
                {
                    var evento = eventos.FirstOrDefault(e => e.IdEvento == eventoPortariaDTO.EventoID);

                    var backupEvento = new BackupEvento
                    {
                        IdEvento = evento.IdEvento,
                        DataInicio = evento.DataInicio,
                        DataFinal = evento.DataFinal,
                        IdUsuario = evento.IdUsuario,
                        DataAtualizacao = evento.DataAtualizacao
                    };
                    _context.BackupEvents.Add(backupEvento);

                    var eventoPortaria = new Evento_Portaria
                    {
                        IdEvento = evento.IdEvento,
                        IdPortaria = portaria.Id_Portaria,
                        DataInicio = eventoPortariaDTO.DataInicio,
                        DataFinal = eventoPortariaDTO.DataFinal,
                        Observacao = eventoPortariaDTO.Observacao
                    };


                    evento.DataInicio = eventoPortaria.DataInicio;
                    evento.DataFinal = eventoPortaria.DataFinal;
                    evento.DataAtualizacao = portariaDTO.DataAtualizacao;
                    evento.IdUsuario = portariaDTO.IdUsuario;

                    _context.Evento_Portarias.Add(eventoPortaria);
                    _context.Eventos.Update(evento);
                }
                await _context.SaveChangesAsync();

                backupNumRes = new BackupNumeroResolucao
                {
                    NumResolucao = calendario.NumeroResolucao
                };
                _context.Add(backupNumRes);
                await _context.SaveChangesAsync();

                calendario.NumeroResolucao = portaria.NumPortaria;
                _context.Calendarios.Update(calendario);
                await _context.SaveChangesAsync();

                var historico = new Historico()
                {
                    Status = calendario.Status,
                    Descricao = MSG_CRIACAO + $" pelo Usuário : {portaria.IdUsuario}",
                    IdUsuario = portaria.IdUsuario,
                    DataMudanca = DateTime.Now,
                    IdCalendario = eventos.First().IdCalendario,
                    IdEvento = eventos.First().IdEvento,
                    IdPortaria = portaria.Id_Portaria
                };
                _context.Add(historico);

                await _context.SaveChangesAsync();

                response.Dados = await _context.Portarias.ToListAsync();
                response.Mensagem = "Portaria criada com sucesso";
                return response;
            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }


        public async Task<ResponseModel<List<Portaria>>> ListarPortarias()
        {
            ResponseModel<List<Portaria>> response = new ResponseModel<List<Portaria>>();
            try
            {
                var portarias = await _context.Portarias.ToListAsync();
                response.Dados = portarias;
                response.Mensagem = "Portarias Disponíveis";
                return response;

            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<Portaria>> DesativarPortaria(int idPortaria)
        {
            ResponseModel<Portaria> response = new ResponseModel<Portaria>();
            try
            {
                var portaria = await _context.Portarias.FirstOrDefaultAsync(portariaDB => portariaDB.Id_Portaria == idPortaria);

                if (portaria == null || !portaria.IsAtivo)
                {
                    response.Mensagem = "Portaria não encontrada";
                    response.Status = false;
                    return response;
                }

                var eventosPortaria = await _context.Evento_Portarias
                    .Where(ep => ep.IdPortaria == portaria.Id_Portaria)
                    .Include(ep => ep.Evento)
                    .ToListAsync();

                foreach (var item in eventosPortaria)
                {
                    var evento = item.Evento;
                    var backupEvento = await _context.BackupEvents.FirstOrDefaultAsync(be => be.IdEvento == evento.IdEvento);
                    evento.DataInicio = backupEvento.DataInicio;
                    evento.DataFinal = backupEvento.DataFinal;
                    evento.DataAtualizacao = backupEvento.DataAtualizacao;
                    evento.IdUsuario = backupEvento.IdUsuario;

                    _context.Eventos.Update(evento);
                    _context.BackupEvents.Remove(backupEvento);
                }

                var calendario = await _context.Calendarios
                    .FirstOrDefaultAsync(c => c.IdCalendario == eventosPortaria.First().Evento.IdCalendario);

                portaria.IsAtivo = false;
                _context.Portarias.Update(portaria);
                await _context.SaveChangesAsync();

                CriarHistoricoDTO historicoDTO = new CriarHistoricoDTO();

                var historico = new Historico()
                {
                    Status = calendario.Status,
                    Descricao = MSG_DESATIVACAO + $" pelo Usuário : {portaria.IdUsuario}",
                    IdUsuario = portaria.IdUsuario,
                    DataMudanca = historicoDTO.DataMudanca,
                    IdCalendario = historicoDTO.IdCalendario,
                    IdPortaria = portaria.Id_Portaria
                };
                _context.Add(historico);
                await _context.SaveChangesAsync();

                var ultimoNumResolucao = await _context.Portarias
                    .Where(p => p.AnoPortaria == portaria.AnoPortaria && p.IsAtivo)
                    .OrderByDescending(p => p.Id_Portaria)
                    .FirstOrDefaultAsync();

                if (ultimoNumResolucao != null)
                {
                    calendario.NumeroResolucao = ultimoNumResolucao.NumPortaria;
                }
                else
                {
                    calendario.NumeroResolucao = $"000/{calendario.Ano}";
                }

                _context.Calendarios.Update(calendario);
                await _context.SaveChangesAsync();
                response.Dados = portaria;
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

        /*public async Task<ResponseModel<List<Portaria>>> EditarPortaria(EditarPortariaDTO editarPortaria, int idPortaria)
        {
            ResponseModel<List<Portaria>> response = new ResponseModel<List<Portaria>>();
            try
            { 
                var portEvento = await _context.Evento_Portarias.FirstOrDefaultAsync(portariaDB => portariaDB.IdPortaria == idPortaria);
                
                if (portEvento == null)
                {
                    response.Mensagem = "Portaria não encontrada";
                    response.Status = false;
                    return response;
                }

                var evento = portEvento.Evento;

                evento.DataFinal = editarPortaria.DataFinal;
                evento.DataInicio = editarPortaria.DataInicio;
                evento.Observacao = editarPortaria.Observacao;
                evento.DataAtualizacao = editarPortaria.DataAtualizacao;
                evento.IdUsuario = editarPortaria.IdUsuario;


            }
            catch (Exception e)
            {
                response.Mensagem = e.Message;
                response.Status = false;
                return response;
            }
        }*/
    }


}
