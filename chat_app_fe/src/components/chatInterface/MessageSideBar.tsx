import React from "react";
import ConversationCard from "./ConversationCard";
import { Conversation, MinimalConversation } from "@/types/conversation";
import { Menu } from "react-feather";

type Props = {
  conversations: (Conversation | MinimalConversation)[];
  isOpen: boolean;
  toggleSidebar: () => void;
  onConversationClick: (conversation: Conversation) => void;
};

const MessageSideBar: React.FC<Props> = ({
  conversations,
  isOpen,
  toggleSidebar,
  onConversationClick,
}) => {
  return (
    <div
      className={`
                min-h-screen bg-gray-800 text-white p-4 overflow-y-auto transition-all duration-300 ease-in-out
                ${isOpen ? "w-64" : "w-32"}
                flex flex-col
            `}
    >
      <div className="flex justify-between items-center mb-4">
        <h2 className={`text-xl font-semibold ${!isOpen && "sr-only"}`}>
          Conversations
        </h2>
        <button
          onClick={toggleSidebar}
          className="text-gray-300 hover:text-white focus:outline-none"
        >
          <Menu size={24} />
        </button>
      </div>
      <div className="space-y-2 flex-grow">
        {conversations.map((conversation) =>
          isOpen ? (
            <ConversationCard
              key={conversation.id}
              conversation={conversation as Conversation}
              onClick={() => onConversationClick(conversation as Conversation)}
            />
          ) : (
            <div
              key={conversation.id}
              className="flex items-center justify-center p-2 bg-gray-700 rounded-lg hover:bg-gray-600 cursor-pointer"
              onClick={() => onConversationClick(conversation as Conversation)}
            >
              <h3 className="text-lg font-semibold truncate">
                {conversation.conversationName}
              </h3>
            </div>
          )
        )}
      </div>
    </div>
  );
};

export default MessageSideBar;
