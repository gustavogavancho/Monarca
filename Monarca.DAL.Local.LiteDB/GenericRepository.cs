using LiteDB;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Monarca.DAL.Local.LiteDB
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        string dbName;
        string direccion;
        string tableName;
        private ILiteDatabase db;

        public GenericRepository()
        {
            direccion = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            VerificarSiLaDireccionExiste(direccion);
            dbName = $@"{direccion}\Monarca.db";
            tableName = typeof(T).Name;
            db = new LiteDatabase(@"Filename=Soyuz.db; Connection=shared");
        }

        private void VerificarSiLaDireccionExiste(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private ILiteCollection<T> Collection() => db.GetCollection<T>(tableName);


        public string Error { get; private set; }

        public IEnumerable<T> Read
        {
            get
            {
                try
                {
                    return Collection().FindAll();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public T Create(T entidad)
        {
            entidad.Id = ObjectId.NewObjectId().ToString();
            entidad.FechaHoraCreacion = DateTime.Now;
            try
            {
                Collection().Insert(entidad);
                this.Error = "";
                return entidad;
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                return null;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                return Collection().Delete(id);
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                return false;
            }
        }
        public IEnumerable<T> Query(Expression<Func<T, bool>> predicado) => this.Read.Where(predicado.Compile());

        public T SearchById(string id) => this.Read.Where(e => e.Id.ToString() == id).FirstOrDefault();

        public T Update(T entidad)
        {
            try
            {
                Collection().Update(entidad);

                this.Error = "";
                return entidad;
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                return null;
            }
        }
    }
}
