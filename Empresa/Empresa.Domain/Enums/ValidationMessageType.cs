namespace Empresa.Domain.Enums
{
    public enum ValidationMessageType
    {
        Success = 0, // Representa uma operação bem-sucedida
        Warning = 1, // Representa um aviso (ex.: validação falhou)
        Error = 2    // Representa um erro (ex.: exceção ou falha crítica)
    }
}
