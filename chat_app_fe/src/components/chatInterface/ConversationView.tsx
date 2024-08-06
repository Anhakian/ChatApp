import React, { useEffect, useState } from "react";
import { Conversation } from "@/types/conversation";
import { Message } from "@/types/message";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import axios from "axios";

type Props = {
  conversation: Conversation;
};

const ConversationView: React.FC<Props> = ({ conversation }) => {
  const [newMessage, setNewMessage] = useState("");
  const [messages, setMessages] = useState<Message[]>([]);
  const [connection, setConnection] = useState<HubConnection | null>(null);

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("http://localhost:7252/api/chat")
      .build();

    setConnection(connect);

    const startConnection = async () => {
      try {
        await connect.start();
        console.log("Connected to SignalR");

        connect.on("ReceiveMessage", async (message: Message) => {
          // Fetch display name
          const userResponse = await axios.get(
            `http://localhost:7252/api/user/${message.senderId}`
          );
          console.log(userResponse);
          const displayName = userResponse.data.displayName;

          setMessages((prevMessages) => [
            ...prevMessages,
            { ...message, senderDisplayName: displayName },
          ]);
        });
      } catch (e) {
        console.error("SignalR Connection Error: ", e);
        setTimeout(startConnection, 5000);
      }
    };

    startConnection();

    return () => {
      connect.stop();
    };
  }, [conversation.id]);

  const handleSendMessage = async () => {
    if (newMessage.trim() === "") return;

    try {
      if (connection) {
        await connection.invoke("SendMessage", conversation.id, newMessage);
        setNewMessage("");
      }
    } catch (e) {
      console.error("Error sending message: ", e);
    }
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      handleSendMessage();
    }
  };

  return (
    <div className="h-full p-4">
      <h2 className="text-2xl font-bold mb-4">
        {conversation.conversationName}
      </h2>
      <div className="bg-white rounded-lg p-4 shadow mb-4">
        <div className="h-80 overflow-y-auto">
          {messages.map((msg) => (
            <div key={msg.messageId} className="mb-2">
              <strong>{msg.senderDisplayName}:</strong> {msg.message}
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
            className="ml-2 bg-blue-500 text-white rounded px-4 py-2"
          >
            Send
          </button>
        </div>
      </div>
    </div>
  );
};

export default ConversationView;
