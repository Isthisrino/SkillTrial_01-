using System;

namespace Elder.Core.Common.BaseClasses
{
    public abstract class DisposableBase : IDisposable
    {
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                DisposeManagedResources();

            DisposeUnmanagedResources();
            CompleteDispose();
            _disposed = true;
        }
        protected virtual void CompleteDispose()
        {

        }
        /// <summary>
        /// �����Ǵ� ���ҽ��� �����մϴ�.
        /// ����:
        /// - IDisposable �������̽��� ������ ��ü (��: Stream, HttpClient, Timer ��)
        /// - �̺�Ʈ ���� ���� (��: SomeEvent -= Handler)
        /// - Unity ��ü ���� ���� (�ʿ�� null ó�� ��)
        /// </summary>
        protected abstract void DisposeManagedResources();
        /// <summary>
        /// ����� ���ҽ��� �����մϴ�.
        /// ����:
        /// - ���� �ڵ�, ������ �ڵ�, ����Ƽ�� ������ ����
        /// - Marshal.AllocHGlobal()�� �Ҵ��� �޸� ����
        /// - Unity�� NativeArray.Dispose() �� ����Ƽ�� ���ҽ� ����
        /// �Ϲ������� �� �޼���� ���� ����Ƽ�� API�� ������� �ʴ� ��� ����Ӵϴ�.
        /// </summary>
        protected abstract void DisposeUnmanagedResources();

        ~DisposableBase()
        {
            Dispose(false);
        }
    }
}
