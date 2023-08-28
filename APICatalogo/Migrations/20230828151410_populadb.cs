using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class populadb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome,ImagemUrl) Values('Bebidas'," +
               " 'http://www.macoratti.net/Imagens/1.jpg')");
            mb.Sql("Insert into Categorias(Nome,ImagemUrl) Values('Lanches'," +
                " 'http://www.macoratti.net/Imagens/2.jpg')");
            mb.Sql("Insert into Categorias(Nome,ImagemUrl) Values('Sobremesas'," +
                " 'http://www.macoratti.net/Imagens/3.jpg')");

            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "Values('Coca-Cola', 'Refrigerante de Cola', '5.45', 'http://www.macoratti.net/Imagens/coca.jpg', '50', now()," +
                "(Select CategoriaId from Categorias where Nome = 'Bebidas'))");

            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "Values('Lanche de Atum', 'Lanche de Atum com maionese', '8.99', 'http://www.macoratti.net/Imagens/atum.jpg', '25', now()," +
                "(Select CategoriaId from Categorias where Nome = 'Lanches'))");

            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)" +
                "Values('Pudim', 'Pudim de Leite 100g', '7.45', 'http://www.macoratti.net/Imagens/pudim.jpg', '12', now()," +
                "(Select CategoriaId from Categorias where Nome = 'Sobremesas'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

        }
    }
}
