# SignalR Basic Flow
These steps form the basic flow of SignalR, allowing real-time communication and updates between the server and connected clients:

## 1. Create SignalR Hub
The first step is to create a SignalR Hub, which acts as a central communication point in the SignalR application. The Hub is responsible for handling incoming and outgoing messages between the server and connected clients.
![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/8c13bf83-f68a-4d63-9d37-a86b5f60df5f)

![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/14f631f3-52cb-4925-94fe-e68a448bf230)

## 2. Add Methods to Hub
In this step, you add methods to the SignalR Hub. These methods define the actions that can be invoked by clients or the server. Each method represents a specific functionality or operation that can be performed through SignalR.
![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/f1dd2eab-19ee-43d5-940a-8457b562928e)

## 3. Add Client side SignalR
To enable SignalR functionality on the client side, you need to include the SignalR JavaScript library in your web application. This library provides the necessary functions and utilities to establish a connection with the SignalR Hub and interact with it.
![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/1eec431e-13f7-4853-8f42-75305867482d)

## 4. Connect to SignalR Hub from Client JS
Using the SignalR library on the client side, you establish a connection to the SignalR Hub. This connection enables real-time communication between the server and the connected clients. The client-side code typically includes a method to establish the connection, passing the necessary configuration options.
![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/7c366f8a-04eb-4f8a-92db-b87cf775dae9)

## 5. Call SignalR Hub method
Once the client is connected to the SignalR Hub, it can invoke methods defined in the Hub. This allows the client to send requests or trigger specific actions on the server-side. The client-side code can call these methods using the SignalR library's provided functions.
![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/db33bee5-1627-411d-b48b-5cf8e09b5400)

## 6. SignalR Hub invokes method in Client JS to notify clients
When the SignalR Hub receives a method invocation from a client, it can perform any necessary processing and invoke other methods in the connected clients. This enables the server to send real-time updates or notifications to the clients. The SignalR Hub uses the client's connection to invoke the corresponding client-side method.
![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/13879a6d-4d34-42ad-9cc9-d2113591b614)

## 7. Client receives update from SignalR hub and performs action
Finally, the client receives the update or notification sent by the SignalR Hub. The client-side method invoked by the Hub can handle the received data and perform the necessary actions, such as updating the user interface, displaying notifications, or triggering further operations based on the received update.
![image](https://github.com/HordeBies/SignalR-Learning/assets/73644073/8de12c3c-2bd4-43f3-a691-c5284f77a5bc)
