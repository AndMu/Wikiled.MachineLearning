﻿using System;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace Wikiled.MachineLearning.Mathematics.Tracking
{
    public class TrackingStream : ITrackingRegister, IRatingStream
    {
        private readonly CompositeDisposable disposable = new CompositeDisposable();

        private readonly Subject<(ITracker, RatingRecord)> stream = new Subject<(ITracker, RatingRecord)>();

        public IObservable<(ITracker Tracker, RatingRecord Rating)> Stream => stream;

        public void Dispose()
        {
            stream.OnCompleted();
            stream.Dispose();
            disposable.Dispose();
        }

        public void Register(ITracker tracker)
        {
            if (tracker == null)
            {
                throw new ArgumentNullException(nameof(tracker));
            }

            var subscribe = tracker.Ratings.Subscribe(item =>
            {
                stream.OnNext((tracker, item));
            });

            disposable.Add(subscribe);
        }
    }
}
