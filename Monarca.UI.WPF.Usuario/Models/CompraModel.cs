using Monarca.UI.WPF.Usuario.Helpers;
using System;

namespace Monarca.UI.WPF.Usuario.Models
{
    public class CompraModel 
    {
        public string Id { get; set; }
        public string IdProducto { get; set; }
        public string IdProveedor { get; set; }
        public string NombreProducto { get; set; }
        public string NombreProveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal Total { get; set; }

        //private string _id;

        //public string Id
        //{
        //    get => _id;
        //    set => SetProperty(ref _id, value);
        //}

        //private string _idProducto;
        //public string IdProducto
        //{
        //    get => _idProducto;
        //    set => SetProperty(ref _idProducto, value);
        //}

        //private string _idProveedor;
        //public string IdProveedor
        //{
        //    get => _idProveedor;
        //    set => SetProperty(ref _idProveedor, value);
        //}

        //private string _nombreProducto;
        //public string NombreProducto
        //{
        //    get => _nombreProducto;
        //    set => SetProperty(ref _nombreProducto, value);
        //}

        //private string _nombreProveedor;
        //public string NombreProveedor
        //{
        //    get => _nombreProveedor;
        //    set => SetProperty(ref _nombreProveedor, value);
        //}

        //private DateTime _fechaCompra;
        //public DateTime FechaCompra
        //{
        //    get => _fechaCompra;
        //    set => SetProperty(ref _fechaCompra, value);
        //}

        //private decimal _cantidad;
        //public decimal Cantidad
        //{
        //    get => _cantidad;
        //    set => SetProperty(ref _cantidad, value);
        //}

        //private decimal _precioCosto;

        //public decimal PrecioCosto
        //{
        //    get => _precioCosto;
        //    set => SetProperty(ref _precioCosto, value);
        //}

        //private decimal _total;
        //public decimal Total
        //{
        //    get => _total;
        //    set => SetProperty(ref _total, value);
        //}
    }
}
