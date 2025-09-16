# Omni-Inbox & Campaign Manager (WhatsApp Business Integration)

A centralized messaging platform that connects WhatsApp Business to human agents and bots, allowing businesses to automate conversations, route chats, and track performance — all in real-time.

---

## 🔥 Key Features

- **Omni-Inbox**: Unified interface for all incoming WhatsApp messages with assignment, timestamps, and statuses.
- **Template Bot**: Structured template replies with buttons (e.g., Talk to Agent, Booking Info, Service Request).
- **Smart Routing**: Automatically assign chats to relevant teams based on intent or user choices.
- **Human Handoff**: Seamless switch from bot to agent with audit logs and real-time messaging.
- **CSAT Feedback**: Trigger customer satisfaction surveys automatically after closure.
- **Campaigns & Broadcasts**: Send WhatsApp templates to user segments and measure interaction.
- **Persistent History**: Store messages and media beyond WhatsApp's default expiry limits.
- **Analytics Dashboard**: Track agent productivity, response time, CSAT scores, and campaign metrics.

---

## 💡 The Problem

Businesses using WhatsApp struggle with:

- Scattered conversations across agents/devices.
- No message routing or ownership.
- No visibility on agent performance.
- Media expiration limits that break history.

---

## ✅ The Solution

This platform centralizes and automates the entire messaging workflow:

- Receives messages via WhatsApp Business API in real-time.
- Engages users with structured bots.
- Routes them smartly to the right team.
- Logs the full conversation flow and user feedback.

---

## 🛠 Tech Stack

- ASP.NET Core (Backend)
- Entity Framework Core
- SignalR for real-time messaging
- SQL Server
- React.js (Frontend)
- WhatsApp Business Cloud API

---

## ⚠️ Project Notes

This project was fully designed and implemented by me under tight deadlines.  
While currently built as a monolith MVP and not yet distributed, the code demonstrates:

- Real-time messaging logic
- Smart bot-to-human routing
- Modular bot/template engine
- Team-based conversation ownership

Some parts may require architectural improvements (e.g., factories, abstraction), and additional social channels (like Instagram or Facebook) can be integrated using the same architecture.

---

## 🚀 Getting Started

> **Note:** This project was not intended for public release, so deployment scripts may be incomplete.

1. Clone the repo
2. Configure WhatsApp Business API credentials
3. Set connection string in `appsettings.json`
4. Run backend and frontend separately
5. Navigate to the web dashboard and start testing the bot flow

---

## 🚧 Future Work

- Convert to distributed architecture (Bot Service, Routing Service, Campaign Service)
- Add multi-channel support (Facebook, Instagram, etc.)
- Improve template builder UX
- Add message queueing and retry logic
- Role-based access for more granular control

---

## 📌 License

Private / Internal Demo — Not for commercial use (yet).
