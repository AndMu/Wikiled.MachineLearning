using System;
using System.IO;
using System.Reactive.Disposables;
using CsvHelper;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class TrackingPersistency : IDisposable
    {
        private TrackingConfiguration configuration;

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        private CsvWriter writer;

        public TrackingPersistency(TrackingConfiguration configuration, IRatingStream stream)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (string.IsNullOrEmpty(configuration.Persistency))
            {
                throw new ArgumentOutOfRangeException(nameof(configuration.Persistency));
            }

            var subscription = stream.Stream.Subscribe(item => Process(item.Item1, item.Item2));
            disposable.Add(subscription);
            var streamWriter = new StreamWriter(configuration.Persistency, false);
            disposable.Add(streamWriter);
            writer = new CsvWriter(streamWriter);
            disposable.Add(writer);
            writer.WriteField("Id");
            writer.WriteField("Date");
            writer.WriteField("Type");
            writer.WriteField("Rating");
            writer.NextRecord();
        }

        private void Process(ITracker tracker, RatingRecord record)
        {
            lock (writer)
            {
                writer.WriteField("Id");
                writer.WriteField("Date");
                writer.WriteField("Type");
                writer.WriteField("Rating");
                writer.NextRecord();
                writer.Flush();

            }
        }

        public void Dispose()
        {
            subscription?.Dispose();
        }
    }
}
