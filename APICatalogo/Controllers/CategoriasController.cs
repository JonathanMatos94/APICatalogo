using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnityOfWork _context;
        private readonly IMapper _mapper;
        public CategoriasController(IUnityOfWork context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
          
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
        {

            var categoriaProduto = await _context.CategoriaRepository.GetCategoriasProdutos();

            var categoriaProdutoDto = _mapper.Map<List<CategoriaDTO>>(categoriaProduto);

            return categoriaProdutoDto;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _context.CategoriaRepository.Get().ToListAsync();

            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

            return categoriasDTO;
        }

        // GET api/<CategoriasController>/id
        [HttpGet("{id}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _context.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if(categoria == null)
            { 
                return BadRequest(); 
            }
            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            
            return categoriaDto;
        }

        // POST api/<CategoriasController>
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post([FromBody] CategoriaDTO categoriaDtoReturn)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDtoReturn);

            _context.CategoriaRepository.Add(categoria);
            await _context.Commit();

            return new CreatedAtRouteResult("ObterCategoria", 
                new { id = categoria.CategoriaId }, categoriaDtoReturn);
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {

            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }
           
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _context.CategoriaRepository.Update(categoria);
            await _context.Commit();
            
            return Ok();
        }
    
        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var categoria = await _context.CategoriaRepository.GetById(c => c.CategoriaId == id);
  
            if (categoria == null)
            {
                return NotFound();
            }
            _context.CategoriaRepository.Delete(categoria);
            await _context.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDto;
        }
    }
}
