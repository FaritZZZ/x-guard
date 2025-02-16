using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace XGuardLibrary
{
    public class SharedMemoryBool : IDisposable
    {
        private readonly string _memoryName;
        private readonly MemoryMappedFile _mmf;
        private readonly Mutex _mutex;
        private bool _disposed = false;

        public SharedMemoryBool(string memoryName)
        {
            _memoryName = memoryName;
            string mutexName = $"{memoryName}_Mutex";

            // Создаем или открываем мьютекс для синхронизации
            _mutex = new Mutex(false, mutexName);

            try
            {
                // Пытаемся создать новую разделяемую память
                _mmf = MemoryMappedFile.CreateNew(_memoryName, 1); // 1 байт для хранения bool
            }
            catch (IOException)
            {
                // Если область уже существует, открываем её
                _mmf = MemoryMappedFile.OpenExisting(_memoryName);
            }
        }

        public void Write(bool value)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SharedMemoryBool));

            _mutex.WaitOne(); // Захватываем мьютекс
            try
            {
                byte byteValue = (byte)(value ? 1 : 0);
                using (var stream = _mmf.CreateViewStream())
                {
                    stream.WriteByte(byteValue);
                }
            }
            finally
            {
                _mutex.ReleaseMutex(); // Освобождаем мьютекс
            }
        }

        public bool Read()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SharedMemoryBool));

            _mutex.WaitOne(); // Захватываем мьютекс
            try
            {
                using (var stream = _mmf.CreateViewStream())
                {
                    return stream.ReadByte() == 1; // Если 1, то true, иначе false
                }
            }
            finally
            {
                _mutex.ReleaseMutex(); // Освобождаем мьютекс
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _mmf?.Dispose();
                    _mutex?.Dispose();
                }

                _disposed = true;
            }
        }

        ~SharedMemoryBool()
        {
            Dispose(false);
        }
    }
}