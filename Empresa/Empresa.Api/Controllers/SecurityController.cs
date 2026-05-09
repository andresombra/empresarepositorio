using Empresa.Api.Security;
using Empresa.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        [HttpGet("Encriptar/{texto}")]
        public async Task<ActionResult<string>> Encriptar(string texto)
        {
            var publicKeyPem = RsaConnectionStringProtector.publicKeyPem; //Environment.GetEnvironmentVariable("RSA_PUBLIC_KEY_PEM")
                //?? throw new InvalidOperationException("Chave pública RSA não configurada.");

            var encryptedText = RsaConnectionStringProtector.Encrypt(texto, publicKeyPem);
            
            return Ok(encryptedText ?? string.Empty);
        }

        [HttpGet("Decriptar/{textoDecriptar}")]
        public async Task<ActionResult<string>> Decriptar(string textoDecriptar)
        {
            var privateKeyPem = Environment.GetEnvironmentVariable("RSA_PRIVATE_KEY_PEM")
                ?? throw new InvalidOperationException("Chave privada RSA não configurada.");

            var decryptedText = RsaConnectionStringProtector.Decrypt(textoDecriptar, privateKeyPem);
            return Ok(decryptedText);
        }

        [HttpGet("GerarKeys")]
        public async Task<ActionResult<string>> GerarChaves()
        {
            var (publicKeyPem, privateKeyPem) = RsaConnectionStringProtector.GeneratePemKeys();
            var keys = $"PublicKey: {publicKeyPem}, PrivateKey: {privateKeyPem}";
            return Ok(keys);
        }
    }
}
