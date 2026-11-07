using DinkToPdf;
using DinkToPdf.Contracts;
using System.Collections.Concurrent;

namespace NovaCare.Helpers
{
    public class STASynchronizedConverter : BasicConverter
    {
        private readonly Thread _thread;
        private readonly BlockingCollection<Action> _queue = new();

        public STASynchronizedConverter(ITools tools) : base(tools)
        {
            _thread = new Thread(Run) { IsBackground = true };
            _thread.SetApartmentState(ApartmentState.STA); // The critical fix for OLE 80010106
            _thread.Start();
        }

        public override byte[] Convert(IDocument document)
        {
            byte[] result = null;
            using var waitHandle = new ManualResetEventSlim(false);
            _queue.Add(() => {
                result = base.Convert(document);
                waitHandle.Set();
            });
            waitHandle.Wait();
            return result;
        }

        private void Run()
        {
            foreach (var action in _queue.GetConsumingEnumerable()) action();
        }
    }
}
