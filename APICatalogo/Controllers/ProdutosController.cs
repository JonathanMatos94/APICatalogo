using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        return _context.Produtos.AsNoTracking().ToList();
    }
    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

        if (produto == null)
        {
            return NotFound();
        }
        return produto;

    }
    [HttpPost]
    public ActionResult<Produto> Post([FromBody()] Produto produto)
    {
        //if(!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);     <<<=== a partir do .NET Core 2.1 isso ficou automático em APIs
        //}
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
    }
    [HttpPut("{id}")]
    public ActionResult<Produto> Put(int id,[FromBody()] Produto produto)
    {
        //if(!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);     <<<=== a partir do .NET Core 2.1 isso ficou automático em APIs
        //}
        if(id != produto.ProdutoId)
        {
            return BadRequest();
        }
        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok();
    }
    [HttpDelete("{id}")]
    public ActionResult<Produto> Delete(int id)
    {
        var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
        //var produto = _context.Produtos.Find(id)   <<<= Procura a id na memória e não no banco de dados,
        //porém só pode ser usado se a ID for a PK 
        if (produto == null)
        {
            return NotFound();
        }
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return produto;
    }
}

