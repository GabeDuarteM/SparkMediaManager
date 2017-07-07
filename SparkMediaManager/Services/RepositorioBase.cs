// Developed by: Gabriel Duarte
// 
// Created at: 14/05/2016 16:37

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using SparkMediaManager.Helpers;
using SparkMediaManager.Localization;
using SparkMediaManager.Models;

namespace SparkMediaManager.Services
{
    public class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        private readonly IContext _context;

        /// <summary>
        ///     Classe de repositório base com as funcionalidades padrões e genéricas.
        /// </summary>
        /// <param name="context">Context a ser utilizado. Atribuido via Dependency injection na classe que a implementa.</param>
        /// <param name="strPropriedadesParaFormat">Array com as propriedades que serão utilizadas nas mensagens</param>
        /// <param name="strMensagemErroAdicionar">
        ///     Mensagem de erro ao adicionar. Será usado string.format() de acordo com as
        ///     propriedades da variavel sPropriedadesParaFormat
        /// </param>
        /// <param name="strMensagemErroGet">
        ///     Mensagem de erro ao pesquisar. Será usado string.format() de acordo com as propriedades
        ///     da variavel sPropriedadesParaFormat
        /// </param>
        /// <param name="strMensagemErroGetLista">
        ///     Mensagem de erro ao pesquisar todos. Será usado string.format() de acordo com as
        ///     propriedades da variavel sPropriedadesParaFormat
        /// </param>
        /// <param name="strMensagemErroRemover">
        ///     Mensagem de erro ao remover. Será usado string.format() de acordo com as
        ///     propriedades da variavel sPropriedadesParaFormat
        /// </param>
        /// <param name="strMensagemErroUpdate">
        ///     Mensagem de erro ao atualizar. Será usado string.format() de acordo com as
        ///     propriedades da variavel sPropriedadesParaFormat
        /// </param>
        public RepositorioBase(IContext context, string[] strPropriedadesParaFormat, string strMensagemErroAdicionar,
                               string strMensagemErroGet, string strMensagemErroGetLista, string strMensagemErroRemover,
                               string strMensagemErroUpdate)
        {
            _context = context;
            StrPropriedadesParaFormat = strPropriedadesParaFormat;
            StrMensagemErroAdicionar = strMensagemErroAdicionar;
            StrMensagemErroGet = strMensagemErroGet;
            StrMensagemErroGetLista = strMensagemErroGetLista;
            StrMensagemErroRemover = strMensagemErroRemover;
            StrMensagemErroUpdate = strMensagemErroUpdate;
        }

        private string StrMensagemErroAdicionar { get; }

        private string StrMensagemErroGet { get; }

        private string StrMensagemErroGetLista { get; }

        private string StrMensagemErroRemover { get; }

        private string StrMensagemErroUpdate { get; }

        private string[] StrPropriedadesParaFormat { get; }

        public bool Adicionar(params T[] objs)
        {
            foreach (T obj in objs)
            {
                try
                {
                    _context.Set<T>().Add(obj);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    try
                    {
                        new SparkException(e).TratarException(string.Format(StrMensagemErroAdicionar, GetValoresDasPropriedades(obj)), Enums.TipoMensagem.Erro);
                    }
                    catch
                    {
                        new SparkException(e).TratarException(string.Format(Mensagem.Ocorreu_um_erro_ao_adicionar_0_, nameof(T)), Enums.TipoMensagem.Erro);
                    }
                    return false;
                }
            }

            return true;
        }

        public T Get(int id)
        {
            try
            {
                T obj = _context.Set<T>().Find(id);

                return obj;
            }
            catch (Exception e)
            {
                try
                {
                    new SparkException(e).TratarException(string.Format(StrMensagemErroGet, id), Enums.TipoMensagem.Erro);
                }
                catch
                {
                    new SparkException(e).TratarException(string.Format(Mensagem.Ocorreu_um_erro_ao_pesquisar_0_, nameof(T)), Enums.TipoMensagem.Erro);
                }
                return null;
            }
        }

        public List<T> GetLista()
        {
            try
            {
                List<T> lstObjs = _context.Set<T>().ToList();

                return lstObjs;
            }
            catch (Exception e)
            {
                try
                {
                    new SparkException(e).TratarException(StrMensagemErroGetLista, Enums.TipoMensagem.Erro);
                }
                catch
                {
                    new SparkException(e).TratarException(string.Format(Mensagem.Ocorreu_um_erro_ao_pesquisar_todos_os_0_, nameof(T)), Enums.TipoMensagem.Erro);
                }
                return null;
            }
        }

        public bool Remover(params T[] objs)
        {
            foreach (T obj in objs)
            {
                try
                {
                    _context.Set<T>().Attach(obj);
                    _context.Set<T>().Remove(obj);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    try
                    {
                        new SparkException(e).TratarException(string.Format(StrMensagemErroRemover, GetValoresDasPropriedades(obj)), Enums.TipoMensagem.Erro);
                    }
                    catch
                    {
                        new SparkException(e).TratarException(string.Format(Mensagem.Ocorreu_um_erro_ao_remover_0_, nameof(T)), Enums.TipoMensagem.Erro);
                    }
                    return false;
                }
            }

            return true;
        }

        public bool Update(params T[] objs)
        {
            // Para poder mostrar no catch.
            T obj = null;
            try
            {
                foreach (T item in objs)
                {
                    obj = item;
                    _context.Set<T>().Attach(item);
                    DbEntityEntry<T> entry = _context.Entry(item);
                    entry.State = EntityState.Modified;
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                try
                {
                    new SparkException(e).TratarException(string.Format(StrMensagemErroUpdate, GetValoresDasPropriedades(obj)), Enums.TipoMensagem.Erro);
                }
                catch
                {
                    new SparkException(e).TratarException(string.Format(Mensagem.Ocorreu_um_erro_ao_atualizar_0_, nameof(T)), Enums.TipoMensagem.Erro);
                }
                return false;
            }
        }

        public bool Remover(params int[] ids)
        {
            return Remover(ids.Select(Get).ToArray());
        }

        private string[] GetValoresDasPropriedades(T obj)
        {
            return StrPropriedadesParaFormat.Select(propriedade => obj.GetType().GetProperty(propriedade).GetValue(obj).ToString()).ToArray();
        }
    }
}
