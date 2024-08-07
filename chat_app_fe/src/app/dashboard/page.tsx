"use client";

import React, { useState, useEffect } from "react";
import AccountHandler from "@/components/AccountHandler";
import MessageSideBar from "@/components/chatInterface/MessageSideBar";
import { Conversation } from "@/types/conversation";
import ConversationView from "@/components/chatInterface/ConversationView";
import AddConversationModal from "@/components/chatInterface/AddConversationModal";

const Dashboard: React.FC = () => {
  const [conversations, setConversations] = useState<Conversation[]>([]);
  const [isSideBarOpen, setIsSideBarOpen] = useState(true);
  const [selectedConversation, setSelectedConversation] =
    useState<Conversation | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    const fetchConversations = async () => {
      try {
        const userToken = localStorage.getItem("token");
        const userName = localStorage.getItem("userName");
        if (!userToken || !userName) {
          throw new Error("User not logged in");
        }

        const response = await fetch(
          `https://localhost:7252/api/conversation?userName=${encodeURIComponent(
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
        setConversations(result.data);
      } catch (error) {
        console.error("Failed to fetch conversations:", error);
        alert("An error occurred: " + error);
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

  const openModal = () => setIsModalOpen(true);
  const closeModal = () => setIsModalOpen(false);

  const handleCreateConversation = async (userName2: string, conversationName: string) => {
    try {
      const userToken = localStorage.getItem('token');
      if (!userToken) throw new Error('User not logged in');

      const userName1 = localStorage.getItem('userName')

      const response = await fetch('https://localhost:7252/api/conversation', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${userToken}`,
        },
        body: JSON.stringify({ userName1, userName2, conversationName }),
      });

      if (!response.ok) {
        throw new Error('Network response was not ok');
      }

      const result = await response.json();
      setConversations((prev) => [...prev, result.data]);
      closeModal();
    } catch (error) {
      console.error('Failed to create conversation:', error);
      alert("An error occurred: " + error);
    }
  };

  return (
    <div className="flex flex-col bg-background">
      <AccountHandler />
      <main className="flex flex-grow">
        <div className="relative flex flex-col w-64">
          <MessageSideBar
            conversations={conversations}
            isOpen={isSideBarOpen}
            toggleSidebar={toggleSideBar}
            onConversationClick={handleConversationClick}
            onAddConversationClick={openModal}
          />
        </div>
        <div className="flex-grow flex flex-col mb-5">
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
      <AddConversationModal
        isOpen={isModalOpen}
        onClose={closeModal}
        onCreate={handleCreateConversation}
      />
    </div>
  );
};

export default Dashboard;
