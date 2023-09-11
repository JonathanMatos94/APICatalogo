using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) 
        { 
        }
        public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return await Get().OrderBy(c=>c.Preco).ToListAsync();
        }
    }
}
