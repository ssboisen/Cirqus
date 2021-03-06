﻿using System;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Numbers;

namespace d60.Cirqus.Events
{
    [Serializable]
    public abstract class DomainEvent
    {
        /// <summary>
        /// Provides the keys of various predefined metadata elements with special meaning
        /// </summary>
        public static class MetadataKeys
        {
            public const string GlobalSequenceNumber = "gl_seq";
            public const string SequenceNumber = "seq";
            public const string AggregateRootId = "root_id";
            public const string BatchId = "batch_id";
            public const string TimeUtc = "time_utc";
            public const string Owner = "owner";
            public const string Type = "type";
            public const string RootVersion = "root_ver";
            public const string EventVersion = "evt_ver";
        }

        protected DomainEvent()
        {
            Meta = new Metadata();
        }

        public Metadata Meta { get; internal set; }

        internal void AssignSequenceNumber(int seq)
        {
            Meta[MetadataKeys.SequenceNumber] = seq.ToString(Metadata.NumberCulture);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}/{2}/{3})", 
                GetType().Name, 
                Meta.ContainsKey(MetadataKeys.AggregateRootId) ? Meta[MetadataKeys.AggregateRootId] : "?",
                Meta.ContainsKey(MetadataKeys.SequenceNumber) ? Meta[MetadataKeys.SequenceNumber] : "?",
                Meta.ContainsKey(MetadataKeys.GlobalSequenceNumber) ? Meta[MetadataKeys.GlobalSequenceNumber] : "?");
        }
    }

    [Serializable]
    public abstract class DomainEvent<TOwner> : DomainEvent where TOwner : AggregateRoot
    {
    }
}
