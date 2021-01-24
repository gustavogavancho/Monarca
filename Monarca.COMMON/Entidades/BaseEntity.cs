using System;

namespace Monarca.COMMON.Entidades
{
    public class BaseEntity : IDisposable
    {
        private bool _isDisposed;

        public string Id { get; set; }
        public DateTime FechaHoraCreacion { get; set; }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                this._isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
