using System;
using System.IO;
using System.Reactive.Disposables;
using CsvHelper;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class TrackingPersistency : IDisposable
    {
        private readonly CompositeDisposable disposable = new CompositeDisposable();

        private readonly CsvWriter writer;

        public TrackingPersistency(TrackingConfiguration configuration, IRatingStream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (string.IsNullOrEmpty(configuration?.Persistency))
            {
                throw new ArgumentNullException(nameof(configuration.Persistency));
            }

            var subscription = stream.Stream.Subscribe(item => Process(item.Item1, item.Item2));
            disposable.Add(subscription);
            var streamWriter = new StreamWriter(configuration.Persistency, false);
            disposable.Add(streamWriter);
            writer = new CsvWriter(streamWriter);
            disposable.Add(writer);
            writer.WriteField("Date");
            writer.WriteField("Type");
            writer.WriteField("Id");
            writer.WriteField("Rating");
            writer.NextRecord();
        }

        private void Process(ITracker tracker, RatingRecord record)
        {
            lock (writer)
            {
                writer.WriteField(record.Date);
                writer.WriteField(tracker.Name);
                writer.WriteField(record.Id);
                writer.WriteField(record.Rating);
                writer.NextRecord();
                writer.Flush();
            }
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}
