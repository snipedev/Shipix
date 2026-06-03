# 🚀 ShipFlow

> High-performance webhook ingestion engine for multi-carrier shipment tracking  
> Built with strict domain modeling, exhaustive pattern matching, and zero-dependency core logic.

---

## ⚡ Overview

**ShipFlow** is a lightweight, extensible engine for ingesting and normalizing webhook events from logistics providers like **FedEx** and **UPS**.

It focuses on:

- ✅ Pure domain modeling  
- ✅ Closed-state lifecycle tracking (union types)  
- ✅ Safe, exhaustive parsing  
- ✅ Decoupled contract schemas  
- ✅ Minimal API surface  

---

## 🧠 Architecture

```
                ┌───────────────────┐
                │    Engine.Api     │
                │ (HTTP Layer)      │
                └────────┬──────────┘
                         │
        ┌───────────────┴────────────────┐
        │                                │
┌───────────────┐              ┌────────────────────┐
│ Engine.Core   │              │ Engine.Contracts   │
│ (Domain)      │              │ (DTO Schemas)      │
└───────────────┘              └────────────────────┘
```

---

## 📦 Projects

### 🔹 `Engine.Core`
- Domain models  
- Shipment lifecycle (union types)  
- Parsing logic  
- Zero external dependencies  

---

### 🔹 `Engine.Contracts`
- Carrier-specific DTOs  
- Mirrors webhook payloads exactly  
- Versioning-ready  

---

### 🔹 `Engine.Api`
- Minimal API layer  
- Handles inbound webhook requests  
- Delegates parsing to Core  

---

### 🔹 `Engine.Tests` *(optional but recommended)*
- Parser validation  
- State transition tests  

---

## 🔄 Shipment Lifecycle Model

Closed hierarchy using record types:

```csharp
ShipmentState:
  ├── Created
  ├── InTransit
  ├── OutForDelivery
  ├── Delivered
  └── Exception
```

✅ Ensures:
- No invalid states  
- Exhaustive pattern matching  
- Compile-time safety  

---

## 🔌 APIs

### 📍 Webhook Endpoints

```
POST /webhooks/fedex
POST /webhooks/ups
```

---

### ✅ Example: FedEx Request

```json
{
  "trackingNumber": "123456",
  "status": "IN_TRANSIT",
  "location": "Memphis",
  "eventTime": "2026-06-01T10:00:00Z"
}
```

---

### ✅ Response

```json
{
  "trackingNumber": "123456",
  "carrier": "FEDEX",
  "state": "InTransit",
  "occurredAt": "2026-06-01T10:00:00Z"
}
```

---

## 🧩 Parsing Strategy

- Uses **C# pattern matching**  
- Fully **type-safe dispatch**  
- No reflection / dynamic usage  

```csharp
payload switch
{
    FedExWebhookDto fedex => ParseFedEx(fedex),
    UpsWebhookDto ups => ParseUps(ups),
    _ => throw new UnsupportedPayloadException()
};
```

---

## 🛠️ Getting Started

### ✅ Prerequisites
- .NET 8 SDK  

---

### ✅ Setup

```bash
git clone https://github.com/your-org/shipflow.git
cd shipflow

dotnet build
dotnet run --project Engine.Api
```

---

### ✅ Test APIs

```
POST http://localhost:5000/webhooks/fedex
POST http://localhost:5000/webhooks/ups
```

---

## 🧪 Testing

```bash
dotnet test
```

Focus areas:
- Parser correctness  
- State transitions  
- Edge cases  

---

## 📈 Future Roadmap

- 🔄 Event streaming (Kafka / EventStore)  
- 🧾 Idempotency & deduplication  
- 🧠 State machine validation  
- 🔌 Plug-in carrier support  
- 📊 Analytics pipeline  

---

## 🎯 Design Principles

- **Domain First** — business logic is isolated and pure  
- **Explicit over implicit** — no magic, no hidden behavior  
- **Fail fast** — invalid payloads are rejected early  
- **Extensible by design** — easy to add new carriers  

---

## 🤝 Contributing

PRs are welcome. Please ensure:

- ✅ Parser logic is exhaustive  
- ✅ Domain remains dependency-free  
- ✅ Tests cover new states and carriers  

---

## 📜 License

MIT License  

---

## 💡 Inspiration

Built with ideas from:

- Event-driven architecture  
- Railway-oriented programming  
- Strongly typed domain modeling  
