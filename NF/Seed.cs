using System;
using System.Collections.Generic;
using System.Linq;
using NF.Models;
using NF.Data;

namespace NF
{
    public class Seed
    {
        private readonly AppDbContext appDbContext;

        public Seed(AppDbContext context)
        {
            this.appDbContext = context;
        }

        public void SeedAppDbContext()
        {
            var clientes = new List<Cliente>();

            if (!appDbContext.Clientes.Any())
            {

                clientes = new List<Cliente>
                {
                    new Cliente
                    {
                        Nome = "Cliente1",
                    },
                    new Cliente
                    {
                        Nome = "Cliente2",
                    },
                };

                appDbContext.Clientes.AddRange(clientes);
                appDbContext.SaveChanges();
            }

            var fornecedores = new List<Fornecedor>();

            if (!appDbContext.Fornecedores.Any())
            {
                fornecedores = new List<Fornecedor>
                {
                    new Fornecedor
                    {
                        Nome = "Fornecedor1",
                    },
                    new Fornecedor
                    {
                        Nome = "Fornecedor2",
                    },
                };

                appDbContext.Fornecedores.AddRange(fornecedores);
                appDbContext.SaveChanges();
            }

             List<Produto> produtos = new List<Produto>();

            if (!appDbContext.Produtos.Any())
            {
                produtos = new List<Produto>
                {
                    new Produto
                    {
                        Descricao = "Produto1",
                        Preco = 50.0f
                    },

                    new Produto
                    {
                        Descricao = "Produto2",
                        Preco = 30.0f
                    },
                };
                appDbContext.Produtos.AddRange(produtos);
                appDbContext.SaveChanges();
             }

            NotaFiscal notaFiscal = new NotaFiscal();

            if (!appDbContext.NotasFiscais.Any())
            {
                notaFiscal =  new NotaFiscal
                {
                    NumeroNota = 1,
                    ValorTotal = 100.0f,
                    Cliente = clientes[0],
                    Fornecedor = fornecedores[0],
           
                };

                appDbContext.NotasFiscais.AddRange(notaFiscal);
                appDbContext.SaveChanges();
            }

       

            if (!appDbContext.NotaFiscalProdutos.Any())
            {
                var notaFiscalProdutos = new List<NotaFiscalProduto>
                {
                    new NotaFiscalProduto
                    {
                        NotaFiscalId = 1,
                        ProdutoId = 1,
                        NotaFiscal = notaFiscal,
                        Produto = produtos[0],
                        Quantidade = 2
                    },
                     new NotaFiscalProduto
                    {
                        NotaFiscalId = 1,
                        ProdutoId = 2,
                        NotaFiscal = notaFiscal,
                        Produto = produtos[1],
                        Quantidade = 2
                    },
                };

                appDbContext.NotaFiscalProdutos.AddRange(notaFiscalProdutos);
                appDbContext.SaveChanges();
            }
        }
    }
}
