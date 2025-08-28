
# 📍 Mobile Location Sharing

A simple C# application that demonstrates how to share a device’s location over a network. This project is ideal for learning how to implement basic location tracking and communication between devices using sockets.

---

## 🧭 Overview

This app showcases:

- 📡 Retrieving device location
- 🔗 Sending location data over a local network
- 🖥️ Basic socket programming in C#
- 🧪 Lightweight demo for testing location sharing

---

## 🚀 Getting Started

### Prerequisites

- Visual Studio (recommended)
- .NET Framework or .NET Core SDK
- Two devices on the same local network (for testing)

### How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/supunsarachitha/Mobile-location-sharing.git
   ```

2. Open `findU.sln` in Visual Studio.

3. Build and run the project.

4. Configure IP and port settings to match your local network setup.

---

## 📁 Project Structure

```
Mobile-location-sharing/
├── findU.sln                 # Solution file
├── .gitignore                # Git settings
├── .gitattributes            # Git attributes
└── README.md                 # Project documentation
```

---

## 🧪 Sample Code Snippet

```csharp
TcpClient client = new TcpClient("192.168.1.100", 9000);
NetworkStream stream = client.GetStream();
string location = "Latitude: 45.5, Longitude: -73.6";
byte[] data = Encoding.ASCII.GetBytes(location);
stream.Write(data, 0, data.Length);
```

---

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## 👤 Author

Created by [Supun Sarachitha](https://github.com/supunsarachitha). Feel free to fork, contribute, or reach out!

---

## 🙌 Contributions

Pull requests are welcome! If you’d like to add features like map integration, real-time updates, or mobile UI enhancements, go ahead and submit a PR.

---

## 📬 Contact

For questions or suggestions, open an issue or connect via GitHub.
