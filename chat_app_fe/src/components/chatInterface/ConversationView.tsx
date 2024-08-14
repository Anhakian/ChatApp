import React, { useEffect, useState } from "react";
import { Conversation } from "@/types/conversation";
import { Message } from "@/types/message";
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
} from "@microsoft/signalr";
import { Send } from "react-feather";
import axios from "axios";

type Props = {
  conversation: Conversation;
  userId: string;
};

const ConversationView: React.FC<Props> = ({ conversation, userId }) => {
  const [newMessage, setNewMessage] = useState("");
  const [messages, setMessages] = useState<Message[]>([]);
  const [connection, setConnection] = useState<HubConnection | null>(null);

  // Fetch messages when conversation changes
  const fetchMessages = async () => {
    try {
      const response = await axios.get(
        `http://localhost:7252/api/conversationmessage/?conversationId=${conversation.id}`
      );
      setMessages(response.data.data);
    } catch (error) {
      console.error("Error fetching messages:", error);
    }
  };

  useEffect(() => {
    fetchMessages();
  }, [conversation.id]);

  // Handle SignalR connection setup
  useEffect(() => {
    const setupConnection = async () => {
      try {
        const newConnection = new HubConnectionBuilder()
          .withUrl("http://localhost:7252/api/chat", {
            skipNegotiation: true,
            transport: HttpTransportType.WebSockets,
          })
          .withAutomaticReconnect([0, 2000, 10000, 30000])
          .build();

        newConnection.on("ReceiveMessage", (message: Message) => {
          setMessages((prevMessages) => [...prevMessages, message]);
        });

        newConnection.onclose((error) => {
          console.error("Connection closed:", error);
        });

        await newConnection.start();
        setConnection(newConnection);
        console.log("Connected to SignalR hub");
      } catch (error) {
        console.error("Error connecting to SignalR hub:", error);
      }
    };

    setupConnection();

    // Cleanup connection on component unmount
    return () => {
      if (connection) {
        connection.stop().then(() => console.log("Connection stopped"));
      }
    };
  }, [conversation.id]); // Only re-setup connection when conversation changes

  // Handle sending a new message
  const handleSendMessage = async () => {
    if (connection && newMessage.trim()) {
      try {
        await connection.send("SendMessage", {
          content: newMessage,
          userId,
          conversationId: conversation.id,
        });
        setNewMessage(""); // Clear input after sending
      } catch (error) {
        console.error("Error sending message:", error);
      }
    }
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      handleSendMessage();
    }
  };

  return (
    <div className="flex flex-col h-[90vh] w-full">
      <h2 className="text-2xl text-center mt-4 font-bold text-white mb-4">
        {conversation.conversationName}
      </h2>
      <div className="bg-black rounded-lg p-4 shadow flex-grow flex flex-col overflow-hidden">
        <div className="flex-grow overflow-y-auto">
          {messages.map((msg) => (
            <div key={msg.messageId} className="mb-2 text-white">
              <strong className="text-white">{msg.senderDisplayName}:</strong>{" "}
              {msg.message}
            </div>
          ))}
        </div>
        <div className="flex items-center mt-4">
          <input
            type="text"
            value={newMessage}
            onChange={(e) => setNewMessage(e.target.value)}
            onKeyDown={handleKeyDown}
            className="border rounded p-2 flex-1"
            placeholder="Type your message..."
          />
          <button
            onClick={handleSendMessage}
            className="ml-2 bg-blue-500 text-white rounded px-4 py-2 disabled:bg-gray-400"
          >
            <Send size={20} />
          </button>
        </div>
      </div>
    </div>
  );
};

export default ConversationView;
