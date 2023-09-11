﻿using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly IMapper _mapper;
    public ProdutosController(IUnityOfWork context, IMapper mapper)
    {
        _uof = context;
        _mapper = mapper;
    }

    [HttpGet("menorpreco")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorPreco()
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosPorPreco();

        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        
        return produtosDto; 
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
    {
        var produtos = await _uof.ProdutoRepository.Get().ToListAsync();

        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

        return produtosDto;
    }
    [HttpGet("{id}", Name = "ObterProduto")]
    public async Task<ActionResult<ProdutoDTO>> Get(int id)
    {
        //throw new Exception("Exception ao retornar produto pelo id");

        //string[] teste = null;
        //if(teste.Length>0) { }
        
        var produto =  await _uof.ProdutoRepository.GetById(p => p.ProdutoId==id);     

        if (produto == null)
        {
            return NotFound();
        }
        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        
        return produtoDto;

    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);

        _uof.ProdutoRepository.Add(produto);
        await _uof.Commit();

        var produtoDtoReturn = _mapper.Map<ProdutoDTO>(produto);

        return new CreatedAtRouteResult("ObterProduto", 
            new { id = produto.ProdutoId }, produtoDtoReturn);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Produto>> Put(int id,[FromBody] ProdutoDTO produtoDto)
    {

        if(id != produtoDto.ProdutoId)
        {
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        _uof.ProdutoRepository.Update(produto);
        await _uof.Commit();

        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id)
    {
        var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
        //var produto = _uof.Produtos.Find(id)   <<<= Procura a id na memória e não no banco de dados,
        //porém só pode ser usado se a ID for a PK 
        if (produto == null)
        {
            return NotFound();
        }
        _uof.ProdutoRepository.Delete(produto);
        await _uof.Commit();

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return produtoDto;
    }
}

