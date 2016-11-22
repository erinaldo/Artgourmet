using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.Global.RegrasNegocio
{
    public class MemoriaWeb
    {
        /// <summary>
        /// Função que criar as variaveis na sessão
        /// </summary>
        /// <returns>true ou false</returns>
        public static bool CriaSessao(GUSUARIO usu)
        {
            try
            {
                Memoria.RoundHouseKick(usu);
                Memoria.Codusuario = usu.codusuario;

                HttpContext.Current.Session["Artebit.ChuckNorris"] = Memoria.ChuckNorris;
                HttpContext.Current.Session["Artebit.Usuario"] = Memoria.Codusuario;
                HttpContext.Current.Session["Artebit.Empresa"] = Memoria.Empresa;
                HttpContext.Current.Session["Artebit.Filial"] = Memoria.Filial;
                HttpContext.Current.Session["Artebit.Perfil"] = Memoria.Perfil;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Função que refaz a sessão caso tenha perdido, mas que ainda esteja logado
        /// </summary>
        /// <returns>true ou false</returns>
        public static bool ValidaSessao()
        {
            using (Contexto.Atual = new Modelo.Restaurante())
            {
                try
                {
                    if (HttpContext.Current.Session["Artebit.ChuckNorris"] == null)
                    {
                        using (var s = new StreamWriter(HttpContext.Current.Server.MapPath("captura.txt")))
                        {
                            s.WriteLine("Sessao ChuckNorris: " +
                                        Convert.ToString(HttpContext.Current.Session["Artebit.ChuckNorris"]));
                        }

                        if (HttpContext.Current.Request.ServerVariables["AUTH_USER"] != null)
                        {
                            using (var s = new StreamWriter(HttpContext.Current.Server.MapPath("captura.txt")))
                            {
                                s.WriteLine("AUTH_USER:" +
                                            Convert.ToString(HttpContext.Current.Request.ServerVariables["AUTH_USER"]));
                            }

                            var usu = new UsuarioControl();
                            var usuario = new GUSUARIO();
                            string[] cookie =
                                Convert.ToString(HttpContext.Current.Request.ServerVariables["AUTH_USER"]).Split('ª');


                            //cook[0] = IDEMPRESA
                            //cook[1] = CODUSUARIO
                            usuario.codusuario = cookie[1];
                            Memoria.Empresa = Convert.ToInt32(cookie[0]);

                            var compl = new List<string>();
                            usuario = usu.Buscar(usuario);

                            using (var s = new StreamWriter(HttpContext.Current.Server.MapPath("captura.txt")))
                            {
                                if (usuario == null)
                                {
                                    s.WriteLine("usuario null");
                                    s.WriteLine(usuario.codusuario + cookie[0]);
                                }
                            }


                            if (usuario != null)
                            {
                                Memoria.Codusuario = usuario.codusuario;

                                CriaSessao(usuario);

                                return true;
                            }
                            else
                            {
                                Process.Start("~/Login.aspx");
                                //HttpContext.Current.Server.Transfer("~/Login.aspx");
                                return false;
                            }
                        }
                        else
                        {
                            Process.Start("~/Login.aspx");
                            //HttpContext.Current.Server.Transfer("~/Login.aspx");
                            return false;
                        }
                    }
                    else
                    {
                        Memoria.ChuckNorris =
                            (IEnumerable<GUSRFILMOD>) HttpContext.Current.Session["Artebit.ChuckNorris"];
                        Memoria.Codusuario = (string) HttpContext.Current.Session["Artebit.Usuario"];
                        Memoria.Empresa = (int) HttpContext.Current.Session["Artebit.Empresa"];
                        Memoria.Filial = (int) HttpContext.Current.Session["Artebit.Filial"];
                        Memoria.Perfil = (int?) HttpContext.Current.Session["Artebit.Perfil"];

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    using (var s = new StreamWriter(HttpContext.Current.Server.MapPath("captura.txt")))
                    {
                        s.WriteLine(ex.ToString());
                    }

                    //HttpContext.Current.Server.Transfer("~/Login.aspx");
                    //return false;

                    throw ex;
                }
            }
        }
    }
}