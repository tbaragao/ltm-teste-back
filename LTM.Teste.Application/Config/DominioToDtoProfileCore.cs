using LTM.Teste.Domain.Entitys;
using LTM.Teste.Dto;

namespace LTM.Teste.Application.Config
{
    public class DominioToDtoProfileCore : AutoMapper.Profile
    {

        public DominioToDtoProfileCore()
        {
            CreateMap(typeof(Produto), typeof(ProdutoDto)).ReverseMap();
            CreateMap(typeof(Produto), typeof(ProdutoDtoSpecialized));
            CreateMap(typeof(Produto), typeof(ProdutoDtoSpecializedResult));
            CreateMap(typeof(Produto), typeof(ProdutoDtoSpecializedReport));
            CreateMap(typeof(Produto), typeof(ProdutoDtoSpecializedDetails));

        }

    }
}
