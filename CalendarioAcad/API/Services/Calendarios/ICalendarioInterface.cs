﻿
using API.Model;
using API.DTO.Calendarios;
using API.DTO.Historicos;
namespace API.Services.Calendarios
{
    public interface ICalendarioInterface
    {
        Task<ResponseModel<Calendario>> AprovarCalendario(int idCalendario);
        Task<ResponseModel<List<Calendario>>> ListarCalendarios(); 
        Task<ResponseModel<Calendario>> CalendarioPorId(int idCalendario);
        Task<ResponseModel<List<Calendario>>> CriarCalendario(CriarCalendarioDTO criarCalendarioDTO);
        Task<ResponseModel<Calendario>> DesativarCalendario(int idCalendario);
        Task<string> GerarNumeroResolucao(int ano);

    }
}
