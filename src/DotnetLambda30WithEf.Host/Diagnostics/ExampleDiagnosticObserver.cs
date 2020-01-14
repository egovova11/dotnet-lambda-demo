﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotnetLambda30WithEf.Host.Diagnostics
{
    public sealed class ExampleDiagnosticObserver :
        IObserver<DiagnosticListener>,
        IObserver<KeyValuePair<string, object>>
    {
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

        void IObserver<DiagnosticListener>.OnNext(DiagnosticListener diagnosticListener)
        {
            if (diagnosticListener.Name == "SqlClientDiagnosticListener")
            {
                var subscription = diagnosticListener.Subscribe(this);
                _subscriptions.Add(subscription);
            }
        }
        void IObserver<DiagnosticListener>.OnError(Exception error)
        { }
        void IObserver<DiagnosticListener>.OnCompleted()
        {
            _subscriptions.ForEach(x => x.Dispose());
            _subscriptions.Clear();
        }

        void IObserver<KeyValuePair<string, object>>.OnNext(KeyValuePair<string, object> pair)
        {
            Write(pair.Key, pair.Value);
        }
        void IObserver<KeyValuePair<string, object>>.OnError(Exception error)
        { }
        void IObserver<KeyValuePair<string, object>>.OnCompleted()
        { }
        private void Write(string name, object value)
        {
            Console.WriteLine(name);
            Console.WriteLine(value);
            Console.WriteLine();
        }
    }
}