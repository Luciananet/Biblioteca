using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
        public static bool verificaLoginSenha(string login,string senha,Controller controller)
        {
        
        using(BibliotecaContext bc = new BibliotecaContext){
        verificaSeUsuarioAdmimExiste(bc);
        senha=Criptografo.TextoCriptografado(senha);
        IQueryable<usuario> UsuarioEncontrado = bc.usuarios.Where(u=>u.login && u.senha==senha);
        List<UsuarioEncontrado=UsuarioEncontrado.ToList();
        if(UsuarioEncontrado.Count==0)
        {
          return false;
        }
        else{
            controller.HttpContext.Session("login",ListaUsuarioEncontrado[0].login);
             controller.HttpContext.Session("nome",ListaUsuarioEncontrado[0].nome);
            
             controller.HttpContext.Session("tipo",ListaUsuarioEncontrado[0].tipo);
            
            return true;

        }
      }

    }
    public static void verificaSeUsuarioAdmimExiste (BibliotecaContext bc)
    {
      IQueryable<usuario> userEncontrado = bc.usuarios.Where(u=>u.login=="admim");
      if(userEncontrado.ToList().Count==0)
      {
       Usuario admim = new Usuario();
       admim.login = "admim";
       admim.senha = Criptografo.TextoCriptografado("123");
       admim.tipo = Usuario.ADMIM;
       admim.Nome = "Administrador";

       bc.usuarios.Add(admim);
       bc.SaveChanges();
      }
    }
    public static void verificaSeUsuarioEAdmim(Controller controller)
    {
        if(!(controller.HttpContext.Session.GetInt32("tipo")== Usuario.ADMIM))
        {
         controller.Request.HttpContext.Response.Redirect("/Usuarios/NeedAdmim");
        }
    }
    }
}