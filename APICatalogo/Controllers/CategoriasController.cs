using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnityOfWork _context;
        private readonly IMapper _mapper;
        public CategoriasController(IUnityOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
          
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
        {
            
            var categoriaProduto = _context.CategoriaRepository.GetCategoriasProdutos().ToList();

            var categoriaProdutoDto = _mapper.Map<List<CategoriaDTO>>(categoriaProduto);

            return categoriaProdutoDto;

        }
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _context.CategoriaRepository.Get().ToList();

            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

            return categoriasDTO;
        }

        // GET api/<CategoriasController>/id
        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _context.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if(categoria == null)
            { 
                return BadRequest(); 
            }
            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            
            return categoriaDto;
        }

        // POST api/<CategoriasController>
        [HttpPost]
        public ActionResult<CategoriaDTO> Post([FromBody] CategoriaDTO categoriaDtoReturn)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDtoReturn);

            _context.CategoriaRepository.Add(categoria);
            _context.Commit();

            return new CreatedAtRouteResult("ObterCategoria", 
                new { id = categoria.CategoriaId }, categoriaDtoReturn);
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public ActionResult<Categoria> Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {

            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }
           
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _context.CategoriaRepository.Update(categoria);
            _context.Commit();
            
            return Ok();
        }
    
        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _context.CategoriaRepository.GetById(c => c.CategoriaId == id);
  
            if (categoria == null)
            {
                return NotFound();
            }
            _context.CategoriaRepository.Delete(categoria);
            _context.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDto;
        }
    }
}
