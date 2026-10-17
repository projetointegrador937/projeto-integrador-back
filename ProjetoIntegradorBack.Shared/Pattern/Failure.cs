using System.Diagnostics;

namespace ProjetoIntegradorBack.Shared.Pattern
{
    public sealed class Failure(int Code, string  Message)
    {
        public static Failure Unauthorized => new(401, "Não autorizado");
        public static Failure NotFound => new(404, "Recurso não encontrado");
        public static Failure BadRequest => new(400, "Requisição inválida");
        public static Failure InternalServerError => new(500, "Erro interno do servidor");

        public override string ToString()
       => Message;
    }
}
