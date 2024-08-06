"use client";

import React, { useState, useEffect } from "react";
import AccountHandler from "@/components/AccountHandler";
import MessageSideBar from "@/components/chatInterface/MessageSideBar";
import { Conversation } from "@/types/conversation";
import ConversationView from "@/components/chatInterface/ConversationView";

const Dashboard: React.FC = () => {
  const [conversations, setConversations] = useState<Conversation[]>([]);
  const [isSideBarOpen, setIsSideBarOpen] = useState(true);
  const [selectedConversation, setSelectedConversation] =
    useState<Conversation | null>(null);

  useEffect(() => {
    const fetchConversations = async () => {
      try {
        const userToken = localStorage.getItem("token");
        const userName = localStorage.getItem("userName");
        if (!userToken || !userName) {
          throw new Error("User not logged in");
        }

        const response = await fetch(
          `http://localhost:7252/api/conversation?userName=${encodeURIComponent(
            userName
          )}`,
          {
            headers: {
              Authorization: `Bearer ${userToken}`,
            },
          }
        );

        if (!response) {
          throw new Error("Network response was not ok");
        }

        const result = await response.json();
        console.log("API Response:", result);
        setConversations(result.data);
        console.log(conversations);
      } catch (error) {
        console.error("Failed to fetch conversations:", error);
      }
    };

    fetchConversations();
  }, []);

  const toggleSideBar = () => {
    setIsSideBarOpen(!isSideBarOpen);
  };

  const handleConversationClick = (conversation: Conversation) => {
    setSelectedConversation(conversation);
    // On mobile, close the sidebar when a conversation is selected
    console.log("Selected conversation:", conversation.conversationName);
    if (window.innerWidth < 768) {
      setIsSideBarOpen(false);
    }
  };

  return (
    <div className="min-h-screen bg-background flex flex-col">
      <AccountHandler />
      <main className="flex flex-grow overflow-hidden">
        <MessageSideBar
          conversations={conversations}
          isOpen={isSideBarOpen}
          toggleSidebar={toggleSideBar}
          onConversationClick={handleConversationClick}
        />
        <div className="flex-grow overflow-hidden">
          {selectedConversation ? (
            <ConversationView conversation={selectedConversation} />
          ) : (
            <div className="flex items-center justify-center h-full">
              <p className="text-gray-500">
                Select or Create a conversation to start chatting
              </p>
            </div>
          )}
        </div>
      </main>
    </div>
  );
};

export default Dashboard;
