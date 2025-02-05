using BlazorApp.Models;

namespace BlazorApp.Services.PortariaService
{
    public interface PortariaService : IPortariaInterface
    {
        public Task<ResponseModel<Portaria>> CreatePortaria();
            
    }
}
