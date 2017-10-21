using LTM.Teste.Domain.Entitys;
using System;
using Xunit;

namespace LTM.Teste.Tests
{
    public class UnitTestProduto
    {
        [Fact]
        public void ProdutoEstaValido()
        {
            var produto = new Produto(0, "Produto de teste 1", 10, 9.99M, true);
            var valid = produto.IsValid();
            Assert.True(valid);
        }

        [Fact]
        public void ProdutoDeveConterDescricao()
        {
            var produto = new Produto(0, "Produto de teste 1", 10, 9.99M, true);
            produto.SetarDescricao("Descricao do produto 1");
            Assert.True(produto.Descricao != null);
        }

        [Fact]
        public void ProdutoNaoDeveTerQuantidadeMinimaEValorNegativo()
        {
            var produto = new Produto(0, "Produto de teste 1", -10, -9.99M, true);
            var valid = produto.IsValid();
            Assert.False(valid);
        }
        
    }
}
