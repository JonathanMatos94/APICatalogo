using APICatalogo.Context;

namespace APICatalogo.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        private ProdutoRepository _produtoRepository;
        private CategoriaRepository _categoriaRepository;
        public AppDbContext _context;

        public UnityOfWork(AppDbContext context)
        {
            _context = context;    
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                // ?? verifica se _produtoRepository é nulo, se for cria uma nova instância.
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public async Task Commit()
        {
           await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
