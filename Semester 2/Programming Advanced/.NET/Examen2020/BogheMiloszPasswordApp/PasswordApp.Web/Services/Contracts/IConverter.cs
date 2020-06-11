namespace PasswordApp.Web.Services.Contracts
{
    public interface IConverter
    {
        TTarget ConvertTo<TTarget>(object source) where TTarget : class;
    }
}