using System;
using System.Collections.Generic;
using System.Text;

namespace PicPayChallenge.Core.ErrorCodes
{
    public class ErrorCodes
    {
        public static string InternalServerError => "Ocorreu um erro interno";
        public static string InvalidaRequestObject => "Dados da requisição incorretos";
        public static string UserNotFound  => "Usuário não encontrado";
    }
}
