import React from "react";
import { Conversation, MinimalConversation } from "@/types/conversation";

type Props = {
  conversation: Conversation | MinimalConversation;
  onClick: () => void;
};

const ConversationCard: React.FC<Props> = ({ conversation, onClick }) => {
  const isFullConversation = (conv: any): conv is Conversation => {
    return "name" in conv && "lastMessage" in conv;
  };

  return (
    <div
      className="flex items-center p-4 bg-gray-700 rounded-lg hover:bg-gray-600 cursor-pointer"
      onClick={onClick}
    >
      <div className="w-2/3 flex flex-col">
        {isFullConversation(conversation) ? (
          <>
            <h3 className="text-lg font-semibold">
              {conversation.conversationName}
            </h3>
            <p className="text-sm text-gray-300 truncate w-full">
              {conversation.lastMessage}
            </p>
          </>
        ) : (
          <p className="text-lg font-semibold">
            {conversation.conversationName}
          </p>
        )}
      </div>
    </div>
  );
};

export default ConversationCard;
