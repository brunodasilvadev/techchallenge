namespace TechChallenge.CadastroContato.Core.CodigoAreaBrasil.Interface
{
    public interface ICodigoAreaBrasilCommandStore
    {
        public Task<bool> CodigoAreaExiste(int ddd);
    }
}