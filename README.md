Smart-X IoT Mesh Ecosystem - IoT PoE Part 1 Project Description

This folder comprises the Data Ingestion and Validation Gateway for the Smart-X IoT ecosystem (Part 1 of the Portfolio of Evidence). The program is made with a modern Blazor WebAssembly Hosted configuration, so the ASP.NET Core backend API and the Blazor client app are deployed jointly as one app. System administratrices can use the app to register IoT sensors, view live simulated sensor data display, and store hardware configs logs/images at certain sensor profiles through this interface.

Tech Specs & Prerequisites

Language:.NET 8.0 (or.NET 10.0) SDK - IDE: Visual Studio 2022 ("ASP.NET and web development" workload)

UI: Blazor WebAssembly - Service Layer: ASP.NET Core Web API

Control changes: Git / GitHub - Recommended (Optional): Docker Desktop

Getting Started & Installation

1. Clone the Project Launch your command prompt (or run Git Bash), and get this project onto your computer:

bash git clone https://github.com/st10451361/SmartX.git
Then change directory to where the project has been cloned:

bash cd SmartX
---

## Part 2: Real-Time Processing & Device Management

### How to Test Part 2 Features
To test the advanced data structures implemented in Part 2, follow these steps:

1. **Navigation:** Click on the new **"Command Stream"** link in the left sidebar (or the yellow "Enter Command Center" card on the Home page).
2. **Dictionaries (O(1) Lookups):** The device list loads instantly using a `Dictionary<string, string>` where the MAC Address is the key. 
3. **Predictive Action Engine:** Type "Moisture" into the search bar and click **Search**. The "Automated Insights" box at the top will update to suggest checking the Hydroponics Pump, based on your search history.
4. **Priority Queues & Stacks (Undo):** 
   * Click the yellow **"Toggle"** button next to a device. This pushes a command onto the `Stack<DeviceCommand>`.
   * Click the red **"↩ Undo Last Command"** button. The system pops the last command off the stack and reverts it.
5. **HashSets (Unique Alerts):** The backend `SmartXDataManager` uses a `HashSet<string>` to track active errors, preventing duplicate disconnect alerts from being processed for the same sensor.

### Advanced Data Structures Implemented:
*   `Queue<T>`: Standard telemetry ingestion (FIFO).
*   `List<T>`: Critical alert priority queue (Bypasses standard queue).
*   `Stack<T>`: Manual command history (LIFO for Undo).
*   `Dictionary<TKey, TValue>`: Active device lookup by MAC address.
*   `SortedDictionary<TKey, TValue>`: Historical logs sorted by timestamp.
*   `HashSet<T>`: Active error state tracking (prevents duplicates).
