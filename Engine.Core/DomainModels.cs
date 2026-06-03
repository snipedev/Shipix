namespace Engine.Core
{
    public sealed record TrackingId
    {
        public string Value { get; }

        public TrackingId(string value)
        {
            if(!string.IsNullOrWhiteSpace(value))
            {
                Value = value;
            }
            else
            {
                throw new ArgumentException("Tracking ID cannot be null or whitespace.", nameof(value));
            }
        }
        public override string ToString() => Value;
    }

    public sealed record Carrier
    {
        public string Value { get; }
        private Carrier(string value) => Value = value;
        public static readonly Carrier FedEx = new("FEDEX");
        public static readonly Carrier Ups = new("UPS");

        public static Carrier From(string value) =>
            value.ToUpper() switch
            {
                "FEDEX" => FedEx,
                "UPS" => Ups,
                _ => throw new InvalidOperationException($"Unsupported carrier: {value}")
            };

        public override string ToString() => Value;
    }

    public sealed record EventTimestamp
    {
        public DateTime Value { get; }

        public EventTimestamp(DateTime value)
        {
            if (value == default)
                throw new ArgumentException("Timestamp cannot be default");

            Value = value;
        }

        public static EventTimestamp UtcNow() => new(DateTime.UtcNow);

        public override string ToString() => Value.ToString("O");
    }


    public abstract record ShipmentState
    {
        private ShipmentState() { }

        public sealed record Created(DateTime Timestamp) : ShipmentState;

        public sealed record InTransit(string Location, DateTime Timestamp) : ShipmentState;

        public sealed record OutForDelivery(string Courier, DateTime Timestamp) : ShipmentState;

        public sealed record Delivered(DateTime Timestamp, string ReceivedBy) : ShipmentState;

        public sealed record Exception(string Reason, DateTime Timestamp) : ShipmentState;
    }



    public sealed record TrackingEvent
    {
        public TrackingId TrackingId { get; }
        public Carrier Carrier { get; }
        public ShipmentState State { get; }
        public EventTimestamp OccurredAt { get; }

        private TrackingEvent(
            TrackingId trackingId,
            Carrier carrier,
            ShipmentState state,
            EventTimestamp occurredAt)
        {
            TrackingId = trackingId;
            Carrier = carrier;
            State = state;
            OccurredAt = occurredAt;
        }

        public static TrackingEvent Create(
            TrackingId trackingId,
            Carrier carrier,
            ShipmentState state,
            EventTimestamp occurredAt)
        {
            if (trackingId is null) throw new ArgumentNullException(nameof(trackingId));
            if (carrier is null) throw new ArgumentNullException(nameof(carrier));
            if (state is null) throw new ArgumentNullException(nameof(state));

            return new TrackingEvent(trackingId, carrier, state, occurredAt);
        }
    }

}
